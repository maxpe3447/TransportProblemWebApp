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
    public class MinElemetTests
    {
        [TestMethod()]
        public void MinElemetTest()
        {
            double expected = 464;

            TransportAlgorithm alg = new MinElemet(SeedData.Reserves, SeedData.Needs, SeedData.Prices);
            var plan = alg.GetPlan();

            for (int i = 0; i < plan.GetLength(0); i++)
            {
                for (int j = 0; j < plan.GetLength(1); j++)
                {
                    Debug.Write($"{plan[i, j]} ");
                }
                Debug.WriteLine("");
            }

            double actual = alg.GetSum(plan, SeedData.Prices);
            Assert.AreEqual(expected, actual);

        }

        
    }
}