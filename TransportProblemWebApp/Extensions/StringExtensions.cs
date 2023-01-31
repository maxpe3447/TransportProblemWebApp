namespace TransportProblemWebApp.Extensions
{
    public static class StringExtensions
    {
        public static string CutController(this string s)
        {
            return s.Replace("Controller", string.Empty);
        }
    }
}
