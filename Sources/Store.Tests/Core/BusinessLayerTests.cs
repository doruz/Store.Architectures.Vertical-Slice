namespace Store.Tests.Core;

public class BusinessLayerTests
{
    [Fact]
    public void BusinessServices_Should_BePublicSealedClasses()
    {
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith("Service")
            .Should()
            .BePublic().And().BeSealed().And().BeClasses().And().NotBeRecords()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessServices_Should_NotExposeExtraLayerOfAbstraction()
    {
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith("Service")
            .ShouldNot()
            .ImplementInterfaces()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessModels_Should_BePublicImmutableRecords()
    {
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith("Model")
            .Should()
            .BePublic().And().BeRecords().And().HaveAllPropertiesWithInitOnly()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void BusinessLayer_Should_NotExposeDomainTypes()
    {
        var policy = Policy
            .Define("Business Layer", "Should not expose business domain types")

            .For(SolutionTypes.Core.Business)

            .Add(types => types
                    .That()
                    .HaveNameEndingWith("Model")
                    .ShouldNot()
                    .HavePropertiesWithTypesFrom(SolutionNamespaces.Core.Domain),
                "Business models",
                "Models should not expose domain types through their properties."
            )

            .Add(types => types
                    .That()
                    .HaveNameEndingWith("Service")
                    .ShouldNot()
                    .UseTypesOnPublicMethodsFrom(SolutionNamespaces.Core.Domain),
                "Business services",
                "Services should not expose domain types through their public methods."
            );

        policy.Evaluate().ShouldBeSuccessful();
    }

    [Fact]
    public void ErrorsAndMappers_Should_StaticInternalClasses()
    {
        var result = SolutionTypes.Core.Business
            .That()
            .HaveNameEndingWith(@"\b\w+(Mapper|Errors)\b")
            .Should()
            .BeStatic().And().NotBePublic()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }
}