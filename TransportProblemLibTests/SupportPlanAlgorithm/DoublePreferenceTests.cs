using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TransportProblemLib.Abstruct;
using TransportProblemLib.Extention;
using TransportProblemLibTests;

namespace TransportProblemLib.SupportPlanAlgorithm.Tests
{
    [TestClass()]
    public class DoublePreferenceTests
    {
        [TestMethod()]
        public void GetPlanTest()
        {
            int expected = 464;
            TransportAlgorithm alg = new DoublePreference(SeedData.Reserves, SeedData.Needs, SeedData.Prices);
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
            Assert.AreEqual(actual, expected);
            
        }
    }
}