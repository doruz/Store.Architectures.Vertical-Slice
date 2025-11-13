using Mono.Cecil;

namespace Store.Tests;

internal sealed class PropertiesUseTypesFromRule(string @namespace) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
        => type.Properties.Any(prop => prop.PropertyType.Namespace.Contains(@namespace)) ||
           type.Fields.Any(field => field.DeclaringType.Namespace.Contains(@namespace));
}


internal static partial class CustomRules
{
    public static ConditionList HavePropertiesWithTypesFrom(this Conditions conditions, string @namespace)
        => conditions.MeetCustomRule(new PropertiesUseTypesFromRule(@namespace));
}