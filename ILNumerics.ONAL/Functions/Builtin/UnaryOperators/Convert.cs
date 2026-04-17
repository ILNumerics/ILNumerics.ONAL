//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;



namespace ILNumerics.Core.Functions.Builtin {



    internal static partial class MathInternal {

        /// <summary>
        /// Converts numeric array of unknown type to a specific array type. 
        /// </summary>
        /// <typeparam name="outT">Target element type.</typeparam>
        /// <param name="A">Source array.</param>
        /// <returns>Array of the same shape and size and element type of <typeparamref name="outT"/>.</returns>
        internal static Array<outT> convert<outT>(BaseArray A) where outT : struct {
            outT d = default(outT); 
            if (object.Equals(A, null)) {
                return null;
            } else if (d is double) {
                return todouble(A) as Array<outT>;
            } else if (d is float) {
                return tosingle(A) as Array<outT>;
            } else if (d is long) {
                return toint64(A) as Array<outT>;
            } else if (d is ulong) {
                return touint64(A) as Array<outT>;
            } else if (d is int) {
                return toint32(A) as Array<outT>;
            } else if (d is uint) {
                return touint32(A) as Array<outT>;
            } else if (d is short) {
                return toint16(A) as Array<outT>;
            } else if (d is ushort) {
                return touint16(A) as Array<outT>;
            } else if (d is sbyte) {
                return toint8(A) as Array<outT>;
            } else if (d is byte) {
                return touint8(A) as Array<outT>;
            } else if (d is complex) {
                return tocomplex(A) as Array<outT>;
            } else if (d is fcomplex) {
                return tofcomplex(A) as Array<outT>;
            } else {
                throw new InvalidCastException($"Unable to cast BaseArray of type { informTypeName(A) } to Array<{ typeof(outT).Name}>.");
            }
        }

        private static string informTypeName(object o) {
            if (object.Equals(o, null)) {
                return "null"; 
            }
            var t = o.GetType(); 
            var ret = t.Name; 
            if (t.IsGenericType) {
                // only first gen. arg. for now
                ret += $"<{t.GetGenericArguments()[0].Name}>";
            }
            return ret; 
        }

        /// <summary>
        /// Convert typed source array <paramref name="A"/> into array of element type <typeparamref name="outT"/>.
        /// </summary>
        /// <typeparam name="inT">Source element type.</typeparam>
        /// <typeparam name="outT">Target element type.</typeparam>
        /// <param name="A">Source array.</param>
        /// <returns>Array of the same shape  and size than <paramref name="A"/>, having the element values converted to type <typeparamref name="outT"/>.</returns>
        internal static Array<outT> convert<inT,outT>(BaseArray<inT> A) 
            where outT : struct 
            where inT : struct {
                return convert<outT>(A);
            
        }
    }

}
