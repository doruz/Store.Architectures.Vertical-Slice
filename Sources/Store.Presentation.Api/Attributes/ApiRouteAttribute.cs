[AttributeUsage(AttributeTargets.Class)]
internal sealed class ApiRouteAttribute(string template) 
    : RouteAttribute($"api/{template}");