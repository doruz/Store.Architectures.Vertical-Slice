using MediatR;

namespace Store.Tests.Solution;

public class CqrsPatternTests
{
    private const string Command = "Command";
    private const string CommandHandler = "CommandHandler";

    private const string Query = "Query";
    private const string QueryHandler = "QueryHandler";

    private readonly Types _allTypes = Types.FromPath(Directory.GetCurrentDirectory());

    [Fact]
    public void Cqrs_Should_FollowNamingConventions()
    {
        var policy = Policy
            .Define("CQRS", "Should follow naming conventions")

            .For(_allTypes)

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest))
                    .Should()
                    .HaveNameEndingWith(Command),
                "Commands",
                $"Names of types that implement {typeof(IRequest).FullName} should end with {Command}"
            )
            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequestHandler<>))
                    .Should()
                    .HaveNameEndingWith(CommandHandler),
                "Commands Handlers",
                $"Types that implement {typeof(IRequestHandler<>).FullName} should have name ending with {CommandHandler}"
            )

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest<>))
                    .Should()
                    .HaveNameMatching($"{Query}|{Command}$"),
                "Commands & Queries",
                $"Names of types that implement {typeof(IRequest<>).FullName} should end with {Query} or {Command}"
            )
            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequestHandler<,>))
                    .Should()
                    .HaveNameMatching($"{QueryHandler}$|{CommandHandler}$"),
                "Commands & Queries Handlers",
                $"Types that implement {typeof(IRequestHandler<,>).FullName} should have name ending with {QueryHandler} or {CommandHandler}"
            );

        policy.Evaluate().ShouldBeSuccessful();
    }

    [Fact]
    public void Cqrs_Should_FollowImplementationConventions()
    {
        var policy = Policy
            .Define("CQRS", "Should follow implementation conventions")

            .For(_allTypes)

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest)).Or().ImplementInterface(typeof(IRequest<>))
                    .Or()
                    .HaveNameMatching($"({Query}|{Command})Result$")
                    .Should()
                    .BePublic().And().BeRecords().And().HaveAllPropertiesWithInitOnly(),
                "Commands & Queries",
                $"Types that implement {typeof(IRequest).FullName} should be public immutable records"
            )

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequestHandler<>)).Or().ImplementInterface(typeof(IRequestHandler<,>))
                    .Should()
                    .NotBePublic().And().BeSealed().And().BeClasses().And().NotBeRecords(),
                "Commands & Queries Handlers",
                $"Types that implement {typeof(IRequestHandler<>).FullName} should be non-public sealed classes"
            );

        policy.Evaluate().ShouldBeSuccessful();
    }

    [Fact]
    public void Cqrs_Should_NotExposeDomainTypes()
    {
        var policy = Policy
            .Define("CQRS", "Should not expose business domain types")

            .For(_allTypes)

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest)).Or().ImplementInterface(typeof(IRequest<>))
                    .Or()
                    .HaveNameMatching($"({Query}|{Command})Result$")
                    .Or()
                    .ImplementInterface(typeof(IRequestHandler<>)).Or().ImplementInterface(typeof(IRequestHandler<,>))
                    .ShouldNot()
                    .HavePropertiesWithTypesFrom(SolutionNamespaces.Core.Domain),
                "Commands & Queries",
                "Types should not expose domain types through their properties."
            )

            .Add(types => types
                    .That()
                    .ImplementInterface(typeof(IRequest)).Or().ImplementInterface(typeof(IRequest<>))
                    .Or()
                    .ImplementInterface(typeof(IRequestHandler<>)).Or().ImplementInterface(typeof(IRequestHandler<,>))
                    .ShouldNot()
                    .UseTypesOnPublicMethodsFrom(SolutionNamespaces.Core.Domain),
                "Commands & Queries",
                "Types should not expose domain types through their public methods."
            );

        policy.Evaluate().ShouldBeSuccessful();
    }
}