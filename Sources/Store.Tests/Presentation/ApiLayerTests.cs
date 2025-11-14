using Microsoft.AspNetCore.Mvc;

namespace Store.Tests.Presentation;

public class ApiLayerTests
{
    [Fact]
    public void ApiControllers_Should_NotDependOnDomain()
    {
        var result = SolutionTypes.Presentation.Api
            .That()
            .Inherit(typeof(ControllerBase))
            .ShouldNot()
            .HaveDependencyOn(SolutionNamespaces.Core.Domain)
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ApiControllersActions_Should_ReturnIActionResult()
    {
        var result = SolutionTypes.Presentation.Api
            .That()
            .Inherit(typeof(ControllerBase))
            .Should()
            .PublicMethodsReturn<IActionResult>()
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ApiControllers_Should_FollowRoutesConventions()
    {
        var policy = Policy
            .Define("API routes", "Should follow route conventions")

            .For(SolutionTypes.Presentation.Api)

            .Add(types => types
                    .That()
                    .Inherit(typeof(ControllerBase)).And().HaveNameStartingWith("Admin")
                    .Should()
                    .HaveAllRoutesPrefixedWith("admins"),
                "Admin controllers",
                "Admin routes should be prefixed with /api/admins/"
            )

            .Add(types => types
                    .That()
                    .Inherit(typeof(BaseApiController)).And().HaveNameStartingWith("Customer")
                    .Should()
                    .HaveAllRoutesPrefixedWith("customers/current"),
                "Customer controllers",
                "Customer routes should be prefixed with /api/customers/current/"
            );

        policy.Evaluate().ShouldBeSuccessful();
    }
}