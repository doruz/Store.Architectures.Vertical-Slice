using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Store.Tests;

internal sealed class PublicMethodsReturnRule<T>() : ICustomRule
{
    private readonly string[] _allowedReturnTypes =
    [
        typeof(T).FullName!,
        $"System.Threading.Tasks.Task`1<{typeof(T).FullName}>"
    ];

    public bool MeetsRule(TypeDefinition type)
        => type.GetMethods()
            .Where(method => method.IsPublic == true)
            .All(MethodIsReturningExpectedType);

    private bool MethodIsReturningExpectedType(MethodDefinition method)
        => _allowedReturnTypes.Contains(method.ReturnType.FullName);
}


internal static partial class CustomRules
{
    public static ConditionList PublicMethodsReturn<T>(this Conditions conditions)
        => conditions.MeetCustomRule(new PublicMethodsReturnRule<T>());
}
