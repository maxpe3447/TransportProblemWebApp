namespace TransportProblemLib
{
    public static class Validation
    {
        public static bool IsUpdate(ref double[] needs, ref double[] reserves)
        {
            double reservesSum = reserves.Sum();
            double needsSum = needs.Sum();

            if (reservesSum == needsSum)
            {
                return false;
            }
            if (reservesSum > needsSum)
            {
                UpdateArray(ref needs, needsSum, reservesSum);
            }
            else
            {
                UpdateArray(ref reserves, needsSum, reservesSum);
            }
            return true;
        }

        private static void UpdateArray(ref double[] lst, double value1, double value2)
        {
            Array.Resize(ref lst, lst.Length + 1);
            lst[lst.Length - 1] = Math.Abs(value1 - value1);
        }
    }
}
