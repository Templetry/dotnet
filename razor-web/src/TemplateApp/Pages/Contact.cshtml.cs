using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TemplateApp.Pages;

public class ContactModel : PageModel
{
    [BindProperty]
    public ContactForm Form { get; set; } = new();

    public bool Sent { get; private set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Nothing leaves the process: wire this to your mail or ticket system.
        Sent = true;

        // Clear both the bound values and the model state so the tag helpers
        // render an empty form again instead of echoing what was just sent.
        ModelState.Clear();
        Form = new ContactForm();

        return Page();
    }
}

public class ContactForm
{
    [Required]
    [StringLength(80, MinimumLength = 2)]
    [Display(Name = "Your name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(2000, MinimumLength = 10)]
    [Display(Name = "Message")]
    public string Message { get; set; } = string.Empty;
}
