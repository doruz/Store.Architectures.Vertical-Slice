namespace Store.Presentation.Api.IntegrationTests;

internal abstract class ValidationTheoryData<T> : TheoryData<T, ValidationError>
{
    protected abstract T ValidModel { get; }

    protected void Add(T model, string property, params string[] messages)
        => Add(model, new ValidationError(property, messages));
}