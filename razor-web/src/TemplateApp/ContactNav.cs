namespace TemplateApp;

/// <summary>
/// Puts the contact page in the navigation. This file is the whole wiring:
/// removing the feature removes the page and its link together, and no shared
/// file has to be edited either way.
/// </summary>
public sealed class ContactNav : INavContributor
{
    public NavLink Link { get; } = new("/Contact", "Contact", 10);
}
