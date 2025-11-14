global using Store.Core.Shared;
using System.Reflection;

namespace Store.Core.Domain;

public static class DomainLayer
{
    public static Assembly Assembly => typeof(DomainLayer).Assembly;
}