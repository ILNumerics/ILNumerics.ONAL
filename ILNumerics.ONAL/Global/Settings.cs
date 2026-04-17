using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace ILNumerics {

    /// <summary>
    /// The class provides static setting properties to control the behaviour of ILNumerics, see <a href="http://ilnumerics.net/$Configuration.html">Configuration</a> in the online documentation
    /// </summary>
    
    public sealed class Settings {

        [DllImport("mkl_custom", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern int mkl_domain_get_max_threads(ref int mask);

        private static object s_lock = new object();

        
        internal static uint GetMaxThreads()
        {
            // !MKL_BLAS is in MKLValues class!
            int MKL_BLAS = 1;
            int dummy = MKL_BLAS;
            int ret = mkl_domain_get_max_threads(ref dummy);
            return (uint)ret;

        }

        static Settings() {
            // Load the static cache values from the Default instance
            // This enables the user to change default values via app.config 
            // and still provides a fast setting provider for internal functions.
            LoadDefaults();
        }

        #region static local attributes
        // thread local settings
        [ThreadStatic]
        private static Core.Segments.ThreadingContext s_threadContext;  
        public static Core.Segments.ThreadingContext ThreadStatic {
            get {
                if (s_threadContext == null) {
                    s_threadContext = new Core.Segments.ThreadingContext();
                }
                return s_threadContext;
            }
            set {
                s_threadContext = value; 
            }
        }

        // local settings caches 
        internal static int s_minimumQuicksortLength;
        internal static bool s_maxNumberThreadsConfigured;
        internal static uint s_maxNumberThreadsDefault;   // for new threads / auto setting

        internal static ulong s_minParallelCopyCount;
        internal static int s_minElementLength4SystemArrayCopy;
        internal static string s_nativeDependenciesAbsolutePath; 
        internal static int s_managedMultiplyBlockSize;
        internal static ArrayStyles s_defaultArrayStyle;
        private static uint s_toStringMaxNumberElementsPerDimension;
        private static uint s_toStringMaxNumberElements;
        #endregion

        #region static global caches

        /// <summary>
        /// True: instruct <i>integer</i> array operations to saturate (clamp) results to the limits of the respective data type.
        /// False: perform wrapping around on overflow. Default: (null) the behavior depends on the setting of <see cref="ArrayStyle"/> for the current thread.
        /// </summary>
        public static bool? SaturateIntegerOps {
            get {
                return ThreadStatic.SaturateIntegerOps; 
            }
            set {
                ThreadStatic.SaturateIntegerOps = value;
            }
        }

        /// <summary>
        /// Controls the way nd-arrays work. Affects the shape of arrays, the default storage order and the handling of subarray / indexing expressions. Thread static. Default: <see cref="ArrayStyles.ILNumericsV4"/>.
        /// </summary>
        /// <remarks><para>This settings controls the way arrays work. Valid options are <see cref="ArrayStyles.ILNumericsV4"/> and <see cref="ArrayStyles.numpy"/>.</para>
        /// <para>This affects the number and shape of vectors and scalars, of arrays returned from subarray expressions / indexing, 
        /// the handling of unspecified trailing dimensions in indexing expressions, the default storage order, (saturating or clipping) behavior 
        /// for integer operations, and others. </para>
        /// <para>The default value is <see cref="ArrayStyles.ILNumericsV4"/>. It corresponds to the ILNumerics version 4, Matlab(R), octave and Julia systems. 
        /// The setting of <see cref="ArrayStyles.numpy"/> corresponds to numpys ndarrays. See <see href="/array-styles_v5.html"/> 
        /// for a comprehensive list of aspects affected by this setting.</para>
        /// <para>Other settings which directly derive their value from the value of <see cref="ArrayStyle"/> are: 
        /// <see cref="Settings.DefaultStorageOrder"/> and <see cref="Settings.MinNumberOfArrayDimensions"/>.</para>
        /// <para>This setting is controlled on a thread level! Modifications affect the current thread only
        /// and do not change the behavior in other threads! This enables one to safely switch between individual  
        /// styles in multithreaded algorithms.</para>
        /// <para>New threads are initialized with a value for <see cref="ArrayStyle"/> according to <see cref="DefaultArrayStyle"/>.</para>
        /// <para>In order to set <see cref="ArrayStyle"/> to <see cref="ArrayStyles.numpy"/> a valid license for the ILNumerics.numpy module is required.</para>
        /// </remarks>
        /// <see cref="Ensure{T}(Expression{Func{T}}, T)"/>
        /// <see cref="ArrayStyles"/>
        /// <seealso href="/array-styles_v5.html"/>
        public static ArrayStyles ArrayStyle {
            get {
                if ((int)ThreadStatic.ArrayStyle == 0) {
                    var tmp = DefaultArrayStyle; // tryLoadSetting<ArrayStyles>($"ILN{nameof(ArrayStyle)}", ref s_arrayStyle, parseArrayStyle, ArrayStyles.ILNumericsV4);
                    ThreadStatic.ArrayStyle = tmp; 
                }
                return ThreadStatic.ArrayStyle;
            }
            set {
                if (ThreadStatic.ArrayStyle != value) {
                    ThreadStatic.ArrayStyle = value;
                }
            }
        }

        /// <summary>
        /// Default array style for initializing the property in new threads. Default: <see cref="ArrayStyles.ILNumericsV4"/>.
        /// </summary>
        /// <remarks>The value of this property is used as the <see cref="Settings.ArrayStyle"/> of new computational threads.
        /// </remarks>
        /// <see cref="Ensure{T}(Expression{Func{T}}, T)"/>
        /// <see cref="ArrayStyles"/>
        public static ArrayStyles DefaultArrayStyle {
            get {
                if ((int)s_defaultArrayStyle == 0) {
                    tryLoadSetting<ArrayStyles>($"ILN{nameof(DefaultArrayStyle)}", ref s_defaultArrayStyle, parseArrayStyle, ArrayStyles.ILNumericsV4);
                }
                return s_defaultArrayStyle;
            }
            set {
                if (s_defaultArrayStyle != value) {
                    s_defaultArrayStyle = value;
                }
            }
        }

        /// <summary>
        ///  The default storage order for new storages. Default: <see cref="StorageOrders.ColumnMajor"/>. Readonly (controlled by ArrayStyle). 
        /// </summary>
        /// <remarks>
        /// <para>This setting is controlled by the <see cref="ArrayStyle"/> configuration setting. It returns <see cref="StorageOrders.ColumnMajor"/> 
        /// for <see cref="ArrayStyle"/> = <see cref="ArrayStyles.ILNumericsV4"/>, and <see cref="StorageOrders.RowMajor"/> for <see cref="ArrayStyle"/> = <see cref="ArrayStyles.numpy"/>.</para>
        /// <para>Valid values are:<see cref="StorageOrders.ColumnMajor"/> and <see cref="StorageOrders.RowMajor"/>.</para></remarks>
        /// <seealso cref="ArrayStyles"/>
        /// <seealso cref="ArrayStyle"/>
        public static StorageOrders DefaultStorageOrder {
            get {
                return ArrayStyle == ArrayStyles.ILNumericsV4 ? StorageOrders.ColumnMajor : StorageOrders.RowMajor;
            } 
        }

        /// <summary>
        /// Minimum number of array dimensions. Default: 2 (Matlab, Octave, ILNumerics v4 compatibility). Readonly, controlled by <see cref="ArrayStyle"/>.
        /// </summary>
        /// <remarks><para>This configuration value affects the number of dimensions of newly created arrays. By 
        /// default ILNumerics maintains compatibility with Matlab / Octave bahavior, where all arrays are considered 
        /// matrices and carry two dimensions or more. Scalars and 1-dim arrays (vectors) are represented by matrices of 
        /// [1 x 1], [n x 1] or [1 x n] shapes.</para>
        /// <para>Attempts to create an array with a number of dimensions of less than 2 will transparently create 
        /// a corresponding matrix (2 dimensions) with trailing singleton dimensions of length 1.</para>
        /// <para>This behavior can be changed by configuring the library for <see cref="ArrayStyle"/> = <see cref="ArrayStyles.numpy"/>.  
        /// which will also allow true vectors (1 dimension) and 'numpy array scalars' with 0 dimensions.</para>
        /// </remarks>
        /// <seealso cref="ArrayStyles"/>
        /// <seealso cref="ArrayStyle"/>
        public static uint MinNumberOfArrayDimensions {
            get {
                return ArrayStyle == ArrayStyles.ILNumericsV4 ? 2u : 0u; 
            }
        }

        /// <summary>
        /// The absolute directory where ILNumerics should look for native dependencies (LAPACK, HDF5 etc.); default: empty string 
        /// </summary>
        /// <remarks><para>By default (i.e.: empty string) ILNumerics will automatically determine the include path for native dependencies on startup. 
        /// In order to do so, the bitrate (Environment.Is64BitProcess) is examined and depending on its value one of 'bin32' or 'bin64' is 
        /// added to the beginning of the current PATH environment variable.</para>
        /// <para>In order to overwrite this behavior, one may set the absolut path to be included here. Note, this will prevent the 
        /// automatic (bitrate dependend) behaviour! When configuring the NativeDependenciesAbsolutePath the user must keep the 
        /// current bitrate into account and is responsible for placing the right binary distribution files into that folder. </para></remarks>
        public static string NativeDependenciesAbsolutePath {
            get {
                return s_nativeDependenciesAbsolutePath;
            }
            set {
                s_nativeDependenciesAbsolutePath = value;
            }
        }
        /// <summary>
        /// Block size used for blocked managed matrix multiply, default: 150
        /// </summary>
        public static int ManagedMultiplyBlockSize {
            get { return s_managedMultiplyBlockSize; }
            set { s_managedMultiplyBlockSize = value; }
        }

        /// <summary>
        /// Determine the minimum length for arrays to be sorted via Quicksort algorithm, smaller arrays are sorted via insertion sort
        /// </summary>
        public static int MinimumQuicksortLength {
            get { return s_minimumQuicksortLength; }
            set { s_minimumQuicksortLength = value; }
        }

        /// <summary>
        /// Maximum number of threads for parallel execution of internal functions in ILNumerics.
        /// </summary>
        /// <remarks>
        /// <para>In order to maximize execution speed of numerical algorithms, the value of <see cref="MaxNumberThreads"/> should be equal to the number 
        /// of <b>real</b> processor cores on the system. For processors utilizing <a href="http://en.wikipedia.org/wiki/Hyper-threading">Hyper-threading</a> the number on virtual cores 
        /// may be higher. However, since those virtual cores share certain ressources for execution they would not be of great help for many computational functions. Hence, 
        /// the number of 'cores' appearing e.g. in the windows task manager is misleading and the true number of independent cores should be considered for <see cref="MaxNumberThreads"/> 
        /// instead. Consult your proccessor vendor in order to find out, how many independant cores your system utilizes.</para>
        /// <para>If, on the other hand, your algorithm mainly performs search and replace style data processing where the numerical coprocessor is rarely needed it 
        /// may pay of to utilize more threads in parallel. In this case you may try higher values here. The default value is the number of main processor cores, though.</para>
        /// <para>If your algorithm uses a custom parallel execution models, it may 
        /// be necessary to set this value to '1'. ILNumerics will run single threaded then - leaving you the option to configure 
        /// the execution on parallel threads on your own.</para>
        /// <para>The setting of this value also effects the corresponding value of any unmanaged optimized support library (e.g. MKL), which is internally used by ILNumerics.</para>
        /// </remarks>
        public static uint MaxNumberThreads {
            get { 
                if (ThreadStatic.MaxNumberThreads == 0) {
                    // first request from new thread: not configured yet
                    if (s_maxNumberThreadsDefault == 0) {
                        LoadMaxNumberThreadsDefault(); 
                    }
                    ThreadStatic.MaxNumberThreads = s_maxNumberThreadsDefault; 
                } 
                return ThreadStatic.MaxNumberThreads; 
            }
            set {
                if (value < 1)
                    throw new ArgumentException("Number of worker threads must be greater than 0.");
                if (value == ThreadStatic.MaxNumberThreads) {
                    return; 
                }
                lock (s_lock) {
                    ILNumerics.Core.Global.ThreadPool.Pool.MaxNumberThreads = value - 1;
                    s_maxNumberThreadsConfigured = true;
                    ThreadStatic.MaxNumberThreads = value;   // calls UpdateMNT()

                }
            }
        }
        internal static void UpdateMaxNumberThreads() {
            //configure native provider
            if (MathInternal.Lapack != null && MathInternal.Lapack is Core.Native.LapackMKL10_0) {
                ILNumerics.Core.Native.LapackMKL10_0.Init();
            }
            if (MathInternal.FFTImplementation is MKLFFT) {
                ILNumerics.Core.Native.MKLFFT.Init();    // should actually be redundant, since set_num_threads was used for LAPACK above.
            }
        }
        /// <summary>
        /// Determines, whether the current setting of <see cref="MaxNumberThreads"/> is the result of a custom configuration (true) or automatic system inspection (false).
        /// </summary>
        public static bool MaxNumberThreadsConfigured {
            get { return s_maxNumberThreadsConfigured; }
            private set { s_maxNumberThreadsConfigured = value; }
        }
        
        /// <summary>
        /// Upper limit on the number of elements per dimension printed for an array in textual representations. Default: 21.
        /// </summary>
        /// <remarks><para>This setting limits the number of elements shown along any dimension when arrays are converted to strings. 
        /// This value affects the result of calling <see cref="object.ToString()"/> on ILNumerics arrays. It does also 
        /// affect the ouput when displaying arrays contents in the Visual Studio 'Immediate Window' and the information displayed in data tips during a 
        /// Visual Studio debug session.</para>
        /// </remarks>
        /// <seealso cref="ToStringMaxNumberElements"/>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.ToString()"/>
        /// <seealso cref="LoadDefaults()"></seealso>
        public static uint ToStringMaxNumberElementsPerDimension {
            get { return s_toStringMaxNumberElementsPerDimension; }
            set { s_toStringMaxNumberElementsPerDimension = value; }
        }

        /// <summary>
        /// Upper limit on the overall number of elements printed for an array in textual representations. Default: 501.
        /// </summary>
        /// <remarks><para>This setting limits the overall number of elements shown when arrays are converted to strings.</para>
        /// <para>In difference to the value of <see cref="ToStringMaxNumberElementsPerDimension"/>, which controls the number
        /// of elements shown along any dimension - 
        /// <see cref="ToStringMaxNumberElements"/> limits the number of elements shown for the whole array. </para>
        /// <para>This value affects the result of calling <see cref="object.ToString()"/> on ILNumerics arrays. It does also 
        /// affect the ouput when displaying arrays contents in Visual Studio tool windows, like the 'Immediate Window' and the 
        /// information displayed in data tips during a Visual Studio debug session.</para>
        /// </remarks>
        /// <seealso cref="ToStringMaxNumberElementsPerDimension"/>
        /// <seealso cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.ToString()"/>
        /// <seealso cref="LoadDefaults()"></seealso>
        public static uint ToStringMaxNumberElements {
            get { return s_toStringMaxNumberElements; }
            set { s_toStringMaxNumberElements = value; }
        }

        public static bool ThreadIgnoreSegmentLocks {
            get {
                return ThreadStatic.ThreadIgnoreSegmentLocks ?? false; 
            }
            set {
                ThreadStatic.ThreadIgnoreSegmentLocks = value;
            }
        }

        public static bool ThreadBinLoggingEnabled {
            get {
                return ThreadStatic.ThreadBinLoggingEnabled ?? false; 
            }
            set {
                ThreadStatic.ThreadBinLoggingEnabled = value;
            }
        }

        #endregion

        #region public interface 

        /// <summary>
        /// (Re)load settings from the application configuration file or reset to default values.
        /// </summary>
        public static void LoadDefaults() {

            tryLoadSetting<int>("ILNMinimumQuicksortLength", ref s_minimumQuicksortLength, int.Parse, 10);

            #region deal with number of threads
            string numThreadsSource = LoadMaxNumberThreadsDefault();

            System.Diagnostics.Trace.WriteLine("Setting MaxNumberThreads: " + MaxNumberThreads + " " + numThreadsSource);
            // this sets the MKL to the same number of threads. Other LAPACK providers must handle their internal parallelization differently somehow. 

            // TODO: configure native provider!
            // Native.ILLapackMKL10_0.Init(); 
            #endregion

            tryLoadSetting<int>("ILN" + nameof(ManagedMultiplyBlockSize), ref s_managedMultiplyBlockSize, int.Parse, 150);
            tryLoadSetting<string>("ILN" + nameof(NativeDependenciesAbsolutePath), ref s_nativeDependenciesAbsolutePath, dummyStringCopy, "");
            tryLoadSetting<ArrayStyles>("ILN" + nameof(DefaultArrayStyle), ref s_defaultArrayStyle, parseArrayStyle, ArrayStyles.ILNumericsV4);
            tryLoadSetting<uint>("ILN" + nameof(ToStringMaxNumberElementsPerDimension), ref s_toStringMaxNumberElementsPerDimension, uint.Parse, 21);
            tryLoadSetting<uint>("ILN" + nameof(ToStringMaxNumberElements), ref s_toStringMaxNumberElements, uint.Parse, 501);
        }
        
        private static string LoadMaxNumberThreadsDefault() {
            string numThreadsSource = "(read from config)";
            uint newValue = 1; 
            if (!tryLoadSetting<uint>("ILNMaxNumberThreads", ref newValue, uint.Parse, 1)) {
                // must determine number of threads in other ways
                // In debug mode we run single threaded only. The debug engine / immediate window / watch window 
                // do not like to run multithreaded and there is a risk of death blocking the debug session.
                if (Environment.ProcessorCount > 1 && !System.Diagnostics.Debugger.IsAttached) {

                    // use MKL to query the number of physical cores
                    newValue = 0;
                    try {
                        newValue = GetMaxThreads();
                        if (newValue < 1)
                            newValue = 1;
                        numThreadsSource = "(from MKL)";

                    } catch (Exception exc) {
                        System.Diagnostics.Trace.WriteLine("Error retrieving number of threads from MKL: " + exc.ToString());
                        newValue = (uint)(Environment.ProcessorCount / 2);  // -> hyperthreading is useless for us! -> ho: not always true!! 
                        System.Diagnostics.Trace.WriteLine("Falling back to number of processors provided by Environment.ProcessorCount / 2: " + newValue);
                        numThreadsSource = "(Environment.ProcessorCount fallback)";

                    }
                    MaxNumberThreadsConfigured = false;
                    // Consider: try to determine number of PHYSICAL cores automatically (ho: done with above WMI / MKL attempts)
                    // try to load number of physical cores rather than logical cores - utilize CPUID via native function pointer? 

                } else {
                    if (System.Diagnostics.Debugger.IsAttached) {
                        numThreadsSource = "(running with debugger)";
                    } else {
                        numThreadsSource = "(reported by Environment.ProcessorCount)";
                    }
                }
                // take at least number of physical cores or above
                s_maxNumberThreadsDefault = (uint)Math.Max(newValue, 1); // Environment.ProcessorCount);
            } else {
                s_maxNumberThreadsDefault = newValue; 
                MaxNumberThreadsConfigured = true;
            }
            return numThreadsSource;
        }


        #endregion

        #region private helper 
        private static bool tryLoadSetting<T>(string settingsName, ref T obj, System.Converter<string, T> convert, T defaultValue) {
            obj = defaultValue;
            return false;
        }
        internal static string dummyStringCopy(string val) { return val.Trim(); }

        internal static StorageOrders? parseStorageOrders(string value) {
            if (String.IsNullOrEmpty(value)) return null;
            StorageOrders ret;
            if (Enum.TryParse(value, true, out ret)) {
                if (ret != StorageOrders.RowMajor && ret != StorageOrders.ColumnMajor) {
                    throw new ArgumentException($"The configuration value for 'ILN{nameof(DefaultStorageOrder)}' is not valid. Valid options are: {nameof(StorageOrders.RowMajor)} and {nameof(StorageOrders.ColumnMajor)}.");
                }
                return ret;
            }
            throw new ArgumentException($"The configuration value for 'ILN{nameof(DefaultStorageOrder)}' is not valid. Valid options are: {nameof(StorageOrders.RowMajor)} and {nameof(StorageOrders.ColumnMajor)}.");
        }

        internal static ArrayStyles parseArrayStyle(string value) {
            if (String.IsNullOrEmpty(value)) return (ArrayStyles)0;
            ArrayStyles ret;
            if (Enum.TryParse(value, true, out ret)) {
                return ret;
            }
            throw new ArgumentException($"The configuration value for 'ILN{nameof(ArrayStyle)}' is not valid. Valid options are: '{nameof(ArrayStyles.ILNumericsV4)}' and '{nameof(ArrayStyles.numpy)}'.");
        }

        /// <summary>
        /// Ensure the value for a specific (static) setting with robust rollback. This should be used in a 'using' directive. 
        /// </summary>
        /// <typeparam name="T">Type of the settings value.</typeparam>
        /// <param name="Name">The name of the setting to modify.</param>
        /// <param name="Value">The value for the setting.</param>
        /// <returns>Disposable object which resets the setting property to its original value when <see cref="IDisposable.Dispose()"/> is called.</returns>
        /// <remarks>This method is useful when a temporary modification to a global setting is needed. It is applied as follows: 
        /// <list type="bullet">
        /// <item>Enclose the instructions where a certain value for one of the ILNumerics settings is needed into an 'using' block.</item>
        /// <item>In the head of the 'using' the <see cref="Ensure{T}(string, T)"/> method is called with the name of the setting to control and the new value for it.</item>
        /// <item>During the execution of the instructions in the body of the 'using' block the setting <paramref name="Name"/> will have the value provided by <paramref name="Value"/>.</item>
        /// <item>Once the body of the 'using' block is left the value of the setting <paramref name="Name"/> will be reset to its original value automatically.</item>
        /// </list>
        /// <para>It is advisable to use the C# keyword 'nameof()' in order to provide the name of the setting.</para>
        /// </remarks>
        /// <example><![CDATA[ <code>using (Settings.Ensure(nameof(Settings.MaxNumberThreads), 1)) {
        ///     //this part runs in single threaded mode
        ///     //
        ///     // ... 
        /// }
        /// //here we continue with Settings.MaxNumberThreads set to the original value, before we modified it. 
        /// </code>]]></example>
        public static IDisposable Ensure<T>(string Name, T Value) where T: struct {
            return new SettingsPropertyEnsurer<T>(Name, Value);
        }

        /// <summary>
        /// Ensure the value for a specific (static) setting with robust rollback. This should be used in a 'using' directive. 
        /// </summary>
        /// <typeparam name="T">Type of the settings value.</typeparam>
        /// <param name="p">Lambda expression _reading_ the target property.</param>
        /// <param name="v">The value to be ensured for settings.</param>
        /// <returns><see cref="IDisposable"/> which resets the setting property to its original value when <see cref="IDisposable.Dispose()"/> is called.</returns>
        /// <remarks> Here is how this function is to be used: <list type="bullet">
        /// <item>Enclose the instructions where a certain value for one of the ILNumerics settings is needed into an 'using' block.</item>
        /// <item>In the head of the 'using' the <see cref="Ensure{T}(Expression{Func{T}}, T)"/> method is called with a 
        /// lambda function wrapping the setting to control and the new value for it.</item>
        /// <item>During the execution of the instructions in the body of the 'using' block the property 
        /// will have the value provided by <paramref name="v"/>.</item>
        /// <item>Once the body of the 'using' block is left the value of the <see cref="Settings"/> property will be 
        /// reset to its original value automatically.</item>
        /// </list>
        /// </remarks>
        /// <example><![CDATA[ <code>using (Settings.Ensure(() => Settings.MaxNumberThreads, 1u)) {
        ///     //this part runs in single threaded mode
        ///     //
        ///     // ... 
        /// }
        /// // here we continue with Settings.MaxNumberThreads set to the original value, before we had modified it. 
        /// </code>]]></example>
        public static IDisposable Ensure<T>(Expression<Func<T>> p, T v) where T : struct {
            // TODO: this seriously needs caching!! 

            var memExpr = p.Body as MemberExpression;
            if (memExpr == null) throw new ArgumentException("Invalid expression provided. Expected is a lambda expression in this style: () => Settings.TargetProperty");
            var prop = memExpr.Member as PropertyInfo; 
            if (prop == null) {
                throw new ArgumentException("Invalid expression provided. Expected is a lambda expression in this style: () => Settings.TargetProperty"); 
            }
            return new SettingsPropertyEnsurer<T>(prop, v);
        }

        internal struct SettingsPropertyEnsurer<T> : IDisposable where T : struct{

            T m_oldValue;
            PropertyInfo m_property; 

            public void Dispose() {
                // todo: implement correctly! 

                if (m_property != null) {

                    System.Diagnostics.Debug.Assert(m_property != null && typeof(T).IsAssignableFrom(m_property.PropertyType));
                    // here we only set the old value back 
                    m_property.SetValue(null, m_oldValue, null);

                }
            }

            internal SettingsPropertyEnsurer(PropertyInfo property, T value) {
                m_property = property;
                if (m_property != null && typeof(T).IsAssignableFrom(m_property.PropertyType)) {
                    // keep the old value
                    object val = m_property.GetValue(null, null);
                    m_oldValue = (T)val;
                    // now set the new value
                    m_property.SetValue(null, value, null);
                } else {
                    throw new ArgumentException($"The type of 'value' is {typeof(T).Name} which cannot be assigned to the type of '{property}' ({m_property.PropertyType.Name}).");
                }
            }

            internal SettingsPropertyEnsurer(string property, T value) {
                // find the setting via reflection
                var settingsClass = typeof(ILNumerics.Settings);
                m_property = settingsClass.GetProperties().First(p => p.Name == property);
                if (m_property != null && typeof(T).IsAssignableFrom(m_property.PropertyType)) {
                    // keep the old value
                    object val = m_property.GetValue(null, null);
                    m_oldValue = (T)val;
                    // now set the new value
                    m_property.SetValue(null, value, null);
                } else {
                    throw new ArgumentException($"The type of 'value' is {typeof(T).Name} which cannot be assigned to the type of '{property}' ({m_property.PropertyType.Name}).");
                }
            }
        }
        #endregion

    }
}
