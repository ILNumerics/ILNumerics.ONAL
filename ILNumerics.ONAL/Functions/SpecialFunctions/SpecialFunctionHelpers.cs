using System;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
using ILNumerics.Core.Functions.Builtin;

// TODO: check for performance optimization potential (local arrays for those constants, they are all unique among the functions!)
namespace ILNumerics
{
    /// <summary>
    /// Useful functions for the SpecialFunctions class. It contains all internal methods.
    /// </summary>
    internal static class SpecialFunctionsHelper
    {
        public const double SwichtNumber = 500;

        #region Helper constants

        #region gammaPApprox
        private static readonly double[] gammaPApprox_y = new double[18]{0.0021695375159141994,
                    0.011413521097787704,0.027972308950302116,0.051727015600492421,
                0.082502225484340941, 0.12007019910960293, 0.16415283300752470,
                0.21442376986779355, 0.27051082840644336, 0.33199876341447887,
                0.39843234186401943, 0.46931971407375483, 0.54413605556657973,
                0.62232745288031077, 0.70331500465597174, 0.78649910768313447,
                0.87126389619061517, 0.95698180152629142};

        private static readonly double[] gammaPApprox_w = new double[18]{0.0055657196642445571,
                0.012915947284065419,0.020181515297735382,0.027298621498568734,
                0.034213810770299537,0.040875750923643261,0.047235083490265582,
                0.053244713977759692,0.058860144245324798,0.064039797355015485,
                0.068745323835736408,0.072941885005653087,0.0776598410645870640,
                0.0796878289112071670,0.082187266704339706,0.084078218979661945,
                0.085346685739338721,0.085983275670394821};
        #endregion

        #region betaApprox
        private static readonly double[] betaApprox_y = {0.0021695375159141994,
                    0.011413521097787704,0.027972308950302116,0.051727015600492421,
                0.082502225484340941, 0.12007019910960293, 0.16415283300752470,
                0.21442376986779355, 0.27051082840644336, 0.33199876341447887,
                0.39843234186401943, 0.46931971407375483, 0.54413605556657973,
                0.62232745288031077, 0.70331500465597174, 0.78649910768313447,
                0.87126389619061517, 0.95698180152629142};

        private static readonly double[] betaApprox_w = {0.0055657196642445571,
                0.012915947284065419,0.020181515297735382,0.027298621498568734,
                0.034213810770299537,0.040875750923643261,0.047235083490265582,
                0.053244713977759692,0.058860144245324798,0.064039797355015485,
                0.068745323835736408,0.072941885005653087,0.0776598410645870640,
                0.0796878289112071670,0.082187266704339706,0.084078218979661945,
                0.085346685739338721,0.085983275670394821};
        #endregion

        #region erfc

        private static readonly double[] erfc_nc_1 = { 0.00337916709551257388990745, -0.00073695653048167948530905, -0.374732337392919607868241, 0.0817442448733587196071743, -0.0421089319936548595203468, 0.0070165709512095756344528, -0.00495091255982435110337458, 0.000871646599037922480317225 };
        private static readonly double[] erfc_dc_1 = { 1, -0.218088218087924645390535, 0.412542972725442099083918, -0.0841891147873106755410271, 0.0655338856400241519690695, -0.0120019604454941768171266, 0.00408165558926174048329689, -0.000615900721557769691924509 };

        private static readonly double[] erfc_nc_2 = { -0.0361790390718262471360258, 0.292251883444882683221149, 0.281447041797604512774415, 0.125610208862766947294894, 0.0274135028268930549240776, 0.00250839672168065762786937 };
        private static readonly double[] erfc_dc_2 = { 1, 1.8545005897903486499845, 1.43575803037831418074962, 0.582827658753036572454135, 0.124810476932949746447682, 0.0113724176546353285778481 };

        private static readonly double[] erfc_nc_3 = { -0.0397876892611136856954425, 0.153165212467878293257683, 0.191260295600936245503129, 0.10276327061989304213645, 0.029637090615738836726027, 0.0046093486780275489468812, 0.000307607820348680180548455 };
        private static readonly double[] erfc_dc_3 = { 1, 1.95520072987627704987886, 1.64762317199384860109595, 0.768238607022126250082483, 0.209793185936509782784315, 0.0319569316899913392596356, 0.00213363160895785378615014 };

        private static readonly double[] erfc_nc_4 = { -0.0300838560557949717328341, 0.0538578829844454508530552, 0.0726211541651914182692959, 0.0367628469888049348429018, 0.00964629015572527529605267, 0.00133453480075291076745275, 0.778087599782504251917881e-4 };
        private static readonly double[] erfc_dc_4 = { 1, 1.75967098147167528287343, 1.32883571437961120556307, 0.552528596508757581287907, 0.133793056941332861912279, 0.0179509645176280768640766, 0.00104712440019937356634038, -0.106640381820357337177643e-7 };

        private static readonly double[] erfc_nc_5 = { -0.0117907570137227847827732, 0.014262132090538809896674, 0.0202234435902960820020765, 0.00930668299990432009042239, 0.00213357802422065994322516, 0.00025022987386460102395382, 0.120534912219588189822126e-4 };
        private static readonly double[] erfc_dc_5 = { 1, 1.50376225203620482047419, 0.965397786204462896346934, 0.339265230476796681555511, 0.0689740649541569716897427, 0.00771060262491768307365526, 0.000371421101531069302990367 };

        private static readonly double[] erfc_nc_6 = { -0.00546954795538729307482955, 0.00404190278731707110245394, 0.0054963369553161170521356, 0.00212616472603945399437862, 0.000394984014495083900689956, 0.365565477064442377259271e-4, 0.135485897109932323253786e-5 };
        private static readonly double[] erfc_dc_6 = { 1, 1.21019697773630784832251, 0.620914668221143886601045, 0.173038430661142762569515, 0.0276550813773432047594539, 0.00240625974424309709745382, 0.891811817251336577241006e-4, -0.465528836283382684461025e-11 };

        private static readonly double[] erfc_nc_7 = { -0.00270722535905778347999196, 0.0013187563425029400461378, 0.00119925933261002333923989, 0.00027849619811344664248235, 0.267822988218331849989363e-4, 0.923043672315028197865066e-6 };
        private static readonly double[] erfc_dc_7 = { 1, 0.814632808543141591118279, 0.268901665856299542168425, 0.0449877216103041118694989, 0.00381759663320248459168994, 0.000131571897888596914350697, 0.404815359675764138445257e-11 };

        private static readonly double[] erfc_nc_8 = { -0.00109946720691742196814323, 0.000406425442750422675169153, 0.000274499489416900707787024, 0.465293770646659383436343e-4, 0.320955425395767463401993e-5, 0.778286018145020892261936e-7 };
        private static readonly double[] erfc_dc_8 = { 1, 0.588173710611846046373373, 0.139363331289409746077541, 0.0166329340417083678763028, 0.00100023921310234908642639, 0.24254837521587225125068e-4 };

