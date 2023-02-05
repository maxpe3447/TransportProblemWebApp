using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportProblemLib.Abstruct;

namespace TransportProblemLib.SupportPlanAlgorithm
{
    public class VogelApproximation :TransportAlgorithm
    {
        private double[,] _prices;
        private bool[] _rowDone;
        private bool[] _colDone;
        public VogelApproximation(double[] reserves, double[] needs, double[,] prices) : base(reserves, needs)
        {
            _prices = (double[,])prices.Clone();

            _rowDone=new bool[Reserves.Length];
            _colDone = new bool[Needs.Length];

            Array.Fill(_rowDone, false);
            Array.Fill(_colDone, false);
        }

        public override double[,] GetPlan()
        {
            double supplyLeft = Reserves.Sum();//std::accumulate(supply.cbegin(), supply.cend(), 0, [](int a, int b) { return a + b; });
            //int totalCost = 0;

            double[,] result = new double[Reserves.Length, Needs.Length];

            while (supplyLeft > 0)
            {
                var cell = NextCell();
                int r = (int)cell[0];
                int c = (int)cell[1];

                double quantity = Math.Min(Reserves[r], Needs[c]);

                Needs[c] -= quantity;
                if (Needs[c] == 0)
                {
                    _colDone[c] = true;
                }

                Reserves[r] -= quantity;
                if (Reserves[r] == 0)
                {
                    _rowDone[r] = true;
                }

                result[r,c] = quantity;
                supplyLeft -= quantity;

                //totalCost += quantity * costs[r][c];
            }

            //for (auto & a : result)
            //{
            //    std::cout << a << '\n';
            //}

            //std::cout << "Total cost: " << totalCost;
            NullToNan(result);
            return result;
        }
        List<double> Diff(int j, int len, bool isRow)
        {
            double min1 = int.MaxValue;
            double min2 = int.MaxValue;
            double minP = -1;
            for (int i = 0; i < len; i++)
            {
                if (isRow ? _colDone[i] : _rowDone[i])
                {
                    continue;
                }
                double c = isRow
                    ? _prices[j,i]
                    : _prices[i,j];
                if (c < min1)
                {
                    min2 = min1;
                    min1 = c;
                    minP = i;
                }
                else if (c < min2)
                {
                    min2 = c;
                }
            }
            return new List<double>{ min2 - min1, min1, minP };
        }
        List<double> MaxPenalty(int len1, int len2, bool isRow)
        {
            double md = int.MinValue;
            double pc = -1;
            double pm = -1;
            double mc = -1;
            for (int i = 0; i < len1; i++)
            {
                if (isRow ? _rowDone[i] : _colDone[i])
                {
                    continue;
                }
                var res = Diff(i, len2, isRow);
                if (res[0] > md)
                {
                    md = res[0];    // max diff
                    pm = i;         // pos of max diff
                    mc = res[1];    // min cost
                    pc = res[2];    // pos of min cost
                }
            }
            return isRow
                ? new List<double> { pm, pc, mc, md }
                : new List<double> { pc, pm, mc, md };
        }

        List<double> NextCell()
        {
            var res1 = MaxPenalty(Reserves.Length, Needs.Length, true);
            var res2 = MaxPenalty(Needs.Length, Reserves.Length, false);

            if (res1[3] == res2[3])
            {
                return res1[2] < res2[2]
                    ? res1
                    : res2;
            }

            return res1[3] > res2[3]
                ? res2
                : res1;
        }

    }
}
