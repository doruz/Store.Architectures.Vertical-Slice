using Store.Core.Shared;

namespace Store.Tests.Solution;

public class GeneralArchitectureTests
{
    private static readonly Types AllTypes = Types.FromPath(Directory.GetCurrentDirectory());

    [Fact]
    public void TypeExtensions_Should_BeStatic()
    {
        var result = AllTypes
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
        var result = AllTypes
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
        var result = AllTypes
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
        var result = AllTypes
            .That()
            .ImplementInterface(typeof(IAppInitializer))
            .Should()
            .NotBePublic().And().BeSealed()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}