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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

#pragma warning disable 1591

using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILNumerics.Core.Native {

    
    internal unsafe partial class MKLDescriptor : IDisposable, IEqualityComparer<MKLDescriptor> {

        #region attributes 
        internal static DescriptorCache<MKLDescriptor> Cache = new DescriptorCache<MKLDescriptor>();
        #endregion

        private int _hashCode;  
        private int Dimensionality; 
        private int Precision;
        private int Domain;
        private bool InPlace;

        private int NumberOfTransforms; 
        private int Distance;
        private MemoryHandle m_bsdHandle;
        protected int* m_pSizes; 
        protected int* m_pStrides; 
        private int* Dimensions { get { return m_pSizes; } }
        private int* Strides { get { return m_pStrides; } }

        internal MKLHandle DescriptorHandle; 

        internal IntPtr Pointer {
            
            get {
                return DescriptorHandle.DangerousGetHandle(); 
            }
        }

        /// <summary>
        /// Create a slim descriptor object, at first only used for look-up and retrieval of descriptor objects from the cache. Call Commit() to make it persistent! 
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="domain"></param>
        /// <param name="inPlace"></param>
        /// <param name="ndims"></param>
        /// <param name="sizes"></param>
        /// <param name="strides"></param>
        /// <param name="numberOfTransforms"></param>
        /// <param name="distance"></param>
        public MKLDescriptor(int precision, int domain, bool inPlace, int ndims, int* sizes, int* strides,
                             int numberOfTransforms, int distance) {
            // note, compared to a regular BSD (as in ILNumerics.Size) we only store
            // * dimensions (inplace, lengths, [ndims]). 
            // * strides (-> inplace, the same for input and output, [ndims + 1]),m and

            Dimensionality = ndims; 
            Precision = precision;
            Domain = domain;
            InPlace = inPlace;
            NumberOfTransforms = numberOfTransforms;
            Distance = distance;
            m_pSizes = sizes;
            m_pStrides = strides;
        }

        protected MKLDescriptor Commit(MKLHandle nativeHandle) {
            System.Diagnostics.Debug.Assert(!nativeHandle.IsInvalid);

            m_bsdHandle = MathInternal.New<long>(Dimensionality * 2 + 1);
            var oldSizes = m_pSizes; 
            var oldStrides = m_pStrides;
            m_pSizes = (int*)m_bsdHandle.Pointer;
            m_pStrides = (int*)m_bsdHandle.Pointer + Dimensionality; 
            SetSizes(oldSizes);
            SetStrides(oldStrides);
            DescriptorHandle = nativeHandle; 
            return this; 
        }
        public override int GetHashCode() {
            if (_hashCode == 0) {
                _hashCode = createHashCode(); 
            }
            return _hashCode; 
        }
        private int createHashCode() {

            var ret = Precision;
            ret = (ret * 13) ^ Domain;
            ret = (ret * 13) ^ NumberOfTransforms;
            ret = (ret * 13) ^ Distance;
            ret = (ret * 13) ^ Dimensionality;
            ret = (ret * 13) ^ (InPlace ? 7 : -1);
            for (int i = 0; i < Dimensionality; i++) {
                ret = (ret * 13) ^ m_pSizes[i]; 
                ret = (ret * 13) ^ m_pStrides[i]; 
            }
            ret = (ret * 13) ^ m_pStrides[Dimensionality]; 
            return ret;
        }
        int IEqualityComparer<MKLDescriptor>.GetHashCode(MKLDescriptor obj) {
            return obj?.GetHashCode() ?? 0; 
        }
        public override bool Equals(object obj) {
            if (obj is MKLDescriptor desc) {
                var ret = Precision == desc.Precision
                    && Dimensionality == desc.Dimensionality
                    && Domain == desc.Domain
                    && InPlace == desc.InPlace
                    && NumberOfTransforms == desc.NumberOfTransforms
                    && Distance == desc.Distance
                    && isSame(desc.Dimensionality, desc.Dimensions, desc.Strides);
                return ret; 
            }
            return false; 
        }
        bool IEqualityComparer<MKLDescriptor>.Equals(MKLDescriptor a, MKLDescriptor b) {
            if (object.Equals(a, null)) {
                if (object.Equals(b, null)) return true;
                return false;
            } else {
                if (object.Equals(b, null)) return false; 
                return a.Equals(b);
            }
        }
        private bool isSame(int dimensionality, int* dimensions, int* strides) {
            if (dimensionality != Dimensionality) return false; 
            for (int i = 0; i < dimensionality; i++) {
                if (Dimensions[i] != dimensions[i]
                    || Strides[i] != strides[i])
                    return false; 
            }
            return Strides[dimensionality] == strides[dimensionality]; 
        }

        /// <summary>
        /// Releases this descriptor into the cache for later reusing. 
        /// </summary>
        /// <param name="descriptor">The descriptor which is no longer after the current transform.</param>
        
        public static void Free(MKLDescriptor descriptor) {
            Cache.AddDescriptor(descriptor); 
        }
        /// <summary>
        /// Retrieves a descriptor from the cache or creates a new one.
        /// </summary>
        /// <param name="precision"></param>
        /// <param name="domain"></param>
        /// <param name="dimensionality"></param>
        /// <param name="sizes"></param>
        /// <param name="strides"></param>
        /// <param name="numberOfTransforms"></param>
        /// <param name="distance"></param>
        /// <param name="inplace"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        
        public static unsafe MKLDescriptor GetOrCreate(int precision, int domain, int dimensionality, int* sizes, int* strides,
                    int numberOfTransforms, int distance, bool inplace) {

            //System.Diagnostics.Debug.Assert(dimension.StorageOrder == StorageOrders.ColumnMajor, $"Storages must be in ColumnMajor for current version of MKLFFT!"); // no! all storage orders supported!

            if (dimensionality < 1 || dimensionality > Size.MaxNumberOfDimensions) {
                throw new ArgumentException($"Error creating FFT descriptor: dimensionality must be in range [1...{Size.MaxNumberOfDimensions}].");
            }
            // first, create a slim object, reusing our sizes & strides for retrieving a cached object
            var proxy = new MKLDescriptor(precision, domain, inplace, dimensionality, sizes, strides, numberOfTransforms, distance);

            var ret = Cache.GetDescriptor(proxy);

            if (ret != null) {
                return ret;
            }

            // must create a new one 
            IntPtr tmpHandle = default(IntPtr);
            try {
                int error = 0;

                if (dimensionality == 1) { // what a crude interface! It expects a scalar OR a pointer to an array depending on 'dimensionality' :| 
                    error = MKLImports.DftiCreateDescriptor(ref tmpHandle, precision, domain, 1, sizes[0]);
                    //System.Diagnostics.Debug.WriteLine($"MKLFFT: DFTICreateDescriptor: tmpDescriptor[in]: ref {tmpHandle}, precision:{precision}, domain:{domain}, dimensionality:_1, dimensions[0]:{sizes[0]}");
                } else {
                    error = MKLImports.DftiCreateDescriptor(ref tmpHandle, precision, domain, dimensionality, sizes);
                    //System.Diagnostics.Debug.WriteLine($"MKLFFT: DFTICreateDescriptor: tmpDescriptor[in]: ref {tmpHandle}, precision:{precision}, domain:{domain}, dimensionality:{dimensionality}, dimensions[0]:{sizes[0]}");
                }
                if (0 != error) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.NUMBER_OF_TRANSFORMS, numberOfTransforms))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                if (domain == MKLValues.REAL && !inplace) {
                    if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.INPUT_DISTANCE, distance * 2))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                } else {
                    if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.INPUT_DISTANCE, distance))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                }
                if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.OUTPUT_DISTANCE, distance))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));

                if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.INPUT_STRIDES, strides))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.OUTPUT_STRIDES, strides))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                if (!inplace) {
                    if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.PLACEMENT, MKLValues.NOT_INPLACE))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                    if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.CONJUGATE_EVEN_STORAGE, MKLValues.COMPLEX_REAL))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                    //error = MKLImports.DftiSetValue(tmpDescriptor, MKLParameter.REAL_STORAGE, __arglist(MKLValues.REAL_REAL));
                } else {
                    if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.PLACEMENT, MKLValues.INPLACE))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                    if (0 != (error = MKLImports.DftiSetValue(tmpHandle, MKLParameter.CONJUGATE_EVEN_STORAGE, MKLValues.COMPLEX_COMPLEX))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));
                }
                // Committing the new descriptor
                if (0 != (error = MKLImports.DftiCommitDescriptor(tmpHandle))) throw new InvalidOperationException("Error creating FFT descriptor: " + MKLFFT.MKLGetError(error));

                ret = proxy.Commit(new MKLHandle(tmpHandle));
            } catch {
            } finally {

            }
            //System.Diagnostics.Debug.WriteLine(ret.GetDescriptorInfo());
            return ret;
        }

        private unsafe void SetStrides(int* strides) {
            System.Diagnostics.Debug.Assert(m_bsdHandle != null && m_bsdHandle.Pointer != IntPtr.Zero,
                "This descriptor appears to be invalid / corrupt.");
            for (int i = 0; i <= Dimensionality; i++) {  // sic: '<=' -> strides +1 for S0 (BaseOffset / displacement)
                m_pStrides[i] = strides[i]; 
            }
        }

        private void SetSizes(int* dimensions) {
            System.Diagnostics.Debug.Assert(m_bsdHandle != null && m_bsdHandle.Pointer != IntPtr.Zero,
                "This descriptor appears to be invalid / corrupt.");

            for (int i = 0; i < Dimensionality; i++) {  
                m_pSizes[i] = dimensions[i];
            }
        }

        public string GetDescriptorInfo() {
            if (!DescriptorHandle.IsInvalid) {
                return getDescriptorInfoInternal();
            } else {
                return "(not commited)";
            }
        }
        
        public unsafe string getDescriptorInfoInternal() {

            StringBuilder ret = new StringBuilder();
            int val = 0, ndims = 0;
            MKLImports.DftiGetValue(Pointer, MKLParameter.PRECISION, ref val); ret.AppendLine("Precision: " + val + " ");
            MKLImports.DftiGetValue(Pointer, MKLParameter.FORWARD_DOMAIN, ref val); ret.AppendLine("ForwDomain: " + val + " ");
            MKLImports.DftiGetValue(Pointer, MKLParameter.DIMENSION, ref ndims); ret.AppendLine("DIMENSION: " + ndims + " ");
            var buffer = stackalloc int[ndims + 1];
            ret.AppendLine("Length: ");
            addArrayValue(this, MKLParameter.LENGTHS, ret, ndims, buffer);

            MKLImports.DftiGetValue(Pointer, MKLParameter.PLACEMENT, ref val); ret.AppendLine("PLACEMENT: " + val + " ");
            MKLImports.DftiGetValue(Pointer, MKLParameter.NUMBER_OF_TRANSFORMS, ref val); ret.AppendLine("NUMBER_OF_TRANSFORMS: " + val + " ");
            MKLImports.DftiGetValue(Pointer, MKLParameter.COMPLEX_STORAGE, ref val); ret.AppendLine("COMPLEX_STORAGE: " + val + " ");
            MKLImports.DftiGetValue(Pointer, MKLParameter.CONJUGATE_EVEN_STORAGE, ref val); ret.AppendLine("CONJUGATE_EVEN_STORAGE: " + val + " ");
            MKLImports.DftiGetValue(Pointer, MKLParameter.INPUT_DISTANCE, ref val); ret.Append("INPUT_DISTANCE: " + val + " ");
            MKLImports.DftiGetValue(Pointer, MKLParameter.OUTPUT_DISTANCE, ref val); ret.AppendLine("OUTPUT_DISTANCE: " + val + " ");

            ret.Append("INPUT_STRIDES&: ");
            addArrayValue(this, MKLParameter.INPUT_STRIDES, ret, ndims+1, buffer);

            ret.Append("OUTPUT_STRIDES&: ");
            addArrayValue(this, MKLParameter.OUTPUT_STRIDES, ret, ndims+1, buffer);
            return ret.ToString();
        }

        private static void addArrayValue(MKLDescriptor descriptor, int valueID, StringBuilder ret, int len, int* buffer) {
            MKLImports.DftiGetValue(descriptor.Pointer, valueID, ref buffer[0]);
            for (int i = 0; i < len; i++) {
                ret.Append($"{buffer[i]},");
            }
            ret.AppendLine();
        }

        #region IDisposable Members
        
        internal void Dispose(bool manual) {
            lock (this) {
                if ((!m_bsdHandle?.IsInvalid) ?? false) {
                    if (manual) {
                        MathInternal.free<long>(m_bsdHandle, 0);
                    } else {
                        GC.SuppressFinalize(m_bsdHandle);
                        m_bsdHandle.Dispose();
                    }
                }
                if ((!DescriptorHandle?.IsInvalid) ?? false) {
                    GC.SuppressFinalize(DescriptorHandle);
                    DescriptorHandle.Dispose();
                    DescriptorHandle = null;
                }

            }
        }
    
        public void Dispose() {
            Dispose(true); 
        }
        ~MKLDescriptor() {
            Dispose(false);
        }
        #endregion
    }
}
