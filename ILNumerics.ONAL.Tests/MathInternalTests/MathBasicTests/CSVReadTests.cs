using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 


namespace ILNumerics.UnitTests.Legacy_Tests {

    internal class GeoCoordinate {
        public double A, B, C;

        public GeoCoordinate(double a, double b, double c) {
            A = a; B = b; C = c; 
        }
        public override string ToString() {
            return $"A:{A},B:{B},C:{C}";
        }
        public override bool Equals(object obj) {
            var other = obj as GeoCoordinate;
            if (other == null) return false;
            return other.A == A && other.B == B && other.C == C; 
        }
    }
    [TestClass]
    public class CSVReadTests {

        internal const string csvread_sample34x24_NAME = "csvread_sample34x24.csv"; 

        [TestMethod]
        public void CSV_CustomLocation()
        {
            GeoCoordinate gc1 = new GeoCoordinate(40.3232321321, 21.34234234, 1.0);
            GeoCoordinate gc2 = new GeoCoordinate(41.7567554321, -51.14234234, 2.0);
            GeoCoordinate gc3 = new GeoCoordinate(42.3232423321, 25.76234234, 3.0);

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string[] lines = new string[] {

                string.Format("{0}|{1}|{2}", gc1.A, gc1.B, gc1.C),
                string.Format("{0}|{1}|{2}", gc2.A, gc2.B, gc2.C),
                string.Format("{0}|{1}|{2}", gc3.A, gc3.B, gc3.C),
            };

            File.WriteAllLines("location.csv", lines);

            Array<GeoCoordinate> coords1 = csvread<GeoCoordinate>(File.Open("location.csv", FileMode.Open), elementConverter: ParseLocation);
            Array<GeoCoordinate> coords2 = csvread<GeoCoordinate>(File.ReadAllText("location.csv"), elementConverter: ParseLocation);

            Assert.AreEqual(gc1, coords1.GetValue(0));
            Assert.AreEqual(gc2, coords1.GetValue(1));
            Assert.AreEqual(gc3, coords1.GetValue(2));

            Assert.AreEqual(gc1, coords2.GetValue(0));
            Assert.AreEqual(gc2, coords2.GetValue(1));
            Assert.AreEqual(gc3, coords2.GetValue(2));
            
        }

        private GeoCoordinate ParseLocation(string element)
        {
            // Let say we a location tracked data stored in the following format: 
            // LATITUDE | LONGITUDE | ALTITUDE
            // Therefore we first need to split it at the '|' character
            // And then parse the numbers
            string[] latLongAlt = element.Split('|');

            double latitude = double.Parse(latLongAlt[0]);
            double longitude = double.Parse(latLongAlt[1]);
            double altitude = double.Parse(latLongAlt[2]);

            // The return type should be compile-time equivalent to the type one wants to parse.
            // This method automatically provides the element and the culture info (if specified)
            // which is US by default.
            return new GeoCoordinate(latitude, longitude, altitude);
        }

       

        [TestMethod]
        public void CSV_CustomParsingDelegate()
        {
            if (File.Exists("test_float_custom.csv"))
            {
                File.Delete("test_float_custom.csv");
            }
            Array<float> written = tosingle(rand(500, 500));
            csvwrite<float>(written, "test_float_custom.csv");

            Array<float> read = csvread<float>(File.Open("test_float_custom.csv", FileMode.Open), elementConverter: ParseCustom);

            Assert.IsTrue(allall(abs(written - read) < 0.0001f));
            //Assert(allall(abs(written - read) < 0.0001f));
           
        }

        private float ParseCustom(string element)
        {
            // The return type should be compile-time equivalent to the type one wants to parse
            // This method automatically provides the element.
            return float.Parse(element);
        }

