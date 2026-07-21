#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Provides common action items intended to be rendered inside <see cref="FormBuilder" /> forms.
    /// </summary>
    public static class FormActionItemFactory
    {
        #region Static methods

        /// <summary>
        ///     Creates a Bootstrap action item that cancels form edition by navigating to an MVC controller action.
        /// </summary>
        /// <param name="controller">The target MVC controller.</param>
        /// <param name="action">The target MVC action.</param>
        /// <param name="title">The localized button title.</param>
        /// <param name="area">The optional MVC area.</param>
        /// <param name="icon">The button icon. A default cancel icon is used when no icon is provided.</param>
        /// <returns>A configured cancel action item.</returns>
        public static AspRouteActionItem Cancel(string controller, string action, string title, string? area = null, IconStruct icon = default)
        {
            return ActionItemFactory.AspRoute(controller, action, area)
                .SetTitle(title)
                .SetIcon(icon.IsEmpty ? IconStruct.Bootstrap("bi-x-circle") : icon)
                .SetVariant(VariantStyle.Secondary)
                .SetOutlined()
                .SetAttribut("data-formbuilder-cancel", "true");
        }

        /// <summary>
        ///     Creates a Bootstrap action item that renders as a native HTML reset button.
        /// </summary>
        /// <param name="title">The localized button title.</param>
        /// <param name="icon">The button icon. A default reset icon is used when no icon is provided.</param>
        /// <returns>A configured reset action item.</returns>
        public static JavaScriptActionItem Reset(string title = "Reset", IconStruct icon = default)
        {
            return ActionItemFactory.JavaScript(string.Empty, string.Empty)
                .SetTitle(title)
                .SetIcon(icon.IsEmpty ? IconStruct.Bootstrap("bi-arrow-counterclockwise") : icon)
                .SetVariant(VariantStyle.Secondary)
                .SetOutlined()
                .SetAttribut("type", "reset")
                .SetAttribut("data-formbuilder-reset", "true");
        }

        /// <summary>
        ///     Creates a Bootstrap action item that renders as a native HTML submit button.
        /// </summary>
        /// <param name="title">The localized button title.</param>
        /// <param name="icon">The button icon. A default send icon is used when no icon is provided.</param>
        /// <param name="lockUntilChanged">
        ///     <see langword="true" /> to disable the button until a form using
        ///     <see cref="FormBuilder.EnableSubmitWhenChanged" /> has changed.
        /// </param>
        /// <returns>A configured submit action item.</returns>
        public static JavaScriptActionItem Sent(string title = "Send", IconStruct icon = default, bool lockUntilChanged = false)
        {
            JavaScriptActionItem action = ActionItemFactory.JavaScript(string.Empty, string.Empty)
                .SetTitle(title)
                .SetIcon(icon.IsEmpty ? IconStruct.Bootstrap("bi-send") : icon)
                .SetVariant(VariantStyle.Primary)
                .SetAttribut("type", "submit");

            if (lockUntilChanged)
            {
                action.SetDisabled()
                    .SetAttribut("data-formbuilder-submit-lock", "true");
            }

            return action;
        }

        #endregion
    }
}
