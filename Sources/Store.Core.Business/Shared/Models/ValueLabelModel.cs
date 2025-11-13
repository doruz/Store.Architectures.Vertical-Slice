namespace Store.Core.Business.Shared;

public record ValueLabelModel<T>(T Value, string Label)
{
    public override string ToString() => Label;
}