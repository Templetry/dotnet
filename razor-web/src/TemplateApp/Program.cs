using TemplateApp;

// Template App — ASP.NET Core Razor Pages.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

// tpl:if healthz
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));
// tpl:endif

// Pages contribute their own nav entries; the layout renders whatever is found.
Navigation.Discover(typeof(Program).Assembly);

app.Run();

// Exposed so the test project's WebApplicationFactory can boot this app.
public partial class Program;
