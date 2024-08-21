using FluentAssertions;
using NetArchTest.Rules;
using Ticky.Domain;

namespace Ticky.Tests.Architecture;

public class LayerTests
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOnApplication()
    {
        // Act
        var result = Types.InAssembly(DomainAssembly.Assembly)
            .Should()
            .NotHaveDependencyOnAll("Ticky.Api", "Ticky.Application", "Ticky.Infrastructure")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}