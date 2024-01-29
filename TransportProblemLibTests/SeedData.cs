namespace TransportProblemLibTests;

internal static class SeedData
{
    public static double[,] Prices { get; }=
        {
            { 4,3,4,11,9 },
            { 3,4,7,15,8 },
            { 7,4,2,8,15}
        };

    public static double[] Reserves { get; } = { 40, 30, 10 };
    public static double[] Needs { get; } = { 20, 21, 7, 24, 8 };
}
