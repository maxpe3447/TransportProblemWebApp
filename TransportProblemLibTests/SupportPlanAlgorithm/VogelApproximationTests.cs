using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportProblemLib.SupportPlanAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportProblemLib.Extention;
using TransportProblemLibTests;

namespace TransportProblemLib.SupportPlanAlgorithm.Tests
{
    [TestClass()]
    public class VogelApproximationTests
    {
        [TestMethod()]
        public void GetPlanTest()
        {
            double expected = 476;

            VogelApproximation alg = new VogelApproximation(SeedData.Reserves, SeedData.Needs, SeedData.Prices);
            var plan = alg.GetPlan();
            double actual = alg.GetSum(plan, SeedData.Prices);
            Assert.AreEqual(expected, actual, $"actual: {actual}, but expected: {expected}");
            //Assert.Fail();
        }
    }
}