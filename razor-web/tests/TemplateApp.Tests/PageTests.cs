using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TemplateApp.Tests;

public class PageTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Index_RendersThroughTheLayout()
    {
        var res = await _client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);

        var html = await res.Content.ReadAsStringAsync();
        Assert.Contains("Template App", html);
        Assert.Contains("Your Razor Pages app is running.", html);
        Assert.Contains("/css/site.css", html);
    }

    [Fact]
    public async Task Navigation_AlwaysContainsHome()
    {
        var html = await _client.GetStringAsync("/");
        Assert.Contains(">Home</a>", html);
    }

    [Fact]
    public async Task UnknownPath_Returns404()
    {
        var res = await _client.GetAsync("/no-such-page");
        Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
    }
}
