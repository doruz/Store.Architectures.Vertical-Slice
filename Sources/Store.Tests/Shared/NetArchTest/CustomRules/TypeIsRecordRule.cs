using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace Store.Tests;

internal static class TypeIsRecordRule
{
    public static ConditionList BeRecords(this Conditions conditions)
        => conditions.MeetCustomRule(IsRecord);

    public static ConditionList NotBeRecords(this Conditions conditions)
        => conditions.MeetCustomRule(type => IsRecord(type) is false);

    public static PredicateList AreRecords(this Predicates predicates)
        => predicates.MeetCustomRule(IsRecord);

    private static bool IsRecord(TypeDefinition type)
        => type
            .GetMethods()
            .Any(m => m.Name == "<Clone>$");
}