namespace Store.Tests;

internal static class SolutionNamespaces
{
    public static class Core
    {
        public const string All = "Store.Core";

        public const string Shared = $"{All}.Shared";
        public const string Domain = $"{All}.Domain";
        public const string Business = $"{All}.Business";
    }

    public static class Infrastructure
    {
        public const string All = "Store.Infrastructure";

        public const string Persistence = $"{All}.Persistence";
    }

    public static class Presentation
    {
        public const string All = "Store.Presentation";

        public const string Api = $"{All}.Api";
    }
}