        private static readonly double[] erfc_nc_9 = { -0.00056907993601094962855594, 0.000169498540373762264416984, 0.518472354581100890120501e-4, 0.382819312231928859704678e-5, 0.824989931281894431781794e-7 };
        private static readonly double[] erfc_dc_9 = { 1, 0.339637250051139347430323, 0.043472647870310663055044, 0.00248549335224637114641629, 0.535633305337152900549536e-4, -0.117490944405459578783846e-12 };

        private static readonly double[] erfc_nc_10 = { -0.000241313599483991337479091, 0.574224975202501512365975e-4, 0.115998962927383778460557e-4, 0.581762134402593739370875e-6, 0.853971555085673614607418e-8 };
        private static readonly double[] erfc_dc_10 = { 1, 0.233044138299687841018015, 0.0204186940546440312625597, 0.000797185647564398289151125, 0.117019281670172327758019e-4 };

        private static readonly double[] erfc_nc_11 = { -0.000146674699277760365803642, 0.162666552112280519955647e-4, 0.269116248509165239294897e-5, 0.979584479468091935086972e-7, 0.101994647625723465722285e-8 };
        private static readonly double[] erfc_dc_11 = { 1, 0.165907812944847226546036, 0.0103361716191505884359634, 0.000286593026373868366935721, 0.298401570840900340874568e-5 };

        private static readonly double[] erfc_nc_12 = { -0.583905797629771786720406e-4, 0.412510325105496173512992e-5, 0.431790922420250949096906e-6, 0.993365155590013193345569e-8, 0.653480510020104699270084e-10 };
        private static readonly double[] erfc_dc_12 = { 1, 0.105077086072039915406159, 0.00414278428675475620830226, 0.726338754644523769144108e-4, 0.477818471047398785369849e-6 };

        private static readonly double[] erfc_nc_13 = { -0.196457797609229579459841e-4, 0.157243887666800692441195e-5, 0.543902511192700878690335e-7, 0.317472492369117710852685e-9 };
        private static readonly double[] erfc_dc_13 = { 1, 0.052803989240957632204885, 0.000926876069151753290378112, 0.541011723226630257077328e-5, 0.535093845803642394908747e-15 };

        private static readonly double[] erfc_nc_14 = { -0.789224703978722689089794e-5, 0.622088451660986955124162e-6, 0.145728445676882396797184e-7, 0.603715505542715364529243e-10 };
        private static readonly double[] erfc_dc_14 = { 1, 0.0375328846356293715248719, 0.000467919535974625308126054, 0.193847039275845656900547e-5 };

        #endregion

        #region erfi
        private static readonly double[] erfi_nc_1 = { -0.000508781949658280665617, -0.00836874819741736770379, 0.0334806625409744615033, -0.0126926147662974029034, -0.0365637971411762664006, 0.0219878681111168899165, 0.00822687874676915743155, -0.00538772965071242932965 };
        private static readonly double[] erfi_dc_1 = { 1, -0.970005043303290640362, -1.56574558234175846809, 1.56221558398423026363, 0.662328840472002992063, -0.71228902341542847553, -0.0527396382340099713954, 0.0795283687341571680018, -0.00233393759374190016776, 0.000886216390456424707504 };

        private static readonly double[] erfi_nc_2 = { -0.202433508355938759655, 0.105264680699391713268, 8.37050328343119927838, 17.6447298408374015486, -18.8510648058714251895, -44.6382324441786960818, 17.445385985570866523, 21.1294655448340526258, -3.67192254707729348546 };
        private static readonly double[] erfi_dc_2 = { 1, 6.24264124854247537712, 3.9713437953343869095, -28.6608180499800029974, -20.1432634680485188801, 48.5609213108739935468, 10.8268667355460159008, -22.6436933413139721736, 1.72114765761200282724 };

        private static readonly double[] erfi_nc_3 = { -0.131102781679951906451, -0.163794047193317060787, 0.117030156341995252019, 0.387079738972604337464, 0.337785538912035898924, 0.142869534408157156766, 0.0290157910005329060432, 0.00214558995388805277169, -0.679465575181126350155e-6, 0.285225331782217055858e-7, -0.681149956853776992068e-9 };
        private static readonly double[] erfi_dc_3 = { 1, 3.46625407242567245975, 5.38168345707006855425, 4.77846592945843778382, 2.59301921623620271374, 0.848854343457902036425, 0.152264338295331783612, 0.01105924229346489121 };

        private static readonly double[] efri_nc_4 = { -0.0350353787183177984712, -0.00222426529213447927281, 0.0185573306514231072324, 0.00950804701325919603619, 0.00187123492819559223345, 0.000157544617424960554631, 0.460469890584317994083e-5, -0.230404776911882601748e-9, 0.266339227425782031962e-11 };
        private static readonly double[] erfi_dc_4 = { 1, 1.3653349817554063097, 0.762059164553623404043, 0.220091105764131249824, 0.0341589143670947727934, 0.00263861676657015992959, 0.764675292302794483503e-4 };

        private static readonly double[] erfi_nc_5 = { -0.0167431005076633737133, -0.00112951438745580278863, 0.00105628862152492910091, 0.000209386317487588078668, 0.149624783758342370182e-4, 0.449696789927706453732e-6, 0.462596163522878599135e-8, -0.281128735628831791805e-13, 0.99055709973310326855e-16 };
        private static readonly double[] erfi_dc_5 = { 1, 0.591429344886417493481, 0.138151865749083321638, 0.0160746087093676504695, 0.000964011807005165528527, 0.275335474764726041141e-4, 0.282243172016108031869e-6 };

        private static readonly double[] erfi_nc_6 = { -0.0024978212791898131227, -0.779190719229053954292e-5, 0.254723037413027451751e-4, 0.162397777342510920873e-5, 0.396341011304801168516e-7, 0.411632831190944208473e-9, 0.145596286718675035587e-11, -0.116765012397184275695e-17 };
        private static readonly double[] erfi_dc_6 = { 1, 0.207123112214422517181, 0.0169410838120975906478, 0.000690538265622684595676, 0.145007359818232637924e-4, 0.144437756628144157666e-6, 0.509761276599778486139e-9 };

        private static readonly double[] erfi_nc_7 = { -0.000539042911019078575891, -0.28398759004727721098e-6, 0.899465114892291446442e-6, 0.229345859265920864296e-7, 0.225561444863500149219e-9, 0.947846627503022684216e-12, 0.135880130108924861008e-14, -0.348890393399948882918e-21 };
        private static readonly double[] erfi_dc_7 = { 1, 0.0845746234001899436914, 0.00282092984726264681981, 0.468292921940894236786e-4, 0.399968812193862100054e-6, 0.161809290887904476097e-8, 0.231558608310259605225e-11 };
        #endregion

