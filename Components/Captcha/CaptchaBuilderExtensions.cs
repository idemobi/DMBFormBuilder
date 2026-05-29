#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Provides Razor helper entry points for <see cref="CaptchaBuilder" />.
    /// </summary>
    public static class CaptchaBuilderExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a captcha builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="CaptchaBuilder" /> writing to the current view output.</returns>
        public static CaptchaBuilder CaptchaBuilder(this IHtmlHelper html)
        {
            return new CaptchaBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a captcha builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="CaptchaBuilder" /> writing to the current view output.</returns>
        public static CaptchaBuilder CaptchaBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new CaptchaBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a captcha builder bound to a model property expression.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive input name, identifier, label, and prompt.</param>
        /// <returns>A configured <see cref="CaptchaBuilder" /> for the selected property.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expression" /> is not a member expression.</exception>
        public static CaptchaBuilder CaptchaBuilderFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body is not MemberExpression memberExpression)
            {
                throw new ArgumentException("Expression must be a member expression.", nameof(expression));
            }

            string propertyName = memberExpression.Member.Name;
            PropertyInfo? propertyInfo = typeof(TModel).GetProperty(propertyName);
            string inputName = propertyName;
            string inputId = inputName.Replace(".", "_", StringComparison.Ordinal);
            string label = propertyName;
            string placeholder = propertyName;
            string description = string.Empty;

            if (propertyInfo != null)
            {
                DisplayAttribute? display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                if (display != null)
                {
                    label = WebLocalizer.GetDataAnnotation(display.Name ?? propertyName);
                    placeholder = WebLocalizer.GetDataAnnotation(display.Prompt ?? display.Name ?? propertyName);
                    description = FormFieldDisplayMetadata.ResolveDescription(display);
                }
            }

            return html.CaptchaBuilder()
                .SetInput(inputId, inputName)
                .SetLabel(label)
                .SetDescription(description)
                .SetPlaceholder(placeholder);
        }

        #endregion
    }
}