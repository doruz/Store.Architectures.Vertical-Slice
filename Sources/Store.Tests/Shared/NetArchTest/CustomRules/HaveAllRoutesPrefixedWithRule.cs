using Mono.Cecil;
using Mono.Cecil.Rocks;
using Store.Core.Shared;

namespace Store.Tests;

internal sealed class HaveAllRoutesPrefixedWithRule(string prefix) : ICustomRule
{
    public bool MeetsRule(TypeDefinition type) 
        => IsControllerRouteValid(type) && AreActionsRoutesValid(type);

    private bool IsControllerRouteValid(TypeDefinition type)
    {
        var controllerRouteAttribute = type.CustomAttributes
            .FilterByType<ApiRouteAttribute>()
            .First();

        return GetRouteTemplate(controllerRouteAttribute).StartsWith($"{prefix}/");
    }

    private bool AreActionsRoutesValid(TypeDefinition type)
    {
        return type.GetMethods()
            .Where(method => method.IsPublic is true)
            .SelectMany(method => method.CustomAttributes.FilterByType("Microsoft.AspNetCore.Mvc.Http"))
            .Select(GetRouteTemplate)
            .All(IsActionRouteValid);

        bool IsActionRouteValid(string routeTemplate) =>
            routeTemplate == string.Empty ||
            routeTemplate.StartsWith($"/api/{prefix}/") ||
            routeTemplate.StartsWith("/") is false;
    }

    private static string GetRouteTemplate(CustomAttribute attribute)
    {
        if (attribute.ConstructorArguments.IsEmpty())
        {
            return string.Empty;
        }

        return (string)attribute.ConstructorArguments[0].Value;
    }
}


internal static partial class CustomRules
{
    public static ConditionList HaveAllRoutesPrefixedWith(this Conditions conditions, string prefix)
        => conditions.MeetCustomRule(new HaveAllRoutesPrefixedWithRule(prefix));
}