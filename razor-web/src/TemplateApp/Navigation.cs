using System.Reflection;

namespace TemplateApp;

/// <summary>A link rendered in the shared layout's navigation.</summary>
/// <param name="Page">Razor page route, e.g. <c>/Contact</c>.</param>
/// <param name="Text">Label shown to the user.</param>
/// <param name="Order">Lower comes first; Home is 0.</param>
public sealed record NavLink(string Page, string Text, int Order = 100);

/// <summary>
/// Implement this on any public class with a parameterless constructor to put a
/// page in the navigation. Nothing else has to be edited — not the layout, not
/// startup — which is what lets optional pages be added or removed cleanly.
/// </summary>
public interface INavContributor
{
    NavLink Link { get; }
}

/// <summary>The navigation the layout renders.</summary>
public static class Navigation
{
    private static readonly NavLink Home = new("/Index", "Home", 0);

    private static IReadOnlyList<NavLink> _links = [Home];

    public static IReadOnlyList<NavLink> Links => _links;

    /// <summary>
    /// Rebuilds the navigation from every <see cref="INavContributor"/> in the
    /// assembly. Idempotent: calling it again replaces the list rather than
    /// appending to it, so repeated host starts (as in tests) stay clean.
    /// </summary>
    public static void Discover(Assembly assembly)
    {
        var contributed = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                        && typeof(INavContributor).IsAssignableFrom(t)
                        && t.GetConstructor(Type.EmptyTypes) is not null)
            .Select(t => Activator.CreateInstance(t))
            .OfType<INavContributor>()
            .Select(c => c.Link);

        _links = contributed
            .Prepend(Home)
            .OrderBy(l => l.Order)
            .ThenBy(l => l.Text, StringComparer.Ordinal)
            .ToList();
    }
}
