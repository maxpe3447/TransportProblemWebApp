using System.ComponentModel.DataAnnotations;
using TransportProblemLib.Enums;

namespace TransportProblemWebApp.Model
{
    public class MatrixViewModel
    {
        public int Row { get; set; } = 2;
        public int Coll { get; set; } = 2;
        public double[] Array { get; set; } = new double[4];
        public double[] Needs { get; set; } = new double[2];
        public double[] Reserves { get; set; } = new double[2];

        [Required]
        [Display(Name = "Метод знаходження першого опорного плану")]
        public AlgorithmType SupportPlanType{ get; set; }
        [Required]
        [Display(Name = "Метод покращення опорного плану")]
        public AlgorithmType OptimisationType{ get; set; }
        public double[,] GetMatrix()
        {
            var matrix = new double[Row, Coll];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Coll; j++)
                {
                    matrix[i, j] = Array[i * Coll + j];
                }
            }
            return matrix;
        }
        public void RemoveColl()
        {
            if (Coll == 2) return;

            var matrix = GetMatrix();

            Coll--;
            var needs = (double[])Needs.Clone();
            System.Array.Resize(ref needs, Coll);
            Needs = needs;
            Resize(matrix);

        }
        public void RemoveRow()
        {
            if (Row == 2) return;

            var matrix = GetMatrix();

            Row--;
            var reserves = (double[])Reserves.Clone();
            System.Array.Resize(ref reserves, Row);
            Reserves = reserves;
            Resize(matrix);

        }
        public void AddColl()
        {
            var matrix = GetMatrix();

            Coll++;
            var needs = (double[])Needs.Clone();
            System.Array.Resize(ref needs, Coll);
            Needs = needs;
            Resize(matrix);

        }
        public void AddRow()
        {
            var matrix = GetMatrix();

            Row++;
            var reserves = (double[])Reserves.Clone();
            System.Array.Resize(ref reserves, Row);
            Reserves = reserves;
            Resize(matrix);
        }
        public void Resize(double[,] matrix)
        {
            var arr = Array;

            System.Array.Resize(ref arr, Row * Coll);
            System.Array.Fill(arr, 0);

            int row = Math.Min(matrix.GetLength(0), Row);
            int coll = Math.Min(matrix.GetLength(1), Coll);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < coll; j++)
                {
                    arr[i * Coll + j] = matrix[i, j];
                }
            }
            Array = arr;
        }
    }
}
