using System.Reflection;

namespace Store.Core.Shared;

public static class SharedLayer
{
    public static Assembly Assembly => typeof(SharedLayer).Assembly;
}