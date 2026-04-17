using ILNumerics.Core.Runtime;
using ILNumerics.F2NET.Formatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#pragma warning disable CS1591

namespace ILNumerics.F2NET {
    public static class Helper {

        public static readonly Dictionary<int, F90File> Files = new Dictionary<int, F90File>();
        public static NullObject Null = new NullObject();  // not readonly: used for reading, too.
        public class NullObject { }
        static Helper() {
            Files.Add(5, new F90File(Console.OpenStandardInput())); 
            Files.Add(6, new F90File(Console.OpenStandardOutput()));
            Files.Add(0, new F90File(Console.OpenStandardError()));
        }

        internal static unsafe string PrettyPrintNumber(double scaling, double element, int formatLength, int padding) {
            string sElement;
            if (double.IsNaN(element)) {
                sElement = "NaN";
            } else if (double.IsPositiveInfinity(element)) {
                sElement = "∞";
            } else if (double.IsNegativeInfinity(element)) {
                sElement = "-∞";
            } else if ((scaling == 1 && (long)element == element) || element == 0) {
                sElement = element.ToString();
            } else {
                element /= scaling;
                sElement = String.Format($"{{0:f{formatLength}}}", element);
                if (element < 0 && !sElement.StartsWith("-")) {
                    sElement = "-" + sElement;
                }
            }
            if (padding >= 0) {
                sElement = sElement.PadLeft(padding);
            } else {
                sElement = sElement.PadRight(-padding);
            }
            return sElement;
        }

        internal static unsafe string PrettyPrintNumber(double scaling, complex element, int formatLength, int padLeft) {
            bool hidei = double.IsNaN(element.imag) || double.IsInfinity(element.imag);
            string signi = (element.imag < 0) ? "-" : "+";
            var innerPadding = padLeft != 0 ? formatLength + 4 : 0;
            string sElement = PrettyPrintNumber(scaling, element.real, formatLength, innerPadding)
                            + $"{signi}{(hidei ? " " : "i")}"
                            + ((element.imag * element.imag == 1) ? "".PadRight(innerPadding) : PrettyPrintNumber(scaling, Math.Abs(element.imag), formatLength, -innerPadding));
            sElement = sElement.PadLeft(padLeft);
            return sElement;
        }