        #endregion

        public static Array<double> gammaQ(InArray<double> a, InArray<double> x)
        {
            using (Scope.Enter(a, x, ArrayStyles.ILNumericsV4))
            {
                if (a <= 0)
                {
                    throw new ArgumentException("a must be a positive number");
                }
                Array<double> val = zeros<double>(x.S);
                val[x == 0.0] = zeros<double>(x[x == 0.0].S);
                val[floor(a) > SwichtNumber] = gammaPApprox(a[floor(a) > SwichtNumber], x[floor(a) > SwichtNumber], 0);
                val[x < a + 1.0] = 1.0 - gSerie(a, x[x < a + 1.0]);
                val[x >= a + 1.0] = gcf(a, x[x >= a + 1.0]);
                return val;
            }
        }

        public static Array<double> gcf(InArray<double> a, InArray<double> x)
        {
            using (Scope.Enter(a, x, ArrayStyles.ILNumericsV4))
            {
                Array<double> an, b, c, d, del, h, logg;
                logg = gammaLog(a);
                b = x + 1.0 - a;
                c = 1 / double.Epsilon;
                d = 1.0 / b;
                h = d.C;
                del = d;
                int count = 1;
                do
                {
                    an = -count * (count - a);
                    b[abs(del - 1.0) < eps] = b[abs(del - 1.0) < eps] + 2.0;
                    d[abs(del - 1.0) < eps] = (an * d * b)[abs(del - 1.0) < eps];
                    del[abs(del - 1.0) < eps] = (d * c)[abs(del - 1.0) < eps];
                    if (anyall(abs(d[abs(del - 1.0) < eps]) < double.Epsilon))
                    {
                        d[abs(d[abs(del - 1.0) < eps]) < double.Epsilon] = array(double.Epsilon, d[and(abs(d) < double.Epsilon, abs(del - 1.0) < eps)].S);
                    }
                    c[abs(del - 1.0) < eps] = (b + an / c)[abs(del - 1.0) < eps];
                    if (anyall(abs(c) < double.Epsilon))
                    {
                        c[abs(c) < double.Epsilon] = array(double.Epsilon, c[and(abs(del - 1.0) < eps, abs(c) < double.Epsilon)].S);
                    }
                    d[abs(del - 1.0) < eps] = (1.0 / d)[abs(del - 1.0) < eps];
                    del[abs(del - 1.0) < eps] = (d * c)[abs(del - 1.0) < eps];
                    h[abs(del - 1.0) < eps] = (h * del)[abs(del - 1.0) < eps];
                }
                while (anyall(abs(del - 1.0) < eps));
                return exp(-x + a * log(x) - logg) * h;
            }
        }
        public static Array<double> gSerie(InArray<double> a, InArray<double> x)
        {
            using (Scope.Enter(a, x, ArrayStyles.ILNumericsV4))
            {
                Array<double> Sum, del, ap, gln;
                gln = gammaLog(a);
                ap = a;
                del = 1.0 / a;
                Sum = 1.0 / a;
                while (anyall(abs(del) < abs(Sum) * eps))
                {
                    ap[abs(del) < abs(Sum) * eps] = ap[abs(del) < abs(Sum) * eps] + 1.0;
                    del[abs(del) < abs(Sum) * eps] = (del * x / ap)[abs(del) < abs(Sum) * eps];
                    Sum[abs(del) < abs(Sum) * eps] = Sum[abs(del) < abs(Sum) * eps] + del[abs(del) < abs(Sum) * eps];
                }
                return Sum * exp(-x + a * log(x) - gln);
            }
        }

