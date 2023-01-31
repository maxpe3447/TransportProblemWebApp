namespace TransportProblemWebApp.Model
{
    public class AlgorithmResultViewModel
    {
        public double[] SupportPlan { get; set; }
        public double[] OptimalPlan { get; set; }
        public int Row { get; set; }
        public int Coll { get; set; }

        public double SupportPlanSum { get; set; }
        public double OptimalPlanSum { get; set; }

    }
}
