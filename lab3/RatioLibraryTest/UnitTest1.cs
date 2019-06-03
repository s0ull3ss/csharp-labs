using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatioLibrary;

namespace RatioLibraryTest
{

    [TestClass]
    public class UnitTest1
    {
        private Ratio r1, r2;
        [TestMethod]
        public void Test_CreateRatio()
        {
            r1 = new Ratio(1, 2);
            Assert.AreEqual(0.5, r1.ToDouble());
        }

        [TestMethod]
        public void Test_ZeroDenominatorException()
        {
            Action action = () => r1 = new Ratio(1, 0);
            Assert.ThrowsException<DenominatorException>(action);
        }

        [TestMethod]
        public void Test_CorrectnessOfReduction() {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(128, 256);
            Assert.AreEqual(r1.ToString(), r2.ToString());
        }


    }
}
