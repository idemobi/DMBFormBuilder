#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBFormBuilderLabs.Navigation;

/// <summary>
///     Provides reusable navigation fragments for DMBFormBuilder labs hosts.
/// </summary>
/// <remarks>
///     The agent only builds DMBFormBuilder-specific menu, sidebar, title, and icon fragments. Host websites remain
///     responsible for assembling these fragments into their own navbar providers, sidebar filters, and global
///     navigation structures.
/// </remarks>
public static class DMBFormBuilderLabsNavigationAgent
{
    #region Static methods

    /// <summary>
    ///     Creates an action item for a DMBFormBuilder labs page.
    /// </summary>
    /// <param name="action">The MVC action name on the <c>FormBuilder</c> controller.</param>
    /// <param name="title">The action title shown in navigation UI.</param>
    /// <param name="icon">The Bootstrap Icons CSS class used by the action.</param>
    /// <param name="currentController">The current MVC controller name used to mark the action active.</param>
    /// <param name="currentAction">The current MVC action name used to mark the action active.</param>
    /// <returns>The configured <see cref="AspRouteActionItem"/>.</returns>
    public static AspRouteActionItem CreateAction(
        string action,
        string title,
        string icon,
        string? currentController = null,
        string? currentAction = null
    )
    {
        bool active =
            string.Equals(currentController, "FormBuilder", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase);

        return ActionItemFactory.AspRoute("FormBuilder", action)
            .SetTitle(title)
            .SetIcon(IconStruct.Bootstrap(icon))
            .SetActive(active);
    }

    /// <summary>
    ///     Creates the DMBFormBuilder navbar menu group.
    /// </summary>
    /// <returns>The configured <see cref="GroupActionItem"/> containing DMBFormBuilder labs page links.</returns>
    public static GroupActionItem CreateMenuGroup()
    {
        return ActionItemFactory.Group("DMBFormBuilder", IconStruct.Bootstrap("bi-input-cursor-text"))
            .AddItems(
                ActionItemFactory.Group("General", IconStruct.Bootstrap("bi-info-circle"))
                    .AddItems(
                        CreateAction("Introduction", "Introduction", "bi-info-circle"),
                        CreateAction("GettingStarted", "Getting Started", "bi-play-circle"),
                        CreateAction("Architecture", "Architecture", "bi-diagram-3")
                    ),
                ActionItemFactory.Group("Usage", IconStruct.Bootstrap("bi-terminal"))
                    .AddItems(
                        CreateAction("CSHtml", "Using in Razor (cshtml)", "bi-file-earmark-code")
                    ),
                ActionItemFactory.Group("Fields", IconStruct.Bootstrap("bi-ui-checks-grid"))
                    .AddItems(
                        CreateAction("TextFields", "Text fields", "bi-input-cursor"),
                        CreateAction("EmailFields", "Email fields", "bi-envelope-at"),
                        CreateAction("PasswordFields", "Password fields", "bi-key"),
                        CreateAction("TextAreaFields", "Textarea fields", "bi-textarea-t"),
                        CreateAction("BooleanFields", "Boolean fields", "bi-toggle-on"),
                        CreateAction("DateInputFields", "Date inputs", "bi-calendar-event"),
                        CreateAction("DateTimeInputFields", "Date time inputs", "bi-calendar2-week"),
                        CreateAction("TimeInputFields", "Time inputs", "bi-clock"),
                        CreateAction("ColorInputFields", "Color inputs", "bi-palette"),
                        CreateAction("SliderInputFields", "Slider inputs", "bi-sliders"),
                        CreateAction("SelectFields", "Select fields", "bi-menu-button-wide"),
                        CreateAction("EnumRadioFields", "Enum radio fields", "bi-ui-radios"),
                        CreateAction("FlagFields", "Flag fields", "bi-list-check"),
                        CreateAction("CountryFields", "Country fields", "bi-globe2"),
                        CreateAction("TokenFields", "Token fields", "bi-key"),
                        CreateAction("Captcha", "Captcha", "bi-shield-lock")
                    ),
                ActionItemFactory.Group("Examples", IconStruct.Bootstrap("bi-code-square"))
                    .AddItems(
                        CreateAction("ExamplesNormal", "Normal label + field", "bi-input-cursor"),
                        CreateAction("ExamplesFloating", "Floating label", "bi-window"),
                        CreateAction("ExamplesInline", "Inline label + field", "bi-layout-sidebar-inset"),
                        CreateAction("ExamplesGrouped", "Grouped label + field", "bi-collection"),
                        CreateAction("ExamplesLocked", "Locked field", "bi-lock")
                    )
            );
    }

