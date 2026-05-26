#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj TokenFieldBuilderExtensions.cs create at 2026/05/13
// ©2024-2026 idéMobi SARL FRANCE

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
    /// Provides Razor helper entry points for token fields.
    /// </summary>
    public static class TokenFieldBuilderExtensions
    {
        /// <summary>
        /// Creates a token field builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="TokenFieldBuilder"/> writing to the current view output.</returns>
        public static TokenFieldBuilder TokenFieldBuilder(this IHtmlHelper html)
        {
            return new TokenFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a token field builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="TokenFieldBuilder"/> writing to the current view output.</returns>
        public static TokenFieldBuilder TokenFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new TokenFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a token field builder bound to a model property expression.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata, value, label, prompt, and validation attributes.</param>
        /// <returns>A configured <see cref="TokenFieldBuilder"/> for the selected property.</returns>
        public static TokenFieldBuilder TokenFieldBuilderFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body is not MemberExpression memberExpression)
            {
                throw new ArgumentException("Expression must be a member expression.", nameof(expression));
            }

            string propertyName = memberExpression.Member.Name;
            PropertyInfo? propertyInfo = typeof(TModel).GetProperty(propertyName);
            string label = propertyName;
            string placeholder = propertyName;
            string description = string.Empty;
            string? value = null;

            if (html.ViewData.Model != null && propertyInfo != null)
            {
                value = propertyInfo.GetValue(html.ViewData.Model)?.ToString();
            }

            TokenFieldBuilder builder = html.TokenFieldBuilder()
                .SetInput(propertyName.Replace(".", "_", StringComparison.Ordinal), propertyName)
                .SetValue(value);

            if (propertyInfo != null)
            {
                DisplayAttribute? display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                if (display != null)
                {
                    label = WebLocalizer.GetDataAnnotation(display.Name ?? propertyName);
                    placeholder = WebLocalizer.GetDataAnnotation(display.Prompt ?? display.Name ?? propertyName);
                    description = FormFieldDisplayMetadata.ResolveDescription(display);
                }

                RequiredAttribute? required = propertyInfo.GetCustomAttribute<RequiredAttribute>();
                if (required != null)
                {
                    builder.SetRequired(true, WebLocalizer.GetDataAnnotation(string.IsNullOrWhiteSpace(required.ErrorMessage) ? nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required) : required.ErrorMessage));
                }

                MaxLengthAttribute? maxLength = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLength != null)
                {
                    builder.SetInputAttribute("maxlength", maxLength.Length.ToString());
                }
            }

            return builder
                .SetLabel(label)
                .SetDescription(description)
                .SetPlaceholder(placeholder);
        }
    }
}
