using Store.Core.Domain.Entities;

namespace Store.Tests.Core;

public class DomainLayerTests
{
    [Fact]
    public void DomainEntitiesAndValues_Should_BePublicOnFixedNamespace()
    {
        var result = SolutionTypes.Core.Domain
            .That()
            .Inherit(typeof(BaseEntity)).Or().AreRecords()
            .Should()
            .ResideInFixedNamespace($"{SolutionNamespaces.Core.Domain}.Entities").And().BePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainEntitiesAndValues_Should_NotHavePropertiesSettersExposedExternally()
    {
        var result = SolutionTypes.Core.Domain
            .That()
            .ResideInNamespace($"{SolutionNamespaces.Core.Domain}.Entities").And().DoNotHaveName(nameof(BaseEntity))
            .Should()
            .HaveAllPropertiesWithoutPublicSetters()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void DomainValues_Should_BePublicImmutableRecords()
    {
        var result = SolutionTypes.Core.Domain
            .That()
            .AreRecords()
            .Should()
            .BePublic().And().HaveAllPropertiesWithInitOnly()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void RepositoriesInterfaces_Should_ResideInCorrectNamespace()
    {
        var result = SolutionTypes.Core.Domain
            .That()
            .HaveNameEndingWith("Repository").Or().HaveNameStartingWith("Repositories")
            .Should()
            .ResideInFixedNamespace($"{SolutionNamespaces.Core.Domain}.Repositories")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}