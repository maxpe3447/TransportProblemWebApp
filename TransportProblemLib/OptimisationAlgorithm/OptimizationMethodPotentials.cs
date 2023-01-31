using System.Drawing;
using TransportProblemLib.Abstruct;
using TransportProblemLib.OptimisationAlgorithm.Service;
using System.Linq;

namespace TransportProblemLib.OptimisationAlgorithm
{
    public class OptimizationMethodPotentials : TransportAlgorithm
    {

        private readonly double[,] _matrix;
        private readonly double[,] _prices;
        private Point[] _allowed;
        public OptimizationMethodPotentials(double[] reserves, double[] needs, double[,] supportPlan, double[,] prices)
            : base(reserves, needs)
        {
            _matrix = (double[,])supportPlan.Clone();
            _prices = (double[,])prices.Clone();
        }
        public override double[,] GetPlan()
        {
            var helpMatrix = CreateHelpMatrix();

            double[] U = new double[Reserves.Length];
            double[] V = new double[Needs.Length];

            FindUV(U, V, helpMatrix);

            var sMatrix = CreateSMatrix(helpMatrix, U, V);

            while (!IsAllPositive(sMatrix))
            {
                Roll(sMatrix);

                for (int i = 0; i < helpMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < helpMatrix.GetLength(1); j++)
                    {
                        if (_matrix[i, j] == double.PositiveInfinity)
                        {
                            helpMatrix[i, j] = _prices[i, j];
                            _matrix[i, j] = 0;
                            continue;
                        }
                        helpMatrix[i, j] = double.IsNaN(_matrix[i, j]) ? double.NaN : _prices[i, j];
                    }
                }

                FindUV(U, V, helpMatrix);
                sMatrix = CreateSMatrix(helpMatrix, U, V);
            }

            return _matrix;
        }
        private void Roll(double[,] sMatrix)
        {
            Point minInd = new Point();
            double min = double.MaxValue;
            int k = 0;
            _allowed = new Point[Reserves.Length + Needs.Length];

            for (int i = 0; i < Reserves.Length; i++)
                for (int j = 0; j < Needs.Length; j++)
                {
                    if (_matrix[i, j] == _matrix[i, j])
                    {
                        _allowed[k].X = i;
                        _allowed[k].Y = j;
                        k++;
                    }
                    // find max abs
                    if (sMatrix[i, j] < min)
                    {
                        min = sMatrix[i, j];
                        minInd.X = i;
                        minInd.Y = j;
                    }
                }
            //search cicle
            _allowed[_allowed.Length - 1] = minInd;
            Point[] Cycle = GetCycle(minInd.X, minInd.Y);
            double[] Cycles = new double[Cycle.Length];
            bool[] bCycles = new bool[Cycle.Length];
            for (int i = 0; i < bCycles.Length; i++)
                bCycles[i] = i == bCycles.Length - 1 ? false : true;
            min = double.MaxValue;

            // find min element
            for (int i = 0; i < Cycle.Length; i++)
            {
                Cycles[i] = _matrix[Cycle[i].X, Cycle[i].Y];
                if ((i % 2 == 0) && (Cycles[i] == Cycles[i]) && (Cycles[i] < min))
                {
                    min = Cycles[i];
                    minInd = Cycle[i];
                }
                if (Cycles[i] != Cycles[i]) Cycles[i] = 0;
            }
            // diff-add
            for (int i = 0; i < Cycle.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Cycles[i] -= min;
                    _matrix[Cycle[i].X, Cycle[i].Y] -= min;
                }
                else
                {
                    Cycles[i] += min;
                    if (double.IsNaN(_matrix[Cycle[i].X, Cycle[i].Y]))
                    {
                        _matrix[Cycle[i].X, Cycle[i].Y] = 0;
                    }
                    _matrix[Cycle[i].X, Cycle[i].Y] += min;
                }
            }
            _matrix[minInd.X, minInd.Y] = double.NaN;
        }

        private Point[] GetCycle(int x, int y)
        {
            Point Beg = new Point(x, y);
            FindWay fw = new FindWay(x, y, true, _allowed, Beg, null);
            fw.BuildTree();
            Point[] Way = Array.FindAll(_allowed, p => (p.X != -1) && (p.Y != -1));
            return Way;
        }

        private bool IsAllPositive(double[,] sMatrix) => Array.TrueForAll(sMatrix.Cast<double>().ToArray(), x => x > 0);

        private double[,] CreateHelpMatrix()
        {
            var matrix = new double[Reserves.Length, Needs.Length];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = (!double.IsNaN(_matrix[i, j])) ? _prices[i, j] : double.NaN;
                }
            }

            return matrix;
        }

        private bool IsAllTrue(bool[] lst) => Array.TrueForAll(lst, x => x);
        private void FindUV(double[] U, double[] V, double[,] helpMatr)
        {
            //to check if Ui Vi is calculated, we will use an array of booleans
            //even 2 arrays. in one indication of whether the corresponding potential has been calculated
            //in the second, did we go through the line / line of this potential
            //the algorithm will allow for a finite number of iterations to calculate all the potentials
            bool[] U1 = new bool[Reserves.Length];
            bool[] U2 = new bool[Reserves.Length];
            bool[] V1 = new bool[Needs.Length];
            bool[] V2 = new bool[Needs.Length];

            var FirstStep = (bool[] isCalc, bool[] isVisited, ref int index) =>
            {
                for (int i = isCalc.Length - 1; i >= 0; i--)
                {
                    if (isCalc[i] && !isVisited[i])
                    {
                        index = i;
                    }
                }
            };
            var SecondStep = (bool[] isCalc, bool[] isVisited, ref int index, double[] UorV) =>
            {
                for (int i = isCalc.Length - 1; i >= 0; i--)
                {
                    if (!isCalc[i] && !isVisited[i])
                    {
                        index = i;
                        UorV[index] = 0;
                        isCalc[index] = true;
                        break;
                    }
                }
            };

            // until all elements of arrays V1 and U1 are equal to true
            while (!(IsAllTrue(V1) && IsAllTrue(U1)))
            {
                int i = -1;
                int j = -1;

                FirstStep(V1, V2, ref i);
                FirstStep(U1, U2, ref j);

                if ((j == -1) && (i == -1))
                {
                    SecondStep(V1, V2, ref i, V);
                }
                if ((j == -1) && (i == -1))
                {
                    SecondStep(U1, U2, ref j, U);
                }

                if (i != -1)
                {
                    for (int j1 = 0; j1 < U1.Length; j1++)
                    {
                        if (!U1[j1]) U[j1] = helpMatr[j1, i] - V[i];
                        if (U[j1] == U[j1]) U1[j1] = true;
                    }
                    V2[i] = true;
                }

                if (j != -1)
                {
                    for (int i1 = 0; i1 < V1.Length; i1++)
                    {
                        if (!V1[i1]) V[i1] = helpMatr[j, i1] - U[j];
                        if (V[i1] == V[i1]) V1[i1] = true;
                    }
                    U2[j] = true;
                }

            }
        }

        private double[,] CreateSMatrix(double[,] helpMatrix, double[] U, double[] V)
        {
            double[,] HM = new double[U.Length, V.Length];

            for (int i = 0; i < U.Length; i++)
            {
                for (int j = 0; j < V.Length; j++)
                {
                    HM[i, j] = helpMatrix[i, j];
                    if (HM[i, j] != HM[i, j])
                        HM[i, j] = _prices[i, j] - (U[i] + V[j]);
                }
            }
            return HM;
        }
    }
}
