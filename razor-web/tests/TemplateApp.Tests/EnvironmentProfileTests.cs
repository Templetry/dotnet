using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace TemplateApp.Tests;

/// <summary>
/// Proves the profiles are wired, not decorative: each one is booted and its
/// values read back from the container.
///
/// Deliberately not through an endpoint — /healthz is its own feature here,
/// and a test that needed both would only exist in the combination where
/// both are on.
/// </summary>
public class EnvironmentProfileTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private AppOptions OptionsFor(string environment) =>
        factory.WithWebHostBuilder(builder => builder.UseEnvironment(environment))
            .Services.GetRequiredService<IOptions<AppOptions>>().Value;

    [Theory]
    [InlineData("Development", "development")]
    [InlineData("Staging", "staging")]
    [InlineData("Production", "production")]
    public void EachProfileDeclaresItsOwnName(string environment, string expected)
    {
        Assert.Equal(expected, OptionsFor(environment).Environment);
    }

    [Fact]
    public void Development_KeepsDetailOnAndCachingOff()
    {
        var options = OptionsFor("Development");

        Assert.True(options.VerboseErrors);
        Assert.Equal(0, options.CacheSeconds);
    }

    [Fact]
    public void Production_TurnsDetailOffAndCachesLongest()
    {
        var options = OptionsFor("Production");

        Assert.False(options.VerboseErrors);
        Assert.Equal(300, options.CacheSeconds);
    }

    [Fact]
    public void Staging_DiffersFromBothNeighbours()
    {
        var staging = OptionsFor("Staging");

        Assert.True(staging.VerboseErrors);
        Assert.Equal(30, staging.CacheSeconds);
    }
}
