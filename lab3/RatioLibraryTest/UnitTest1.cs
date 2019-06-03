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
        public void Test_ZeroDenominatorException_atCreation()
        {
            Action action = () => r1 = new Ratio(1, 0);
            Assert.ThrowsException<DenominatorException>(action);
        }

        [TestMethod]
        public void Test_ZeroDenominatorException_atDivision()
        {
            Action action = () =>
            {
                r1 = new Ratio(1, 1);
                r2 = new Ratio(0, 1);
                double result = (r1 / r2).ToDouble();
            };
            Assert.ThrowsException<DenominatorException>(action);
        }


        [TestMethod]
        public void Test_CorrectnessOfReduction() {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(128, 256);
            Assert.AreEqual(r1.ToString(), r2.ToString());
        }

        [TestMethod]
        public void Test_Addition()
        {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);
            Assert.AreEqual(new Ratio(3,4).ToDouble(), (r1 + r2).ToDouble());
        }

        [TestMethod]
        public void Test_Subtraction()
        {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);
            Assert.AreEqual(new Ratio(1,4).ToDouble(), (r1 - r2).ToDouble());
        }

        [TestMethod]
        public void Test_Multiplication()
        {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);
            Assert.AreEqual(new Ratio(1,8).ToDouble(), (r1 * r2).ToDouble());
        }

        [TestMethod]
        public void Test_Division()
        {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);
            Assert.AreEqual(new Ratio(2,1).ToDouble(), (r1 / r2).ToDouble());
        }

    }
}
