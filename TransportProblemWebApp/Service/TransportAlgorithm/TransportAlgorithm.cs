using System.Diagnostics;
using System.Drawing.Drawing2D;
using TransportProblemLib;
using TransportProblemLib.Abstruct;
using TransportProblemLib.Enums;
using TransportProblemLib.Extention;
using TransportProblemLib.OptimisationAlgorithm;
using TransportProblemLib.SupportPlanAlgorithm;
using TransportProblemWebApp.Model;

namespace TransportProblemWebApp.Service.MinElementAlgorithm
{
    public class TransportAlgorithm: ITransportAlgorithm
    {
        public AlgorithmResultViewModel GetResultModel(MatrixViewModel model)
        {
            var prices = model.GetMatrix();
            var reserves = model.Reserves;
            var needs = model.Needs;

            TransportProblemLib.Abstruct.TransportAlgorithm algorithm =
                model.SupportPlanType switch
            {
                AlgorithmType.MINIMAL_ELEMENT => new MinElemet(reserves, needs, prices),
                AlgorithmType.NORD_WEST => new NordWest(reserves, needs)
            };

            var supportPlan = algorithm.GetPlan();
            var supportSum = algorithm.GetSum(supportPlan, prices);

            TransportProblemLib.Abstruct.TransportAlgorithm optimAlgorithm=
                model.OptimisationType switch
                {
                    AlgorithmType.POTENTIAL => new OptimizationMethodPotentials(reserves, needs, supportPlan, prices)
                };

            var optimalPlan = optimAlgorithm.GetPlan();
            var optimalSum = algorithm.GetSum(optimalPlan, prices);

            return new AlgorithmResultViewModel()
            { 
                 SupportPlan = MatrixToArray(supportPlan),
                 OptimalPlan = MatrixToArray(optimalPlan),

                 OptimalPlanSum = optimalSum,
                 SupportPlanSum = supportSum,

                 Row = optimalPlan.GetLength(0),
                 Coll = optimalPlan.GetLength(1)
            };
        }

        private double[] MatrixToArray(double[,] matrix)
        {
            var arr = new double[matrix.Length];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    arr[IndexConvert.Convert(i, j, matrix.GetLength(1))] = matrix[i, j];
                }
            }

            return arr;
        }
    }
}
