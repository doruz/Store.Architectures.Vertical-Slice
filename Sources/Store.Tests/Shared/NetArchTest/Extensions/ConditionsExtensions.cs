using Mono.Cecil;

namespace Store.Tests;

internal static class ConditionsExtensions
{
    public static ConditionList ResideInFixedNamespace(this Conditions conditions, string @namespace)
        => conditions.ResideInNamespaceMatching($"^{@namespace}$");

    public static ConditionList MeetCustomRule(this Conditions conditions, Func<TypeDefinition, bool> typeCheck)
        => conditions.MeetCustomRule(new GenericCustomRule(typeCheck));
}