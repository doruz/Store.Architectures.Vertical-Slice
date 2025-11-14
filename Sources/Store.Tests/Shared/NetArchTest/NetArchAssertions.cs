namespace Store.Tests;

internal static class NetArchAssertions
{
    public static void ShouldBeSuccessful(this PolicyResults policyResults)
    {
        policyResults.Results
            .Should()
            .OnlyContain(result => result.IsSuccessful == true);
    }
}