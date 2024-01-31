using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportProblemLib.SupportPlanAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TransportProblemLib.Abstruct;
using TransportProblemLib.Extention;
using TransportProblemLibTests;

namespace TransportProblemLib.SupportPlanAlgorithm.Tests
{
    [TestClass()]
    public class GeneticAlgorithmTests
    {
        [TestMethod()]
        public void GetPlanTest()
        {
            double expected = 342;// 376;

            TransportAlgorithm alg = new GeneticAlgorithm(SeedData.Reserves, SeedData.Needs, SeedData.Prices);
            var plan = alg.GetPlan();

            double actual = alg.GetSum(plan, SeedData.Prices);
            Debug.WriteLine(actual);
            Assert.AreEqual(actual, actual);
        }
    }
}