    /// <summary>
    ///     Creates the DMBFormBuilder sidebar section.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <returns>The configured <see cref="SideBarSectionComponent"/>.</returns>
    public static SideBarSectionComponent CreateSidebarSection(string? currentController, string? currentAction)
    {
        return new SideBarSectionComponent("FormBuilder")
            .Add(
                ActionItemFactory.Group("General", IconStruct.Bootstrap("bi-info-circle"))
                    .AddItems(
                        CreateAction("Introduction", "Introduction", "bi-info-circle", currentController, currentAction),
                        CreateAction("GettingStarted", "Getting Started", "bi-play-circle", currentController, currentAction),
                        CreateAction("Architecture", "Architecture", "bi-diagram-3", currentController, currentAction)
                    ),
                ActionItemFactory.Group("Usage", IconStruct.Bootstrap("bi-terminal"))
                    .AddItems(
                        CreateAction("CSHtml", "Using in Razor (cshtml)", "bi-file-earmark-code", currentController, currentAction)
                    ),
                ActionItemFactory.Group("Fields", IconStruct.Bootstrap("bi-ui-checks-grid"))
                    .AddItems(
                        CreateAction("TextFields", "Text fields", "bi-input-cursor", currentController, currentAction),
                        CreateAction("EmailFields", "Email fields", "bi-envelope-at", currentController, currentAction),
                        CreateAction("PasswordFields", "Password fields", "bi-key", currentController, currentAction),
                        CreateAction("TextAreaFields", "Textarea fields", "bi-textarea-t", currentController, currentAction),
                        CreateAction("BooleanFields", "Boolean fields", "bi-toggle-on", currentController, currentAction),
                        CreateAction("DateInputFields", "Date inputs", "bi-calendar-event", currentController, currentAction),
                        CreateAction("DateTimeInputFields", "Date time inputs", "bi-calendar2-week", currentController, currentAction),
                        CreateAction("TimeInputFields", "Time inputs", "bi-clock", currentController, currentAction),
                        CreateAction("ColorInputFields", "Color inputs", "bi-palette", currentController, currentAction),
                        CreateAction("SliderInputFields", "Slider inputs", "bi-sliders", currentController, currentAction),
                        CreateAction("SelectFields", "Select fields", "bi-menu-button-wide", currentController, currentAction),
                        CreateAction("EnumRadioFields", "Enum radio fields", "bi-ui-radios", currentController, currentAction),
                        CreateAction("FlagFields", "Flag fields", "bi-list-check", currentController, currentAction),
                        CreateAction("CountryFields", "Country fields", "bi-globe2", currentController, currentAction),
                        CreateAction("TokenFields", "Token fields", "bi-key", currentController, currentAction),
                        CreateAction("Captcha", "Captcha", "bi-shield-lock", currentController, currentAction)
                    ),
                ActionItemFactory.Group("Examples", IconStruct.Bootstrap("bi-code-square"))
                    .AddItems(
                        CreateAction("ExamplesNormal", "Normal label + field", "bi-input-cursor", currentController, currentAction),
                        CreateAction("ExamplesFloating", "Floating label", "bi-window", currentController, currentAction),
                        CreateAction("ExamplesInline", "Inline label + field", "bi-layout-sidebar-inset", currentController, currentAction),
                        CreateAction("ExamplesGrouped", "Grouped label + field", "bi-collection", currentController, currentAction),
                        CreateAction("ExamplesLocked", "Locked field", "bi-lock", currentController, currentAction)
                    )
            );
    }

