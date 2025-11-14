using Mono.Cecil;
using Mono.Collections.Generic;

namespace Store.Tests;

internal static class TypeDefinitionExtensions
{
    public static IEnumerable<CustomAttribute> FilterByType(this Collection<CustomAttribute> attributes, string type) 
        => attributes.Where(a => a.AttributeType.FullName.StartsWith(type));

    public static IEnumerable<CustomAttribute> FilterByType<TAttribute>(this Collection<CustomAttribute> attributes) 
        => attributes.Where(a => a.AttributeType.Name == typeof(TAttribute).Name);
}