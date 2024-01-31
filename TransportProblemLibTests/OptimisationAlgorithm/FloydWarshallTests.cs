using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportProblemLib.Abstruct;
using TransportProblemLib.Extention;
using TransportProblemLib.SupportPlanAlgorithm;
using TransportProblemLibTests;

namespace TransportProblemLib.OptimisationAlgorithm.Tests
{
    [TestClass()]
    public class FloydWarshallTests
    {


        [TestMethod()]
        public void GetPlanTest()
        {
            double expected = 464;
            TransportAlgorithm supportAlg = new NordWest(SeedData.Reserves, SeedData.Needs);
            var sPlan = supportAlg.GetPlan();
            TransportAlgorithm alg = new FloydWarshall(SeedData.Reserves, SeedData.Needs, sPlan);
            var plan = alg.GetPlan();

            double actual = alg.GetSum(plan, SeedData.Prices);
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