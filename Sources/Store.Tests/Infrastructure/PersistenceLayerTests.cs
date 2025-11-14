using Store.Infrastructure.Persistence;

namespace Store.Tests.Infrastructure;

public class PersistenceLayerTests
{
    [Fact]
    public void AllTypes_Should_ShouldBeExposedOnlyThroughAbstractions()
    {
        var result = SolutionTypes.Infrastructure.Persistence
            .That()
            .DoNotHaveName(nameof(PersistenceLayer))
            .ShouldNot()
            .BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AllRepositories_Should_HaveNameEndingWithRepository()
    {
        var result = SolutionTypes.Infrastructure.Persistence
            .That()
            .ImplementInterfacesEndingWithName("Repository")
            .Should()
            .HaveNameEndingWith("Repository")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void CosmosPersistence_Should_ShouldResideInNamespaceAndFollowNamingConvention()
    {
        var result = SolutionTypes.Infrastructure.Persistence
            .That()
            .HaveDependencyOn("Microsoft.Azure.Cosmos")
            .Should()
            .ResideInFixedNamespace($"{SolutionNamespaces.Infrastructure.Persistence}.Cosmos")
            .And().HaveNameStartingWith("Cosmos")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void InMemoryPersistence_Should_ShouldResideInNamespace()
    {
        var result = SolutionTypes.Infrastructure.Persistence
            .That()
            .HaveNameStartingWith("InMemory")
            .Should()
            .ResideInFixedNamespace($"{SolutionNamespaces.Infrastructure.Persistence}.InMemory")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}