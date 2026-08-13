using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TemplateApp.Tests;

public class ApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Healthz_ReturnsOk()
    {
        var res = await _client.GetAsync("/healthz");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        Assert.Contains("ok", await res.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Hello_GreetsByName()
    {
        var res = await _client.GetAsync("/api/hello/DotNet");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        Assert.Contains("Hello, DotNet!", await res.Content.ReadAsStringAsync());
    }
}
