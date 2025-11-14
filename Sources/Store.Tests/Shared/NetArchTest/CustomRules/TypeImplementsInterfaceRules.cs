using Mono.Cecil;

namespace Store.Tests;

internal static partial class TypeImplementsInterfaceRules
{
    public static ConditionList ImplementInterfaces(this Conditions conditions)
        => conditions.MeetCustomRule(type => type is { HasInterfaces: true, IsInterface: false });

    public static PredicateList ImplementInterfacesEndingWithName(this Predicates predicates, string nameSuffix)
        => predicates.MeetCustomRule(type => type.IsImplementingInterfaceEndingWithName(nameSuffix));

    private static bool IsImplementingInterfaceEndingWithName(this TypeDefinition type, string nameSuffix) 
        => type.Interfaces.Any(@interface => @interface.InterfaceType.Name.EndsWith(nameSuffix));
}