    /// <summary>
    ///     Creates the DMBFormBuilder sidebar component.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <param name="sidebarId">The HTML identifier applied to the sidebar component.</param>
    /// <param name="localStorageKey">The browser local-storage key used for sidebar state.</param>
    /// <returns>The configured <see cref="SideBarComponent"/>.</returns>
    public static SideBarComponent CreateSidebar(
        string? currentController,
        string? currentAction,
        string sidebarId = "form_builder_sidebar",
        string localStorageKey = "dmbformbuilder.labs.sidebar"
    )
    {
        SideBarComponent sidebar = new SideBarComponent()
            .WithId(sidebarId)
            .WithLocalStorageKey(localStorageKey)
            .WithAutoExpandActivePath()
            .WithRememberExpandedState();

        sidebar.AddSection(CreateSidebarSection(currentController, currentAction));

        return sidebar;
    }

    /// <summary>
    ///     Resolves the Bootstrap icon for a DMBFormBuilder labs action.
    /// </summary>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The icon value represented as an <see cref="IconStruct"/>.</returns>
    public static IconStruct ResolveActionIcon(string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => IconStruct.Bootstrap("bi-play-circle"),
            "Architecture" => IconStruct.Bootstrap("bi-diagram-3"),
            "CSHtml" => IconStruct.Bootstrap("bi-file-earmark-code"),
            "Examples" or "ExamplesNormal" or "ExamplesFloating" or "ExamplesInline" or "ExamplesGrouped" or "ExamplesLocked" => IconStruct.Bootstrap("bi-code-square"),
            "TextFields" => IconStruct.Bootstrap("bi-input-cursor"),
            "EmailFields" => IconStruct.Bootstrap("bi-envelope-at"),
            "PasswordFields" => IconStruct.Bootstrap("bi-key"),
            "TextAreaFields" => IconStruct.Bootstrap("bi-textarea-t"),
            "BooleanFields" => IconStruct.Bootstrap("bi-toggle-on"),
            "DateInputFields" => IconStruct.Bootstrap("bi-calendar-event"),
            "DateTimeInputFields" => IconStruct.Bootstrap("bi-calendar2-week"),
            "TimeInputFields" => IconStruct.Bootstrap("bi-clock"),
            "ColorInputFields" => IconStruct.Bootstrap("bi-palette"),
            "SliderInputFields" => IconStruct.Bootstrap("bi-sliders"),
            "SelectFields" => IconStruct.Bootstrap("bi-menu-button-wide"),
            "EnumRadioFields" => IconStruct.Bootstrap("bi-ui-radios"),
            "FlagFields" => IconStruct.Bootstrap("bi-list-check"),
            "CountryFields" => IconStruct.Bootstrap("bi-globe2"),
            "TokenFields" => IconStruct.Bootstrap("bi-key"),
            "Captcha" => IconStruct.Bootstrap("bi-shield-lock"),
            _ => IconStruct.Bootstrap("bi-info-circle")
        };
    }

    /// <summary>
    ///     Resolves the display title for a DMBFormBuilder labs action.
    /// </summary>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The display title for the action.</returns>
    public static string ResolveActionTitle(string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => "Getting Started",
            "Architecture" => "Architecture",
            "CSHtml" => "Using in Razor (cshtml)",
            "Examples" => "Examples",
            "ExamplesNormal" => "Normal label + field",
            "ExamplesFloating" => "Floating label",
            "ExamplesInline" => "Inline label + field",
            "ExamplesGrouped" => "Grouped label + field",
            "ExamplesLocked" => "Locked field",
            "TextFields" => "Text fields",
            "EmailFields" => "Email fields",
            "PasswordFields" => "Password fields",
            "TextAreaFields" => "Textarea fields",
            "BooleanFields" => "Boolean fields",
            "DateInputFields" => "Date inputs",
            "DateTimeInputFields" => "Date time inputs",
            "TimeInputFields" => "Time inputs",
            "ColorInputFields" => "Color inputs",
            "SliderInputFields" => "Slider inputs",
            "SelectFields" => "Select fields",
            "EnumRadioFields" => "Enum radio fields",
            "FlagFields" => "Flag fields",
            "CountryFields" => "Country fields",
            "TokenFields" => "Token fields",
            "Captcha" => "Captcha",
            _ => "Introduction"
        };
    }

    #endregion
}