        [TestMethod]
        public void CSV_ComplexReadWriteDouble()
        {
            Array<complex> cplx = ccomplex(rand(50, 2), rand(50, 2));
            if (File.Exists("test_cplxd.csv"))
            {
                File.Delete("test_cplxd.csv");
            }
            csvwrite<complex>(cplx, "test_cplxd.csv");
            Array<complex> readCplx = csvread<complex>(File.Open("test_cplxd.csv", FileMode.Open));
            Assert.IsTrue(allall(abs(cplx - readCplx) < 0.001));
            
        }

        [TestMethod]
        public void CSV_ComplexReadWriteCustomSeparator()
        {
            Array<complex> cplx = ccomplex(rand(50, 2), rand(50, 2));
            if (File.Exists("test_customSeparator.csv"))
            {
                File.Delete("test_customSeparator.csv");
            }
            csvwrite<complex>(cplx, "test_customSeparator.csv", elementConverter: null, elementSeparator: " aba ");
            Array<complex> readCplx = csvread<complex>(File.Open("test_customSeparator.csv", FileMode.Open), elementSeparator: " aba ");
            Assert.IsTrue(allall(abs(cplx - readCplx) < 0.001));
            
        }

        [TestMethod]
        public void CSV_ComplexReadWriteFloat()
        {
            Array<fcomplex> cplx = tofcomplex(ccomplex(rand(50, 2), rand(50, 2)));
            if (File.Exists("test_cplxf.csv"))
            {
                File.Delete("test_cplxf.csv");
            }
            csvwrite<fcomplex>(cplx, "test_cplxf.csv");
            Array<fcomplex> readCplx = csvread<fcomplex>(File.Open("test_cplxf.csv", FileMode.Open));
            Assert.IsTrue(allall(abs(cplx - readCplx) < 0.0001f));
            
        }

        [TestMethod]
        public void CSV_ComplexSpeedTest()
        {
            Array<complex> cplx = array<complex>(new complex((double)rand(1), (double)rand(1)), vector<long>(500, 500));
            if (File.Exists("test_cplxs.csv"))
            {
                File.Delete("test_cplxs.csv");
            }
            csvwrite<complex>(cplx, "test_cplxs.csv");
            Stopwatch swRead = Stopwatch.StartNew();
            Array<complex> readCplx = csvread<complex>(File.Open("test_cplxs.csv", FileMode.Open));
            swRead.Stop();
            Assert.IsTrue(allall(abs(cplx - readCplx) < 0.0001));           
        }

        [TestMethod]        
        public void CSV_ComplexRead()
        {
            File.WriteAllLines("csvread_complex.csv",
            new string[] {
                    "1, 1+i1, 1+i0, -i, 1+i",
                    "0, 0-i5, 0-i5.5, i5, 1-i",
                    "-5+i3, -5-i6, i3.4, i0, i-1"
                });

            Array<complex> cplx = csvread<complex>(File.Open("csvread_complex.csv", FileMode.Open));

            Assert.AreEqual(cplx[0, 0], new complex(1, 0));
            Assert.AreEqual(cplx[0, 1], new complex(1, 1));
            Assert.AreEqual(cplx[0, 2], new complex(1, 0));
            Assert.AreEqual(cplx[0, 3], new complex(0, -1));
            Assert.AreEqual(cplx[0, 4], new complex(1, 1));

            Assert.AreEqual(cplx[1, 0], new complex(0, 0));
            Assert.AreEqual(cplx[1, 1], new complex(0, -5));
            Assert.AreEqual(cplx[1, 2], new complex(0, -5.5));
            Assert.AreEqual(cplx[1, 3], new complex(0, 5));
            Assert.AreEqual(cplx[1, 4], new complex(1, -1));

            Assert.AreEqual(cplx[2, 0], new complex(-5, 3));
            Assert.AreEqual(cplx[2, 1], new complex(-5, -6));
            Assert.AreEqual(cplx[2, 2], new complex(0, 3.4));
            Assert.AreEqual(cplx[2, 3], new complex(0, 0));
            Assert.AreEqual(cplx[2, 4], new complex(-1, 1));

        }

