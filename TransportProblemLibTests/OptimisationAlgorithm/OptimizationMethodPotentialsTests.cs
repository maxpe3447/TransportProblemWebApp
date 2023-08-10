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

namespace TransportProblemLib.OptimisationAlgorithm.Tests
{
    [TestClass()]
    public class OptimizationMethodPotentialsTests
    {
        [TestMethod()]
        public void OptimizationMethodPotentialsTest()
        {
            double[,] prices =
           {
                { 4,3,4,11,9 },
                { 3,4,7,15,8 },
                { 7,4,2,8,15}
            };

            double[] reserves = { 40, 30, 10 };
            double[] needs = { 20, 21, 7, 24, 8 };
            double expected = 451;
            TransportAlgorithm supportAlg = new NordWest(reserves, needs);
            var sPlan = supportAlg.GetPlan();
            TransportAlgorithm alg = new OptimizationMethodPotentials(reserves, needs, sPlan, prices);
            var plan = alg.GetPlan();

            double actual = alg.GetSum(plan, prices);
            Assert.AreEqual(expected, actual);
        }
    }
}