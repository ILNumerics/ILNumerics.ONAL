using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class NumpyTestClass {

        ArrayStyles m_oldArrayStyle; 

        [TestInitialize]
        public void Initialize() {
            m_oldArrayStyle = Settings.ArrayStyle; 
            Settings.ArrayStyle = ArrayStyles.numpy;
        }

        [TestCleanup]
        public void CleanUp() {
            Settings.ArrayStyle = m_oldArrayStyle;
        }

    }
}