        [TestMethod]
        public void CSV_Write()
        {
            if (!File.Exists(csvread_sample34x24_NAME)) {
                ILNumerics.Core.UnitTests.Helper.GetResourceFileName(csvread_sample34x24_NAME);
            }
            string file = File.ReadAllText(csvread_sample34x24_NAME);
            Array<double> A = csvread<double>(file, elementConverter: e => double.Parse(e, CultureInfo.InvariantCulture));
            csvwrite<double>(A, "test.csv", elementConverter: e => e.ToString(CultureInfo.InvariantCulture));
            Array<double> Res = csvread<double>(File.ReadAllText("test.csv"));
            Assert.IsTrue(allall(Res - A < 0.0001f));

        }

        [TestMethod]
        public void CSV_read()
        {
            if (!File.Exists(csvread_sample34x24_NAME)) {
                ILNumerics.Core.UnitTests.Helper.GetResourceFileName(csvread_sample34x24_NAME);
            }
            string file = File.ReadAllText(csvread_sample34x24_NAME);
            
            Array<double> A = csvread<double>(file);
            Array<double> Res = csvread_samples; 
            Assert.IsTrue(allall(Res - A < 0.0001));

            Array<float> Af = csvread<float>(file);
            Array<float> Resf = csvread_samples;
            Assert.IsTrue(allall(Resf - Af < 0.0001f));
        }

        [TestMethod]
        public void Call_CSV_readRows()
        {
            if (!File.Exists(csvread_sample34x24_NAME)) {
                ILNumerics.Core.UnitTests.Helper.GetResourceFileName(csvread_sample34x24_NAME);
            }
            CSV_readRows(0, 1000, 0, 1000);
            CSV_readRows(0, 10, 0, 10);
            CSV_readRows(10, 1000, 10, 1000);
            CSV_readRows(10, 12, 10, 12);
        }

        public void CSV_readRows(int r0, int r1, int c0, int c1)
        {
            string file;
            file = File.ReadAllText(csvread_sample34x24_NAME);
            Array<double> A = csvread<double>(file, r0, c0, r1, c1);
            Array<double> Res = csvread_samples;
            r1 = Math.Min(r1, 33); c1 = Math.Min(c1, 23);
            Array<double> temp = Res[r(r0, r1), r(c0, c1)];
            Assert.IsTrue(allall(temp - A < 0.0001));
        }




