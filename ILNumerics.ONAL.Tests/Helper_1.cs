// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace ILNumerics.Core.UnitTests {
    public static partial class Helper {
        // from: https://stackoverflow.com/questions/3303126/how-to-get-the-value-of-private-field-in-c
        public static object GetInstanceField(Type type, object instance, string fieldName) {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
        public static void SetInstanceField(Type type, object instance, string fieldName, object value) {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            field.SetValue(instance, value);
        }
        public static Array generateSystemArrayDouble(ulong v1, ulong v2, ref double[] expRow, ref double[] expCol) {
            var ret = new double[v1, v2];
            for (ulong i1 = 0; i1 < v1; i1++) {
                for (ulong i2 = 0; i2 < v2; i2++) {
                    ret[i1, i2] = i2 + v2 * i1;
                }
            }

            if (expRow != null) {
                expRow = new double[v1 * v2];
                for (ulong n = 0, i1 = 0; i1 < v1; i1++) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        expRow[n++] = ret[i1, i2];
                    }
                }

            }
            if (expCol != null) {
                expCol = new double[v1 * v2];
                for (ulong n = 0, i2 = 0; i2 < v2; i2++) {
                    for (ulong i1 = 0; i1 < v1; i1++) {
                        expCol[n++] = ret[i1, i2];
                    }
                }
            }

            return ret;
        }
        public static Array generateSystemArray<T>(ulong v1, ulong v2, ref T[] expRow, ref T[] expCol) where T : IConvertible {
            var ret = new T[v1, v2];
            for (ulong i1 = 0; i1 < v1; i1++) {
                #region types
                if (ret is Double[,]) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        ret[i1, i2] = (T)(object)(double)(i2 + v2 * i1);
                    }
                } else if (ret is Single[,]) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        ret[i1, i2] = (T)(object)(float)(i2 + v2 * i1);
                    }
                } else if (ret is Int32[,]) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        ret[i1, i2] = (T)(object)(int)(i2 + v2 * i1);
                    }
                } else if (ret is UInt32[,]) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        ret[i1, i2] = (T)(object)(uint)(i2 + v2 * i1);
                    }
                } else if (ret is Int64[,]) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        ret[i1, i2] = (T)(object)(long)(i2 + v2 * i1);
                    }
                } else if (ret is UInt64[,]) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        ret[i1, i2] = (T)(object)(ulong)(i2 + v2 * i1);
                    }
                } else {
                    throw new NotSupportedException($"The type {typeof(T).Name} is not implemented!");
                }
                #endregion
            }

            if (expRow != null) {
                expRow = new T[v1 * v2];
                for (ulong n = 0, i1 = 0; i1 < v1; i1++) {
                    for (ulong i2 = 0; i2 < v2; i2++) {
                        expRow[n++] = ret[i1, i2];
                    }
                }

            }
            if (expCol != null) {
                expCol = new T[v1 * v2];
                for (ulong n = 0, i2 = 0; i2 < v2; i2++) {
                    for (ulong i1 = 0; i1 < v1; i1++) {
                        expCol[n++] = ret[i1, i2];
                    }
                }
            }

            return ret;
        }

        public static T Timeit<T>(Func<T> work, string name, Func<string> postfix = null, double rep = 10) {
            if (postfix == null) {
                postfix = () => "";
            }
            T ret = default(T); 
            var oldThreads = Settings.MaxNumberThreads;
            try {
                var sw = new Stopwatch(); 
                for (int i = 1; i < Environment.ProcessorCount * 2; i++) {
                    Settings.MaxNumberThreads = (uint)i;

                    sw.Restart();
                    for (int k = 0; k < rep; k++) {
                        ret = work();
                    }
                    sw.Stop();
                    Console.WriteLine($"{name} took: {sw.ElapsedMilliseconds / rep}ms. #Threads: {Settings.MaxNumberThreads} {postfix()}");

                }
            } finally {
                Settings.MaxNumberThreads = oldThreads;
            }
            return ret;
        }

        public static string GetResourceText(string name) {

            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(name))
            using (StreamReader reader = new StreamReader(stream)) {
                string result = reader.ReadToEnd();
                return result;
            }
        }
        public static string GetResourceFileName(string resourceName) {

            var assembly = Assembly.GetExecutingAssembly();
            if (File.Exists(resourceName)) {
                File.Delete(resourceName);
            }

            byte[] buffer = new byte[1024];
            int bufLen = buffer.Length;
            var foundName = assembly.GetManifestResourceNames().Where(n => n.Contains(resourceName)).First(); 
            using (Stream stream = assembly.GetManifestResourceStream(foundName))
            using (var file = File.OpenWrite(resourceName)) {
                while (bufLen > 0) {
                    bufLen = stream.Read(buffer, 0, bufLen);
                    file.Write(buffer, 0, bufLen); 
                }
            }
            return resourceName; 
        }


    }
}
