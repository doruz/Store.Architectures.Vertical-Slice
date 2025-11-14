using Store.Core.Shared;

namespace Store.Tests.Solution;

public class GeneralArchitectureTests
{
    private readonly Types _allTypes = Types.FromPath(Directory.GetCurrentDirectory());

    [Fact]
    public void TypeExtensions_Should_BeStatic()
    {
        var result = _allTypes
            .That()
            .ResideInNamespaceMatching("Store.*").And().HaveNameEndingWith("Extensions")
            .Should()
            .BeStatic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Interfaces_Should_BePublicAndFollowNamingConvention()
    {
        var result = _allTypes
            .That()
            .ResideInNamespaceMatching("Store.*").And().AreInterfaces()
            .Should()
            .BePublic().And().HaveNameStartingWith("I")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AppInitializers_Should_FollowNamingConvention()
    {
        var result = _allTypes
            .That()
            .ImplementInterface(typeof(IAppInitializer))
            .Should()
            .HaveNameEndingWith("Initializer")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void AppInitializers_Should_NotBeExposedExternally()
    {
        var result = _allTypes
            .That()
            .ImplementInterface(typeof(IAppInitializer))
            .Should()
            .NotBePublic().And().BeSealed()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}