        static double[,] csvread_samples = {
{ 0.010267161,0.970313952,0.940031175,0.907032226,0.957721846,0.256358883,0.020518365,0.526172762,0.300876874,0.952602009,0.965862174,0.903529971,0.116227586,0.542858168,0.604709111,0.065675449,0.72970054    ,0.226332862,0.921134412,0.376518036,0.309775934,0.887838089,0.960422961,0.310615689},
{ 0.508339447,0.079437921,0.787085014,0.809895476,0.297977293,0.038882299,0.080475236,0.73464161    ,0.648083335,0.582214756,0.938707292,0.453182038,0.813851098,0.351120804,0.556411644,0.25000536 ,0.91028937 ,0.075674056,0.604242005,0.246501821,0.36529758 ,0.124615709,0.01570136 ,0.125641924},
{ 0.232713266,0.54805622    ,0.054164496,0.53342505 ,0.352611775,0.977835671,0.113159694,0.347179251,0.049214259,0.702603831,0.144239219,0.419577908,0.733000668,0.706648083,0.120375855,0.811944274,0.085620569,0.839888086,0.099046644,0.336514872,0.181641789,0.897815776,0.045960435,0.635379866},
{ 0.47927578    ,0.31309566 ,0.333589642,0.138148782,0.950025063,0.09428729 ,0.592109416,0.647844308,0.702388371,0.827224248,0.834687468,0.878816726,0.930516302,0.049280964,0.787006425,0.459543638,0.537606137,0.455305752,0.940980313,0.828896136,0.676875451,0.478275956,0.611704999,0.502988429},
{ 0.872586689,0.512072145,0.633267719,0.22235226    ,0.889211945,0.684328732,0.510473072,0.736153965,0.28518644 ,0.900931578,0.951122935,0.577158687,0.19232041 ,0.790650813,0.349972666,0.836574172,0.738020173,0.645048863,0.504771131,0.181985566,0.040864474,0.083328219,0.80973392 ,0.139196027},
{ 0.769075026,0.964904036,0.725609591,0.056231023,0.989093061,0.409902112,0.866555782,0.713627387,0.826992934,0.498951223,0.490252028,0.287663301,0.724339228,0.495176982,0.590494398,0.793028029,0.049403748,0.155055358,0.084797388,0.374119271,0.427663742,0.788161825,0.532649295,0.607942005},
{ 0.019536853,0.497889604,0.960565868,0.6588295 ,0.620617203,0.303529489,0.499079033,0.471692987,0.627645857,0.053623809,0.938377984,0.108110024,0.936198991,0.353320318,0.873049884,0.445619121,0.059094789,0.792776006,0.603629059,0.494309787,0.291880906,0.378680068,0.063318904,0.952602645},
{ 0.716096328,0.445027414,0.081026451,0.072324889,0.799100915,0.79587099    ,0.419604424,0.332901535,0.940403905,0.085932075,0.401987611,0.806830138,0.457074608,0.385450723,0.76493327 ,0.567661997,0.702304869,0.042159968,0.116612833,0.257754024,0.041856139,0.944308837,0.688526957,0.352489049},
{ 0.560314933,0.132566331,0.376567465,0.766755876,0.879219301,0.837133604,0.67380281    ,0.432269018,0.290120443,0.458332919,0.336717024,0.737165498,0.991763947,0.746548969,0.68170733 ,0.795991891,0.260515842,0.775835659,0.080283672,0.900832188,0.832234676,0.565541821,0.167994884,0.766256792},
{ 0.455491196,0.223143009,0.635105712,0.298714856,0.51727208    ,0.665323856,0.233854527,0.829721582,0.345524963,0.562050923,0.699439468,0.871442931,0.929991984,0.47252051 ,0.704999452,0.55088069 ,0.920597162,0.456759543,0.729618785,0.25739377 ,0.030520723,0.115950671,0.86257437 ,0.967817424},
{ 0.35635967    ,0.991973346,0.108019933,0.235317324,0.571449382,0.286989971,0.248095457,0.73441535 ,0.838203455,0.379755313,0.326786655,0.618960811,0.316886282,0.762030806,0.077606844,0.71603934 ,0.765342758,0.027054024,0.077637901,0.496138181,0.656356926,0.761719833,0.752965349,0.234607906},
{ 0.526673614,0.29787619    ,0.382415609,0.440610956,0.965861891,0.049924874,0.697279438,0.844661864,0.724452361,0.650143251,0.658645791,0.644359267,0.664099187,0.639954586,0.30304041 ,0.894729252,0.065607895,0.444259822,0.077122626,0.79868841 ,0.199511415,0.520796693,0.664619967,0.813898932},
{ 0.286176927,0.443138384,0.851642359,0.691053594,0.277896048,0.276627912,0.203648005,0.897492809,0.5127765 ,0.933144086,0.74240432 ,0.722016261,0.015187116,0.911278416,0.76059356 ,0.284993807,0.885315298,0.448138537,0.342303906,0.972717169,0.941479367,0.58308868 ,0.213526252,0.764565157},
{ 0.735667324,0.241359887,0.39258054    ,0.125080484,0.794002692,0.845305415,0.623865707,0.731771055,0.73944729 ,0.660333787,0.282372452,0.555927114,0.906630125,0.375876176,0.865382174,0.167598381,0.580322745,0.450507118,0.866333144,0.873228266,0.560786574,0.823911485,0.536935113,0.810096934},
{ 0.99564952    ,0.806576375,0.650477395,0.523450371,0.713277273,0.581416026,0.046840939,0.206773828,0.087764817,0.169359161,0.290060646,0.679880386,0.827829567,0.91961877 ,0.593040367,0.807606244,0.565685477,0.277516732,0.54495365 ,0.767052201,0.840627432,0.407617988,0.243933002,0.241877077},
{ 0.947085785,0.872714131,0.380015488,0.821074023,0.335145708,0.756567277,0.800996975,0.796416547,0.694303037,0.197077254,0.493553384,0.589870953,0.256199678,0.821919828,0.222474995,0.88619293    ,0.696870851,0.625013364,0.327740285,0.768132804,0.368708616,0.504422362,0.8226498  ,0.803656839},
{ 0.005933282,0.564247887,0.689366176,0.448008099,0.839364631,0.968726209,0.634458293,0.203131106,0.705638507,0.825782722,0.742781572,0.77216774    ,0.22527729 ,0.945389048,0.888727004,0.666582362,0.474177233,0.0052353  ,0.676407275,0.975499062,0.843888096,0.085936175,0.432416488,0.272792955},
{ 0.364643516,0.126214811,0.682725859,0.141401602,0.085457447,0.372318923,0.594558968,0.923641477,0.270942389,0.286884836,0.614011522,0.048920977,0.675217757,0.855479503,0.059407239,0.159735411,0.178213522,0.024414734,0.321794061,0.634164634,0.482947028,0.037130348,0.585610506,0.520616615},
{ 0.28796733    ,0.692044452,0.667724427,0.166824392,0.488793323,0.47370543 ,0.916888916,0.474354461,0.888809056,0.236303166,0.94581037 ,0.933981244,0.380360501,0.89941318 ,0.702005964,0.999685037,0.88884764 ,0.736497337,0.416489956,0.372397876,0.994014178,0.046045609,0.495779412,0.778910131},
{ 0.709362222,0.897025914,0.862624042,0.352811343,0.879508032,0.490274836,0.965583088,0.261018731,0.701108695,0.476566655,0.816381848,0.885318186,0.464841726,0.905275931,0.048237966,0.74858593    ,0.364665428,0.854316392,0.672644575,0.433917904,0.453320801,0.273550634,0.808478266,0.333450528},
{ 0.979582218,0.560132931,0.019273727,0.197399721,0.355276127,0.082083458,0.932006857,0.588066108,0.70757526    ,0.314539102,0.026355438,0.186188558,0.748055822,0.874473457,0.398335741,0.400140654,0.976481912,0.451025538,0.766495058,0.651494377,0.298063917,0.278903965,0.179541177,0.046283345},
{ 0.391340685,0.12599302    ,0.369097497,0.293076541,0.610944122,0.830886147,0.716286603,0.941727295,0.493264025,0.017783806,0.998181731,0.896478342,0.595768334,0.139974683,0.054439616,0.871338541,0.860387724,0.028796377,0.793108043,0.163403231,0.480121313,0.052265891,0.628018766,0.736569297},
{ 0.409064984,0.290825179,0.380602737,0.281675099,0.463200084,0.61400654    ,0.136385056,0.871106221,0.979979888,0.299973849,0.174662051,0.447561346,0.40862164 ,0.694866272,0.428463269,0.041678833,0.460598022,0.38230234 ,0.909577823,0.979878634,0.820673564,0.571752282,0.725919333,0.058239899},
{ 0.202242072,0.158503513,0.234689481,0.462277702,0.09112263    ,0.441687176,0.900023786,0.51867576 ,0.769232218,0.861491293,0.002275569,0.30208821 ,0.938980148,0.562831289,0.062419895,0.660356622,0.652872881,0.862005084,0.814212244,0.307718927,0.001999743,0.012845332,0.686653346,0.580572112},
{ 0.104464732,0.273340143,0.493849448,0.088423489,0.38732692    ,0.383628253,0.33636178 ,0.986384757,0.366589763,0.414494233,0.577525595,0.410675536,0.731266438,0.23847656 ,0.818858185,0.365043578,0.662187788,0.763622842,0.671748598,0.977012404,0.199882736,0.724812569,0.590291909,0.82475157 },
{ 0.500009574,0.993660143,0.040577151,0.782792586,0.046948199,0.124499417,0.646225427,0.193372862,0.077466898,0.843693972,0.224136005,0.504672543,0.62787485    ,0.935082844,0.494381217,0.977067454,0.416569157,0.077802686,0.506368601,0.707298491,0.723598947,0.962424803,0.133243171,0.741930675},
{ 0.491770226,0.409457178,0.227883062,0.987210656,0.805921308,0.566618314,0.160267516,0.408602257,0.150020971,0.517299243,0.683022474,0.971083048,0.404032941,0.00371253    ,0.793247275,0.426693144,0.941779201,0.548019897,0.687107391,0.486654044,0.210588361,0.560931455,0.946712001,0.664836504},
{ 0.613545136,0.471867068,0.305567531,0.410370331,0.783826498,0.964021031,0.999530415,0.006179049,0.919603278,0.69188378    ,0.561097901,0.744913054,0.727697804,0.220671161,0.348359141,0.933167973,0.843983186,0.317352218,0.375663439,0.086394906,0.567343422,0.202268991,0.942357013,0.938530836},
{ 0.245106193,0.760609306,0.585093994,0.331303014,0.677532282,0.304597163,0.591076095,0.0250236 ,0.372824012,0.140288229,0.073899532,0.893435355,0.004943202,0.223057103,0.045514576,0.969062346,0.761954673,0.546273253,0.097239786,0.599331508,0.947734333,0.472430607,0.773419703,0.168136591},
{ 0.465238958,0.841603667,0.092614327,0.965926762,0.363792827,0.747584492,0.539028868,0.968367125,0.677786174,0.822791834,0.825561229,0.063191521,0.754635535,0.405917611,0.617361931,0.850198886,0.674677942,0.816730452,0.005812762,0.613573523,0.39099229    ,0.343209899,0.081876153,0.05159267 },
{ 0.186471111,0.839858915,0.138529194,0.744319747,0.556107402,0.609682827,0.319484767,0.343619105,0.07278313    ,0.526232577,0.425095557,0.577647149,0.408190278,0.577160813,0.814781379,0.03642996 ,0.210297304,0.55923234 ,0.242820979,0.715251521,0.105793561,0.300921829,0.881007152,0.39081059 },
{ 0.915384373,0.160202176,0.410323049,0.236662121,0.141024582,0.811512575,0.011666293,0.901682954,0.506704924,0.298446963,0.023194044,0.348634723,0.029870559,0.148966679,0.388517875,0.892781073,0.580456236,0.166055576,0.744542357,0.843497203,0.982820982,0.886891332,0.826296386,0.474525105},
{ 0.308160831,0.632619052,0.191039694,0.678685379,0.791693761,0.307633486,0.262567658,0.134479485,0.278316963,0.56647832    ,0.82223302 ,0.882315996,0.725497641,0.770545967,0.037344451,0.020677243,0.243832787,0.735809962,0.219752983,0.013002394,0.750044574,0.820659294,0.741035608,0.009958464},
{ 0.148045577,0.059642832,0.954997862,0.531520948,0.071632479,0.103280214,0.153726616,0.804053001,0.186377687,0.746578213,0.527213918,0.429568226,0.634460965,0.398868906,0.691512568,0.705856096,0.825732364,0.30230278    ,0.220446941,0.653608496,0.960219653,0.863164282,0.095455797,0.108408929},
//{ 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };







    }
}