        public static string WRITEObjectFormatted<T>(int fileUnit, T obj, IEnumerator<FormatItem> formats) {
            string ret = null;
            
            //if (object.Equals(formats.Current, null)) {
                // initially required
                formats.MoveNext(); 
            //}

            do { //&& !(formats.Current.Reverted && object.Equals(obj,null))) {

                switch (formats.Current) {

                    case DataEditDescriptor ded:

                        //if (ded.Manner == EditDescriptorManner.Asterisc) {
                        //     // list directed
                        //     if (object.Equals(obj, null) || obj is NullObject) {
                        //        Files[fileUnit].Write(Environment.NewLine); 
                        //    }
                        //}

                        if (object.Equals(obj, null)) {
                            return ret;
                        }

                        switch (obj) {
                            //case Double double_obj:
                            //    ret = double_obj.ToString();
                            //    break;
                            //case Int32 int_obj:
                            //    ret = int_obj.ToString();
                            //    break;

                            case NullObject nll:
                                ret = Environment.NewLine;
                                break; 

                            default:
                                ret = obj.ToString();
                                if (ded.W.HasValue) {
                                    ret = ret.PadLeft(ded.W.GetValueOrDefault()); 
                                }
                                break;
                        }

                        Files[fileUnit].Write(ret);
                        return ret;   // note, the callee should call a finishing WRITEObjectFormatted after all items!

                    case ControlEditDescriptor ced:

                        switch (ced.Manner) {
                            case EditDescriptorManner.Slash:
                                Files[fileUnit].Write(Environment.NewLine);
                                if (ced.Reverted && obj is NullObject) {
                                    return Environment.NewLine;
                                } else {
                                    break;
                                }
                            case EditDescriptorManner.P:
                                var formatList = ced.Parent;
                                formatList.Scale(ced.K.GetValueOrDefault()); 
                                break;
                            case EditDescriptorManner.S:
                                ced.Parent.PlusSignState = PlusSignStates.S;
                                break;
                            case EditDescriptorManner.SS:
                                ced.Parent.PlusSignState = PlusSignStates.SS;
                                break;
                            case EditDescriptorManner.SP:
                                ced.Parent.PlusSignState = PlusSignStates.SP;
                                break;

                            default:
                                throw new NotImplementedException($"TODO: implement {ced}.");
                        }
                        break;

                    case PositionEditDescriptor ped: 
                        switch (ped.Manner) {
                            case EditDescriptorManner.X when ped.N >= 0:
                                ret = new string(' ', ped.N);
                                Files[fileUnit].Write(ret);
                                break; 
                            default:
                                throw new NotImplementedException(); 
                        }
                        break; 

                    case CharacterEditDescriptor charEd:
                        ret = charEd.Literal.ToString();
                        Files[fileUnit].Write(ret);
                        break;

                    default:
                        break;
                }
            } while (formats.MoveNext()); 

            return ret;

        }
        public static string READObjectFormatted<T>(int fileUnit, ref T obj, ref IEnumerator<object> read, IEnumerator<FormatItem> formats) {
            string ret = null;
            var stream = Files[fileUnit];

            //if (object.Equals(formats.Current, null)) {
            //    // initially required
            formats.MoveNext();
            //}

            do {

                switch (formats.Current) {

                    case DataEditDescriptor ded:

                        // note, that a READ statement does require an input item. It may initializes its input item. But this depends on the data found in the file.

                        switch (ded.Manner) {
                            case EditDescriptorManner.A: {
                                    var fstr = obj as FString;
                                    if (typeof(T) != typeof(FString)) {
                                        throw new FormatException($"Unmatched input object for data edit descriptor A. Expected receiver object of type 'FString'. Found: '{typeof(T).Name}'.");
                                    }
                                    if (!ded.W.HasValue && object.Equals(fstr, null)) {
                                        throw new ArgumentException($"When reading character data of manner A either the target object or the format data edit descriptor must define a length (number of characters) to read.");
                                    }
                                    var w = ded.W.HasValue ? ded.W.GetValueOrDefault() : fstr.Length;
                                    if (w < 0) {
                                        throw new ArgumentException($"Invalid record field length: A{w}. Check format string: '{formats.Current}' and/or item: '{obj}'.");
                                    }
                                    ret = Files[fileUnit].ReadRaw(w);
                                    obj = (T)(object)FString.Create(ret);
                                    return ret;
                                }
                            case EditDescriptorManner.I: {

                                    if (typeof(T) != typeof(int) && typeof(T) != typeof(long) && typeof(T) != typeof(uint) && typeof(T) != typeof(ulong)) {
                                        throw new FormatException($"Unmatched input object type for data edit descriptor I. Expected receiver object of integer type ([U]Int[32|64]). Found: '{typeof(T).Name}'.");
                                    }
                                    if (!ded.W.HasValue) {
                                        throw new ArgumentException($"Incomplete format specification: missing 'w' length specifier for 'I' data edit descriptor.");
                                    }
                                    var w = ded.W.GetValueOrDefault();
                                    if (w < 0) {
                                        throw new ArgumentException($"Invalid record field length: I{w}. Check format string: '{formats.Current}' and/or item: '{obj}'.");
                                    }
                                    string val = Files[fileUnit].ReadRaw(w); 
                                    if (obj is Int32) obj = (T)(object)Convert.ToInt32(val);
                                    else if (obj is Int64) obj = (T)(object)val; 
                                    else if (obj is UInt32) obj = (T)(object)Convert.ToUInt32(val); 
                                    else if (obj is UInt64) obj = (T)(object)Convert.ToUInt64(val);
                                    ret = obj.ToString();
                                    return ret;
                                }
                            case EditDescriptorManner.Asterisc:
                                // list directed input 
                                if (obj is Helper.NullObject || object.Equals(read, null)) {
                                    read = Files[fileUnit].GetEnumerator(); // starts & skips all until end of line
                                }
                                read.MoveNext();
                                #region obsolete
                                //switch (obj) {
                                //    case Int32 i32_read:
                                //        obj = (T)(object)(int)read.Current;
                                //        break;
                                //    case Int64 i64_read:
                                //        obj = (T)(object)(long)read.Current;
                                //        break;
                                //    case UInt32 ui32_read:
                                //        obj = (T)(object)(uint)(long)read.Current;
                                //        break;
                                //    case UInt64 ui64_read:
                                //        obj = (T)(object)(ulong)(long)read.Current;
                                //        break;
                                //    case Double dlb_read:
                                //        obj = (T)(object)(double)read.Current;
                                //        break;
                                //    case Single sngl_read:
                                //        obj = (T)(object)(float)(double)read.Current;
                                //        break;
                                //    case complex cmplx_read:
                                //        obj = (T)(object)(float)(complex)read.Current;
                                //        break;
                                //    case fcomplex fcmplx_read:
                                //        obj = (T)(object)(fcomplex)(complex)read.Current;
                                //        break;
                                //    case FString fstr_read: {
                                //            var str = FString.Create((string)read.Current);
                                //            if (!object.Equals(fstr_read, null)) {
                                //                obj = (T)(object)str.AssignTo(fstr_read.Length);
                                //            } else {
                                //                obj = (T)(object)str;
                                //            }
                                //            break;
                                //        }
                                //    case Boolean bool_read:
                                //        obj = (T)(object)(bool)read.Current;
                                //        break;

                                //        //case null:
                                //        //    obj = (T)(object)read.Current; 

                                //}

                                #endregion
                                if (object.Equals(read.Current, null) || obj is Helper.NullObject) {
                                    return ""; 
                                }
                                switch (typeof(T)) {
                                    case var t when t == typeof(Int32):
                                        obj = (T)(object)Convert.ToInt32(read.Current); 
                                        break;
                                    case var t when t == typeof(Int64):
                                        obj = (T)(object)Convert.ToInt64(read.Current);
                                        break;
                                    case var t when t == typeof(UInt32):
                                        obj = (T)(object)Convert.ToUInt32(read.Current);
                                        break;
                                    case var t when t == typeof(UInt64):
                                        obj = (T)(object)Convert.ToUInt64(read.Current);
                                        break;
                                    case var t when t == typeof(Double):
                                        obj = (T)(object)Convert.ToDouble(read.Current);
                                        break;
                                    case var t when t == typeof(Single):
                                        obj = (T)(object)Convert.ToSingle(read.Current); ; ;
                                        break;
                                    case var t when t == typeof(complex):
                                        obj = (T)(object)(complex)read.Current;
                                        break;
                                    case var t when t == typeof(fcomplex):
                                        obj = (T)(object)(fcomplex)(complex)read.Current;
                                        break;
                                    case var t when t == typeof(FString): {
                                            var str = FString.Create((string)read.Current);
                                            if (!object.Equals(obj as FString, null)) {
                                                obj = (T)(object)str.AssignTo((obj as FString).Length);
                                            } else {
                                                obj = (T)(object)str;
                                            }
                                            break;
                                        }
                                    case var t when t == typeof(Boolean): {
                                            if (read.Current is bool) {
                                                obj = (T)(object)(bool)read.Current;
                                            } else {
                                                var val = read.Current.ToString().ToLower();
                                                obj = (T)(object)(val.StartsWith("t") || val.StartsWith(".t"));
                                            }
                                            break;
                                        }
                                    default:
                                        throw new ArgumentException($"Invalid type of argument '{nameof(obj)}'. Supported types are: Int32,Int64,UInt32,UInt64,Double,Single,ILNumerics.complex,ILNumerics.fcomplex,FString,Boolean.");
                                }
                                ret = obj.ToString();
                                return ret;

                            default:
                                throw new NotImplementedException($"{ded.ToString()} is not yet available."); 
                        }

                    case ControlEditDescriptor ced:
                        throw new NotImplementedException(); 

                    default:
                        break;
                }

            } while (formats.MoveNext()); 
            return ret;

        }
        public static void READRecordEnd(int fileUnit) {
            Files[fileUnit]?.SkipToNextRecord(); 
        }
        public static void WRITERecordEnd(int fileUnit) {
            var stream = Files[fileUnit];
            stream.Write(Environment.NewLine); 
        }
        public static void Write(this Stream stream, string value) {
            stream.Write(UTF8Encoding.UTF8.GetBytes(value), 0, value.Length); 
        }

