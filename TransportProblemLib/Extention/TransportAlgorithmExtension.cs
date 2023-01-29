using System.Diagnostics;
using TransportProblemLib.Abstruct;

namespace TransportProblemLib.Extention
{
    public static class TransportAlgorithmExtension
    {
        public static double GetSum(this TransportAlgorithm alg, double[,] plan, double[,] prices)
        {
            double sum = 0;
            for (int i = 0; i < plan.Length; i++)
            {
                int j = (i - i % alg.Needs.Length) / alg.Needs.Length;
                int k = i % alg.Needs.Length;
                if (plan[j, k] == plan[j, k])
                    sum += plan[j, k] * prices[j, k];
            }
            return sum;
        }
    }
}
