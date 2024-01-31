using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportProblemLib.OptimisationAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportProblemLib.SupportPlanAlgorithm;
using TransportProblemLib.Abstruct;
using TransportProblemLib.Extention;
using TransportProblemLibTests;

namespace TransportProblemLib.OptimisationAlgorithm.Tests
{
    [TestClass()]
    public class OptimizationMethodPotentialsTests
    {
        [TestMethod()]
        public void OptimizationMethodPotentialsTest()
        {
            double expected = 451;
            TransportAlgorithm supportAlg = new NordWest(SeedData.Reserves, SeedData.Needs);
            var sPlan = supportAlg.GetPlan();
            TransportAlgorithm alg = new OptimizationMethodPotentials(SeedData.Reserves, SeedData.Needs, sPlan, SeedData.Prices);
            var plan = alg.GetPlan();

            double actual = alg.GetSum(plan, SeedData.Prices);
            Assert.AreEqual(expected, actual);
        }
    }
}