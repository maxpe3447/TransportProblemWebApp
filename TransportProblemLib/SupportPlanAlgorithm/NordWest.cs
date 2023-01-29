using TransportProblemLib.Abstruct;

namespace TransportProblemLib.SupportPlanAlgorithm
{
    public class NordWest : TransportAlgorithm
    {
        public NordWest(double[] reserves, double[] needs) : base(reserves, needs)
        {

        }
        public override double[,] GetPlan()
        {
            var resultMatrix = new double[Reserves.Length, Needs.Length];

            NullToNan(resultMatrix);

            CalcLoop(resultMatrix);

            return resultMatrix;
        }

        private bool IsEmpty(double[] arr) => Array.TrueForAll(arr, x => x == 0);

        private void CalcLoop(double[,] resultMatrix)
        {
            int i = 0, j = 0;
            while (!IsEmpty(Needs) && !IsEmpty(Reserves))
            {
                var dif = Math.Min(Reserves[i], Needs[j]);
                resultMatrix[i, j] = dif;
                Reserves[i] -= dif;
                Needs[j] -= dif;

                if (Reserves[i] == 0 && Needs[j] == 0 && j + 1 < Needs.Length)
                {
                    resultMatrix[i, j + 1] = 0;
                }
                if (Reserves[i] == 0)
                {
                    i++;
                }
                if (Needs[j] == 0)
                {
                    j++;
                }

                if (i >= Reserves.Length || j >= Needs.Length)
                {
                    break;
                }
            }
        }
    }
}
