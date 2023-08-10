using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportProblemLib.Abstruct;

namespace TransportProblemLib.OptimisationAlgorithm
{
    public class FloydWarshall : TransportAlgorithm
    {
        private double[,] _supportPlan;
        public FloydWarshall(double[] reserves, double[] needs, double[,] supportPlan) : base(reserves, needs)
        {
            _supportPlan = (double[,])supportPlan.Clone();
        }

        public override double[,] GetPlan()
        {
            int m = _supportPlan.GetLength(0); // Кількість постачальників
            int n = _supportPlan.GetLength(1); // Кількість клієнтів

            // Матриця шляхів
            int[,] paths = new int[m, n];

            // Алгоритм Флойда-Уоршелла
            for (int k = 0; k < m; k++)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (_supportPlan[i, j] > _supportPlan[i, k] + _supportPlan[k, j])
                        {
                            _supportPlan[i, j] = _supportPlan[i, k] + _supportPlan[k, j];
                            paths[i, j] = k;
                        }
                    }
                }
            }
            return _supportPlan;
        }
    }
}
