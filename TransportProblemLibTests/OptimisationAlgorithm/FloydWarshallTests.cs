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
    public class FloydWarshallTests
    {


        [TestMethod()]
        public void GetPlanTest()
        {
            double[,] prices =
            {
                { 4,3,4,11,9 },
                { 3,4,7,15,8 },
                { 7,4,2,8,15}
            };

            double[] reserves = { 40, 30, 10 };
            double[] needs = { 20, 21, 7, 24, 8 };
            double expected = 464;
            TransportAlgorithm supportAlg = new NordWest(reserves, needs);
            var sPlan = supportAlg.GetPlan();
            TransportAlgorithm alg = new FloydWarshall(reserves, needs, sPlan);
            var plan = alg.GetPlan();

            double actual = alg.GetSum(plan, prices);
            Assert.AreEqual(expected, actual);
        }

        private void PrintMatrixDebug()
        {
            //for (int i = 0; i < plan.GetLength(0); i++)
            //{
            //    for (int j = 0; j < plan.GetLength(1); j++)
            //    {
            //        Debug.Write($"{plan[i, j]} ");
            //    }
            //    Debug.WriteLine("");
            //}
        }

    }
}