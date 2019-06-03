using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RatioLibrary;

namespace RatioLibraryTest
{

    [TestClass]
    public class UnitTest1
    {
        private Ratio r;
        [TestMethod]
        public void Test_CreateRatio()
        {
            r = new Ratio(1, 2);
        }

        [TestMethod]
        public void Test_ZeroDenominatorException()
        {
            Action action = () => r = new Ratio(1, 0);
            Assert.ThrowsException<DenominatorException>(action);
        }



    }
}
