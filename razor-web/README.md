# Template App

ASP.NET Core **Razor Pages** web app: server-rendered pages, a shared layout, static files and xUnit integration tests through `WebApplicationFactory`.

## Run

```sh
dotnet run --project src/TemplateApp
```

## Test

```sh
dotnet build
dotnet test
```

## Layout

```
src/TemplateApp/
  Program.cs              startup and routing
  Navigation.cs           the nav a page joins by existing (see below)
  Pages/
    Index.cshtml(.cs)     home
    Error.cshtml(.cs)     production error page
<!-- tpl:if contact_form -->
    Contact.cshtml(.cs)   sample form: binding, validation, antiforgery
<!-- tpl:endif -->
    Shared/_Layout.cshtml
  wwwroot/css/site.css
tests/TemplateApp.Tests/  one file per surface
```

## Adding a page

Drop `Pages/Thing.cshtml` and its `Thing.cshtml.cs` in, and it routes at `/Thing`. To put it in the navigation, add a class implementing `INavContributor` — the layout is never edited:

```csharp
public sealed class ThingNav : INavContributor
{
    public NavLink Link { get; } = new("/Thing", "Thing", 20);
}
```

`Navigation.Discover` picks it up at startup. That indirection is what lets optional pages be added and removed without touching shared files.

## Notes

- **Validation is server-side.** No client-side validation scripts are wired up, so the app has no external asset dependency; add `_ValidationScriptsPartial` if you want live feedback.
- **No HTTPS redirection** is configured — terminate TLS at your host or proxy, or add `app.UseHttpsRedirection()`.
<!-- tpl:if contact_form -->
- The contact page confirms receipt and sends nothing; wire `OnPost` to your mail or ticket system.
<!-- tpl:endif -->
<!-- tpl:if healthz -->
- `/healthz` answers `{"status":"ok"}` for liveness probes.
<!-- tpl:endif -->
