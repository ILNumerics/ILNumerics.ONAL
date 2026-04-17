using System;
using ILNumerics; 

namespace ILNumerics.Core.Native {

    /// <summary>
    /// Interface for all FFT methods supported
    /// </summary>
    public interface IFFT {
        /// <summary>
        /// performs backward n-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimensions of fft</param>
        /// <returns>result, same size as A</returns>
        Array<complex> FFTBackward(InArray<complex> A, uint nDims);
        /// <summary>
        /// performs backward n-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimensions of fft</param>
        /// <returns>result, same size as A</returns>
        Array<fcomplex> FFTBackward(InArray<fcomplex> A, uint nDims);
        /// <summary>
        /// performs backward 1-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        Array<fcomplex> FFTBackward1D(InArray<fcomplex> A, uint dim);
        /// <summary>
        /// performs backward 1-dimensional fft 
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        Array<complex> FFTBackward1D(InArray<complex> A, uint dim);
        /// <summary>
        /// performs backward n-dimensional fft on hermitian sequence
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimensions of fft</param>
        /// <returns>result, same size as A</returns>
        /// <remarks>This function brings increased performance if the implementation supports it. 
        /// If not, the method will be implemented by repeated calls of (inplace) 1D fft.</remarks>
        Array<float> FFTBackwSym(InArray<fcomplex> A, uint nDims);
        /// <summary>
        /// performs backward n-dimensional fft on hermitian sequence
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimensions of fft</param>
        /// <returns>result, same size as A</returns>
        /// <remarks>This function brings increased performance if the implementation supports it. 
        /// If not, the method will be implemented by repeated calls of (inplace) 1D fft.</remarks>
        Array<double> FFTBackwSym(InArray<complex> A, uint nDims);
        /// <summary>
        /// performs backward 1-dimensional fft on hermitian sequence
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        /// <remarks>This function brings increased performance if the implementation supports it. 
        /// If not, the method will be implemented by repeated calls of (inplace) 1D fft.</remarks>
        Array<float> FFTBackwSym1D(InArray<fcomplex> A, uint dim);
        /// <summary>
        /// performs backward 1-dimensional fft on hermitian sequence
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        /// <remarks>This function brings increased performance if the implementation supports it. 
        /// If not, the method will be implemented by repeated calls of (inplace) 1D fft.</remarks>
        Array<double> FFTBackwSym1D(InArray<complex> A, uint dim);
        /// <summary>
        /// performs n-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimension of fft</param>
        /// <returns>result, same size as A</returns>
        Array<fcomplex> FFTForward(InArray<fcomplex> A, uint nDims);
        /// <summary>
        /// performs n-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimension of fft</param>
        /// <returns>result, same size as A</returns>
        Array<complex> FFTForward(InArray<double> A, uint nDims);
        /// <summary>
        /// performs n-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimension of fft</param>
        /// <returns>result, same size as A</returns>
        Array<complex> FFTForward(InArray<complex> A, uint nDims);
        /// <summary>
        /// performs n-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="nDims">number of dimension of fft</param>
        /// <returns>result, same size as A</returns>
        Array<fcomplex> FFTForward(InArray<float> A, uint nDims);
        /// <summary>
        /// performs 1-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        Array<complex> FFTForward1D(InArray<complex> A, uint dim);
        /// <summary>
        /// performs 1-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        Array<complex> FFTForward1D(InArray<double> A, uint dim);
        /// <summary>
        /// performs 1-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        Array<fcomplex> FFTForward1D(InArray<fcomplex> A, uint dim);
        /// <summary>
        /// performs 1-dimensional fft
        /// </summary>
        /// <param name="A">input array</param>
        /// <param name="dim">dimension to perform fft along</param>
        /// <returns>result, same size as A</returns>
        Array<fcomplex> FFTForward1D(InArray<float> A, uint dim);
        /// <summary>
        /// true, if the implementation caches plans between subsequent calls
        /// </summary>
        bool CachePlans { get; }
        /// <summary>
        /// Clear all currently cached plans. Tasks like FreePlans should be left to the ILNumerics memory management. Calling the method manually (without good reasons) may lead to poor performance! 
        /// </summary>
        void FreePlans();
        /// <summary>
        /// true, if the implementation efficiently transforms from/to hermitian sequences (hermitian symmetry). 
        /// </summary>
        /// <remarks>If this property returns 'true', the implementation brings increased performance. 
        /// If not, the symmetry methods will bring no performance advantage over the 1D transforms. </remarks>
        bool SpeedyHermitian { get; }

    }
}
