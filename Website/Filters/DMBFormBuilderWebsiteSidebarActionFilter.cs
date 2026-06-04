#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;
using DMBFormBuilderLabs.Navigation;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace DMBFormBuilderWebsite;

internal sealed class DMBFormBuilderWebsiteSidebarActionFilter : IActionFilter
{
    #region Instance methods

    #region From interface IActionFilter

    /// <summary>
    ///     Completes the action filter lifecycle after the action has executed.
    /// </summary>
    /// <param name="context">The current action executed context.</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    /// <summary>
    ///     Injects the local DMBFormBuilder sidebar and breadcrumb for FormBuilder labs pages.
    /// </summary>
    /// <param name="context">The current action execution context.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is not RawBootstrapController controller)
        {
            return;
        }

        string? currentController = context.RouteData.Values["controller"]?.ToString();
        string? currentAction = context.RouteData.Values["action"]?.ToString();

        if (!string.Equals(currentController, "FormBuilder", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        string actionName = string.IsNullOrWhiteSpace(currentAction) ? "Introduction" : currentAction;

        controller.SetSidebar(DMBFormBuilderLabsNavigationAgent.CreateSidebar(currentController, actionName));
        controller.AddBreadcrumb(
            ActionItemFactory.Url("Home", "/", IconStruct.Bootstrap("bi-house")),
            ActionItemFactory.AspRoute("FormBuilder", "Introduction")
                .SetTitle("DMBFormBuilder")
                .SetIcon(IconStruct.Bootstrap("bi-input-cursor-text")),
            ActionItemFactory.AspRoute("FormBuilder", actionName)
                .SetTitle(DMBFormBuilderLabsNavigationAgent.ResolveActionTitle(actionName))
                .SetIcon(DMBFormBuilderLabsNavigationAgent.ResolveActionIcon(actionName))
        );
    }

    #endregion

    #endregion
}
