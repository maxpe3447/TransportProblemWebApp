using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TransportProblemLib.Abstruct;

namespace TransportProblemLib.SupportPlanAlgorithm
{
    public class DoublePreference : TransportAlgorithm
    {
        private double[,] _prices;

        private enum AstericsType
        {
            ONE = 1,
            TWO = 2
        }
        public DoublePreference(double[] reserves, double[] needs, double[,] prices) : base(reserves, needs)
        {
            _prices = (double[,])prices.Clone();
        }

        public override double[,] GetPlan()
        {
            byte[,] markers = new byte[_prices.GetLength(0), _prices.GetLength(1)];
            double[,]plan = new double[_prices.GetLength(0), _prices.GetLength(1)];
            //search asterics
            MarkMinElementInRow(markers);
            MarkMinElementInCol(markers);

            CalcAstericsFor(markers, AstericsType.TWO, plan);
            CalcAstericsFor(markers, AstericsType.ONE, plan);

            while (true)
            {
                var reserv = Reserves.FirstOrDefault(x => x != 0);
                var need = Needs.FirstOrDefault(x => x != 0);
                if (reserv == default || need == default)
                {
                    break;
                }

                int indexOfReserv = Reserves.ToList().IndexOf(reserv);
                int indexOfNeed = Needs.ToList().IndexOf(need);

                double min = Math.Min(reserv, need);
                Reserves[indexOfReserv] -= min;
                Needs[indexOfNeed] -= min;
                plan[indexOfReserv, indexOfNeed] += min;

            }

            NullToNan(plan);
            return plan;
        }

        private void MarkMinElementInRow(byte[,] markers)
        {
            for (int i = 0; i < _prices.GetLength(0); i++)
            {
                double min = double.MaxValue;
                for (int j = 0; j < _prices.GetLength(1); j++)
                {
                    if (_prices[i, j] < min)
                    {
                        min = _prices[i, j];
                    }
                }

                for (int j = 0; j < _prices.GetLength(1); j++)
                {
                    if (_prices[i, j].Equals(min))
                    {
                        markers[i, j]++;
                    }
                }
            }
        }
        private void MarkMinElementInCol(byte[,] markers)
        {
            for (int i = 0; i < _prices.GetLength(1); i++)
            {
                double min = double.MaxValue;
                for (int j = 0; j < _prices.GetLength(0); j++)
                {
                    if (_prices[j, i] < min)
                    {
                        min = _prices[j, i];
                    }
                }

                for (int j = 0; j < _prices.GetLength(0); j++)
                {
                    if (_prices[j, i].Equals(min))
                    {
                        markers[j, i]++;
                    }
                }
            }
        }

        private void CalcAstericsFor(byte[,] markers, AstericsType asterics, double[,]plan)
        {
            for (int i = 0; i < markers.GetLength(0); i++)
            {
                for (int j = 0; j < markers.GetLength(1); j++)
                {
                    if (markers[i, j] == (int)asterics)
                    {
                        var min = Math.Min(Reserves[i], Needs[j]);
                        Reserves[i] -= min;
                        Needs[j]-= min;
                        plan[i, j] += min;
                    }
                }
            }
        }
    }
}
