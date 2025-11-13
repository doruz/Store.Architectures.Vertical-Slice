using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace Store.Presentation.Api.IntegrationTests;

internal sealed class HttpResponseAssertions(HttpResponseMessage response)
{
    public AndConstraint<HttpResponseAssertions> HaveStatusCode(HttpStatusCode statusCode)
    {
        response.StatusCode.Should().Be(statusCode);

        return new AndConstraint<HttpResponseAssertions>(this);
    }

    public async Task<AndConstraint<ObjectAssertions>> ContainContentAsync<T>(T expectedValue)
    {
        var responseContent = await response.ContentAsAsync<T>();

        return responseContent.Should().BeEquivalentTo(expectedValue, options => options.WithStrictOrdering());
    }

    public async Task<AndConstraint<ObjectAssertions>> ContainContentAsync<T>(Func<T, T> expectedValue)
    {
        var responseContent = await response.ContentAsAsync<T>();

        return responseContent.Should().BeEquivalentTo(expectedValue(responseContent), options => options.WithStrictOrdering());
    }

    public Task<AndConstraint<HttpResponseAssertions>> ContainValidationErrorAsync(ValidationError validationError)
        => ContainValidationErrorAsync(validationError.ToKeyValuePair());

    private async Task<AndConstraint<HttpResponseAssertions>> ContainValidationErrorAsync(KeyValuePair<string, string[]> expectedError)
    {
        var responseContent = await response.ContentAsAsync<ValidationProblemDetails>();

        responseContent
            .Should()
            .NotBeNull();

        responseContent.Errors
            .Should()
            .ContainKey(expectedError.Key);

        responseContent.Errors.First(e => e.Key == expectedError.Key)
            .Should()
            .BeEquivalentTo(expectedError);

        return new AndConstraint<HttpResponseAssertions>(this);
    }

    public async Task<AndConstraint<StringAssertions>> ContainIdAsync()
    {
        var responseContent = await response.ContentAsAsync<EntityId>();

        responseContent.Should().NotBeNull();

        return responseContent.Id.Should()
            .NotBeNullOrEmpty()
            .And.NotBeNullOrWhiteSpace();
    }
}