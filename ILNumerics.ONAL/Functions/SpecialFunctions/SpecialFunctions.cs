using System;
using ILNumerics;
using ILNumerics.Core.Arrays;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics
{
    public static partial class ILMath {

        // Private constant values pre-allocated - for taking performance considerations into account
        #region Pre-allocated constants

        #region gammaLog constants

        private static readonly double[] gammaLogCoefs = new double[14]
                   {57.1562356658629235,-59.5979603554754912,14.13609797747417471,-0.491913816097620199,
                    0.339946499848118887e-4,0.465236289270485756e-4,-0.983744753048795646e-4,0.158088703224912494e-3,
                    -0.210264441724104883e-3,0.217439618115212643e-3,-0.164318106536763890e-3,
                    0.844182239838527433e-4,-0.261908384015814087e-4,0.3689918265955316234e-5};

        private static readonly double gammaRational = 671.0 / 128.0;

        #endregion

        #region bessel constants
        private static readonly double[] BesselI0A = { -4.41534164647933937950e-18, 3.33079451882223809783e-17, -2.43127984654795469359e-16, 1.71539128555513303061e-15, -1.16853328779934516808e-14, 7.67618549860493561688e-14, -4.85644678311192946090e-13, 2.95505266312963983461e-12, -1.72682629144155570723e-11, 9.67580903537323691224e-11, -5.18979560163526290666e-10, 2.65982372468238665035e-9, -1.30002500998624804212e-8, 6.04699502254191894932e-8, -2.67079385394061173391e-7, 1.11738753912010371815e-6, -4.41673835845875056359e-6, 1.64484480707288970893e-5, -5.75419501008210370398e-5, 1.88502885095841655729e-4, -5.76375574538582365885e-4, 1.63947561694133579842e-3, -4.32430999505057594430e-3, 1.05464603945949983183e-2, -2.37374148058994688156e-2, 4.93052842396707084878e-2, -9.49010970480476444210e-2, 1.71620901522208775349e-1, -3.04682672343198398683e-1, 6.76795274409476084995e-1 };
        private static readonly double[] BesselI0B = { -7.23318048787475395456e-18, -4.83050448594418207126e-18, 4.46562142029675999901e-17, 3.46122286769746109310e-17, -2.82762398051658348494e-16, -3.42548561967721913462e-16, 1.77256013305652638360e-15, 3.81168066935262242075e-15, -9.55484669882830764870e-15, -4.15056934728722208663e-14, 1.54008621752140982691e-14, 3.85277838274214270114e-13, 7.18012445138366623367e-13, -1.79417853150680611778e-12, -1.32158118404477131188e-11, -3.14991652796324136454e-11, 1.18891471078464383424e-11, 4.94060238822496958910e-10, 3.39623202570838634515e-9, 2.26666899049817806459e-8, 2.04891858946906374183e-7, 2.89137052083475648297e-6, 6.88975834691682398426e-5, 3.36911647825569408990e-3, 8.04490411014108831608e-1 };

        private static readonly double[] BesselJ0A = { -4.41534164647933937950e-18, 3.33079451882223809783e-17, -2.43127984654795469359e-16, 1.71539128555513303061e-15, -1.16853328779934516808e-14, 7.67618549860493561688e-14, -4.85644678311192946090e-13, 2.95505266312963983461e-12, -1.72682629144155570723e-11, 9.67580903537323691224e-11, -5.18979560163526290666e-10, 2.65982372468238665035e-9, -1.30002500998624804212e-8, 6.04699502254191894932e-8, -2.67079385394061173391e-7, 1.11738753912010371815e-6, -4.41673835845875056359e-6, 1.64484480707288970893e-5, -5.75419501008210370398e-5, 1.88502885095841655729e-4, -5.76375574538582365885e-4, 1.63947561694133579842e-3, -4.32430999505057594430e-3, 1.05464603945949983183e-2, -2.37374148058994688156e-2, 4.93052842396707084878e-2, -9.49010970480476444210e-2, 1.71620901522208775349e-1, -3.04682672343198398683e-1, 6.76795274409476084995e-1 };
        private static readonly double[] BesselJ0B = { -7.23318048787475395456e-18, -4.83050448594418207126e-18, 4.46562142029675999901e-17, 3.46122286769746109310e-17, -2.82762398051658348494e-16, -3.42548561967721913462e-16, 1.77256013305652638360e-15, 3.81168066935262242075e-15, -9.55484669882830764870e-15, -4.15056934728722208663e-14, 1.54008621752140982691e-14, 3.85277838274214270114e-13, 7.18012445138366623367e-13, -1.79417853150680611778e-12, -1.32158118404477131188e-11, -3.14991652796324136454e-11, 1.18891471078464383424e-11, 4.94060238822496958910e-10, 3.39623202570838634515e-9, 2.26666899049817806459e-8, 2.04891858946906374183e-7, 2.89137052083475648297e-6, 6.88975834691682398426e-5, 3.36911647825569408990e-3, 8.04490411014108831608e-1 };

        private static readonly double[] BesselI1A = { 2.77791411276104639959e-18, -2.11142121435816608115e-17, 1.55363195773620046921e-16, -1.10559694773538630805e-15, 7.60068429473540693410e-15, -5.04218550472791168711e-14, 3.22379336594557470981e-13, -1.98397439776494371520e-12, 1.17361862988909016308e-11, -6.66348972350202774223e-11, 3.62559028155211703701e-10, -1.88724975172282928790e-9, 9.38153738649577178388e-9, -4.44505912879632808065e-8, 2.00329475355213526229e-7, -8.56872026469545474066e-7, 3.47025130813767847674e-6, -1.32731636560394358279e-5, 4.78156510755005422638e-5, -1.61760815825896745588e-4, 5.12285956168575772895e-4, -1.51357245063125314899e-3, 4.15642294431288815669e-3, -1.05640848946261981558e-2, 2.47264490306265168283e-2, -5.29459812080949914269e-2, 1.02643658689847095384e-1, -1.76416518357834055153e-1, 2.52587186443633654823e-1 };
        private static readonly double[] BesselI1B = { 7.51729631084210481353e-18, 4.41434832307170791151e-18, -4.65030536848935832153e-17, -3.20952592199342395980e-17, 2.96262899764595013876e-16, 3.30820231092092828324e-16, -1.88035477551078244854e-15, -3.81440307243700780478e-15, 1.04202769841288027642e-14, 4.27244001671195135429e-14, -2.10154184277266431302e-14, -4.08355111109219731823e-13, -7.19855177624590851209e-13, 2.03562854414708950722e-12, 1.41258074366137813316e-11, 3.25260358301548823856e-11, -1.89749581235054123450e-11, -5.58974346219658380687e-10, -3.83538038596423702205e-9, -2.63146884688951950684e-8, -2.51223623787020892529e-7, -3.88256480887769039346e-6, -1.10588938762623716291e-4, -9.76109749136146840777e-3, 7.78576235018280120474e-1 };

        private static readonly double[] BesselK0A = { 1.37446543561352307156e-16, 4.25981614279661018399e-14, 1.03496952576338420167e-11, 1.90451637722020886025e-9, 2.53479107902614945675e-7, 2.28621210311945178607e-5, 1.26461541144692592338e-3, 3.59799365153615016266e-2, 3.44289899924628486886e-1, -5.35327393233902768720e-1 };
        private static readonly double[] BesselK0B = { 5.30043377268626276149e-18, -1.64758043015242134646e-17, 5.21039150503902756861e-17, -1.67823109680541210385e-16, 5.51205597852431940784e-16, -1.84859337734377901440e-15, 6.34007647740507060557e-15, -2.22751332699166985548e-14, 8.03289077536357521100e-14, -2.98009692317273043925e-13, 1.14034058820847496303e-12, -4.51459788337394416547e-12, 1.85594911495471785253e-11, -7.95748924447710747776e-11, 3.57739728140030116597e-10, -1.69753450938905987466e-9, 8.57403401741422608519e-9, -4.66048989768794782956e-8, 2.76681363944501510342e-7, -1.83175552271911948767e-6, 1.39498137188764993662e-5, -1.28495495816278026384e-4, 1.56988388573005337491e-3, -3.14481013119645005427e-2, 2.44030308206595545468e0 };

        private static readonly double[] BesselK1A = { -7.02386347938628759343e-18, -2.42744985051936593393e-15, -6.66690169419932900609e-13, -1.41148839263352776110e-10, -2.21338763073472585583e-8, -2.43340614156596823496e-6, -1.73028895751305206302e-4, -6.97572385963986435018e-3, -1.22611180822657148235e-1, -3.53155960776544875667e-1, 1.52530022733894777053e0 };
        private static readonly double[] BesselK1B = { -5.75674448366501715755e-18, 1.79405087314755922667e-17, -5.68946255844285935196e-17, 1.83809354436663880070e-16, -6.05704724837331885336e-16, 2.03870316562433424052e-15, -7.01983709041831346144e-15, 2.47715442448130437068e-14, -8.97670518232499435011e-14, 3.34841966607842919884e-13, -1.28917396095102890680e-12, 5.13963967348173025100e-12, -2.12996783842756842877e-11, 9.21831518760500529508e-11, -4.19035475934189648750e-10, 2.01504975519703286596e-9, -1.03457624656780970260e-8, 5.74108412545004946722e-8, -3.50196060308781257119e-7, 2.40648494783721712015e-6, -1.93619797416608296024e-5, 1.95215518471351631108e-4, -2.85781685962277938680e-3, 1.03923736576817238437e-1, 2.72062619048444266945e0 };

        private static readonly double[] BesselModK1A = { -7.02386347938628759343e-18, -2.42744985051936593393e-15, -6.66690169419932900609e-13, -1.41148839263352776110e-10, -2.21338763073472585583e-8, -2.43340614156596823496e-6, -1.73028895751305206302e-4, -6.97572385963986435018e-3, -1.22611180822657148235e-1, -3.53155960776544875667e-1, 1.52530022733894777053e0 };
        private static readonly double[] BesselModK1B = { -5.75674448366501715755e-18, 1.79405087314755922667e-17, -5.68946255844285935196e-17, 1.83809354436663880070e-16, -6.05704724837331885336e-16, 2.03870316562433424052e-15, -7.01983709041831346144e-15, 2.47715442448130437068e-14, -8.97670518232499435011e-14, 3.34841966607842919884e-13, -1.28917396095102890680e-12, 5.13963967348173025100e-12, -2.12996783842756842877e-11, 9.21831518760500529508e-11, -4.19035475934189648750e-10, 2.01504975519703286596e-9, -1.03457624656780970260e-8, 5.74108412545004946722e-8, -3.50196060308781257119e-7, 2.40648494783721712015e-6, -1.93619797416608296024e-5, 1.95215518471351631108e-4, -2.85781685962277938680e-3, 1.03923736576817238437e-1, 2.72062619048444266945e0 };
        #endregion

        #endregion

        /// <summary>
        /// Evaluates the natural logarithm of the Gamma function at a given point.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Gamma_function">Wikipedia - Gamma function</a> 
        /// and <a href="http://mathworld.wolfram.com/LogGammaFunction.html">Wolfram MathWorld - Log Gamma function</a>.
        /// </summary>
        /// <param name="x">The point where the function is evaluated.</param>
        /// <returns>log(gamma(<paramref name="x"/>)) with <c>gamma(<paramref name="x"/>) = from 0 to &#8734; &#8747;(t^(<paramref name="x"/>-1)exp(-t))dt</c>.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>The value of the Gamma function can be found simply using the exponential of the value found from the function. Example: exp(gammaLog(10.0)) will give the value of the gamma function at 10.0.</item>
        /// <item>In this particular implementation the positive real numbers are supported.</item>
        /// <item>If the given point <paramref name="x"/> is negative the return value will be double.NaN.</item>
        /// <item>If the given point <paramref name="x"/> is zero the return value will be double.PositiveInfinity.</item>
        /// <item>If any of the given points is double.NaN or double.IsInfinity is true, an ArgumentException will be thrown.</item>
        /// <item>If the input array is empty, an empty array will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> gammaLog(InArray<double> x) {
            using (Scope.Enter(x, ArrayStyles.ILNumericsV4)) {
                if (isnull(x)) {
                    throw new ArgumentNullException("The input array x must not be null!");
                }
                if (x.IsEmpty) {
                    return empty<double>(x.S);
                }
                if ((bool)anyall(isnan(x)) || (bool)anyall(isinf(x))) {
                    throw new ArgumentException("The input parameter should not contain NaN or Infinity!");
                }

                Array<double> tmp = x + gammaRational;
                Array<double> xx = x.C;
                tmp = (x + 0.5) * log(tmp) - tmp;
                Array<double> ser = 0.999999999999997092;
                for (int i = 0; i < gammaLogCoefs.Length; i++) {
                    xx = xx + 1.0;
                    ser = ser + gammaLogCoefs[i] / (xx);
                }
                return tmp + log(2.5066282746310005 * ser / x);
            }
        }

        /// <summary>
        /// Evaluates the Gamma function at a given point.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Gamma_function">Wikipedia - Gamma function</a>.
        /// </summary>
        /// <param name="x">The point where the function is evaluated.</param>
        /// <returns><c>gamma(<paramref name="x"/>) = from 0 to &#8734; &#8747;(t^(<paramref name="x"/>-1)exp(-t))dt</c>.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>The value of the Gamma function is simply calculated as the exponential of the value found from the natural logarithm of Gamma function.</item>
        /// <item>In this particular implementation the positive real numbers are supported.</item>
        /// <item>If the given point <paramref name="x"/> is negative the return value will be double.NaN.</item>
        /// <item>If the given point <paramref name="x"/> is zero the return value will be double.PositiveInfinity.</item>
        /// <item>If any of the given points is double.NaN or double.IsInfinity is true, an ArgumentException will be thrown.</item>
        /// <item>If the input array is empty, an empty array will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> gamma(InArray<double> x) {
            return exp(gammaLog(x));
        }

        /// <summary>Factorial, from an array of integer elements.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Factorial">Wikipedia - Factorial</a>.
        /// </summary>
        /// <param name="n">Input array of non-negative integers.</param>
        /// <returns>An array of same size as <paramref name="n"/> with the factorial of each element.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input array is empty, an empty array will be returned.</item>
        /// <item>If an element of the array <paramref name="n"/> is negative, the result will be double.NaN.</item>
        /// <item>If any of the given points is NaN or Infinity an ArgumentException will be thrown.</item>
        /// <item>If the input array is null, an ArgumentNullException will be thrown.</item>
        /// <item>If the input array is empty, an empty array will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> factorial(InArray<int> n) {
            using (Scope.Enter(n, ArrayStyles.ILNumericsV4)) {
                if (isnull(n)) {
                    throw new ArgumentNullException("The input array n must not be null!");
                }
                if (n.IsEmpty) {
                    return empty<double>(n.S);
                }
                Array<double> NewN = todouble(n);
                Array<double> Fct = NewN.C;

                Fct[NewN >= 2] = floor(gamma(NewN[NewN >= 2] + 1));
                Fct[n == 0] = ones<double>((n[n == 0]).S);
                Fct[n == 1] = ones<double>((n[n == 1]).S);
                Fct[n < 0] = array(double.NaN, (n[n < 0]).S);
                return floor(Fct);
            }
        }

        /// <summary>Natural logarithm of factorial, from an array of integer elements.</summary>
        /// <param name="n">Input array of non-negative integers.</param>
        /// <returns>An array of same size as <paramref name="n"/> with the natural logarithm of factorials of each element in <paramref name="n"/>.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input array is empty, an empty array will be returned.</item>
        /// <item>If an element of the array is negative, the result will be double.NaN.</item>
        /// <item>If the factorial of an element of the array is infinity, the result will be infinity.</item>
        /// <item>If the input array is null, an ArgumentNullException will be thrown.</item>
        /// <item>If the input array is empty, an empty array will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> factorialLog(InArray<int> n) {
            using (Scope.Enter(n, ArrayStyles.ILNumericsV4)) {
                if (isnull(n)) {
                    throw new ArgumentNullException("The input array n must not be null!");
                }
                if (n.IsEmpty) {
                    return empty<double>(n.S);
                }
                Array<double> NewN = todouble(n);
                Array<double> Fct = zeros<double>(NewN.S);
                Fct[NewN >= 1] = gammaLog(NewN[NewN >= 1] + 1);
                Fct[n < 0] = array(double.NaN, (n[n < 0]).S);
                return Fct;
            }
        }

        /// <summary>Evaluates the componentwise beta function at an array of points defined by <paramref name="w"/> and <paramref name="z"/>.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Beta_function">Wikipedia - Beta function</a>.
        /// </summary>
        /// <param name="z">Input array.</param>
        /// <param name="w">Input array so that <paramref name="z"/> &gt; <paramref name="w"/> for all component.</param>
        /// <returns> An array of same size as <paramref name="z"/> and with component values of the beta function.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If one of the input arrays is empty, an empty array will be returned.</item>
        /// <item>If for any component, one has <paramref name="z"/>i &lt; <paramref name="w"/>i, the solution will be double.NaN.</item>
        /// <item>If the input array is null, an ArgumentNullException will be thrown.</item>
        /// <item>If z and w size are not equal, an ArgumentException will be thrown.</item>
        /// </list>
        /// </remarks>
        public static Array<double> beta(InArray<double> z, InArray<double> w) {
            using (Scope.Enter(z, w, ArrayStyles.ILNumericsV4)) {
                if (z.IsEmpty)
                    return empty<double>(z.S);

                if (w.IsEmpty)
                    return empty<double>(w.S);

                if (isnull(z)) {
                    throw new ArgumentNullException("The beta z array must not be null!");
                }
                if (isnull(w)) {
                    throw new ArgumentNullException("The beta w array must not be null!");
                }
                if (!z.S.IsSameSize(w.S)) {
                    throw new ArgumentException("Input arrays z and w must have the same size!");
                }
                Array<double> comb = ones<double>(z.S);
                comb[z <= 0] = array(double.NaN, z[z <= 0].S);
                comb[w <= 0] = array(double.NaN, z[w <= 0].S);
                comb = exp(gammaLog(z) + gammaLog(w) - gammaLog(z + w));
                return comb;
            }
        }

        /// <summary>
        /// Binomial coefficients of elements in <paramref name="n"/> and <paramref name="k"/>.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Binomial_coefficient">Wikipedia - Binomial coefficient</a>.
        /// </summary>
        /// <param name="n">Input array <paramref name="n"/>.</param>
        /// <param name="k">Input array <paramref name="k"/>.</param>
        /// <returns>An array of same size as <paramref name="n"/> and <paramref name="k"/> with values of the binomial coefficients.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If one of the input arrays is empty, an empty array will be returned.</item>
        /// <item>If for any component, one has <paramref name="n"/>i &lt; <paramref name="k"/>i, the solution will be double.NaN.</item>
        /// <item>If any elements in <paramref name="n"/> and <paramref name="k"/> has the same value, the solution will be +1.0.</item>
        /// <item>If any elements in <paramref name="n"/> or <paramref name="k"/> has a negative value, the solution will be double.NaN.</item>
        /// <item>If any of the input arrays is null, an ArgumentNullException will be thrown.</item>
        /// <item>If n and k size are not equal, an ArgumentException will be thrown.</item>
        /// </list>
        /// </remarks>
        public static Array<double> binomialCoefficients(InArray<int> n, InArray<int> k) {
            using (Scope.Enter(n, k, ArrayStyles.ILNumericsV4)) {
                if (n.IsEmpty)
                    return empty<double>(n.S);

                if (k.IsEmpty)
                    return empty<double>(k.S);

                if (isnull(n)) {
                    throw new ArgumentNullException("The binomial argument n must not be null!");
                }
                if (isnull(k)) {
                    throw new ArgumentNullException("The binomial argument k must not be null!");
                }
                if (!n.S.IsSameSize(k.S)) {
                    throw new ArgumentException("Input arrays n and p must have the same size!");
                }

                return floor(0.5 + exp(factorialLog(n) - factorialLog(k) - factorialLog(n - k))); ;
            }
        }

        /// <summary>
        /// Natural logarithm of binomial coefficients of elements in <paramref name="n"/> and <paramref name="k"/>.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Binomial_coefficient">Wikipedia - Binomial coefficient</a>.
        /// </summary>
        /// <param name="n">Input array <paramref name="n"/>.</param>
        /// <param name="k">Input array <paramref name="k"/>.</param>
        /// <returns>An array of same size as <paramref name="n"/> and <paramref name="k"/> with values of the binomial coefficients.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If one of the input arrays is empty, an empty array will be returned.</item>
        /// <item>If for any component, one has <paramref name="n"/>i &lt; <paramref name="k"/>i, the solution will be double.NaN.</item>
        /// <item>If any elements in <paramref name="n"/> and <paramref name="k"/> has the same value, the solution will be +1.0.</item>
        /// <item>If any elements in <paramref name="n"/> or <paramref name="k"/> has a negative value, the solution will be double.NaN.</item>
        /// <item>If any of the input arrays is null, an ArgumentNullException will be thrown.</item>
        /// <item>If n and k size are not equal, an ArgumentException will be thrown.</item>
        /// </list>
        /// </remarks>
        public static Array<double> binomialCoefficientsLog(InArray<int> n, InArray<int> k) {
            using (Scope.Enter(n, k, ArrayStyles.ILNumericsV4)) {
                if (n.IsEmpty)
                    return empty<double>(n.S);
                if (k.IsEmpty)
                    return empty<double>(k.S);
                if (isnull(n)) {
                    throw new ArgumentNullException("The binomial array n must not be null!");
                }
                if (isnull(k)) {
                    throw new ArgumentNullException("The binomial array k must not be null!");
                }
                if (!n.S.IsSameSize(k.S)) {
                    throw new ArgumentException("Input arrays n and k must have the same size!");
                }
                return factorialLog(n) - factorialLog(k) - factorialLog(n - k);
            }
        }

        /// <summary>
        /// Evaluates the lower incomplete Gamma function at a given point.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Incomplete_gamma_function">Wikipedia - Incomplete gamma function</a>.
        /// </summary>
        /// <param name="x">The point where the function is evaluated.</param>
        /// <param name="a">The parameter of Gamma.</param>
        /// <returns>The value of the lower incomplete Gamma function.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If any of the input arrays is null, an ArgumentNullException will be thrown.</item>
        /// <item>If x is empty, an empty array will be returned.</item>
        /// <item>If any of the given points is NaN or Infinity an ArgumentException will be thrown.</item>
        /// <item>If any elements in <paramref name="x"/> is negative, an ArgumentException will be thrown.</item>
        /// </list>
        /// </remarks>
        public static Array<double> gammaIncomplete(InArray<double> a, InArray<double> x) {
            using (Scope.Enter(a, x, ArrayStyles.ILNumericsV4)) {
                if (isnull(x)) {
                    throw new ArgumentNullException("The argument x must not be null!");
                }
                if (isnull(a)) {
                    throw new ArgumentNullException("The argument a must not be null!");
                }
                if (x.IsEmpty) {
                    return empty<double>(x.S);
                }
                if ((bool)anyall(isnan(x)) || (bool)anyall(isinf(x))) {
                    throw new ArgumentException("The input array should not contain NaN or infinity!");
                }
                if (anyall(x < 0)) {
                    throw new ArgumentException("The input array has to be non-negative!");
                }
                if (a <= 0) {
                    throw new ArgumentException("The input parameter a must be a positive number!");
                }
                Array<double> val = zeros<double>(x.S);

                if (anyall(x > 0)) {
                    val[floor(a) > SpecialFunctionsHelper.SwichtNumber] =
                        SpecialFunctionsHelper.gammaPApprox(a[floor(a) > SpecialFunctionsHelper.SwichtNumber], x[floor(a) > SpecialFunctionsHelper.SwichtNumber], 1);
                    val[x[x > 0] < a + 1.0] = SpecialFunctionsHelper.gSerie(a, x[and(x > 0, x < a + 1.0)]);
                    val[x[x > 0] >= a + 1.0] = 1.0 - SpecialFunctionsHelper.gcf(a, x[and(x > 0, x >= a + 1.0)]);
                }
                return val;
            }
        }

        /// <summary>
        /// Evaluates the incomplete Beta function at a given point.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Beta_function#Incomplete_beta_function">Wikipedia - Incomplete beta function</a>.
        /// </summary>
        /// <param name="x">The point where the function is evaluated. <paramref name="x"/>  must be in [0,1].</param>
        /// <param name="a">A scalar, first parameter of the incomplete Beta function.</param>
        /// <param name="b">A scalar, second parameter of the incomplete Beta function.</param>
        /// <returns>The value of the incomplete Beta function at a given point.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If any of the input arrays is null, an ArgumentNullException will be thrown.</item>
        /// <item>If x is empty, an empty array will be returned.</item>
        /// <item>If any element of x is <c>xi &gt; 1</c> and <c>xi &lt; 1</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of x is <c>xi == 0</c>, 0.0 will be returned at that element.</item>
        /// <item>If any element of x is <c>xi == 1</c>, 1.0 will be returned at that element.</item>
        /// <item>If a or b is negative, an ArgumentException will be thrown.</item>
        /// </list>
        /// </remarks>
        public static Array<double> betaIncomplete(double a, double b, InArray<double> x)  // TODO: change to make it upper case? (refactor!)
        {
            using (Scope.Enter(a, b, x, ArrayStyles.ILNumericsV4)) {
                if (isnull(a)) {
                    throw new ArgumentNullException("Input parameter a must not be null!");
                }
                if (isnull(b)) {
                    throw new ArgumentNullException("Input parameter b must not be null!");
                }
                if (isnull(x)) {
                    throw new ArgumentNullException("Input array x must not be null!");
                }

                if (x.IsEmpty)
                    return empty<double>(x.S);

                if ((a < 0) || (b < 0)) {
                    throw new ArgumentException("Bad parameter values. a and b have to b 0 <= x <=1 for each elements.");
                }

                Array<double> bt = zeros<double>(x.S);
                Array<double> Sol = zeros<double>(x.S);
                Array<long> I = find(or(x < 0.0, x > 1.0));
                Sol[I] = array(double.NaN, x[I].S);
                I = find(or(x == 0, x == 1.0));
                Sol[I] = x[I];
                if ((a > SpecialFunctionsHelper.SwichtNumber) && (b > SpecialFunctionsHelper.SwichtNumber)) {
                    I = find(and(and(x != 0, x != 1.0), and(x > 0.0, x < 1.0)));
                    Sol[I] = SpecialFunctionsHelper.BetaApprox(a, b, x[I]);
                } else {
                    I = find(and((x < (a + 1.0) / (a + b + 2.0)), and(and(x != 0, x != 1.0), and(x > 0.0, x < 1.0))));
                    if (!I.IsEmpty) {
                        bt[I] = exp(gammaLog(a + b) - gammaLog(a) - gammaLog(b) + a * log(x[I]) + b * log(1.0 - x[I]));
                        Sol[I] = bt[I] * SpecialFunctionsHelper.BetaComplentf(a, b, x[I]) / a;
                    }
                    I = find(and((x >= (a + 1.0) / (a + b + 2.0)), and(and(x != 0, x != 1.0), and(x > 0.0, x < 1.0))));
                    if (!I.IsEmpty) {
                        bt[I] = exp(gammaLog(a + b) - gammaLog(a) - gammaLog(b) + a * log(x[I]) + b * log(1.0 - x[I]));
                        // as in Numerical Recipes
                        Sol[I] = 1.0 - bt[I] * SpecialFunctionsHelper.BetaComplentf(b, a, 1.0 - x[I]) / b;

                        //Sol[I] = 1.0 - bt[I] * SpecialFunctionsHelper.BetaComplentf(a, b, 1.0 - x[I]) / a;

                    }
                }
                return Sol;
            }
        }

        /// <summary>
        /// Evaluates the (Gauss) error function.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Error_function">Wikipedia - Error function</a>.
        /// </summary>
        /// <param name="x">The point where the function is evaluated.</param>
        /// <returns>The value of the error function at the given point <paramref name="x"/>.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If <paramref name="x"/> is null, an ArgumentNullException will be thrown.</item>
        /// <item>If <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of x is <c>xi == double.PositiveInfinity</c>, +1.0 will be returned at that element.</item>
        /// <item>If any element of x is <c>xi == double.NegativeInfinity</c>, -1.0 will be returned at that element.</item>
        /// <item>If any element of x is <c>xi == double.NaN</c>, double.NaN will be returned at that element.</item>
        /// <item>If </item>
        /// </list>
        /// </remarks>
        public static Array<double> errorFunction(InArray<double> x) {
            using (Scope.Enter(x, ArrayStyles.ILNumericsV4)) {
                if (isnull(x)) {
                    throw new ArgumentNullException("The input array x must not be null!");
                }

                if (x.IsEmpty)
                    return empty<double>(x.S);

                Array<double> Sol = ones<double>(x.S);
                int index = 0;
                foreach (double xi in x) {
                    if (xi == 0.0) {
                        Sol[index] = 0.0;
                    } else if (Double.IsPositiveInfinity(xi)) {
                        Sol[index] = 1.0;
                    } else if (Double.IsNegativeInfinity(xi)) {
                        Sol[index] = -1.0;
                    } else if (isnan(xi)) {
                        Sol[index] = double.NaN;
                    } else {
                        Sol[index] = SpecialFunctionsHelper.ErfImp(xi, false);
                    }
                    index++;
                }
                return Sol;
            }
        }

        /// <summary>
        /// Evaluates of the complementary (Gauss) error function.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Error_function">Wikipedia - Complementary Error function</a>.
        /// </summary>
        /// <param name="x">The point where the function is evaluated.</param>
        /// <returns>The value of the complementary error function at the given point <paramref name="x"/>.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If <paramref name="x"/> is null, an ArgumentNullException will be thrown.</item>
        /// <item>If <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of x is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned at that element.</item>
        /// <item>If any element of x is <c>xi == double.NegativeInfinity</c>, +2.0 will be returned at that element.</item>
        /// </list>
        /// </remarks>
        public static Array<double> errorFunctionComplement(InArray<double> x) {
            using (Scope.Enter(x, ArrayStyles.ILNumericsV4)) {
                if (isnull(x)) {
                    throw new ArgumentNullException("The input array x must not be null!");
                }

                if (x.IsEmpty)
                    return empty<double>(x.S);

                Array<double> Sol = ones<double>(x.S);
                int index = 0;
                foreach (double xi in x) {
                    if (Double.IsPositiveInfinity(xi)) {
                        Sol[index] = 0.0;
                    } else if (Double.IsNegativeInfinity(xi)) {
                        Sol[index] = 2.0;
                    } else if (isnan(xi)) {
                        Sol[index] = double.NaN;
                    } else {
                        Sol[index] = SpecialFunctionsHelper.ErfImp(xi, true);
                    }
                    index++;
                }

                return Sol;
            }
        }

        /// <summary>
        /// Evaluates the inverse of the (Gauss) error function.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Error_function#Inverse_functions">Wikipedia - Inverse Error function</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the inverse error function at the given point <paramref name="x"/>.</returns>>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If <paramref name="x"/> is null, an ArgumentNullException will be thrown.</item>
        /// <item>If <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of x is <c>xi &gt;= +1.0</c>, double.PositiveInfinity will be returned at that element.</item>
        /// <item>If any element of x is <c>xi &lt;= -1.0</c>, double.NegativeInfinity will be returned at that element.</item>
        /// </list>
        /// </remarks>
        public static Array<double> errorFunctionInverse(InArray<double> x) {
            using (Scope.Enter(x, ArrayStyles.ILNumericsV4)) {
                if (isnull(x)) {
                    throw new ArgumentNullException("The input argument must not be null");
                }
                if (x.IsEmpty)
                    return empty<double>(x.S);
                Array<double> Sol = ones<double>(x.S);
                int index = 0;
                foreach (double xi in x) {
                    if (xi == 0.0) {
                        Sol[index] = 0.0;
                    } else if (xi >= 1.0) {
                        Sol[index] = double.PositiveInfinity;
                    } else if (Double.IsNegativeInfinity(xi)) {
                        Sol[index] = 2.0;
                    } else if (xi <= -1.0) {
                        Sol[index] = double.NegativeInfinity;
                    } else if (isnan(xi)) {
                        Sol[index] = double.NaN;
                    } else {
                        double p, q, s;
                        if (xi < 0) {
                            p = -xi;
                            q = 1 - p;
                            s = -1;
                        } else {
                            p = xi;
                            q = 1 - xi;
                            s = 1;
                        }

                        Sol[index] = SpecialFunctionsHelper.ErfInvImpl(p, q, s);
                    }
                    index++;
                }

                return Sol;
            }
        }

        /// <summary>
        /// Evaluates the sigmoid logistic function at a given point.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Logistic_function">Wikipedia - Logistic function</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If <paramref name="x"/> is null, an ArgumentNullException will be thrown.</item>
        /// <item>If <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, +1.0 will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.NegativeInfinity</c>, 0.0 will be returned at that element.</item>
        /// </list>
        /// </remarks>
        /// <returns>The value of the logistic function at the given point <paramref name="x"/>.</returns>
        public static Array<double> logistic(InArray<double> x) {
            using (Scope.Enter(x, ArrayStyles.ILNumericsV4)) {
                if (isnull(x))
                    throw new ArgumentNullException("The input array x must not be null!");

                if (x.IsEmpty)
                    return empty<double>(x.S);

                return 1.0 / (exp(-x) + 1.0);
            }
        }

        /// <summary>
        /// Evaluates the inverse of the sigmoid logistic function.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Logit_function">Wikipedia - Logit function</a>.
        /// </summary>
        /// <param name="x">point defined between 0 and 1.</param>
        /// <returns>The logarithm of <paramref name="x"/> divided by 1.0 - <paramref name="x"/>.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If <paramref name="x"/> is null, an ArgumentNullException will be thrown.</item>
        /// <item>If <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &#8805; 1.0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &#8804; 0.0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 1.0</c>, double.PositiveInfinity will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, double.NegativeInfinity will be returned at that element.</item>
        /// </list>
        /// </remarks>
        public static Array<double> logit(InArray<double> x) {
            using (Scope.Enter(x, ArrayStyles.ILNumericsV4)) {
                if (isnull(x))
                    throw new ArgumentNullException("The input array x must not be null!");

                if (x.IsEmpty)
                    return empty<double>(x.S);

                Array<double> Sol = zeros<double>(x.S);
                Sol[and(x >= 0.0, x <= 1.0)] = log(x[and(x >= 0.0, x <= 1.0)] / (1.0 - x[and(x >= 0.0, x <= 1.0)]));
                Sol[or(x < 0.0, x > 1.0)] = array<double>(double.NaN, x[or(x < 0.0, x > 1.0)].S);
                return Sol;
            }
        }

        /// <summary>
        /// Internal helper delegate to compute an arbitrary Bessel function at the specified order and kind.
        /// </summary>
        /// <param name="x">The given point.</param>
        /// <param name="n">The given order.</param>
        /// <returns>The evaluated function.</returns>
        public delegate double BesselElement(double x, int n);

        /// <summary>
        /// Common method to evaluate the Bessel-functions.
        /// This helps to easily maintain the code, and only change at this point.
        /// Also this method ensures to test the input parameters once, and they are automatically applied to other
        /// Bessel-functions as well.
        /// </summary>
        /// <param name="x">At the points in x.</param>
        /// <param name="function">The arbitrary Bessel-function.</param>
        /// <param name="n">Given order n.</param>
        /// <returns>Evaluated values of the function.</returns>
        public static Array<double> besselGenerator_INTERNAL(InArray<double> x, BesselElement function, int n)
        {
            using (Scope.Enter(x, ArrayStyles.ILNumericsV4))
            {
                if (isnull(x))
                    throw new ArgumentNullException("The input array x must not be null!");

                if (x.IsEmpty)
                    return empty<double>(x.S);

                if (n < 0)
                    throw new ArgumentException("The integer order must be greater or equal to 0!");

                Array<double> sol = zeros<double>(x.S);
                int index = 0;

                foreach (double elem in x)
                {
                    if (elem < 0)
                    {
                        sol[index++] = double.NaN;
                    }
                    else
                    {
                         sol[index++] = function(elem, n);
                    }
                }

                return sol;
            }
        }

        /// <summary>
        /// Evaluates the Bessel function of the first kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_first_kind:_J.CE.B1">Wikipedia - Bessel functions of the first kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, 1.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselJ0(InArray<double> x)
        {
            return besselJn(x, 0);
        }

        /// <summary>
        /// Evaluates the Bessel function of the first kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_first_kind:_J.CE.B1">Wikipedia - Bessel functions of the first kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselJ1(InArray<double> x)
        {
            return besselJn(x, 1);
        }

        /// <summary>
        /// Evaluates the Bessel function of the first kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_first_kind">Wikipedia - Bessel functions of the first kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselJn(InArray<double> x, int n)
        {
            return besselGenerator_INTERNAL(x, SpecialFunctionsHelper.besselJnElem, n);
        }

        /// <summary>
        /// Evaluates the Bessel function of the second kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, double.NegativeInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselY0(InArray<double> x)
        {
            return besselYn(x, 0);
        }

        /// <summary>
        /// Evaluates the Bessel function of the second kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, double.NegativeInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselY1(InArray<double> x)
        {
            return besselYn(x, 1);
        }

        /// <summary>
        /// Evaluates the Bessel function of the second kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, double.NegativeInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselYn(InArray<double> x, int n)
        {
            return besselGenerator_INTERNAL(x, SpecialFunctionsHelper.besselYnElem, n);
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the second kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, double.PositiveInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselModifiedK0(InArray<double> x)
        {
            return besselModifiedKn(x, 0);
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the second kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, double.PositiveInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselModifiedK1(InArray<double> x)
        {
            return besselModifiedKn(x, 1);
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the second kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, double.PositiveInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselModifiedKn(InArray<double> x, int n)
        {
            return besselGenerator_INTERNAL(x, SpecialFunctionsHelper.besselModKnElem, n);
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the first kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Modified_Bessel_functions">Wikipedia - Modified Bessel functions</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, double.PositiveInfinity will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselModifiedI0(InArray<double> x)
        {
            return besselModifiedIn(x, 0);
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the first kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Modified_Bessel_functions">Wikipedia - Modified Bessel functions</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, double.PositiveInfinity will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselModifiedI1(InArray<double> x)
        {
            return besselModifiedIn(x, 1);
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the first kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Modified_Bessel_functions">Wikipedia - Modified Bessel functions</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, double.PositiveInfinity will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<double> besselModifiedIn(InArray<double> x, int n)
        {
            return besselGenerator_INTERNAL(x, SpecialFunctionsHelper.besselModInElem, n);
        }

        /// <summary>
        /// Computes the Digamma function which is mathematically defined as the derivative of the logarithm of the gamma function.
        /// For more details about this function, see <a href="http://en.wikipedia.org/wiki/Digamma_function">Wikipedia - Digamma function</a>
        /// and <a href="http://mathworld.wolfram.com/DigammaFunction.html">Wolfram MathWorld - Digamma function</a>.
        /// </summary>
        /// <param name="xi">The point where the function is evaluated.</param>
        /// <returns>The value of the DiGamma function at <paramref name="xi"/>.</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="xi"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="xi"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of x is <c>xi == double.NaN</c> or <c>xi == double.NegativeInfinity</c>, there will be a double.NaN returned at that element.</item>
        /// <item>If any element of x is <c>xi == double.PositiveInfinity</c>, there will be a double.PositiveInfinity returned at that element.</item>
        /// <item>If any element of x is <c>xi &lt; 0</c> and is a round number, there will be a double.NegativeInfinity returned at that element.</item>
        /// </list>
        /// </remarks>
        public static Array<double> diGamma(InArray<double> xi)
        {
            using (Scope.Enter(xi, ArrayStyles.ILNumericsV4))
            {
                if (isnull(xi))
                    throw new ArgumentNullException("The input array xi must not be null!");

                if (xi.IsEmpty)
                    return empty<double>(xi.S);

                const double c = 12.0;
                const double d1 = -0.57721566490153286;
                const double d2 = 1.6449340668482264365;
                const double s = 1e-6;
                const double s3 = 1.0 / 12.0;
                const double s4 = 1.0 / 120.0;
                const double s5 = 1.0 / 252.0;
                const double s6 = 1.0 / 240.0;
                const double s7 = 1.0 / 132.0;

                Array<double> result = zeros<double>(xi.S);
                int count = -1;

                foreach (double elem in xi)
                {
                    double x = elem;
                    count++;
                    if (Double.IsNegativeInfinity(x) || Double.IsNaN(x))
                    {
                        result[count] = double.NaN;
                        continue;
                    }

                    if (x <= 0 && Math.Floor(x) == x)
                    {
                        // Handle special cases.
                        result[count] = double.NegativeInfinity;
                        continue;
                    }

                    if (x < 0)
                    {
                        // Use inversion formula for negative numbers.
                        result[count] = diGamma(1.0 - x) + (Math.PI / Math.Tan(-Math.PI * x));
                        continue;
                    }

                    if (x <= s)
                    {
                        result[count] = d1 - (1 / x) + (d2 * x);
                        continue;
                    }

                    // b.kurtan: it is a possible performance bottleneck
                    while (x < c)
                    {
                        result[count] -= 1 / x;
                        x = x + 1.0;
                    }

                    if (x >= c)
                    {
                        var r = 1 / x;
                        result[count] = result[count] + (double)log(x) - (0.5 * r);
                        r *= r;

                        result[count] -= r * (s3 - (r * (s4 - (r * (s5 - (r * (s6 - (r * s7))))))));
                        continue;
                    }
                }
                return result;
            }
        }

        #region float overloads - converts to / from double

        /// <summary>
        /// Evaluates the Bessel function of the first kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_first_kind:_J.CE.B1">Wikipedia - Bessel functions of the first kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, 1.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselJ0(InArray<float> x) {
            return tosingle(besselJ0(todouble(x)));
        }

        /// <summary>
        /// Evaluates the Bessel function of the first kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_first_kind:_J.CE.B1">Wikipedia - Bessel functions of the first kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselJ1(InArray<float> x) {
            return tosingle(besselJ1(todouble(x)));
        }

        /// <summary>
        /// Evaluates the Bessel function of the first kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_first_kind">Wikipedia - Bessel functions of the first kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselJn(InArray<float> x, int n) {
            return tosingle(besselJn(todouble(x), n));
        }

        /// <summary>
        /// Evaluates the Bessel function of the second kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, double.NegativeInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselY0(InArray<float> x) {
            return tosingle(besselY0(todouble(x)));
        }

        /// <summary>
        /// Evaluates the Bessel function of the second kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, double.NegativeInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselY1(InArray<float> x) {
            return tosingle(besselY1(todouble(x)));
        }

        /// <summary>
        /// Evaluates the Bessel function of the second kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0.0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == 0.0</c>, double.NegativeInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselYn(InArray<float> x, int n) {
            return tosingle(besselYn(todouble(x), n));
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the first kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Modified_Bessel_functions">Wikipedia - Modified Bessel functions</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, double.PositiveInfinity will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselModifiedI0(InArray<float> x) {
            return tosingle(besselModifiedI0(todouble(x)));
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the first kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Modified_Bessel_functions">Wikipedia - Modified Bessel functions</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, double.PositiveInfinity will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselModifiedI1(InArray<float> x) {
            return tosingle(besselModifiedI1(todouble(x)));
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the first kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Modified_Bessel_functions">Wikipedia - Modified Bessel functions</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, double.PositiveInfinity will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, 0.0 will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselModifiedIn(InArray<float> x, int n) {
            return tosingle(besselModifiedIn(todouble(x), n));
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the second kind, integer order 0 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, double.PositiveInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselModifiedK0(InArray<float> x) {
            return tosingle(besselModifiedK0(todouble(x)));
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the second kind, integer order 1 of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, double.PositiveInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselModifiedK1(InArray<float> x) {
            return tosingle(besselModifiedK1(todouble(x)));
        }

        /// <summary>
        /// Evaluates the modified Bessel function of the second kind, integer order n-th of the argument.
        /// For more details about this function, see <a href="http://en.wikipedia.org/w/index.php?title=Bessel_function#Bessel_functions_of_the_second_kind">Wikipedia - Bessel functions of the second kind</a>.
        /// </summary>
        /// <param name="x">The point where the function will be evaluated.</param>
        /// <param name="n">Integer order of the argument.</param>
        /// <returns>The value of the function.
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>If the input <paramref name="x"/> is null, ArgumentNullException will be thrown.</item>
        /// <item>If the input <paramref name="x"/> is empty, an empty array will be returned.</item>
        /// <item>If the order <paramref name="n"/> is negative, an ArgumentException will be thrown.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi &lt; 0</c>, double.NaN will be returned at that element.</item>
        /// <item>If any element of <paramref name="x"/> is <c>xi == double.PositiveInfinity</c>, 0 will be returned.</item>
        /// <item>If any element of <paramref name="x"/> is <c>== 0.0</c>, double.PositiveInfinity will be returned.</item>
        /// </list>
        /// </remarks>
        public static Array<float> besselModifiedKn(InArray<float> x, int n) {
            return tosingle(besselModifiedKn(todouble(x), n));
        }
        #endregion
    }
}