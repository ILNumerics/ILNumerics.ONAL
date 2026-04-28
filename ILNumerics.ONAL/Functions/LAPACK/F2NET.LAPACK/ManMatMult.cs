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
using ILNumerics.Core.MemoryLayer;

// Non-F2NET functionality, not coming from FORTRAN but supporting the managed LAPACK implementation.
namespace ILNumerics.Core.LAPACK {

    public static unsafe partial class Helper {

        private static readonly uint ALIGN = 1024 * 4;

        #region HYCALPER LOOPSTART Managed Blocked Matrix Multiplication
/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>float</destination>
</type>
</hycalper>
*/

        
        unsafe public static void MMultBlocked(double* A, double* B, double* C, long m, long n, long k, long kc,
                                            long strideA0, long strideA1, long strideB0, long strideB1) {

            // block parameters
            uint mc = 256;
            uint mr = 12;
            uint nr = 12;

            MemoryHandle Apack = null;
            MemoryHandle Bpack = null;
            try {

                Apack = ILNumerics.Core.Functions.Builtin.MathInternal.New<double>((ulong)(kc * mc + ALIGN));
                Bpack = ILNumerics.Core.Functions.Builtin.MathInternal.New<double>((ulong)(kc * n + ALIGN));

                inner_k_loop_managed(m, n, k, kc, mc, mr, nr, A, B, C, (double*)Bpack.Pointer, (double*)Apack.Pointer, 0, n, 0, m,
                                    strideA0, strideA1, strideB0, strideB1);

            } finally {

                if (Apack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<double>(Apack, 0);
                if (Bpack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<double>(Bpack, 0);
            }
        }

        unsafe private static void inner_k_loop_managed(long m, long n, long k, long kc, uint mc, uint mr, uint nr,
                                            double* pA, double* pB, double* pC, double* pBpack, double* pApack,
                                            long n_start, long n_end, long m_start, long m_end, 
                                            long strideA0, long strideA1, long strideB0, long strideB1) {
            // Defaults: 
            // kc (settings):150
            // mc: 512
            // mr: 4
            // nr: 4

            pApack = (double*)((byte*)pApack + (ALIGN - ((ulong)pApack % ALIGN)));
            pBpack = (double*)((byte*)pBpack + (ALIGN - ((ulong)pBpack % ALIGN)));
            double* pApackTmp; 
            double* pBpackTmp; 
            double* pBArrTmp, pAArrTmp, pCTmp;

            long n_len = n_end - n_start;
            for (long ki = 0; ki < k; ki += kc) {
                //handle end block
                if (k - ki < kc) kc = k - ki;

                #region pack B
                pBpackTmp = pBpack;
                for (long nb = 0; nb < n_len; nb++) {
                    pBArrTmp = pB + ki * strideB0 + strideB1 * (nb + n_start);
                    int c = 0;
                    for (; c < kc - 8; c += 8) {
                        pBpackTmp[0] = pBArrTmp[0 * strideB0];
                        pBpackTmp[1] = pBArrTmp[1 * strideB0];
                        pBpackTmp[2] = pBArrTmp[2 * strideB0];
                        pBpackTmp[3] = pBArrTmp[3 * strideB0];
                        pBpackTmp[4] = pBArrTmp[4 * strideB0];
                        pBpackTmp[5] = pBArrTmp[5 * strideB0];
                        pBpackTmp[6] = pBArrTmp[6 * strideB0];
                        pBpackTmp[7] = pBArrTmp[7 * strideB0];
                        pBpackTmp += 8; pBArrTmp += 8 * strideB0;
                    }
                    for (; c < kc; c++) {
                        *pBpackTmp++ = *pBArrTmp;
                        pBArrTmp += strideB0; 
                    }
                }
                #endregion

                long mcc = mc;
                long m_len = m_end - m_start;
                for (long ai = 0; ai < m_len; ai += mcc) {
                    //handle end block
                    if (m_len - ai < mcc) mcc = m_len - ai;

                    #region pack A (transposed)
                    pApackTmp = pApack;
                    for (long ra = 0; ra < mcc; ra++) {
                        pAArrTmp = pA + (ra + ai) * strideA0 + strideA1 * (m_start + ki);
                        long ca = 0; 
                        for (; ca < kc - 8; ca += 8) {

                            pApackTmp[0] = pAArrTmp[0 * strideA1];
                            pApackTmp[1] = pAArrTmp[1 * strideA1];
                            pApackTmp[2] = pAArrTmp[2 * strideA1];
                            pApackTmp[3] = pAArrTmp[3 * strideA1];
                            pApackTmp[4] = pAArrTmp[4 * strideA1];
                            pApackTmp[5] = pAArrTmp[5 * strideA1];
                            pApackTmp[6] = pAArrTmp[6 * strideA1];
                            pApackTmp[7] = pAArrTmp[7 * strideA1];
                            pApackTmp += 8; pAArrTmp += 8 * strideA1;
                        }
                        for (; ca < kc; ca++) {
                            *(pApackTmp++) = *pAArrTmp;
                            pAArrTmp += strideA1; 
                        }
                    }
                    #endregion

                    #region subblocked
                    long nrLen = nr;
                    for (long nri = 0; nri < n_len; nri += nrLen) {
                        if (n_len - nri < nrLen) nrLen = n_len - nri;
                        long mrLen = mr;
                        for (long mri = 0; mri < mcc; mri += mrLen) {
                            if (mcc - mri < mrLen) mrLen = mcc - mri;

                            // prefetch CAux 
                            for (int nii = 0; nii < nrLen; nii++) {
                                pCTmp = pC + ai + mri + (nri + nii + n_start) * m;
                                for (int mii = 0; mii < mrLen; mii++) {
                                    pApackTmp = pApack + (mri + mii) * kc; // <-- transposed packed!
                                    pBpackTmp = pBpack + (nri + nii) * kc;
                                    double sum = 0;

                                    int jj = (int)kc;
                                    for (; jj > 8; jj -= 8) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3]
                                            + pApackTmp[4] * pBpackTmp[4]
                                            + pApackTmp[5] * pBpackTmp[5]
                                            + pApackTmp[6] * pBpackTmp[6]
                                            + pApackTmp[7] * pBpackTmp[7];

                                        pApackTmp += 8;
                                        pBpackTmp += 8;

                                    }
                                    for (; jj > 4; jj -= 4) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3];

                                        pApackTmp += 4;
                                        pBpackTmp += 4;

                                    }
                                    for (; jj --> 0; ) {
                                        sum += *pApackTmp++ * *pBpackTmp++;
                                    }
                                    pCTmp[mii] += sum;

                                    #region vector attempt (obsolete, not working)
                                    //sum += Vector.Dot(
                                    //    new Vector<double>(tmpA, jj), 
                                    //    new Vector<double>(tmpA, jj + VectLen));
                                    //sum += Vector.Dot(
                                    //    new Vector<double>(tmpA, jj + VectLen * 2), 
                                    //    new Vector<double>(tmpA, jj + VectLen * 1));



                                    //a1 = Unsafe.As<double, Vector<double>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<double, Vector<double>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //sum += Vector.Dot(new Vector<double>(tmpA,jj), new Vector<double>(tmpA,jj+VectLen));
                                    //sum += Vector.Dot(new Vector<double>(tmpA,jj+VectLen*2), new Vector<double>(tmpA,jj+VectLen*3));

                                    //Unsafe.CopyBlock(pA_, pApackTmp, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //Unsafe.CopyBlock(pA_, pApackTmp + 4, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp + 4, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;




                                    //a1 = Unsafe.As<double, Vector<double>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<double, Vector<double>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //a1 = Unsafe.As<double, Vector<double>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<double, Vector<double>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;


                                    //*addressOfa1 = (IntPtr)pApackTmp;
                                    //*addressOfb1 = (IntPtr)pBpackTmp;
                                    //sum += Vector.Dot(a1, b1);

                                    //*addressOfa1 = (IntPtr)(pApackTmp + 4);
                                    //*addressOfb1 = (IntPtr)(pBpackTmp + 4);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;


                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //00007FFA0FB2E48B  jle         00007FFA0FB2E530  
                                    //                                        // original version
                                    //                                        sum += pApackTmp[0] * pBpackTmp[0]
                                    //                                            + pApackTmp[1] * pBpackTmp[1]
                                    //                                            + pApackTmp[2] * pBpackTmp[2]
                                    //                                            + pApackTmp[3] * pBpackTmp[3]
                                    //                                            + pApackTmp[4] * pBpackTmp[4]
                                    //                                            + pApackTmp[5] * pBpackTmp[5]
                                    //                                            + pApackTmp[6] * pBpackTmp[6]
                                    //                                            + pApackTmp[7] * pBpackTmp[7];

                                    //00007FFA0FB2E491  vmovsd      xmm1,qword ptr [r13]  
                                    //00007FFA0FB2E497  vmulsd      xmm1,xmm1,mmword ptr [rbp]  
                                    //00007FFA0FB2E49D  vmovsd      xmm2,qword ptr [r13+8]  
                                    //00007FFA0FB2E4A3  vmulsd      xmm2,xmm2,mmword ptr [rbp+8]  
                                    //00007FFA0FB2E4A9  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4AE  vmovsd      xmm2,qword ptr [r13+10h]  
                                    //00007FFA0FB2E4B4  vmulsd      xmm2,xmm2,mmword ptr [rbp+10h]  
                                    //00007FFA0FB2E4BA  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4BF  vmovsd      xmm2,qword ptr [r13+18h]  
                                    //00007FFA0FB2E4C5  vmulsd      xmm2,xmm2,mmword ptr [rbp+18h]  
                                    //00007FFA0FB2E4CB  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4D0  vmovsd      xmm2,qword ptr [r13+20h]  
                                    //00007FFA0FB2E4D6  vmulsd      xmm2,xmm2,mmword ptr [rbp+20h]  
                                    //00007FFA0FB2E4DC  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4E1  vmovsd      xmm2,qword ptr [r13+28h]  
                                    //00007FFA0FB2E4E7  vmulsd      xmm2,xmm2,mmword ptr [rbp+28h]  
                                    //00007FFA0FB2E4ED  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4F2  vmovsd      xmm2,qword ptr [r13+30h]  
                                    //00007FFA0FB2E4F8  vmulsd      xmm2,xmm2,mmword ptr [rbp+30h]  
                                    //00007FFA0FB2E4FE  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E503  vmovsd      xmm2,qword ptr [r13+38h]  
                                    //00007FFA0FB2E509  vmulsd      xmm2,xmm2,mmword ptr [rbp+38h]  
                                    //00007FFA0FB2E50F  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E514  vaddsd      xmm0,xmm0,xmm1  

                                    //                                        pApackTmp += 8;
                                    //00007FFA0FB2E519  add         r13,40h  
                                    //                                        pBpackTmp += 8;
                                    //00007FFA0FB2E51D  add         rbp,40h  
                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 


        
        unsafe public static void MMultBlocked(float* A, float* B, float* C, long m, long n, long k, long kc,
                                            long strideA0, long strideA1, long strideB0, long strideB1) {

            // block parameters
            uint mc = 256;
            uint mr = 12;
            uint nr = 12;

            MemoryHandle Apack = null;
            MemoryHandle Bpack = null;
            try {

                Apack = ILNumerics.Core.Functions.Builtin.MathInternal.New<float>((ulong)(kc * mc + ALIGN));
                Bpack = ILNumerics.Core.Functions.Builtin.MathInternal.New<float>((ulong)(kc * n + ALIGN));

                inner_k_loop_managed(m, n, k, kc, mc, mr, nr, A, B, C, (float*)Bpack.Pointer, (float*)Apack.Pointer, 0, n, 0, m,
                                    strideA0, strideA1, strideB0, strideB1);

            } finally {

                if (Apack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<float>(Apack, 0);
                if (Bpack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<float>(Bpack, 0);
            }
        }

        unsafe private static void inner_k_loop_managed(long m, long n, long k, long kc, uint mc, uint mr, uint nr,
                                            float* pA, float* pB, float* pC, float* pBpack, float* pApack,
                                            long n_start, long n_end, long m_start, long m_end, 
                                            long strideA0, long strideA1, long strideB0, long strideB1) {
            // Defaults: 
            // kc (settings):150
            // mc: 512
            // mr: 4
            // nr: 4

            pApack = (float*)((byte*)pApack + (ALIGN - ((ulong)pApack % ALIGN)));
            pBpack = (float*)((byte*)pBpack + (ALIGN - ((ulong)pBpack % ALIGN)));
            float* pApackTmp; 
            float* pBpackTmp; 
            float* pBArrTmp, pAArrTmp, pCTmp;

            long n_len = n_end - n_start;
            for (long ki = 0; ki < k; ki += kc) {
                //handle end block
                if (k - ki < kc) kc = k - ki;

                #region pack B
                pBpackTmp = pBpack;
                for (long nb = 0; nb < n_len; nb++) {
                    pBArrTmp = pB + ki * strideB0 + strideB1 * (nb + n_start);
                    int c = 0;
                    for (; c < kc - 8; c += 8) {
                        pBpackTmp[0] = pBArrTmp[0 * strideB0];
                        pBpackTmp[1] = pBArrTmp[1 * strideB0];
                        pBpackTmp[2] = pBArrTmp[2 * strideB0];
                        pBpackTmp[3] = pBArrTmp[3 * strideB0];
                        pBpackTmp[4] = pBArrTmp[4 * strideB0];
                        pBpackTmp[5] = pBArrTmp[5 * strideB0];
                        pBpackTmp[6] = pBArrTmp[6 * strideB0];
                        pBpackTmp[7] = pBArrTmp[7 * strideB0];
                        pBpackTmp += 8; pBArrTmp += 8 * strideB0;
                    }
                    for (; c < kc; c++) {
                        *pBpackTmp++ = *pBArrTmp;
                        pBArrTmp += strideB0; 
                    }
                }
                #endregion

                long mcc = mc;
                long m_len = m_end - m_start;
                for (long ai = 0; ai < m_len; ai += mcc) {
                    //handle end block
                    if (m_len - ai < mcc) mcc = m_len - ai;

                    #region pack A (transposed)
                    pApackTmp = pApack;
                    for (long ra = 0; ra < mcc; ra++) {
                        pAArrTmp = pA + (ra + ai) * strideA0 + strideA1 * (m_start + ki);
                        long ca = 0; 
                        for (; ca < kc - 8; ca += 8) {

                            pApackTmp[0] = pAArrTmp[0 * strideA1];
                            pApackTmp[1] = pAArrTmp[1 * strideA1];
                            pApackTmp[2] = pAArrTmp[2 * strideA1];
                            pApackTmp[3] = pAArrTmp[3 * strideA1];
                            pApackTmp[4] = pAArrTmp[4 * strideA1];
                            pApackTmp[5] = pAArrTmp[5 * strideA1];
                            pApackTmp[6] = pAArrTmp[6 * strideA1];
                            pApackTmp[7] = pAArrTmp[7 * strideA1];
                            pApackTmp += 8; pAArrTmp += 8 * strideA1;
                        }
                        for (; ca < kc; ca++) {
                            *(pApackTmp++) = *pAArrTmp;
                            pAArrTmp += strideA1; 
                        }
                    }
                    #endregion

                    #region subblocked
                    long nrLen = nr;
                    for (long nri = 0; nri < n_len; nri += nrLen) {
                        if (n_len - nri < nrLen) nrLen = n_len - nri;
                        long mrLen = mr;
                        for (long mri = 0; mri < mcc; mri += mrLen) {
                            if (mcc - mri < mrLen) mrLen = mcc - mri;

                            // prefetch CAux 
                            for (int nii = 0; nii < nrLen; nii++) {
                                pCTmp = pC + ai + mri + (nri + nii + n_start) * m;
                                for (int mii = 0; mii < mrLen; mii++) {
                                    pApackTmp = pApack + (mri + mii) * kc; // <-- transposed packed!
                                    pBpackTmp = pBpack + (nri + nii) * kc;
                                    float sum = 0;

                                    int jj = (int)kc;
                                    for (; jj > 8; jj -= 8) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3]
                                            + pApackTmp[4] * pBpackTmp[4]
                                            + pApackTmp[5] * pBpackTmp[5]
                                            + pApackTmp[6] * pBpackTmp[6]
                                            + pApackTmp[7] * pBpackTmp[7];

                                        pApackTmp += 8;
                                        pBpackTmp += 8;

                                    }
                                    for (; jj > 4; jj -= 4) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3];

                                        pApackTmp += 4;
                                        pBpackTmp += 4;

                                    }
                                    for (; jj --> 0; ) {
                                        sum += *pApackTmp++ * *pBpackTmp++;
                                    }
                                    pCTmp[mii] += sum;

                                    #region vector attempt (obsolete, not working)
                                    //sum += Vector.Dot(
                                    //    new Vector<float>(tmpA, jj), 
                                    //    new Vector<float>(tmpA, jj + VectLen));
                                    //sum += Vector.Dot(
                                    //    new Vector<float>(tmpA, jj + VectLen * 2), 
                                    //    new Vector<float>(tmpA, jj + VectLen * 1));



                                    //a1 = Unsafe.As<float, Vector<float>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<float, Vector<float>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //sum += Vector.Dot(new Vector<float>(tmpA,jj), new Vector<float>(tmpA,jj+VectLen));
                                    //sum += Vector.Dot(new Vector<float>(tmpA,jj+VectLen*2), new Vector<float>(tmpA,jj+VectLen*3));

                                    //Unsafe.CopyBlock(pA_, pApackTmp, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //Unsafe.CopyBlock(pA_, pApackTmp + 4, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp + 4, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;




                                    //a1 = Unsafe.As<float, Vector<float>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<float, Vector<float>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //a1 = Unsafe.As<float, Vector<float>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<float, Vector<float>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;


                                    //*addressOfa1 = (IntPtr)pApackTmp;
                                    //*addressOfb1 = (IntPtr)pBpackTmp;
                                    //sum += Vector.Dot(a1, b1);

                                    //*addressOfa1 = (IntPtr)(pApackTmp + 4);
                                    //*addressOfb1 = (IntPtr)(pBpackTmp + 4);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;


                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //00007FFA0FB2E48B  jle         00007FFA0FB2E530  
                                    //                                        // original version
                                    //                                        sum += pApackTmp[0] * pBpackTmp[0]
                                    //                                            + pApackTmp[1] * pBpackTmp[1]
                                    //                                            + pApackTmp[2] * pBpackTmp[2]
                                    //                                            + pApackTmp[3] * pBpackTmp[3]
                                    //                                            + pApackTmp[4] * pBpackTmp[4]
                                    //                                            + pApackTmp[5] * pBpackTmp[5]
                                    //                                            + pApackTmp[6] * pBpackTmp[6]
                                    //                                            + pApackTmp[7] * pBpackTmp[7];

                                    //00007FFA0FB2E491  vmovsd      xmm1,qword ptr [r13]  
                                    //00007FFA0FB2E497  vmulsd      xmm1,xmm1,mmword ptr [rbp]  
                                    //00007FFA0FB2E49D  vmovsd      xmm2,qword ptr [r13+8]  
                                    //00007FFA0FB2E4A3  vmulsd      xmm2,xmm2,mmword ptr [rbp+8]  
                                    //00007FFA0FB2E4A9  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4AE  vmovsd      xmm2,qword ptr [r13+10h]  
                                    //00007FFA0FB2E4B4  vmulsd      xmm2,xmm2,mmword ptr [rbp+10h]  
                                    //00007FFA0FB2E4BA  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4BF  vmovsd      xmm2,qword ptr [r13+18h]  
                                    //00007FFA0FB2E4C5  vmulsd      xmm2,xmm2,mmword ptr [rbp+18h]  
                                    //00007FFA0FB2E4CB  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4D0  vmovsd      xmm2,qword ptr [r13+20h]  
                                    //00007FFA0FB2E4D6  vmulsd      xmm2,xmm2,mmword ptr [rbp+20h]  
                                    //00007FFA0FB2E4DC  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4E1  vmovsd      xmm2,qword ptr [r13+28h]  
                                    //00007FFA0FB2E4E7  vmulsd      xmm2,xmm2,mmword ptr [rbp+28h]  
                                    //00007FFA0FB2E4ED  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4F2  vmovsd      xmm2,qword ptr [r13+30h]  
                                    //00007FFA0FB2E4F8  vmulsd      xmm2,xmm2,mmword ptr [rbp+30h]  
                                    //00007FFA0FB2E4FE  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E503  vmovsd      xmm2,qword ptr [r13+38h]  
                                    //00007FFA0FB2E509  vmulsd      xmm2,xmm2,mmword ptr [rbp+38h]  
                                    //00007FFA0FB2E50F  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E514  vaddsd      xmm0,xmm0,xmm1  

                                    //                                        pApackTmp += 8;
                                    //00007FFA0FB2E519  add         r13,40h  
                                    //                                        pBpackTmp += 8;
                                    //00007FFA0FB2E51D  add         rbp,40h  
                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }


        
        unsafe public static void MMultBlocked(fcomplex* A, fcomplex* B, fcomplex* C, long m, long n, long k, long kc,
                                            long strideA0, long strideA1, long strideB0, long strideB1) {

            // block parameters
            uint mc = 256;
            uint mr = 12;
            uint nr = 12;

            MemoryHandle Apack = null;
            MemoryHandle Bpack = null;
            try {

                Apack = ILNumerics.Core.Functions.Builtin.MathInternal.New<fcomplex>((ulong)(kc * mc + ALIGN));
                Bpack = ILNumerics.Core.Functions.Builtin.MathInternal.New<fcomplex>((ulong)(kc * n + ALIGN));

                inner_k_loop_managed(m, n, k, kc, mc, mr, nr, A, B, C, (fcomplex*)Bpack.Pointer, (fcomplex*)Apack.Pointer, 0, n, 0, m,
                                    strideA0, strideA1, strideB0, strideB1);

            } finally {

                if (Apack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<fcomplex>(Apack, 0);
                if (Bpack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<fcomplex>(Bpack, 0);
            }
        }

        unsafe private static void inner_k_loop_managed(long m, long n, long k, long kc, uint mc, uint mr, uint nr,
                                            fcomplex* pA, fcomplex* pB, fcomplex* pC, fcomplex* pBpack, fcomplex* pApack,
                                            long n_start, long n_end, long m_start, long m_end, 
                                            long strideA0, long strideA1, long strideB0, long strideB1) {
            // Defaults: 
            // kc (settings):150
            // mc: 512
            // mr: 4
            // nr: 4

            pApack = (fcomplex*)((byte*)pApack + (ALIGN - ((ulong)pApack % ALIGN)));
            pBpack = (fcomplex*)((byte*)pBpack + (ALIGN - ((ulong)pBpack % ALIGN)));
            fcomplex* pApackTmp; 
            fcomplex* pBpackTmp; 
            fcomplex* pBArrTmp, pAArrTmp, pCTmp;

            long n_len = n_end - n_start;
            for (long ki = 0; ki < k; ki += kc) {
                //handle end block
                if (k - ki < kc) kc = k - ki;

                #region pack B
                pBpackTmp = pBpack;
                for (long nb = 0; nb < n_len; nb++) {
                    pBArrTmp = pB + ki * strideB0 + strideB1 * (nb + n_start);
                    int c = 0;
                    for (; c < kc - 8; c += 8) {
                        pBpackTmp[0] = pBArrTmp[0 * strideB0];
                        pBpackTmp[1] = pBArrTmp[1 * strideB0];
                        pBpackTmp[2] = pBArrTmp[2 * strideB0];
                        pBpackTmp[3] = pBArrTmp[3 * strideB0];
                        pBpackTmp[4] = pBArrTmp[4 * strideB0];
                        pBpackTmp[5] = pBArrTmp[5 * strideB0];
                        pBpackTmp[6] = pBArrTmp[6 * strideB0];
                        pBpackTmp[7] = pBArrTmp[7 * strideB0];
                        pBpackTmp += 8; pBArrTmp += 8 * strideB0;
                    }
                    for (; c < kc; c++) {
                        *pBpackTmp++ = *pBArrTmp;
                        pBArrTmp += strideB0; 
                    }
                }
                #endregion

                long mcc = mc;
                long m_len = m_end - m_start;
                for (long ai = 0; ai < m_len; ai += mcc) {
                    //handle end block
                    if (m_len - ai < mcc) mcc = m_len - ai;

                    #region pack A (transposed)
                    pApackTmp = pApack;
                    for (long ra = 0; ra < mcc; ra++) {
                        pAArrTmp = pA + (ra + ai) * strideA0 + strideA1 * (m_start + ki);
                        long ca = 0; 
                        for (; ca < kc - 8; ca += 8) {

                            pApackTmp[0] = pAArrTmp[0 * strideA1];
                            pApackTmp[1] = pAArrTmp[1 * strideA1];
                            pApackTmp[2] = pAArrTmp[2 * strideA1];
                            pApackTmp[3] = pAArrTmp[3 * strideA1];
                            pApackTmp[4] = pAArrTmp[4 * strideA1];
                            pApackTmp[5] = pAArrTmp[5 * strideA1];
                            pApackTmp[6] = pAArrTmp[6 * strideA1];
                            pApackTmp[7] = pAArrTmp[7 * strideA1];
                            pApackTmp += 8; pAArrTmp += 8 * strideA1;
                        }
                        for (; ca < kc; ca++) {
                            *(pApackTmp++) = *pAArrTmp;
                            pAArrTmp += strideA1; 
                        }
                    }
                    #endregion

                    #region subblocked
                    long nrLen = nr;
                    for (long nri = 0; nri < n_len; nri += nrLen) {
                        if (n_len - nri < nrLen) nrLen = n_len - nri;
                        long mrLen = mr;
                        for (long mri = 0; mri < mcc; mri += mrLen) {
                            if (mcc - mri < mrLen) mrLen = mcc - mri;

                            // prefetch CAux 
                            for (int nii = 0; nii < nrLen; nii++) {
                                pCTmp = pC + ai + mri + (nri + nii + n_start) * m;
                                for (int mii = 0; mii < mrLen; mii++) {
                                    pApackTmp = pApack + (mri + mii) * kc; // <-- transposed packed!
                                    pBpackTmp = pBpack + (nri + nii) * kc;
                                    fcomplex sum = 0;

                                    int jj = (int)kc;
                                    for (; jj > 8; jj -= 8) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3]
                                            + pApackTmp[4] * pBpackTmp[4]
                                            + pApackTmp[5] * pBpackTmp[5]
                                            + pApackTmp[6] * pBpackTmp[6]
                                            + pApackTmp[7] * pBpackTmp[7];

                                        pApackTmp += 8;
                                        pBpackTmp += 8;

                                    }
                                    for (; jj > 4; jj -= 4) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3];

                                        pApackTmp += 4;
                                        pBpackTmp += 4;

                                    }
                                    for (; jj --> 0; ) {
                                        sum += *pApackTmp++ * *pBpackTmp++;
                                    }
                                    pCTmp[mii] += sum;

                                    #region vector attempt (obsolete, not working)
                                    //sum += Vector.Dot(
                                    //    new Vector<fcomplex>(tmpA, jj), 
                                    //    new Vector<fcomplex>(tmpA, jj + VectLen));
                                    //sum += Vector.Dot(
                                    //    new Vector<fcomplex>(tmpA, jj + VectLen * 2), 
                                    //    new Vector<fcomplex>(tmpA, jj + VectLen * 1));



                                    //a1 = Unsafe.As<fcomplex, Vector<fcomplex>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<fcomplex, Vector<fcomplex>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //sum += Vector.Dot(new Vector<fcomplex>(tmpA,jj), new Vector<fcomplex>(tmpA,jj+VectLen));
                                    //sum += Vector.Dot(new Vector<fcomplex>(tmpA,jj+VectLen*2), new Vector<fcomplex>(tmpA,jj+VectLen*3));

                                    //Unsafe.CopyBlock(pA_, pApackTmp, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //Unsafe.CopyBlock(pA_, pApackTmp + 4, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp + 4, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;




                                    //a1 = Unsafe.As<fcomplex, Vector<fcomplex>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<fcomplex, Vector<fcomplex>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //a1 = Unsafe.As<fcomplex, Vector<fcomplex>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<fcomplex, Vector<fcomplex>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;


                                    //*addressOfa1 = (IntPtr)pApackTmp;
                                    //*addressOfb1 = (IntPtr)pBpackTmp;
                                    //sum += Vector.Dot(a1, b1);

                                    //*addressOfa1 = (IntPtr)(pApackTmp + 4);
                                    //*addressOfb1 = (IntPtr)(pBpackTmp + 4);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;


                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //00007FFA0FB2E48B  jle         00007FFA0FB2E530  
                                    //                                        // original version
                                    //                                        sum += pApackTmp[0] * pBpackTmp[0]
                                    //                                            + pApackTmp[1] * pBpackTmp[1]
                                    //                                            + pApackTmp[2] * pBpackTmp[2]
                                    //                                            + pApackTmp[3] * pBpackTmp[3]
                                    //                                            + pApackTmp[4] * pBpackTmp[4]
                                    //                                            + pApackTmp[5] * pBpackTmp[5]
                                    //                                            + pApackTmp[6] * pBpackTmp[6]
                                    //                                            + pApackTmp[7] * pBpackTmp[7];

                                    //00007FFA0FB2E491  vmovsd      xmm1,qword ptr [r13]  
                                    //00007FFA0FB2E497  vmulsd      xmm1,xmm1,mmword ptr [rbp]  
                                    //00007FFA0FB2E49D  vmovsd      xmm2,qword ptr [r13+8]  
                                    //00007FFA0FB2E4A3  vmulsd      xmm2,xmm2,mmword ptr [rbp+8]  
                                    //00007FFA0FB2E4A9  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4AE  vmovsd      xmm2,qword ptr [r13+10h]  
                                    //00007FFA0FB2E4B4  vmulsd      xmm2,xmm2,mmword ptr [rbp+10h]  
                                    //00007FFA0FB2E4BA  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4BF  vmovsd      xmm2,qword ptr [r13+18h]  
                                    //00007FFA0FB2E4C5  vmulsd      xmm2,xmm2,mmword ptr [rbp+18h]  
                                    //00007FFA0FB2E4CB  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4D0  vmovsd      xmm2,qword ptr [r13+20h]  
                                    //00007FFA0FB2E4D6  vmulsd      xmm2,xmm2,mmword ptr [rbp+20h]  
                                    //00007FFA0FB2E4DC  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4E1  vmovsd      xmm2,qword ptr [r13+28h]  
                                    //00007FFA0FB2E4E7  vmulsd      xmm2,xmm2,mmword ptr [rbp+28h]  
                                    //00007FFA0FB2E4ED  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4F2  vmovsd      xmm2,qword ptr [r13+30h]  
                                    //00007FFA0FB2E4F8  vmulsd      xmm2,xmm2,mmword ptr [rbp+30h]  
                                    //00007FFA0FB2E4FE  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E503  vmovsd      xmm2,qword ptr [r13+38h]  
                                    //00007FFA0FB2E509  vmulsd      xmm2,xmm2,mmword ptr [rbp+38h]  
                                    //00007FFA0FB2E50F  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E514  vaddsd      xmm0,xmm0,xmm1  

                                    //                                        pApackTmp += 8;
                                    //00007FFA0FB2E519  add         r13,40h  
                                    //                                        pBpackTmp += 8;
                                    //00007FFA0FB2E51D  add         rbp,40h  
                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }


        
        unsafe public static void MMultBlocked(complex* A, complex* B, complex* C, long m, long n, long k, long kc,
                                            long strideA0, long strideA1, long strideB0, long strideB1) {

            // block parameters
            uint mc = 256;
            uint mr = 12;
            uint nr = 12;

            MemoryHandle Apack = null;
            MemoryHandle Bpack = null;
            try {

                Apack = ILNumerics.Core.Functions.Builtin.MathInternal.New<complex>((ulong)(kc * mc + ALIGN));
                Bpack = ILNumerics.Core.Functions.Builtin.MathInternal.New<complex>((ulong)(kc * n + ALIGN));

                inner_k_loop_managed(m, n, k, kc, mc, mr, nr, A, B, C, (complex*)Bpack.Pointer, (complex*)Apack.Pointer, 0, n, 0, m,
                                    strideA0, strideA1, strideB0, strideB1);

            } finally {

                if (Apack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<complex>(Apack, 0);
                if (Bpack != null) ILNumerics.Core.Functions.Builtin.MathInternal.free<complex>(Bpack, 0);
            }
        }

        unsafe private static void inner_k_loop_managed(long m, long n, long k, long kc, uint mc, uint mr, uint nr,
                                            complex* pA, complex* pB, complex* pC, complex* pBpack, complex* pApack,
                                            long n_start, long n_end, long m_start, long m_end, 
                                            long strideA0, long strideA1, long strideB0, long strideB1) {
            // Defaults: 
            // kc (settings):150
            // mc: 512
            // mr: 4
            // nr: 4

            pApack = (complex*)((byte*)pApack + (ALIGN - ((ulong)pApack % ALIGN)));
            pBpack = (complex*)((byte*)pBpack + (ALIGN - ((ulong)pBpack % ALIGN)));
            complex* pApackTmp; 
            complex* pBpackTmp; 
            complex* pBArrTmp, pAArrTmp, pCTmp;

            long n_len = n_end - n_start;
            for (long ki = 0; ki < k; ki += kc) {
                //handle end block
                if (k - ki < kc) kc = k - ki;

                #region pack B
                pBpackTmp = pBpack;
                for (long nb = 0; nb < n_len; nb++) {
                    pBArrTmp = pB + ki * strideB0 + strideB1 * (nb + n_start);
                    int c = 0;
                    for (; c < kc - 8; c += 8) {
                        pBpackTmp[0] = pBArrTmp[0 * strideB0];
                        pBpackTmp[1] = pBArrTmp[1 * strideB0];
                        pBpackTmp[2] = pBArrTmp[2 * strideB0];
                        pBpackTmp[3] = pBArrTmp[3 * strideB0];
                        pBpackTmp[4] = pBArrTmp[4 * strideB0];
                        pBpackTmp[5] = pBArrTmp[5 * strideB0];
                        pBpackTmp[6] = pBArrTmp[6 * strideB0];
                        pBpackTmp[7] = pBArrTmp[7 * strideB0];
                        pBpackTmp += 8; pBArrTmp += 8 * strideB0;
                    }
                    for (; c < kc; c++) {
                        *pBpackTmp++ = *pBArrTmp;
                        pBArrTmp += strideB0; 
                    }
                }
                #endregion

                long mcc = mc;
                long m_len = m_end - m_start;
                for (long ai = 0; ai < m_len; ai += mcc) {
                    //handle end block
                    if (m_len - ai < mcc) mcc = m_len - ai;

                    #region pack A (transposed)
                    pApackTmp = pApack;
                    for (long ra = 0; ra < mcc; ra++) {
                        pAArrTmp = pA + (ra + ai) * strideA0 + strideA1 * (m_start + ki);
                        long ca = 0; 
                        for (; ca < kc - 8; ca += 8) {

                            pApackTmp[0] = pAArrTmp[0 * strideA1];
                            pApackTmp[1] = pAArrTmp[1 * strideA1];
                            pApackTmp[2] = pAArrTmp[2 * strideA1];
                            pApackTmp[3] = pAArrTmp[3 * strideA1];
                            pApackTmp[4] = pAArrTmp[4 * strideA1];
                            pApackTmp[5] = pAArrTmp[5 * strideA1];
                            pApackTmp[6] = pAArrTmp[6 * strideA1];
                            pApackTmp[7] = pAArrTmp[7 * strideA1];
                            pApackTmp += 8; pAArrTmp += 8 * strideA1;
                        }
                        for (; ca < kc; ca++) {
                            *(pApackTmp++) = *pAArrTmp;
                            pAArrTmp += strideA1; 
                        }
                    }
                    #endregion

                    #region subblocked
                    long nrLen = nr;
                    for (long nri = 0; nri < n_len; nri += nrLen) {
                        if (n_len - nri < nrLen) nrLen = n_len - nri;
                        long mrLen = mr;
                        for (long mri = 0; mri < mcc; mri += mrLen) {
                            if (mcc - mri < mrLen) mrLen = mcc - mri;

                            // prefetch CAux 
                            for (int nii = 0; nii < nrLen; nii++) {
                                pCTmp = pC + ai + mri + (nri + nii + n_start) * m;
                                for (int mii = 0; mii < mrLen; mii++) {
                                    pApackTmp = pApack + (mri + mii) * kc; // <-- transposed packed!
                                    pBpackTmp = pBpack + (nri + nii) * kc;
                                    complex sum = 0;

                                    int jj = (int)kc;
                                    for (; jj > 8; jj -= 8) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3]
                                            + pApackTmp[4] * pBpackTmp[4]
                                            + pApackTmp[5] * pBpackTmp[5]
                                            + pApackTmp[6] * pBpackTmp[6]
                                            + pApackTmp[7] * pBpackTmp[7];

                                        pApackTmp += 8;
                                        pBpackTmp += 8;

                                    }
                                    for (; jj > 4; jj -= 4) {
                                        sum += pApackTmp[0] * pBpackTmp[0]
                                            + pApackTmp[1] * pBpackTmp[1]
                                            + pApackTmp[2] * pBpackTmp[2]
                                            + pApackTmp[3] * pBpackTmp[3];

                                        pApackTmp += 4;
                                        pBpackTmp += 4;

                                    }
                                    for (; jj --> 0; ) {
                                        sum += *pApackTmp++ * *pBpackTmp++;
                                    }
                                    pCTmp[mii] += sum;

                                    #region vector attempt (obsolete, not working)
                                    //sum += Vector.Dot(
                                    //    new Vector<complex>(tmpA, jj), 
                                    //    new Vector<complex>(tmpA, jj + VectLen));
                                    //sum += Vector.Dot(
                                    //    new Vector<complex>(tmpA, jj + VectLen * 2), 
                                    //    new Vector<complex>(tmpA, jj + VectLen * 1));



                                    //a1 = Unsafe.As<complex, Vector<complex>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<complex, Vector<complex>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 2;
                                    //pBpackTmp += 2;

                                    //sum += Vector.Dot(new Vector<complex>(tmpA,jj), new Vector<complex>(tmpA,jj+VectLen));
                                    //sum += Vector.Dot(new Vector<complex>(tmpA,jj+VectLen*2), new Vector<complex>(tmpA,jj+VectLen*3));

                                    //Unsafe.CopyBlock(pA_, pApackTmp, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //Unsafe.CopyBlock(pA_, pApackTmp + 4, 32);
                                    //Unsafe.CopyBlock(pB_, pBpackTmp + 4, 32);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;




                                    //a1 = Unsafe.As<complex, Vector<complex>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<complex, Vector<complex>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //a1 = Unsafe.As<complex, Vector<complex>>(ref *pApackTmp);
                                    //b1 = Unsafe.As<complex, Vector<complex>>(ref *pBpackTmp);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;


                                    //*addressOfa1 = (IntPtr)pApackTmp;
                                    //*addressOfb1 = (IntPtr)pBpackTmp;
                                    //sum += Vector.Dot(a1, b1);

                                    //*addressOfa1 = (IntPtr)(pApackTmp + 4);
                                    //*addressOfb1 = (IntPtr)(pBpackTmp + 4);
                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;


                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //pA_[0] = pApackTmp[0];
                                    //pA_[1] = pApackTmp[1];
                                    //pA_[2] = pApackTmp[2];
                                    //pA_[3] = pApackTmp[3];

                                    //pB_[0] = pBpackTmp[0];
                                    //pB_[1] = pBpackTmp[1];
                                    //pB_[2] = pBpackTmp[2];
                                    //pB_[3] = pBpackTmp[3];

                                    //sum += Vector.Dot(a1, b1);

                                    //pApackTmp += 4;
                                    //pBpackTmp += 4;

                                    //00007FFA0FB2E48B  jle         00007FFA0FB2E530  
                                    //                                        // original version
                                    //                                        sum += pApackTmp[0] * pBpackTmp[0]
                                    //                                            + pApackTmp[1] * pBpackTmp[1]
                                    //                                            + pApackTmp[2] * pBpackTmp[2]
                                    //                                            + pApackTmp[3] * pBpackTmp[3]
                                    //                                            + pApackTmp[4] * pBpackTmp[4]
                                    //                                            + pApackTmp[5] * pBpackTmp[5]
                                    //                                            + pApackTmp[6] * pBpackTmp[6]
                                    //                                            + pApackTmp[7] * pBpackTmp[7];

                                    //00007FFA0FB2E491  vmovsd      xmm1,qword ptr [r13]  
                                    //00007FFA0FB2E497  vmulsd      xmm1,xmm1,mmword ptr [rbp]  
                                    //00007FFA0FB2E49D  vmovsd      xmm2,qword ptr [r13+8]  
                                    //00007FFA0FB2E4A3  vmulsd      xmm2,xmm2,mmword ptr [rbp+8]  
                                    //00007FFA0FB2E4A9  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4AE  vmovsd      xmm2,qword ptr [r13+10h]  
                                    //00007FFA0FB2E4B4  vmulsd      xmm2,xmm2,mmword ptr [rbp+10h]  
                                    //00007FFA0FB2E4BA  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4BF  vmovsd      xmm2,qword ptr [r13+18h]  
                                    //00007FFA0FB2E4C5  vmulsd      xmm2,xmm2,mmword ptr [rbp+18h]  
                                    //00007FFA0FB2E4CB  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4D0  vmovsd      xmm2,qword ptr [r13+20h]  
                                    //00007FFA0FB2E4D6  vmulsd      xmm2,xmm2,mmword ptr [rbp+20h]  
                                    //00007FFA0FB2E4DC  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4E1  vmovsd      xmm2,qword ptr [r13+28h]  
                                    //00007FFA0FB2E4E7  vmulsd      xmm2,xmm2,mmword ptr [rbp+28h]  
                                    //00007FFA0FB2E4ED  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E4F2  vmovsd      xmm2,qword ptr [r13+30h]  
                                    //00007FFA0FB2E4F8  vmulsd      xmm2,xmm2,mmword ptr [rbp+30h]  
                                    //00007FFA0FB2E4FE  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E503  vmovsd      xmm2,qword ptr [r13+38h]  
                                    //00007FFA0FB2E509  vmulsd      xmm2,xmm2,mmword ptr [rbp+38h]  
                                    //00007FFA0FB2E50F  vaddsd      xmm1,xmm1,xmm2  
                                    //00007FFA0FB2E514  vaddsd      xmm0,xmm0,xmm1  

                                    //                                        pApackTmp += 8;
                                    //00007FFA0FB2E519  add         r13,40h  
                                    //                                        pBpackTmp += 8;
                                    //00007FFA0FB2E51D  add         rbp,40h  
                                    //pApackTmp += 8;
                                    //pBpackTmp += 8;
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE
    }
}
