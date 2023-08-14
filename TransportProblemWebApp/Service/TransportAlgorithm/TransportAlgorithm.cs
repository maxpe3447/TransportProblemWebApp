using TransportProblemLib.Enums;
using TransportProblemLib.Extention;
using TransportProblemLib.OptimisationAlgorithm;
using TransportProblemLib.SupportPlanAlgorithm;
using TransportProblemWebApp.Model;

namespace TransportProblemWebApp.Service.MinElementAlgorithm
{
    public class TransportAlgorithm : ITransportAlgorithm
    {
        TransportProblemLib.Abstruct.TransportAlgorithm GetAlgorithmByModel(MatrixViewModel model)
        {
            var prices = model.GetMatrix();
            var reserves = model.Reserves;
            var needs = model.Needs;

            return model.SupportPlanType switch
            {
                AlgorithmType.MINIMAL_ELEMENT => new MinElemet(reserves, needs, prices),
                AlgorithmType.NORD_WEST => new NordWest(reserves, needs),
                AlgorithmType.VOGEL_APPROXIMATION => new VogelApproximation(reserves, needs, prices),
                AlgorithmType.DOUBLE_PREFERENCE => new DoublePreference(reserves, needs, prices),
                AlgorithmType.GENETIC => new GeneticAlgorithm(reserves, needs, prices),
            };
        }
        TransportProblemLib.Abstruct.TransportAlgorithm GetAlgorithmByModel(MatrixViewModel model, double[,] supportPlan)
        => model.OptimisationType switch
        {
            AlgorithmType.POTENTIAL => new OptimizationMethodPotentials(model.Reserves, model.Needs, 
                                                                        supportPlan, model.GetMatrix()),
            AlgorithmType.Floyd_Warshall => new FloydWarshall(model.Reserves, model.Needs, supportPlan)
        };

        public AlgorithmResultViewModel GetResultModel(MatrixViewModel model)
        {
            var prices = model.GetMatrix();
            var algorithm = GetAlgorithmByModel(model);

            var supportPlan = algorithm.GetPlan();
            var supportSum = algorithm.GetSum(supportPlan, prices);

            var optimAlgorithm = GetAlgorithmByModel(model, supportPlan);

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
