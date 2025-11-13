using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Store.Tests;

internal sealed class PublicMethodsDependencyRule(string @namespace) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
        => type.GetMethods()
            .Where(method => method.IsPublic == true)
            .Any(method => method.FullName.Contains(@namespace));
}


internal static partial class CustomRules
{
    public static ConditionList UseTypesOnPublicMethodsFrom(this Conditions conditions, string @namespace)
        => conditions.MeetCustomRule(new PublicMethodsDependencyRule(@namespace));
}