#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using DMBFormBuilder.Resources;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Provides Razor helper entry points for select fields.
    /// </summary>
    public static class SelectFieldBuilderExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a select field bound to an enum model property and populates one option per enum value.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TEnum">The enum type used to populate options.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata, selected value, and validation attributes.</param>
        /// <returns>A configured <see cref="SelectFieldBuilder" /> for the selected enum property.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expression" /> is not a member expression.</exception>
        public static SelectFieldBuilder EnumSelectFieldBuilderFor<TModel, TEnum>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
            where TEnum : struct, Enum
        {
            if (expression.Body is not MemberExpression memberExpression)
            {
                throw new ArgumentException("Expression must be a member expression.", nameof(expression));
            }

            string propertyName = memberExpression.Member.Name;
            PropertyInfo? propertyInfo = typeof(TModel).GetProperty(propertyName);
            string label = propertyName;
            string description = string.Empty;
            string? value = null;

            if (html.ViewData.Model != null && propertyInfo != null)
            {
                value = propertyInfo.GetValue(html.ViewData.Model)?.ToString();
            }

            SelectFieldBuilder builder = html.SelectFieldBuilder()
                .SetInput(propertyName.Replace(".", "_", StringComparison.Ordinal), propertyName)
                .SetValue(value);

            if (propertyInfo != null)
            {
                DisplayAttribute? display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                if (display != null)
                {
                    label = WebLocalizer.GetDataAnnotation(display.Name ?? propertyName);
                    description = FormFieldDisplayMetadata.ResolveDescription(display);
                }

                RequiredAttribute? required = propertyInfo.GetCustomAttribute<RequiredAttribute>();
                if (required != null)
                {
                    builder.SetRequired(true, WebLocalizer.GetDataAnnotation(string.IsNullOrWhiteSpace(required.ErrorMessage) ? nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required) : required.ErrorMessage));
                }
            }

            foreach (TEnum option in Enum.GetValues<TEnum>())
            {
                string name = option.ToString() ?? string.Empty;
                builder.AddOption(name, WebLocalizer.GetDataAnnotation(name));
            }

            return builder
                .SetLabel(label)
                .SetDescription(description);
        }

        /// <summary>
        ///     Creates a select field builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="SelectFieldBuilder" /> writing to the current view output.</returns>
        public static SelectFieldBuilder SelectFieldBuilder(this IHtmlHelper html)
        {
            return new SelectFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a select field builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="SelectFieldBuilder" /> writing to the current view output.</returns>
        public static SelectFieldBuilder SelectFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new SelectFieldBuilder(html.ViewContext.Writer, html);
        }

        #endregion
    }
}