        public static string WRITEObjects(FormatItemList formats, IEnumerable<object> objects) {

            var buf = new StringBuilder();
            var objIter = objects.GetEnumerator();
            Object nextObj = null; 

            bool nextObjExists() {
                if (nextObj != null) return true; 
                if (objIter.MoveNext()) {
                    nextObj = objIter.Current;
                    return true; 
                } else {
                    return false; 
                }
            }


            // iterate the formats list - forever! 
            foreach (var format in formats) {

                if (format is DataEditDescriptor) {
                    if (nextObjExists() && nextObj != null) {
                        buf.Append(FFormat(nextObj, format));
                        nextObj = null; 
                    } else {
                        break;  // make sure that at least one DataEditDescriptor is present! Also in the *REVERTED* part of a format item list!!!
                    }
                } else if (nextObjExists() || !format.Reverted) {
                    // Control sequences are only used, if more objects exist
                    buf.Append(FFormat(null, format)); 
                }
            }
            return buf.ToString(); 
        }

        private static string FFormat(object obj, FormatItem format) {
            switch (format) {
                case DataEditDescriptor dataED:

                    switch (dataED.Manner) {
                        case EditDescriptorManner.I:
                            var IVal = Convert.ToInt32(obj);
                            return $"{IVal}".PadLeft(dataED.W.GetValueOrDefault()); 
                            
                        case EditDescriptorManner.F:
                        case EditDescriptorManner.E:
                        case EditDescriptorManner.EN:
                        case EditDescriptorManner.ES:
                        case EditDescriptorManner.G:
                        case EditDescriptorManner.D:
                            if (dataED.Parent.PlusSignState == PlusSignStates.S || dataED.Parent.PlusSignState == PlusSignStates.SS) {
                                return $"{Convert.ToDouble(obj)}".PadLeft(dataED.W.GetValueOrDefault());
                            } else {
                                return $"+{Convert.ToDouble(obj)}".PadLeft(Math.Max(dataED.W.GetValueOrDefault() - 1, 0));
                            }
                        default:
                            throw new NotImplementedException(); 
                    }
                    
                case ControlEditDescriptor ctrED:
                    switch (ctrED.Manner) {
                        case EditDescriptorManner.Slash:
                            return Environment.NewLine; 
                            
                        default:
                            throw new NotImplementedException(); 
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public static void redirectConsoleStreams(string[] arguments) {
            if (arguments != null && arguments.Length > 0) {
                // first argument is considered as input text buffer and replaces Console.In
                MemoryStream input = new MemoryStream(ASCIIEncoding.ASCII.GetBytes(arguments[0]));
                Files[5]?.Close();
                Files[5] = new F90File(input); 
            }
        }

        public static void RegisterFileUnitIO(Stream data, int unit) {
            Files[unit] = new F90File(data); 
        }
    }
}
