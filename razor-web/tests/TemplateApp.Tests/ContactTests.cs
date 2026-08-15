using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TemplateApp.Tests;

public class ContactTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Contact_IsLinkedFromTheNavigation()
    {
        var html = await _client.GetStringAsync("/");
        Assert.Contains(">Contact</a>", html);
    }

    [Fact]
    public async Task Post_WithAnEmptyMessage_RedisplaysTheFormWithAnError()
    {
        var token = await AntiforgeryTokenAsync();

        var res = await _client.PostAsync("/Contact",
            FormFields(token, name: "Ada Lovelace", email: "ada@example.com", message: ""));

        Assert.Equal(HttpStatusCode.OK, res.StatusCode);

        var html = await res.Content.ReadAsStringAsync();
        Assert.DoesNotContain("your message was received", html);
        Assert.Contains("The Message field is required.", html);
    }

    [Fact]
    public async Task Post_WithAValidForm_Confirms()
    {
        var token = await AntiforgeryTokenAsync();

        var res = await _client.PostAsync("/Contact",
            FormFields(token, name: "Ada Lovelace", email: "ada@example.com",
                message: "The engine weaves algebraic patterns."));

        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        Assert.Contains("your message was received", await res.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Post_WithoutTheAntiforgeryToken_IsRejected()
    {
        var res = await _client.PostAsync("/Contact",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["Form.Name"] = "Ada Lovelace",
                ["Form.Email"] = "ada@example.com",
                ["Form.Message"] = "The engine weaves algebraic patterns.",
            }));

        Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
    }

    /// The client keeps the antiforgery cookie; only the form field has to be
    /// carried over from the GET.
    private async Task<string> AntiforgeryTokenAsync()
    {
        var html = await _client.GetStringAsync("/Contact");
        var match = Regex.Match(html, """name="__RequestVerificationToken"[^>]*value="([^"]+)""");
        Assert.True(match.Success, "no antiforgery token found in the contact form");
        return match.Groups[1].Value;
    }

    private static FormUrlEncodedContent FormFields(string token, string name, string email, string message) =>
        new(new Dictionary<string, string>
        {
            ["__RequestVerificationToken"] = token,
            ["Form.Name"] = name,
            ["Form.Email"] = email,
            ["Form.Message"] = message,
        });
}
