#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Provides Razor helper entry points for creating <see cref="FormBuilder" /> instances.
    /// </summary>
    public static class FormBuilderExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a fluent FormBuilder instance for the current non-generic Razor view.
        /// </summary>
        /// <param name="html">The Razor HTML helper that supplies the view writer and context.</param>
        /// <returns>A <see cref="FormBuilder" /> writing to the current view output.</returns>
        public static FormBuilder FormBuilder(this IHtmlHelper html)
        {
            return new FormBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a fluent FormBuilder instance for the current strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed Razor HTML helper that supplies the view writer and context.</param>
        /// <returns>A <see cref="FormBuilder" /> writing to the current view output.</returns>
        public static FormBuilder FormBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new FormBuilder(html.ViewContext.Writer, html);
        }

        #endregion
    }
}