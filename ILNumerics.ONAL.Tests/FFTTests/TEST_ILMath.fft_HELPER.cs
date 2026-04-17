using System;
using ILNumerics;
using ILNumerics.Core.Misc;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath;


namespace ILNumerics.UnitTests
{
    class TEST_ILMath_fft_HELPER 
    {
        public static void forwBackwCheck1D(Array<double> A, Array<complex> Result)
        {
            try
            {
                Array<complex> B = fft(A);
                //double errMult = 1/(A.Size.NumberOfElements * Math.Pow(10,A.Size.NumberOfDimensions));
                double errMult = Math.Pow(0.1, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (double)A.Size[A.Size.WorkingDimension()];
                if (sumall(abs(subtract(Result, B))) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                Array<double> ResultR = ifftsym(B);
                if (sumall(abs(ResultR - A)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                B = ifft(B);
                if (sumall(abs(tocomplex(A) - B)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException)
            {
                throw new Exception("unexpected exception was thrown -> error!");
            }
        }

        public static void forwBackwCheck1D(Array<float> A, Array<fcomplex> Result)
        {
            try
            {
                Array<fcomplex> B = fft(A);
                //double errMult = 1/(A.Size.NumberOfElements * Math.Pow(10,A.Size.NumberOfDimensions));
                float errMult = (float)Math.Pow(0.1f, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (float)A.Size[A.Size.WorkingDimension()];
                if (sumall(abs(subtract(Result, B))) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                Array<float> ResultR = ifftsym(B);
                if (sumall(abs(ResultR - A)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                B = ifft(B);
                if (sumall(abs(tofcomplex(A) - B)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException)
            {
                throw new Exception("unexpected exception was thrown -> error!");
            }
        }

        public static void forwBackwCheck1DAlongD(Array<double> A, Array<complex> Result, uint p)
        {
            try
            {
                Array<complex> B = fft(A, p);
                //double errMult = 1/(A.Size.NumberOfElements * Math.Pow(10,A.Size.NumberOfDimensions));
                double errMult = Math.Pow(0.1, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (double)A.Size[A.Size.WorkingDimension()];
                if (sumall(abs(subtract(Result, B))) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                Array<double> ResultR = ifftsym(B, p);
                if (sumall(abs(ResultR - A)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                B = ifft(B, p);
                if (sumall(abs(tocomplex(A) - B)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        public static void forwBackwCheck1DAlongD(Array<float> A, Array<fcomplex> Result, uint p)
        {
            try
            {
                Array<fcomplex> B = fft(A, p);
                //double errMult = 1/(A.Size.NumberOfElements * Math.Pow(10,A.Size.NumberOfDimensions));
                float errMult = (float)Math.Pow(0.1f, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (float)A.Size[A.Size.WorkingDimension()];
                if (sumall(abs(subtract(Result, B))) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                Array<float> ResultR = ifftsym(B, p);
                if (sumall(abs(ResultR - A)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                B = ifft(B, p);
                if (sumall(abs(tofcomplex(A) - B)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        public static void forwBackwCheck2D(Array<double> A, Array<complex> Result)
        {
            try
            {
                Array<complex> B = fft2(A);
                //double errMult = 1/(A.Size.NumberOfElements * Math.Pow(10,A.Size.NumberOfDimensions));
                double errMult = Math.Pow(0.1, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (double)A.Size[A.Size.WorkingDimension()];
                if (sumall(abs(subtract(Result, B))) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                Array<double> ResultR = ifft2sym(B);
                if (sumall(abs(ResultR - A)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                B = ifft2(B);
                if (sumall(abs(tocomplex(A) - B)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        public static void forwBackwCheck2D(Array<float> A, Array<fcomplex> Result)
        {
            try
            {
                Array<fcomplex> B = fft2(A);
                //double errMult = 1/(A.Size.NumberOfElements * Math.Pow(10,A.Size.NumberOfDimensions));
                float errMult = (float)Math.Pow(0.1f, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (float)A.Size[A.Size.WorkingDimension()];
                if (sumall(abs(subtract(Result, B))) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                Array<float> ResultR = ifft2sym(B);
                if (sumall(abs(ResultR - A)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                B = ifft2(B);
                if (sumall(abs(tofcomplex(A) - B)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        public static void forwBackwCheck2Dmn(Array<double> A, Array<complex> Result, Array<double> ResRA, Array<complex> ResIA, uint m, uint n)
        {
            try
            {
                Array<complex> B = fft2(A, m, n);
                Array<long> sizes = A.shape;
                sizes[0, 0] = m; sizes[1, 0] = n; 
                double errMult = Math.Pow(0.1, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (double)A.Size[A.Size.WorkingDimension()];
                if (m == 0 || n == 0)
                {
                    if (!B.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(subtract(Result, B))) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                Array<double> ResultR = ifft2sym(B);
                if (m == 0 || n == 0)
                {
                    if (!ResultR.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResultR - resize4Transform<double>(A, sizes))) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                Array<complex> ResultC = ifft2(tocomplex(A), m, n);
                if (m == 0 || n == 0)
                {
                    if (!ResultC.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResIA - ResultC)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        public static void forwBackwCheck2Dmn(Array<float> A, Array<fcomplex> Result, Array<float> ResRA, Array<fcomplex> ResIA, uint m, uint n)
        {
            try
            {
                Array<fcomplex> B = fft2(A, m, n);
                Array<long> sizes = A.shape;
                sizes[0, 0] = m; sizes[1, 0] = n; 
                float errMult = (float)Math.Pow(0.1f, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (float)A.Size[A.Size.WorkingDimension()];
                if (m == 0 || n == 0)
                {
                    if (!B.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(subtract(Result, B))) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                Array<float> ResultR = ifft2sym(B);
                if (m == 0 || n == 0)
                {
                    if (!ResultR.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResultR - resize4Transform<float>(A, sizes))) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                Array<fcomplex> ResultC = ifft2(tofcomplex(A), m, n);
                if (m == 0 || n == 0)
                {
                    if (!B.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResultC - ResIA)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        public static void forwBackwCheckNDmn(Array<double> A, Array<complex> Result, Array<double> ResRA, Array<complex> ResIA, uint m, uint n, uint q)
        {
            try
            {
                Array<long> sizes = A.shape;
                while (sizes.Length < 3) {
                    sizes[end + 1, 0] = 1; 
                }
                sizes[0] = m; sizes[1] = n; sizes[2] = q;
                Array<complex> B = fftn(A, sizes);

                double errMult = Math.Pow(0.1, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (double)A.Size[A.Size.WorkingDimension()];
                if (m == 0 || n == 0 || q == 0)
                {
                    if (!B.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(subtract(Result, B))) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                Array<double> ResultR = ifftnsym(B);
                if (m == 0 || n == 0 || q == 0)
                {
                    if (!ResultR.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResultR - resize4Transform<double>(A, sizes))) * errMult > Globals.eps)
                    throw new Exception("invalid value");
                Array<complex> ResultC = ifftn(tocomplex(A), sizes);
                if (m == 0 || n == 0 || q == 0)
                {
                    if (!ResultC.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResIA - ResultC)) * errMult > Globals.eps)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        public static void forwBackwCheckNDmn(Array<float> A, Array<fcomplex> Result, Array<float> ResRA, Array<fcomplex> ResIA, uint m, uint n, uint q)
        {
            try
            {
                Array<long> sizes = A.shape;
                sizes[0,0] = m; sizes[1,0] = n; sizes[2,0] = q;

                Array<fcomplex> B = fftn(A, sizes);
                //double errMult = 1/(A.Size.NumberOfElements * Math.Pow(10,A.Size.NumberOfDimensions));
                float errMult = (float)Math.Pow(0.1f, A.Size.NumberOfDimensions) / A.Size.NumberOfElements;
                if (!A.IsScalar)
                    errMult /= (float)A.Size[A.Size.WorkingDimension()];
                if (m == 0 || n == 0 || q == 0)
                {
                    if (!B.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(subtract(Result, B))) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                Array<float> ResultR = ifftnsym(B);
                if (m == 0 || n == 0 || q == 0)
                {
                    if (!ResultR.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResultR - resize4Transform<float>(A, sizes))) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
                Array<fcomplex> ResultC = ifftn(tofcomplex(A), sizes);
                if (m == 0 || n == 0 || q == 0)
                {
                    if (!B.IsEmpty) { throw new Exception("empty matrix should return empty!"); }
                }
                else if (sumall(abs(ResultC - ResIA)) * errMult > Globals.epsf)
                    throw new Exception("invalid value");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("unexpected exception was thrown -> error!", exc);
            }
        }

        internal static Array<T> resize4Transform<T>(InArray<T> A, InArray<long> size)
        {
            using (Scope.Enter(A, size))
            {
                if (Equals(size, null) || size.Length < A.Size.NumberOfDimensions)
                    throw new ArgumentException("length of output dimensions must be &gt; or equal to number of dimensions of input array!");
                Array<long> newDimensions = toint64(size.C);
                if (A.shape.Equals(size))
                {
                    return A;
                }
                else {
                    if (newDimensions.S.NumberOfElements == 0) return empty<T>(newDimensions);
                    Array<T> tmp = array<T>(default(T), newDimensions);
                    DimSpec[] Lindices = new DimSpec[size.Length];
                    DimSpec[] Rindices = new DimSpec[size.Length];
                    Array<T> ret = A.C;
                    while (ret.S.NumberOfDimensions < size.S.NumberOfElements) {
                        using (Settings.Ensure(()=> Settings.ArrayStyle, ArrayStyles.numpy))
                            ret.a = ret[ellipsis, newaxis]; 
                    }                    
                    Array<long> outSizes = min(ret.shape, B: toint64(size)) - 1; 
                    for (uint i = 0; i < size.Length; i++)
                    {
                        if (outSizes[i] < 0)
                            throw new ArgumentException("all dimension lengths of 'size' must be non-negative!");
                        if (outSizes[i] < 0) return empty<T>(outSizes);
                        Lindices[i] = Globals.r(0, outSizes[i]);
                        Rindices[i] = Globals.r(0, outSizes[i]);
                    }
                    tmp[Lindices] = ret[Rindices];
                    return tmp;
                }
            }
        }

    }
}