        public static Array<double> gammaPApprox(InArray<double> a, InArray<double> x, int p)
        {
            using (Scope.Enter(a, x, ArrayStyles.ILNumericsV4))
            {
                Array<double> t;
                Array<double> Sum = 0;
                Array<double> answ;
                Array<double> a1 = 1 - a;
                Array<double> lna1 = log(a1);
                Array<double> sqrta1 = sqrt(a1);
                Array<double> gln = gammaLog(a);
                Array<double> xu = max(0.0, min(a1 - 7.5 * sqrta1, x - 5.0 * sqrta1));
                xu[x > a1] = max(a1 + 11.5 * sqrta1, x + 6.0 * sqrta1);

                for (int i = 0; i < gammaPApprox_y.Length; i++)
                {
                    t = x + (xu - x) * gammaPApprox_y[i];
                    Sum = Sum + gammaPApprox_w[i] * exp(-(t - a1) + a1 * (log(t) - lna1));
                }

                answ = Sum * (xu - x) * exp(a1 * (lna1 - 1.0) - gln);
                Array<double> Sol = answ.C;
                Sol[answ < 0.0] = 1.0 + answ[answ < 0.0];
                if (p == 1)
                {
                    Sol = -answ;
                    Sol[answ > 0.0] = 1.0 - answ[answ > 0.0];
                }
                return Sol;
            }
        }
        public static Array<double> BetaComplentf(InArray<double> a, InArray<double> b, InArray<double> x)
        {
            using (Scope.Enter(a, b, x, ArrayStyles.ILNumericsV4))
            {
                Array<double> aa, c, d, del, h, qab, qam, qap;
                qab = (a + b) * ones<double>(x.S);
                aa = a * ones<double>(x.S);
                qap = a + 1.0;
                qam = a - 1.0;
                c = ones<double>(x.S);
                d = 1.0 - qab * x / qap;
                d[abs(d) < double.Epsilon] = array(double.Epsilon, d[abs(d) < double.Epsilon].S);
                d = 1.0 / d;
                del = d * c;
                h = d;
                int m2, i = 1;
                Array<long> I = find(abs(del - 1.0) <= eps); ;
                do
                {
                    m2 = 2 * i;
                    aa[I] = i * (b - i) * x[I] / ((qam + m2) * (a + m2));
                    d[I] = 1.0 + aa[I] * d[I];
                    d[abs(d[I]) < double.Epsilon] = array(double.Epsilon, d[abs(d[I]) < double.Epsilon].S);
                    c[I] = 1.0 + aa[I] / c[I];
                    c[abs(c[I]) < double.Epsilon] = array(double.Epsilon, c[abs(c[I]) < double.Epsilon].S);
                    d[I] = 1.0 / d[I];
                    h[I] = h[I] * d[I] * c[I];
                    aa[I] = -(a + i) * (qab[I] + i) * x[I] / ((a + m2) * (qap + m2));
                    d[I] = 1.0 + aa[I] * d[I];
                    d[abs(d[I]) < double.Epsilon] = array(double.Epsilon, d[abs(d[I]) < double.Epsilon].S);
                    c[I] = 1.0 + aa[I] / c[I];
                    c[abs(c[I]) < double.Epsilon] = array(double.Epsilon, c[abs(c[I]) < double.Epsilon].S);
                    d[I] = 1.0 / d[I];
                    del[I] = d[I] * c[I];
                    h[I] = h[I] * del[I];
                    I = find(abs(del - 1.0) <= eps);
                    i = i + 1;
                } while ((i < 10000) && (!I.IsEmpty));
                return h;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Array<double> BetaApprox(InArray<double> a, InArray<double> b, InArray<double> x)
        {
            using (Scope.Enter(a, b, x, ArrayStyles.ILNumericsV4))
            {
                Array<double> xu = x.C, t, Su, answ = x.C;
                Array<double> a1 = a - 1;
                Array<double> sol = ones<double>(x.S);
                Array<double> b1 = b - 1;
                Array<double> mu = a / (a + b) * ones<double>(x.S);
                Array<double> logMu = log(mu);
                Array<double> logMuc = log(1.0 - mu);
                t = sqrt(a * b / (pow(a + b, 2) * (a + b + 1.0)));
                Array<long> I = find(x > a / (a + b));
                xu[I] = min(1.0, max(mu[I] + 10.0 * t, x[I] + 5.0 * t));
                sol[x <= 0] = zeros<double>(x[x <= 0].S);
                Array<long> I2 = find((x <= a / (a + b)));
                xu[I2] = (max(0.0, min(mu - 10.0 * t, x - 5.0 * t)))[I2];
                Su = zeros<double>(x.S);
                I = find(or(x > a / (a + b), (x <= a / (a + b))));

                for (int i = 0; i < betaApprox_y.Length; i++)
                {
                    t[I] = x[I] + (xu[I] - x[I]) * betaApprox_y[i];
                    Su[I] = Su[I] + betaApprox_w[i] * exp(a1 * (log(t[I]) - logMu[I]) + b1 * (log(1.0 - t[I]) - logMuc[I]));
                }
                answ[I] = Su[I] * (xu[I] - x[I]) * (a1 * logMu[I] - gammaLog(a) + b1 * logMuc[I] - gammaLog(b) + gammaLog(a + b));
                I = find(answ > 0.0);
                sol[I] = (1.0 - answ[I]);
                I2 = find(answ < 0.0);
                sol[I2] = -answ[I2];
                return sol;
            }
        }
        /// <summary>
        /// Evaluate a polynomial at point x.
        /// Coefficients are ordered by power with power k at index k.
        /// Example: coefficients [3,-1,2] represent y=2x^2-x+3.
        /// </summary>
        /// <param name="z">The location where to evaluate the polynomial at.</param>
        /// <param name="coefficients">The coefficients of the polynomial, coefficient for power k at index k.</param>
        public static double poly(double z, params double[] coefficients)
        {
            double sum = coefficients[coefficients.Length - 1];
            for (int i = coefficients.Length - 2; i >= 0; --i)
            {
                sum *= z;
                sum += coefficients[i];
            }

            return sum;
        }

        /// <summary>
        /// Implementation of the error function.
        /// </summary>
        /// <param name="z">Where to evaluate the error function.</param>
        /// <param name="invert">Whether to compute 1 - the error function.</param>
        /// <returns>the error function.</returns>
        public static double ErfImp(double z, bool invert)
        {
            if (z < 0)
            {
                if (!invert)
                {
                    return -ErfImp(-z, false);
                }

                if (z < -0.5)
                {
                    return 2 - ErfImp((-z), true);
                }

                return 1 + ErfImp(-z, false);
            }

            double result;

            //
            // Big bunch of selection statements now to pick which
            // implementation to use, try to put most likely options
            // first:
            //
            if (z < 0.5)
            {
                //
                // We're going to calculate erf:
                //
                if (z < 1e-10)
                {
                    result = (z * 1.125) + (z * 0.003379167095512573896158903121545171688);
                }
                else
                {
                    // Worst case absolute error found: 6.688618532e-21              
                    result = (z * 1.125) + (z * poly(z, erfc_nc_1) / poly(z, erfc_dc_1));
                }
            }
            else if ((z < 110) || ((z < 110) && invert))
            {
                //
                // We'll be calculating erfc:
                //
                invert = !invert;
                double r, b;
                if (z < 0.75)
                {
                    // Worst case absolute error found: 5.582813374e-21               
                    r = poly(z - 0.5, erfc_nc_2) / poly(z - 0.5, erfc_dc_2);
                    b = 0.3440242112F;
                }
                else if (z < 1.25)
                {
                    // Worst case absolute error found: 4.01854729e-21                 
                    r = poly(z - 0.75, erfc_nc_3) / poly(z - 0.75, erfc_dc_3);
                    b = 0.419990927F;
                }
                else if (z < 2.25)
                {
                    // Worst case absolute error found: 2.866005373e-21                
                    r = poly(z - 1.25, erfc_nc_4) / poly(z - 1.25, erfc_dc_4);
                    b = 0.4898625016F;
                }
                else if (z < 3.5)
                {
                    // Worst case absolute error found: 1.045355789e-21                  
                    r = poly(z - 2.25, erfc_nc_5) / poly(z - 2.25, erfc_dc_5);
                    b = 0.5317370892F;
                }
                else if (z < 5.25)
                {
                    // Worst case absolute error found: 8.300028706e-22                    
                    r = poly(z - 3.5, erfc_nc_6) / poly(z - 3.5, erfc_dc_6);
                    b = 0.5489973426F;
                }
                else if (z < 8)
                {
                    // Worst case absolute error found: 1.700157534e-21                   
                    r = poly(z - 5.25, erfc_nc_7) / poly(z - 5.25, erfc_dc_7);
                    b = 0.5571740866F;
                }
                else if (z < 11.5)
                {
                    // Worst case absolute error found: 3.002278011e-22                 
                    r = poly(z - 8, erfc_nc_8) / poly(z - 8, erfc_dc_8);
                    b = 0.5609807968F;
                }
                else if (z < 17)
                {
                    // Worst case absolute error found: 6.741114695e-21                 
                    r = poly(z - 11.5, erfc_nc_9) / poly(z - 11.5, erfc_dc_9);
                    b = 0.5626493692F;
                }
                else if (z < 24)
                {
                    // Worst case absolute error found: 7.802346984e-22                   
                    r = poly(z - 17, erfc_nc_10) / poly(z - 17, erfc_dc_10);
                    b = 0.5634598136F;
                }
                else if (z < 38)
                {
                    // Worst case absolute error found: 2.414228989e-22                   
                    r = poly(z - 24, erfc_nc_11) / poly(z - 24, erfc_dc_11);
                    b = 0.5638477802F;
                }
                else if (z < 60)
                {
                    // Worst case absolute error found: 5.896543869e-24                 
                    r = poly(z - 38, erfc_nc_12) / poly(z - 38, erfc_dc_12);
                    b = 0.5640528202F;
                }
                else if (z < 85)
                {
                    // Worst case absolute error found: 3.080612264e-21                   
                    r = poly(z - 60, erfc_nc_13) / poly(z - 60, erfc_dc_13);
                    b = 0.5641309023F;
                }
                else
                {
                    // Worst case absolute error found: 8.094633491e-22                
                    r = poly(z - 85, erfc_nc_14) / poly(z - 85, erfc_dc_14);
                    b = 0.5641584396F;
                }

                double g = (double)exp(-z * z) / z;
                result = (g * b) + (g * r);
            }
            else
            {
                //
                // Any value of z larger than 28 will underflow to zero:
                //
                result = 0;
                invert = !invert;
            }

            if (invert)
            {
                result = 1 - result;
            }

            return result;
        }
        /// <summary>
        /// The implementation of the inverse error function.
        /// </summary>
        /// <param name="p">First intermediate parameter.</param>
        /// <param name="q">Second intermediate parameter.</param>
        /// <param name="s">Third intermediate parameter.</param>
        /// <returns>the inverse error function.</returns>
        public static double ErfInvImpl(double p, double q, double s)
        {
            double result;

            if (p <= 0.5)
            {
                //
                // Evaluate inverse erf using the rational approximation:
                //
                // x = p(p+10)(Y+R(p))
                //
                // Where Y is a constant, and R(p) is optimized for a low
                // absolute error compared to |Y|.
                //
                // double: Max error found: 2.001849e-18
                // long double: Max error found: 1.017064e-20
                // Maximum Deviation Found (actual error term at infinite precision) 8.030e-21
                //
                const float y = 0.0891314744949340820313f;
                double g = p * (p + 10);
                double r = poly(p, erfi_nc_1) / poly(p, erfi_dc_1);
                result = (g * y) + (g * r);
            }
            else if (q >= 0.25)
            {
                //
                // Rational approximation for 0.5 > q >= 0.25
                //
                // x = sqrt(-2*log(q)) / (Y + R(q))
                //
                // Where Y is a constant, and R(q) is optimized for a low
                // absolute error compared to Y.
                //
                // double : Max error found: 7.403372e-17
                // long double : Max error found: 6.084616e-20
                // Maximum Deviation Found (error term) 4.811e-20
                //
                const float y = 2.249481201171875f;
                double g = (double)sqrt(-2 * log(q));
                double xs = q - 0.25;
                double r = poly(xs, erfi_nc_2) / poly(xs, erfi_dc_2);
                result = g / (y + r);
            }
            else
            {
                //
                // For q < 0.25 we have a series of rational approximations all
                // of the general form:
                //
                // let: x = sqrt(-log(q))
                //
                // Then the result is given by:
                //
                // x(Y+R(x-B))
                //
                // where Y is a constant, B is the lowest value of x for which
                // the approximation is valid, and R(x-B) is optimized for a low
                // absolute error compared to Y.
                //
                // Note that almost all code will really go through the first
                // or maybe second approximation. After than we're dealing with very
                // small input values indeed: 80 and 128 bit long double's go all the
                // way down to ~ 1e-5000 so the "tail" is rather long...
                //
                double x = (double)sqrt(-log(q));
                if (x < 3)
                {
                    // Max error found: 1.089051e-20
                    const float y = 0.807220458984375f;
                    double xs = x - 1.125;
                    double r = poly(xs, erfi_nc_3) / poly(xs, erfi_dc_3);
                    result = (y * x) + (r * x);
                }
                else if (x < 6)
                {
                    // Max error found: 8.389174e-21
                    const float y = 0.93995571136474609375f;
                    double xs = x - 3;
                    double r = poly(xs, efri_nc_4) / poly(xs, erfi_dc_4);
                    result = (y * x) + (r * x);
                }
                else if (x < 18)
                {
                    // Max error found: 1.481312e-19
                    const float y = 0.98362827301025390625f;
                    double xs = x - 6;
                    double r = poly(xs, erfi_nc_5) / poly(xs, erfi_dc_5);
                    result = (y * x) + (r * x);
                }
                else if (x < 44)
                {
                    // Max error found: 5.697761e-20
                    const float y = 0.99714565277099609375f;
                    double xs = x - 18;
                    double r = poly(xs, erfi_nc_6) / poly(xs, erfi_dc_6);
                    result = (y * x) + (r * x);
                }
                else
                {
                    // Max error found: 1.279746e-20
                    const float y = 0.99941349029541015625f;
                    double xs = x - 44;
                    double r = poly(xs, erfi_nc_7) / poly(xs, erfi_dc_7);
                    result = (y * x) + (r * x);
                }
            }

            return s * result;
        }

        public static void rationalApprox(
            InArray<double> r,
            InArray<double> s,
            double x, int n,
            ref double nump,
            ref double denp,
            ref double y)
        {
            // TODO: Unify, normalize, then make public
            using (Scope.Enter(r, s, x, n, ArrayStyles.ILNumericsV4))
            {
                y = x * x;
                double z = 64.0 - y;
                nump = r.GetValue(n);
                denp = s.GetValue(n);
                for (int i = n - 1; i >= 0; i--)
                {
                    // Todo: change to GetValue syntax (everywhere)
                    nump = nump * z + r.GetValue(i);
                    denp = denp * y + s.GetValue(i);
                }
            }
        }

        public static void asymptoticApprox(
            InArray<double> pn,
            InArray<double> pd,
            InArray<double> qn,
            InArray<double> qd,
            double ax, double fac, int n,
            ref double nump, ref double denp,
            ref double numq, ref double denq,
            ref double y, ref double xx, ref double z)
        {
            // TODO: Unify, normalize, then make public
            using (Scope.Enter(pn, pd, fac, nump, denp))
            {
                z = 8.0 / ax;
                y = z * z;
                const double pio4 = 0.7853981633974483;
                xx = ax - (fac * pio4);

                nump = pn.GetValue(n);
                denp = pd.GetValue(n);
                numq = qn.GetValue(n);
                denq = qd.GetValue(n);
                for (int i = n - 1; i >= 0; i--)
                {
                    nump = nump * y + pn.GetValue(i);
                    denp = denp * y + pd.GetValue(i);
                    numq = numq * y + qn.GetValue(i);
                    denq = denq * y + qd.GetValue(i);
                }
            }
        }

        private const double xj00 = 5.783185962946785;
        private const double xj10 = 3.047126234366209e1;

        private static readonly double[] j0r = { 1.682397144220462e-4, 2.058861258868952e-5, 5.288947320067750e-7, 5.557173907680151e-9, 2.865540042042604e-11, 7.398972674152181e-14, 7.925088479679688e-17 };
        private static readonly double[] j0s = { 1.0, 1.019685405805929e-2, 5.130296867064666e-5, 1.659702063950243e-7, 3.728997574317067e-10, 5.709292619977798e-13, 4.932979170744996e-16 };

        private static readonly double[] j0pn = { 9.999999999999999e-1, 1.039698629715637, 2.576910172633398e-1, 1.504152485749669e-2, 1.052598413585270e-4 };
        private static readonly double[] j0pd = { 1.0, 1.040797262528109, 2.588070904043728e-1, 1.529954477721284e-2, 1.168931211650012e-4 };
        private static readonly double[] j0qn = { -1.562499999999992e-2, -1.920039317065641e-2, -5.827951791963418e-3, -4.372674978482726e-4, -3.895839560412374e-6 };
        private static readonly double[] j0qd = { 1.0, 1.237980436358390, 3.838793938147116e-1, 3.100323481550864e-2, 4.165515825072393e-4 };

        private const double twoopi = 0.6366197723675813;
        public static double besselJ0Elem(double x)
        {
            double ax = Math.Abs(x);
            if (ax < 8.0)
            {
                double nump = 0, denp = 0, y = 0;
                rationalApprox(j0r, j0s, x, 6, ref nump, ref denp, ref y);
                return (double)(nump * (y - xj00) * (y - xj10) / denp);
            }
            else
            {
                double nump = 0, denp = 0, numq = 0, denq = 0, y = 0, xx = 0, z = 0;
                asymptoticApprox(j0pn, j0pd, j0qn, j0qd, ax, 1.0, 4, ref nump, ref denp, ref numq, ref denq, ref y, ref xx, ref z);
                return Math.Sqrt(twoopi / ax) * ((Math.Cos(xx) * nump / denp) - (z * Math.Sin(xx) * numq / denq));
            }
        }

        private const double xj01 = 1.468197064212389e1;
        private const double xj11 = 4.921845632169460e1;

        private static readonly double[] j1r = { 7.309637831891357e-5, 3.551248884746503e-6, 5.820673901730427e-8, 4.500650342170622e-10, 1.831596352149641e-12, 3.891583573305035e-15, 3.524978592527982e-18 };
        private static readonly double[] j1s = { 1.0, 9.398354768446072e-3, 4.328946737100230e-5, 1.271526296341915e-7, 2.566305357932989e-10, 3.477378203574266e-13, 2.593535427519985e-16 };

        private static readonly double[] j1pn = { 1.0, 1.014039111045313, 2.426762348629863e-1, 1.350308200342000e-2, 9.516522033988099e-5 };
        private static readonly double[] j1pd = { 1.0, 1.012208056357845, 2.408580305488938e-1, 1.309511056184273e-2, 7.746422941504713e-5 };
        private static readonly double[] j1qn = { 4.687499999999991e-2, 5.652407388406023e-2, 1.676531273460512e-2, 1.231216817715814e-3, 1.178364381441801e-5 };
        private static readonly double[] j1qd = { 1.0, 1.210119370463693, 3.626494789275638e-1, 2.761695824829316e-2, 3.240517192670181e-4 };
        public static double besselJ1Elem(double x)
        {
            double ax = Math.Abs(x);
            if (ax < 8.0)
            {
                double nump = 0, denp = 0, y = 0;
                rationalApprox(j1r, j1s, x, 6, ref nump, ref denp, ref y);
                return (double)(x * nump * (y - xj01) * (y - xj11) / denp);
            }
            else
            {
                double nump = 0, denp = 0, numq = 0, denq = 0, y = 0, xx = 0, z = 0;
                asymptoticApprox(j1pn, j1pd, j1qn, j1qd, ax, 3.0, 4, ref nump, ref denp, ref numq, ref denq, ref y, ref xx, ref z);
                double ans = Math.Sqrt(twoopi / ax) * ((Math.Cos(xx) * nump / denp) - (z * Math.Sin(xx) * numq / denq));
                return x > 0.0 ? ans : -ans;
            }
        }

        public static double besselJnElem(double x, int n)
        {
            const double ACC = 160.0;
            const int IEXP = 512; // checked in c++ console application
            bool jsum;
            int j, k = 0, m;
            double ax, bj, bjm, bjp, sum, tox, ans;

            if (double.IsPositiveInfinity(x))
                return 0.0;

            if (n == 0)
                return besselJ0Elem(x);

            if (n == 1)
                return besselJ1Elem(x);

            ax = Math.Abs(x);
            if ((ax * ax) <= (8.0 * double.Epsilon))
            {
                return 0.0;
            }
            else if (ax > (double)n)
            {
                tox = 2.0 / ax;
                bjm = besselJ0Elem(ax);
                bj = besselJ1Elem(ax);

                for (j = 1; j < n; j++)
                {
                    bjp = (j * tox * bj) - bjm;
                    bjm = bj;
                    bj = bjp;
                }
                ans = bj;
            }
            else
            {
                tox = 2.0 / ax;
                m = 2 * ((n + (int)(Math.Sqrt(ACC * n))) / 2);
                jsum = false;
                bjp = 0.0;
                ans = 0.0;
                sum = 0.0;
                bj = 1.0;

                for (j = m; j > 0; j--)
                {
                    bjm = (j * tox * bj) - bjp;
                    bjp = bj;
                    bj = bjm;

                    frexp(bj, ref k);
                    if (k > IEXP)
                    {
                        // ldexp == System.Math.Pow: https://msdn.microsoft.com/en-us/library/zx52ds7f.aspx
                        bj = Math.Pow(bj, -IEXP);
                        bjp = Math.Pow(bjp, -IEXP);
                        ans = Math.Pow(ans, -IEXP);
                        sum = Math.Pow(sum, -IEXP);
                    }

                    if (jsum)
                        sum += bj;

                    jsum = !jsum;

                    if (j == n)
                        ans = bjp;

                }

                sum = (2.0 * sum) - bj;
                ans /= sum;
            }

            return ((x < 0.0) && ((n & 1) == 1)) ? -ans : ans;
        }

        private static readonly double[] y0r = { -7.653778457189104e-3, -5.854760129990403e-2, 3.720671300654721e-4, 3.313722284628089e-5, 4.247761237036536e-8, -4.134562661019613e-9, -3.382190331837473e-11, -1.017764126587862e-13, -1.107646382675456e-16 };
        private static readonly double[] y0s = { 1.0, 1.125494540257841e-2, 6.427210537081400e-5, 2.462520624294959e-7, 7.029372432344291e-10, 1.560784108184928e-12, 2.702374957564761e-15, 3.468496737915257e-18, 2.716600180811817e-21 };

        private static readonly double[] y0pn = { 9.999999999999999e-1, 1.039698629715637, 2.576910172633398e-1, 1.504152485749669e-2, 1.052598413585270e-4 };
        private static readonly double[] y0pd = { 1.0, 1.040797262528109, 2.588070904043728e-1, 1.529954477721284e-2, 1.168931211650012e-4 };
        private static readonly double[] y0qn = { -1.562499999999992e-2, -1.920039317065641e-2, -5.827951791963418e-3, -4.372674978482726e-4, -3.895839560412374e-6 };
        private static readonly double[] y0qd = { 1.0, 1.237980436358390, 3.838793938147116e-1, 3.100323481550864e-2, 4.165515825072393e-4 };

        public static double besselY0Elem(double x)
        {
            // only for positive x-s! 
            if (x < 8.0)
            {
                double j0x = besselJ0Elem(x);
                double nump = 0, denp = 0, y = 0;
                rationalApprox(y0r, y0s, x, 8, ref nump, ref denp, ref y);
                return (double)((nump / denp) + (twoopi * j0x * Math.Log(x)));
            }
            else
            {
                double nump = 0, denp = 0, numq = 0, denq = 0, y = 0, xx = 0, z = 0;
                asymptoticApprox(y0pn, y0pd, y0qn, y0qd, x, 1.0, 4, ref nump, ref denp, ref numq, ref denq, ref y, ref xx, ref z);
                return Math.Sqrt(twoopi / x) * ((Math.Sin(xx) * nump / denp) + (z * Math.Cos(xx) * numq / denq));
            }
        }

        private static readonly double[] y1r = { -1.041835425863234e-1, -1.135093963908952e-5, 2.212118520638132e-4, 1.270981874287763e-6, -3.982892100836748e-8, -4.820712110115943e-10, -1.929392690596969e-12, -2.725259514545605e-15 };
        private static readonly double[] y1s = { 1.0, 1.186694184425838e-2, 7.121205411175519e-5, 2.847142454085055e-7, 8.364240962784899e-10, 1.858128283833724e-12, 3.018846060781846e-15, 3.015798735815980e-18 };

        private static readonly double[] y1pn = { 1.0, 1.014039111045313, 2.426762348629863e-1, 1.350308200342000e-2, 9.516522033988099e-5 };
        private static readonly double[] y1pd = { 1.0, 1.012208056357845, 2.408580305488938e-1, 1.309511056184273e-2, 7.746422941504713e-5 };
        private static readonly double[] y1qn = { 4.687499999999991e-2, 5.652407388406023e-2, 1.676531273460512e-2, 1.231216817715814e-3, 1.178364381441801e-5 };
        private static readonly double[] y1qd = { 1.0, 1.210119370463693, 3.626494789275638e-1, 2.761695824829316e-2, 3.240517192670181e-4 };

        public static double besselY1Elem(double x)
        {
            // only for positive x-s! 
            if (x < 8.0)
            {
                double j1x = besselJ1Elem(x);
                double nump = 0, denp = 0, y = 0;
                rationalApprox(y1r, y1s, x, 7, ref nump, ref denp, ref y);
                return (double)((x * nump / denp) + (twoopi * ((j1x * Math.Log(x)) - (1.0 / x))));
            }
            else
            {
                double nump = 0, denp = 0, numq = 0, denq = 0, y = 0, xx = 0, z = 0;
                asymptoticApprox(y1pn, y1pd, y1qn, y1qd, x, 3.0, 4, ref nump, ref denp, ref numq, ref denq, ref y, ref xx, ref z);
                return Math.Sqrt(twoopi / x) * ((Math.Sin(xx) * nump / denp) + (z * Math.Cos(xx) * numq / denq));
            }
        }

        public static double besselYnElem(double x, int n)
        {
            int j;
            double by, bym, byp, tox;

            if (x <= (double.Epsilon * 8))
                return double.NegativeInfinity;

            if (double.IsPositiveInfinity(x))
                return 0.0;

            if (n == 0)
                return besselY0Elem(x);

            if (n == 1)
                return besselY1Elem(x);

            tox = 2.0 / x;
            by = besselY1Elem(x);
            bym = besselY0Elem(x);

            for (j = 1; j < n; j++)
            {
                byp = (j * tox * by) - bym;
                bym = by;
                by = byp;
            }

            return by;
        }

        private static readonly double[] i0p = { 9.999999999999997e-1, 2.466405579426905e-1, 1.478980363444585e-2, 3.826993559940360e-4, 5.395676869878828e-6, 4.700912200921704e-8, 2.733894920915608e-10, 1.115830108455192e-12, 3.301093025084127e-15, 7.209167098020555e-18, 1.166898488777214e-20, 1.378948246502109e-23, 1.124884061857506e-26, 5.498556929587117e-30 };
        private static readonly double[] i0q = { 4.463598170691436e-1, 1.702205745042606e-3, 2.792125684538934e-6, 2.369902034785866e-9, 8.965900179621208e-13 };
        private static readonly double[] i0pp = { 1.192273748120670e-1, 1.947452015979746e-1, 7.629241821600588e-2, 8.474903580801549e-3, 2.023821945835647e-4 };
        private static readonly double[] i0qq = { 2.962898424533095e-1, 4.866115913196384e-1, 1.938352806477617e-1, 2.261671093400046e-2, 6.450448095075585e-4, 1.529835782400450e-6 };

        public static double besselI0Elem(double x)
        {
            double ax = Math.Abs(x);
            if (ax < 15.0)
            {
                double y = x * x;
                return (double)MathInternal.poly(i0p, y) / (double)MathInternal.poly(i0q, 225.0 - y);
            }
            else
            {
                double z = 1.0 - (15.0 / ax);
                return Math.Exp(ax) * (double)MathInternal.poly(i0pp, z) / (double)MathInternal.poly(i0qq, z) * Math.Sqrt(ax);
            }
        }

        private static readonly double[] i1p = { 5.000000000000000e-1, 6.090824836578078e-2, 2.407288574545340e-3, 4.622311145544158e-5, 5.161743818147913e-7, 3.712362374847555e-9, 1.833983433811517e-11, 6.493125133990706e-14, 1.693074927497696e-16, 3.299609473102338e-19, 4.813071975603122e-22, 5.164275442089090e-25, 3.846870021788629e-28, 1.712948291408736e-31 };
        private static readonly double[] i1q = { 4.665973211630446e-1, 1.677754477613006e-3, 2.583049634689725e-6, 2.045930934253556e-9, 7.166133240195285e-13 };
        private static readonly double[] i1pp = { 1.286515211317124e-1, 1.930915272916783e-1, 6.965689298161343e-2, 7.345978783504595e-3, 1.963602129240502e-4 };
        private static readonly double[] i1qq = { 3.309385098860755e-1, 4.878218424097628e-1, 1.663088501568696e-1, 1.473541892809522e-2, 1.964131438571051e-4, -1.034524660214173e-6 };

        public static double besselI1Elem(double x)
        {
            double ax = Math.Abs(x);
            if (ax < 15.0)
            {
                double y = x * x;
                return x * (double)MathInternal.poly(i1p, y) / (double)MathInternal.poly(i1q, 225.0 - y);
            }
            else
            {
                double z = 1.0 - (15.0 / ax);
                double ans = Math.Exp(ax) * (double)MathInternal.poly(i0pp, z) / (double)MathInternal.poly(i0qq, z) * Math.Sqrt(ax);
                return x > 0.0 ? ans : -ans;
            }
        }

        private static readonly double[] k0pi = { 1.0, 2.346487949187396e-1, 1.187082088663404e-2, 2.150707366040937e-4, 1.425433617130587e-6 };
        private static readonly double[] k0qi = { 9.847324170755358e-1, 1.518396076767770e-2, 8.362215678646257e-5 };
        private static readonly double[] k0p = { 1.159315156584126e-1, 2.770731240515333e-1, 2.066458134619875e-2, 4.574734709978264e-4, 3.454715527986737e-6 };
        private static readonly double[] k0q = { 9.836249671709183e-1, 1.627693622304549e-2, 9.809660603621949e-5 };
        private static readonly double[] k0pp = { 1.253314137315499, 1.475731032429900e1, 6.123767403223466e1, 1.121012633939949e2, 9.285288485892228e1, 3.198289277679660e1, 3.595376024148513, 6.160228690102976e-2 };
        private static readonly double[] k0qq = { 1.0, 1.189963006673403e1, 5.027773590829784e1, 9.496513373427093e1, 8.318077493230258e1, 3.181399777449301e1, 4.443672926432041, 1.408295601966600e-1 };

        public static double besselK0Elem(double x)
        {
            // only for positive x-s! 
            if (x <= 1.0)
            {
                double z = x * x;
                double term = (double)MathInternal.poly(k0pi, z) * Math.Log(x) / (double)MathInternal.poly(k0qi, 1.0 - z);
                return ((double)MathInternal.poly(k0p, z) / (double)MathInternal.poly(k0q, 1.0 - z)) - term;
            }
            else
            {
                double z = 1.0 / x;
                return Math.Exp(-x) * (double)MathInternal.poly(k0pp, z) / ((double)MathInternal.poly(k0qq, z) * Math.Sqrt(x));
            }
        }

        private static readonly double[] k1pi = { 0.5, 5.598072040178741e-2, 1.818666382168295e-3, 2.397509908859959e-5, 1.239567816344855e-7 };
        private static readonly double[] k1qi = { 9.870202601341150e-1, 1.292092053534579e-2, 5.881933053917096e-5 };
        private static readonly double[] k1p = { -3.079657578292062e-1, -8.109417631822442e-2, -3.477550948593604e-3, -5.385594871975406e-5, -3.110372465429008e-7 };
        private static readonly double[] k1q = { 9.861813171751389e-1, 1.375094061153160e-2, 6.774221332947002e-5 };
        private static readonly double[] k1pp = { 1.253314137315502, 1.457171340220454e1, 6.063161173098803e1, 1.147386690867892e2, 1.040442011439181e2, 4.356596656837691e1, 7.265230396353690, 3.144418558991021e-1 };
        private static readonly double[] k1qq = { 1.0, 1.125154514806458e1, 4.427488496597630e1, 7.616113213117645e1, 5.863377227890893e1, 1.850303673841586e1, 1.857244676566022, 2.538540887654872e-2 };

        public static double besselK1Elem(double x)
        {
            // only for positive x-s! 
            if (x <= 1.0)
            {
                double z = x * x;
                double term = (double)MathInternal.poly(k1pi, z) * Math.Log(x) / (double)MathInternal.poly(k1qi, 1.0 - z);
                return (x * ((double)MathInternal.poly(k1p, z) / (double)MathInternal.poly(k1q, 1.0 - z) + term)) + (1.0 / x);
            }
            else
            {
                double z = 1.0 / x;
                return Math.Exp(-x) * (double)MathInternal.poly(k1pp, z) / ((double)MathInternal.poly(k1qq, z) * Math.Sqrt(x));
            }
        }

        public static double besselModInElem(double x, int n)
        {
            const double ACC = 200.0;
            const int IEXP = 512; // checked in c++ console application
            int j, k = 0;
            double bi, bim, bip, tox, ans;

            if (double.IsPositiveInfinity(x))
            {
                return double.PositiveInfinity;
            }

            if (n == 0)
                return besselI0Elem(x);

            if (n == 1)
                return besselI1Elem(x);

            if ((x * x) <= (8.0 * double.Epsilon))
            {
                return 0.0;
            }
            else 
            {
                tox = 2.0 / Math.Abs(x);
                bip = 0.0;
                ans = 0.0;
                bi = 1.0;

                for (j=2*(n+(int)Math.Sqrt(ACC * n));j>0;j--)
                {
                    bim = bip + (j * tox * bi);
                    bip = bi;
                    bi = bim;

                    frexp(bi, ref k);
                    if (k > IEXP)
                    {
                        // ldexp == System.Math.Pow: https://msdn.microsoft.com/en-us/library/zx52ds7f.aspx
                        ans = Math.Pow(ans, -IEXP);
                        bi = Math.Pow(bi, -IEXP);
                        bip = Math.Pow(bip, -IEXP);
                    }

                    if (j == n)
                        ans = bip;
                }

                ans *= besselI0Elem(x) / bi;
                return ((x < 0.0) && ((n & 1) == 1)) ? -ans : ans;
            }
        }

        public static double besselModKnElem(double x, int n)
        {
            int j;
            double bk, bkm, bkp, tox;

            if (Math.Abs(x) <= double.Epsilon)
            {
                return double.PositiveInfinity;
            }

            if (n == 0)
                return besselK0Elem(x);

            if (n == 1)
                return besselK1Elem(x);

            tox = 2.0 / x;
            bkm = besselK0Elem(x);
            bk = besselK1Elem(x);            

            for (j = 1; j < n; j++)
            {
                bkp = (j * tox * bk) + bkm;
                bkm = bk;
                bk = bkp;
            }

            return bk;
        }

        public static double frexp(double value, ref int exponent)
        {
            // http://www.codedisqus.com/0mJjgVPePj/is-there-a-java-equivalent-of-frexp.html
            // http://stackoverflow.com/questions/389993/extracting-mantissa-and-exponent-from-double-in-c-sharp
            // https://msdn.microsoft.com/en-us/library/w1xfschh.aspx
            exponent = 0;

            if (value == 0.0 || value == -0.0)
            {
                return 0.0;
            }

            if (double.IsNaN(value))
            {
                exponent = -1;
                return value;
            }

            if (double.IsInfinity(value))
            {
                exponent = -1;
                return value;
            }

            double mantissa = value;
            int sign = 1;

            if (mantissa < 0)
            {
                sign = -1;
                mantissa = -(mantissa);
            }

            while (mantissa < 0.5d)
            {
                mantissa *= 2.0d;
                exponent--;
            }

            while (mantissa >= 1.0d)
            {
                mantissa *= 0.5d;
                exponent++;
            }

            mantissa *= sign;
            return mantissa;
        }
    }
}