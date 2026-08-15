using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace TemplateApp.Tests;

/// <summary>
/// Proves the profiles are wired, not decorative: each one is booted and its
/// values read back. Without this, renaming a file or a section would break
/// production configuration silently.
/// </summary>
public class EnvironmentProfileTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> Booted(string environment) =>
        factory.WithWebHostBuilder(builder => builder.UseEnvironment(environment));

    private AppOptions OptionsFor(string environment) =>
        Booted(environment).Services.GetRequiredService<IOptions<AppOptions>>().Value;

    [Theory]
    [InlineData("Development", "development")]
    [InlineData("Staging", "staging")]
    [InlineData("Production", "production")]
    public async Task Healthz_ReportsTheActiveProfile(string environment, string expected)
    {
        var res = await Booted(environment).CreateClient().GetAsync("/healthz");

        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        Assert.Contains($"\"environment\":\"{expected}\"", await res.Content.ReadAsStringAsync());
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
    public void Staging_SitsBetweenTheOtherTwo()
    {
        var staging = OptionsFor("Staging");

        // Staging exists to be production-like while still debuggable, so it
        // is the one profile whose values must differ from BOTH neighbours.
        Assert.True(staging.VerboseErrors);
        Assert.Equal(30, staging.CacheSeconds);
    }
}
