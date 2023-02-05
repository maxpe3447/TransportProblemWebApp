using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportProblemLib.SupportPlanAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportProblemLib.Extention;

namespace TransportProblemLib.SupportPlanAlgorithm.Tests
{
    [TestClass()]
    public class VogelApproximationTests
    {
        [TestMethod()]
        public void GetPlanTest()
        {
            double[] needs = { 30, 20, 70, 30, 60 };
            double[] reserves = { 50, 60, 50, 50 };
            double[,] prices = {
                {16, 16, 13, 22, 17},
                {14, 14, 13, 19, 15},
                {19, 19, 20, 23, 50},
                {50, 12, 50, 15, 11}
            };
            double expected = 3100;

            VogelApproximation alg = new VogelApproximation(reserves, needs, prices);
            var plan = alg.GetPlan();
            double actual = alg.GetSum(plan, prices);
            Assert.AreEqual(expected, actual, $"actual: {actual}, but expected: {expected}");
            //Assert.Fail();
        }
    }
}