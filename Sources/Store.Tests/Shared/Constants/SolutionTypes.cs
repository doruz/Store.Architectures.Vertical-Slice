using Store.Core.Business;
using Store.Core.Domain;
using Store.Core.Shared;
using Store.Infrastructure.Persistence;

namespace Store.Tests;

internal static class SolutionTypes
{
    public static class Core
    {
        public static Types All => Types.InAssemblies(
        [
            SharedLayer.Assembly,
            DomainLayer.Assembly,
            BusinessLayer.Assembly
        ]);

        public static Types Shared => Types.InAssembly(SharedLayer.Assembly);
        public static Types Domain => Types.InAssembly(DomainLayer.Assembly);
        public static Types Business => Types.InAssembly(BusinessLayer.Assembly);
    }

    public static class Infrastructure
    {
        public static Types All => Types.InAssemblies(
        [
            PersistenceLayer.Assembly,
        ]);

        public static Types Persistence => Types.InAssembly(PersistenceLayer.Assembly);
    }

    public static class Presentation
    {
        public static Types All => Types.InAssemblies(
        [
            ApiLayer.Assembly,
        ]);

        public static Types Api => Types.InAssembly(ApiLayer.Assembly);
    }
}