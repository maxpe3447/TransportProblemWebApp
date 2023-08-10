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

namespace TransportProblemLib.SupportPlanAlgorithm.Tests
{
    [TestClass()]
    public class NordWestTests
    {
        [TestMethod()]
        public void NordWestTest()
        {
            double[,] prices =
            {
                { 4,3,4,11,9 },
                { 3,4,7,15,8 },
                { 7,4,2,8,15}
            };

            double[] reserves = { 40, 30, 10 };
            double[] needs = { 20, 21, 7, 24, 8 };
            double expected = 659;

            TransportAlgorithm alg = new NordWest(reserves, needs);
            var plan = alg.GetPlan();

            for (int i = 0; i < plan.GetLength(0); i++)
            {
                for (int j = 0; j < plan.GetLength(1); j++)
                {
                    Debug.Write($"{plan[i, j]} ");
                }
                Debug.WriteLine("");
            }

            double actual = alg.GetSum(plan, prices);
            Assert.AreEqual(expected, actual);
        }
    }
}