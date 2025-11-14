using Mono.Cecil;

namespace Store.Tests;

internal class GenericCustomRule(Func<TypeDefinition, bool> typeCheck) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) => typeCheck(type);
}