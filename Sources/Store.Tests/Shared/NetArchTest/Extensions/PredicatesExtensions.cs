using Mono.Cecil;

namespace Store.Tests;

internal static class PredicatesExtensions
{
    public static PredicateList MeetCustomRule(this Predicates conditions, Func<TypeDefinition, bool> typeCheck) 
        => conditions.MeetCustomRule(new GenericCustomRule(typeCheck));
}