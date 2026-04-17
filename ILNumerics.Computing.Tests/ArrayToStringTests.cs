using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using static ILNumerics.ILMath;
using static ILNumerics.Globals;
using ILNumerics;
//ILN(enabled=false)

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public unsafe class ArrayToStringTests {
        //ILN(enabled=false)
        [TestInitialize]
        public void TestInit() {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us"); 
        }


        [TestMethod]
        public void ToStringSimpleTest() {

            Array<double> A = new double[] { 102039.3324551, -2, 3, 4.000005522 };
            A = A.T; 
            string s = A.ToString();
            var res = "<Double> [1,4] 1e+005 * \r\n    1.020393   -0.000020    0.000030    0.000040"; 
            Assert.IsTrue(s == res, $"Expected: {res}. Found: {s}"); 
        }

        [TestMethod]
        public void ToStringLargeDoubleTest() {

            Array<double> A = counter<double>(0, 1, 10, 11, StorageOrders.RowMajor).T;
 
            string s = A.ToString();
            string res = "<Double> [11,10]\r\n           0          11          22          33          44          55          66          77          88          99\r\n           1          12          23          34          45          56          67          78          89         100\r\n           2          13          24          35          46          57          68          79          90         101\r\n           3          14          25          36          47          58          69          80          91         102\r\n           4          15          26          37          48          59          70          81          92         103\r\n           5          16          27          38          49          60          71          82          93         104\r\n           6          17          28          39          50          61          72          83          94         105\r\n           7          18          29          40          51          62          73          84          95         106\r\n           8          19          30          41          52          63          74          85          96         107\r\n           9          20          31          42          53          64          75          86          97         108\r\n          10          21          32          43          54          65          76          87          98         109"; 
            Assert.IsTrue(s == res);
        }

        [TestMethod]
        public void ToString3DimDoubleTest() {

            Array<double> A = counter<double>(0, 1, 10, 80, StorageOrders.ColumnMajor);

            var bsd = A.S.GetBSD(true);
            bsd[0] = (3);
            bsd[0] = (3);
            bsd[4] = (40);
            bsd[5] = (2);
            bsd[6] = (80);
            bsd[7] = (2);
            bsd[8] = (1);
            using (Settings.Ensure(() => Settings.ToStringMaxNumberElementsPerDimension, 101u)) 
            using (Settings.Ensure(() => Settings.ToStringMaxNumberElements, 10000u)) {
                var s = A.ToString();
                Assert.IsTrue(s.StartsWith(@"<Double> [10,40,2]
(:,:,0)
           0           2           4           6           8          10          12          14          16          18          20          22          24          26          28          30          32          34          36          38          40          42          44          46          48          50          52          54          56          58          60          62          64          66          68          70          72          74          76          78
          80          82          84          86          88          90          92          94          96          98         100         102         104         106         108         110         112         114         116         118         120         122         124         126         128         130         132         134         136         138         140         142         144         146         148         150         152         154         156         158
         160         162         164         166         168         170         172         174         176         178         180         182         184         186         188         190         192         194         196         198         200         202         204         206         208         210         212         214         216         218         220         222         224         226         228         230         232         234         236         238
         240         242         244         246         248         250         252         254         256         258         260         262         264         266         268         270         272         274         276         278         280         282         284         286         288         290         292         294         296         298         300         302         304         306         308         310         312         314         316         318
         320         322         324         326         328         330         332         334         336         338         340         342         344         346         348         350         352         354         356         358         360         362         364         366         368         370         372         374         376         378         380         382         384         386         388         390         392         394         396         398
         400         402         404         406         408         410         412         414         416         418         420         422         424         426         428         430         432         434         436         438         440         442         444         446         448         450         452         454         456         458         460         462         464         466         468         470         472         474         476         478
         480         482         484         486         488         490         492         494         496         498         500         502         504         506         508         510         512         514         516         518         520         522         524         526         528         530         532         534         536         538         540         542         544         546         548         550         552         554         556         558
         560         562         564         566         568         570         572         574         576         578         580         582         584         586         588         590         592         594         596         598         600         602         604         606         608         610         612         614         616         618         620         622         624         626         628         630         632         634         636         638
         640         642         644         646         648         650         652         654         656         658         660         662         664         666         668         670         672         674         676         678         680         682         684         686         688         690         692         694         696         698         700         702         704         706         708         710         712         714         716         718
         720         722         724         726         728         730         732         734         736         738         740         742         744         746         748         750         752         754         756         758         760         762         764         766         768         770         772         774         776         778         780         782         784         786         788         790         792         794         796         798
(:,:,1)
           1           3           5           7           9          11          13          15          17          19          21          23          25          27          29          31          33          35          37          39          41          43          45          47          49          51          53          55          57          59          61          63          65          67          69          71          73          75          77          79
          81          83          85          87          89          91          93          95          97          99         101         103         105         107         109         111         113         115         117         119         121         123         125         127         129         131         133         135         137         139         141         143         145         147         149         151         153         155         157         159
         161         163         165         167         169         171         173         175         177         179         181         183         185         187         189         191         193         195         197         199         201         203         205         207         209         211         213         215         217         219         221         223         225         227         229         231         233         235         237         239
         241         243         245         247         249         251         253         255         257         259         261         263         265         267         269         271         273         275         277         279         281         283         285         287         289         291         293         295         297         299         301         303         305         307         309         311         313         315         317         319
         321         323         325         327         329         331         333         335         337         339         341         343         345         347         349         351         353         355         357         359         361         363         365         367         369         371         373         375         377         379         381         383         385         387         389         391         393         395         397         399
         401         403         405         407         409         411         413         415         417         419         421         423         425         427         429         431         433         435         437         439         441         443         445         447         449         451         453         455         457         459         461         463         465         467         469         471         473         475         477         479
         481         483         485         487         489         491         493         495         497         499         501         503         505         507         509         511         513         515         517         519         521         523         525         527         529         531         533         535         537         539         541         543         545         547         549         551         553         555         557         559
         561         563         565         567         569         571         573         575         577         579         581         583         585         587         589         591         593         595         597         599         601         603         605         607         609         611         613         615         617         619         621         623         625         627         629         631         633         635         637         639
         641         643         645         647         649         651         653         655         657         659         661         663         665         667         669         671         673         675         677         679         681         683         685         687         689         691         693         695         697         699         701         703         705         707         709         711         713         715         717         719
         721         723         725         727         729         731         733         735         737         739         741         743         745         747         749         751         753         755         757         759         761         763         765         767         769         771         773         775         777         779         781         783         785         787         789         791         793         795         797         799"));

            }
        }

        [TestMethod]
        public void ToStringLargeDoubleLimitElementsTest() {

            Array<double> A = counter<double>(0, 1, 10, 11, StorageOrders.ColumnMajor);
 

            string s = A.ToString(7, 100);
            var rows = s.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.IsTrue(rows != null && rows.Length == 9);
            Assert.IsTrue(s.Contains("<Double> [10,11]")); 
            
            foreach (var row in rows) {
                if (row.Contains("Double")) continue; 
                //if (row.Contains())
                Assert.IsTrue(row.Contains("..."));
                Assert.IsTrue(row.EndsWith("...") || row.Length == 96); 
            }
        }
        [TestMethod]
        public void ToStringEmptyTest() {
            Array<double> A = new double[] { };

            string s = A.ToString();
            Assert.IsTrue(s == "<Double> [0,1] [empty]");

            s = A.ToString(2, 2000, showType: false, showSize: false);
            Assert.IsTrue(s == "[empty]"); // no space before [empty]!

            s = A.ToString(2, 2000, showType: false, showSize: true);
            Assert.IsTrue(s == "[0,1] [empty]"); // no space before [0 but space before [empty]!

            s = A.ToString(2, 2000, showType: true, showSize: false);
            Assert.IsTrue(s == "<Double> [empty]"); // no space at end!

        }

        [TestMethod]
        public void ToStringScalarTest() {
            Array<double> A;
            string s;
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                A = new double[] { 300000000000.33300000000011 };

                s = A.ToString();
                var exptd = "<Double> [1] 1e+011 * \r\n    3.000000";
                Assert.IsTrue(s == exptd, $"Expected: {exptd}. found: {s}");

                A = 300000000000.33300000000011;
                s = A.ToString();
                Assert.IsTrue(s == "<Double> [] 300000000000.333");

                using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {
                    A = new double[] { 300000000000.33300000000011 };

                    s = A.ToString();
                    Assert.IsTrue(s == "<Double> [1,1] 1e+011 * \r\n    3.000000");

                    s = A.ShortInfo();
                    Assert.IsTrue(s.StartsWith("<Double> [1,1] 300000000000.333")); // no space at end!

                    A = new double[] { -0.000000000033300000000011 };

                    s = A.ToString();
                    Assert.IsTrue(s == "<Double> [1,1] 1e-011 * \r\n   -3.330000");

                    s = A.ShortInfo();
                    Assert.IsTrue(s.StartsWith("<Double> [1,1] -3.3300000000011E-11")); // no space at end!
                }
            }
        }

        [TestMethod]
        public void ArrayToStringSpecialValuesTest() {

            Array<double> A = new[] { 1.0, -0.0000000003, double.NaN, 400000000000.0000000002, double.NegativeInfinity, double.PositiveInfinity };
            var s = A.ToString();
            var exptd = "<Double> [6,1] 1e+011 * \r\n    0.000000\r\n   -0.000000\r\n         NaN\r\n    4.000000\r\n          -∞\r\n           ∞";
            Assert.IsTrue(s == exptd, $"ToString gave: '{s}' Expected: '{exptd}'");

        }

        [TestMethod]
        public void ArrayToStringUIntSimpleTest() {

            Array<uint> A = new uint[,] {
                { 1, -0, 10, 4000000, uint.MaxValue, uint.MinValue },
                { 10, 4000000, uint.MaxValue, uint.MinValue, 1, -0 },
                { 0, 10, 4000000, uint.MaxValue, uint.MinValue, 99 }
            }; 

            var s = A.ToString();
            Assert.IsTrue(s == "<UInt32> [3,6]\r\n           1           0          10     4000000  4294967295           0\r\n          10     4000000  4294967295           0           1           0\r\n           0          10     4000000  4294967295           0          99", $"ToString gave: '{s}'");

            A = new uint[] { };
            Assert.IsTrue(A.ToString() == "<UInt32> [0,1] [empty]", "Empty array string failed");

            A = new uint[] { uint.MaxValue };
            Assert.IsTrue(A.ToString() == "<UInt32> [1,1]\r\n  4294967295", "Scalar array string failed");

            using (Scope.Enter(ArrayStyles.numpy)) {
                A = uint.MaxValue;
                Assert.IsTrue(A.IsScalar && A.S.NumberOfDimensions == 0 && A.S.NumberOfElements == 1);
                Assert.IsTrue(A.ToString() == "<UInt32> [] 4294967295", "Scalar array sbyte failed");

                A = uint.MinValue;
                Assert.IsTrue(A.ToString() == "<UInt32> [] 0", "Scalar array sbyte failed");

            }

        }
        [TestMethod]
        public void ArrayToStringIntSimpleTest() {

            Array<int> A = new int[,] {
                { 1, -0, 10, 4000000, int.MaxValue, int.MinValue },
                { 10, 4000000, int.MaxValue, int.MinValue, 1, -0 },
                { 0, 10, 4000000, int.MaxValue, int.MinValue, 99 }
            };

            var s = A.ToString();
            Assert.IsTrue(s == "<Int32> [3,6]\r\n           1           0          10     4000000  2147483647 -2147483648\r\n          10     4000000  2147483647 -2147483648           1           0\r\n           0          10     4000000  2147483647 -2147483648          99", $"ToString gave: '{s}'");

            A = new int[] { };
            Assert.IsTrue(A.ToString() == "<Int32> [0,1] [empty]", $"Empty array string failed. A.ToString():{A.ToString()}");

            A = new int[] { int.MaxValue };
            Assert.IsTrue(A.ToString() == "<Int32> [1,1]\r\n  2147483647", "Scalar array string failed");
            A = new int[] { int.MinValue };
            Assert.IsTrue(A.ToString() == "<Int32> [1,1]\r\n -2147483648", "Scalar array string failed");

        }

        [TestMethod]
        public void ArrayToStringSbyteSimpleTest() {

            Array<sbyte> A = new sbyte[,] {
                { 1, -0, 10, 127, sbyte.MaxValue, sbyte.MinValue },
                { 10, -128, sbyte.MaxValue, sbyte.MinValue, 1, -0 },
                { 0, 10, 40, sbyte.MaxValue, sbyte.MinValue, 99 }
            };

            var s = A.ToString();
            Assert.IsTrue(s == "<SByte> [3,6]\r\n           1           0          10         127         127        -128\r\n          10        -128         127        -128           1           0\r\n           0          10          40         127        -128          99", $"ToString gave: '{s}'");

            A = new sbyte[] { };
            Assert.IsTrue(A.ToString() == "<SByte> [0,1] [empty]", $"Empty array sbyte failed. A.ToString():{A.ToString()}");

            A = new sbyte[] { sbyte.MaxValue };
            Assert.IsTrue(A.ToString() == "<SByte> [1,1]\r\n         127", "Scalar array sbyte failed");
            A = new sbyte[] { sbyte.MinValue };
            Assert.IsTrue(A.ToString() == "<SByte> [1,1]\r\n        -128", "Scalar array sbyte failed");

            using (Scope.Enter(ArrayStyles.numpy)) {
                A = sbyte.MaxValue;
                Assert.IsTrue(A.IsScalar && A.S.NumberOfDimensions == 0 && A.S.NumberOfElements == 1);
                Assert.IsTrue(A.ToString() == "<SByte> [] 127", "Scalar array sbyte failed");

                A = sbyte.MinValue;
                Assert.IsTrue(A.ToString() == "<SByte> [] -128", "Scalar array sbyte failed");

            }

        }
        [TestMethod]
        public void ArrayToStringBoolSimpleTest() {

            Array<bool> A = new bool[,] {
                { true, true, false, true, false, false, true },
                { true, true, false, true, false, false, true },
                { true, true, false, true, false, false, true }
            };

            var s = A.ToString();
            Assert.IsTrue(s == "<Boolean> [3,7]\r\n ▮ ▮ ▯ ▮ ▯ ▯ ▮\r\n ▮ ▮ ▯ ▮ ▯ ▯ ▮\r\n ▮ ▮ ▯ ▮ ▯ ▯ ▮", $"ToString gave: '{s}'");

            A = new bool[] { };
            Assert.IsTrue(A.ToString() == "<Boolean> [0,1] [empty]", $"Empty array string failed. A.ToString:{A.ToString()}");

            A = new bool[] { true };
            Assert.IsTrue(A.ToString() == "<Boolean> [1,1]\r\n ▮", "Scalar array string failed");
            A = new bool[] { false };
            Assert.IsTrue(A.ToString() == "<Boolean> [1,1]\r\n ▯", "Scalar array string failed");

            using (Scope.Enter(ArrayStyles.numpy)) {
                A = true;
                Assert.IsTrue(A.IsScalar && A.S.NumberOfDimensions == 0 && A.S.NumberOfElements == 1);
                Assert.IsTrue(A.ToString() == "<Boolean> [] True", "Scalar array string failed");

                A = false;
                Assert.IsTrue(A.ToString() == "<Boolean> [] False", "Scalar array string failed");

            }

        }

        [TestMethod]
        public void ArrayToStringComplexSimpleTest() {

            Array<complex> A = new complex[,] {
                { new complex(1,1),  new complex(-1,1),  new complex(1,-1),  new complex(-1,-1),  new complex(-0.0000001,8768888888888) },
                { new complex(999999999999.999999999992,-999999999999.999999999992),  new complex(0.00000000092,-999999999999.0),
                        new complex(-1.0,double.NaN),  new complex(double.NegativeInfinity,double.NaN),  new complex(-0.0000000004,-0.000000000005) },
                { new complex(double.PositiveInfinity,1000000000000),  new complex(-0,0),  new complex(double.NaN,-.000000001),  new complex(1,1),  new complex(1,1) }
            };

            Assert.IsTrue(A.ShortInfo().StartsWith("<complex> [3,5] 1+i...1+i "), $"Expected: '<complex> [3,5] 1+i...1+i'. Found: '{A.ShortInfo()}'.");
            var s = A.ToString();
            var res = "<complex> [3,5] 1e+012 * \r\n  0.00000+i          -0.00000+i           0.00000-i          -0.00000-i          -0.00000+i8.76889  \r\n  1.00000-i1.00000    0.00000-i1.00000   -0.00000+ NaN             -∞+ NaN       -0.00000-i0.00000  \r\n        ∞+i1.00000          0+i0              NaN-i0.00000    0.00000+i           0.00000+i         "; 
            Assert.IsTrue(s == res, $"Expected: '{res}'. Found: '{s}'.");

        }
        [TestMethod]
        public void ArrayToStringFComplexSimpleTest() {

            Array<fcomplex> A = new fcomplex[,] {
                { new fcomplex(1,1),  new fcomplex(-1,1),  new fcomplex(1,-1),  new fcomplex(-1,-1),  new fcomplex(-0.0000001f,1000000) },
                { new fcomplex(999999999999.999999999992f,-999999999999.999999999992f),  new fcomplex(0.00000000092f,-999999999999.0f),  new fcomplex(-1.0f,float.NaN),  new fcomplex(float.NegativeInfinity,float.NaN),  new fcomplex(-0.0000000004f,-0.000000000005f) },
                { new fcomplex(float.PositiveInfinity,1000000000000),  new fcomplex(-0,0),  new fcomplex(float.NaN,-.000000001f),  new fcomplex(1,1),  new fcomplex(1,1) }
            };

            Assert.IsTrue(A.ShortInfo().StartsWith("<fcomplex> [3,5] 1+i...1+i"));
            var s = A.ToString();
            var res = "<fcomplex> [3,5] 1e+011 * \r\n  0.00000+i          -0.00000+i           0.00000-i          -0.00000-i          -0.00000+i0.00001  \r\n 10.00000-i10.00000   0.00000-i10.00000  -0.00000+ NaN             -∞+ NaN       -0.00000-i0.00000  \r\n        ∞+i10.00000         0+i0              NaN-i0.00000    0.00000+i           0.00000+i         ";
            Assert.IsTrue(s == res, s);

        }

        [TestMethod]
        public void ArrayToStringBaseOffsetTest() {

            Array<int> A = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var s = A.ToString();
            Assert.IsTrue(s == "<Int32> [10,1]\r\n           0\r\n           1\r\n           2\r\n           3\r\n           4\r\n           5\r\n           6\r\n           7\r\n           8\r\n           9",
                $@"{nameof(Settings.MinNumberOfArrayDimensions)}:{Settings.MinNumberOfArrayDimensions}
                   {nameof(Settings.DefaultStorageOrder)}:{Settings.DefaultStorageOrder}");

            var bsd = A.S.GetBSD(true);
            bsd[0] = (1);
            bsd[1] = (8);
            bsd[2] = (1);
            bsd[3] = (8);
            bsd[4] = (1);
            bsd[5] = (1);
            bsd[6] = (10);

            // is a non-0 base offset considered properly ? 
            s = A.ShortInfo();
            Assert.IsTrue(s.StartsWith("<Int32> [8] 1...8"));

            s = A.ToString();
            Assert.IsTrue(s == "<Int32> [8]\r\n           1\r\n           2\r\n           3\r\n           4\r\n           5\r\n           6\r\n           7\r\n           8");

        }

        [TestMethod]
        public void ArrayToStringSmallAndLargeTest() {

            var spec = new[] { float.NaN, float.PositiveInfinity, float.NegativeInfinity, 0 }; 
            var a = new float[100];
            a[0] = 100000; 
            for (int i = 1; i < a.Length; i++) {
                a[i] = -a[i - 1] * 0.75f; 
            }
            for (int i = 0; i < a.Length / 7; i++) {
                a[i * 7] = spec[i % spec.Length]; 
            }
            Array<float> A = a; 

            var s = A.ToString();

            var bsd = A.S.GetBSD(true);
            bsd[0] = (2);
            bsd[1] = (100);
            bsd[2] = (0);
            bsd[3] = (10);
            bsd[4] = (10);
            bsd[5] = (1);
            bsd[6] = (10);

            // is a non-0 base offset considered properly ? 
            s = A.ShortInfo();
            Assert.IsTrue(s.StartsWith("<Single> [10,10] NaN...-4.27627E-08 | Dev:0"), s);

            s = A.ToString();
            var res = "<Single> [10,10] 1e+004 * \r\n"
                + "         NaN    0.563135    0.031712    0.001786    0.000101    0.000006    0.000000          -∞    0.000000    0.000000" + Environment.NewLine
                + "   -7.500000   -0.422351           0   -0.001339   -0.000075   -0.000004   -0.000000   -0.000000   -0.000000           ∞" + Environment.NewLine
                + "    5.625000    0.316763    0.017838    0.001005          -∞    0.000003    0.000000    0.000000    0.000000    0.000000" + Environment.NewLine
                + "   -4.218750   -0.237573   -0.013379   -0.000753   -0.000042   -0.000002           ∞   -0.000000   -0.000000   -0.000000" + Environment.NewLine
                + "    3.164063          -∞    0.010034    0.000565    0.000032    0.000002    0.000000    0.000000         NaN    0.000000" + Environment.NewLine
                + "   -2.373047   -0.133635   -0.007525           ∞   -0.000024   -0.000001   -0.000000   -0.000000   -0.000000   -0.000000" + Environment.NewLine
                + "    1.779785    0.100226    0.005644    0.000318    0.000018         NaN    0.000000    0.000000    0.000000    0.000000" + Environment.NewLine
                + "           ∞   -0.075169   -0.004233   -0.000238   -0.000013   -0.000001   -0.000000           0   -0.000000   -0.000000" + Environment.NewLine
                + "    1.001129    0.056377         NaN    0.000179    0.000010    0.000001    0.000000    0.000000    0.000000    0.000000" + Environment.NewLine
                + "   -0.750847   -0.042283   -0.002381   -0.000134           0   -0.000000   -0.000000   -0.000000   -0.000000   -0.000000";
        // NOTE: net461 gives res, netcore 3.1 gives res2. Difference is the rounding at element [4,0]:
        // Element at [4,0]: 31640.625
        // Difference: 527->'3|2'
        // It looks ok in both cases, though! So we accept both floating point representations to be considered 'correct'! 
            var res2 = "<Single> [10,10] 1e+004 * \r\n"
                + "         NaN    0.563135    0.031712    0.001786    0.000101    0.000006    0.000000          -∞    0.000000    0.000000" + Environment.NewLine
                + "   -7.500000   -0.422351           0   -0.001339   -0.000075   -0.000004   -0.000000   -0.000000   -0.000000           ∞" + Environment.NewLine
                + "    5.625000    0.316763    0.017838    0.001005          -∞    0.000003    0.000000    0.000000    0.000000    0.000000" + Environment.NewLine
                + "   -4.218750   -0.237573   -0.013379   -0.000753   -0.000042   -0.000002           ∞   -0.000000   -0.000000   -0.000000" + Environment.NewLine
                + "    3.164062          -∞    0.010034    0.000565    0.000032    0.000002    0.000000    0.000000         NaN    0.000000" + Environment.NewLine
                + "   -2.373047   -0.133635   -0.007525           ∞   -0.000024   -0.000001   -0.000000   -0.000000   -0.000000   -0.000000" + Environment.NewLine
                + "    1.779785    0.100226    0.005644    0.000318    0.000018         NaN    0.000000    0.000000    0.000000    0.000000" + Environment.NewLine
                + "           ∞   -0.075169   -0.004233   -0.000238   -0.000013   -0.000001   -0.000000           0   -0.000000   -0.000000" + Environment.NewLine
                + "    1.001129    0.056377         NaN    0.000179    0.000010    0.000001    0.000000    0.000000    0.000000    0.000000" + Environment.NewLine
                + "   -0.750847   -0.042283   -0.002381   -0.000134           0   -0.000000   -0.000000   -0.000000   -0.000000   -0.000000";

            Assert.IsTrue(s == res || s == res2, $"Invalid A.ToString() output: '{s}'.");

            ////for (int i = 0; i < s.Length; i++) {
            ////    if (s[i] != res[i]) {
            ////        Debugger.Break();
            ////    }
            ////}
            //Trace.WriteLine($"Element at [4,0]: {A.GetValue(4)}"); 
            //Trace.WriteLine($"Rounded by 'a.': {A.GetValue(4)}");
            //if (s != res) {
            //    if (s.Length != res.Length) {
            //        Trace.WriteLine($"A.ToString().Length = {s.Length} | expected length:{res.Length}."); 
            //    }
            //    for (int i = 0; i < res.Length; i++) {
            //        if (res[i]!= s[i]) {
            //            Trace.WriteLine($"Difference: {i}->'{res[i]}|{s[i]}'"); 
            //        }
            //    }
            //}
        }

    }
}
