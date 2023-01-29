using System.Drawing;
using TransportProblemLib.Abstruct;

namespace TransportProblemLib.SupportPlanAlgorithm
{
    public class MinElemet : TransportAlgorithm
    {
        private double[,] _prices;
        public MinElemet(double[] reserves, double[] needs, double[,] prices) : base(reserves, needs)
        {
            _prices = (double[,])prices.Clone();
            Reserves = (double[])reserves.Clone();
            Needs = (double[])needs.Clone();
        }

        private Point GetMinimumElements(double[,] prices_, double[] supply, double[] need)
        {

            double min = double.MaxValue;
            Point p = new Point(-1, -1);
            for (int i = 0; i < prices_.GetLength(0); i++)
            {
                for (int j = 0; j < prices_.GetLength(1); j++)
                {
                    if (supply[i] == 0)
                    {
                        for (int k = 0; k < need.Length; k++)
                        {
                            prices_[i, k] = -1;
                        }
                    }
                    if (need[j] == 0)
                    {
                        for (int k = 0; k < supply.Length; k++)
                        {
                            prices_[k, j] = -1;
                        }
                    }

                    if (prices_[i, j] != -1 && prices_[i, j] < min)
                    {
                        min = prices_[i, j];
                        p = new Point(i, j);
                    }
                }
            }

            return p;
        }

        public override double[,] GetPlan()
        {
            // Create a matrix to store the optimal allocation 
            double[,] allocation = new double[Reserves.Length, Needs.Length];

            // Define the variables to store the current allocation 
            double currentAllocation = 0;

            // Execute the algorithm 
            while (true)
            {
                var p = GetMinimumElements(_prices, Reserves, Needs);
                if (p.X == -1 && p.Y == -1)
                {
                    break;
                }

                // Calculate the minimum of supply and need 
                currentAllocation = Math.Min(Reserves[p.X], Needs[p.Y]);

                // Allocate the resources 
                allocation[p.X, p.Y] += currentAllocation;

                // Decrease the demand and supply 
                Reserves[p.X] -= currentAllocation;
                Needs[p.Y] -= currentAllocation;
            }

            NullToNan(allocation);

            return allocation;
        }
    }
}
