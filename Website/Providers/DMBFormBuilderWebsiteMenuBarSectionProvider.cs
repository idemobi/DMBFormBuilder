#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.IO;
using DMBBootstrapBuilder;
using DMBFormBuilderLabs.Navigation;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilderWebsite;

internal sealed class DMBFormBuilderWebsiteMenuBarSectionProvider : IMenuBarSectionProvider
{
    #region Instance fields and properties

    #region From interface IMenuBarSectionProvider

    /// <summary>
    ///     Gets the menu provider ordering value.
    /// </summary>
    public int Order => 100;

    #endregion

    #endregion

    #region Instance methods

    #region From interface IMenuBarSectionProvider

    /// <summary>
    ///     Builds the local DMBFormBuilder navbar module.
    /// </summary>
    /// <param name="writer">The current response writer.</param>
    /// <param name="html">The current HTML helper.</param>
    /// <returns>The menu module result containing DMBFormBuilder navigation items.</returns>
    public MenuBarModuleResult Build(TextWriter writer, IHtmlHelper html)
    {
        MenuBarModuleResult result = new();

        result.ActionList.Add(DMBFormBuilderLabsNavigationAgent.CreateMenuGroup());

        return result;
    }

    /// <summary>
    ///     Determines whether the local DMBFormBuilder menu provider is enabled.
    /// </summary>
    /// <param name="html">The current HTML helper.</param>
    /// <returns><see langword="true"/> because the provider is always enabled for the local website.</returns>
    public bool IsEnabled(IHtmlHelper html)
    {
        return true;
    }

    #endregion

    #endregion
}
