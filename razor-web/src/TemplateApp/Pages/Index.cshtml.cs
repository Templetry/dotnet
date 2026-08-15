using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TemplateApp.Pages;

public class IndexModel : PageModel
{
    public string Message { get; private set; } = string.Empty;

    public DateTimeOffset GeneratedAt { get; private set; }

    public void OnGet()
    {
        Message = "Your Razor Pages app is running.";
        GeneratedAt = DateTimeOffset.UtcNow;
    }
}
