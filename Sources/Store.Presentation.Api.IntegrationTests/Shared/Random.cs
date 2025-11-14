
using EnsureThat;

namespace Store.Presentation.Api.IntegrationTests;

internal static class Random
{
    private static readonly System.Random SystemRandom = System.Random.Shared;

    public static class Numbers
    {
        public static int Generate(int min, int max)
        {
            EnsureArg.IsLte(min, max);

            return SystemRandom.Next(min, max);
        }
    }

    public static class Strings
    {
        private const int MinAscii = 32;
        private const int MaxAscii = 126;

        public static string Generate(int minLength, int maxLength)
        {
            EnsureArg.IsLte(minLength, maxLength);

            return Generate(SystemRandom.Next(minLength, maxLength));
        }

        public static string Generate(int length)
        {
            string result = "";

            for (int i = 1; i <= length; i++)
            {
                result += GetRandomCharacter();
            }

            return result;
        }

        private static char GetRandomCharacter()
            => (char)SystemRandom.Next(MinAscii, MaxAscii);
    }
}