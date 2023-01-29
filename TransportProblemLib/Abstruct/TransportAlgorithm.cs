namespace TransportProblemLib.Abstruct
{
    public abstract class TransportAlgorithm
    {
        public TransportAlgorithm(double[] reserves, double[] needs)
        {
            Reserves = (double[])reserves.Clone();
            Needs = (double[])needs.Clone();
        }

        public virtual double[] Reserves { get; protected set; }
        public virtual double[] Needs { get; protected set; }
        public abstract double[,] GetPlan();

        protected void NullToNan(double[,] resultMatrix)
        {
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    if (resultMatrix[i, j] == 0)
                    {
                        resultMatrix[i, j] = double.NaN;
                    }
                }
            }
        }
    }
}
