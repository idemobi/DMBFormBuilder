#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj BooleanFieldBuilderExtensions.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

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
    /// Provides Razor helper entry points for checkbox and switch fields.
    /// </summary>
    public static class BooleanFieldBuilderExtensions
    {
        /// <summary>
        /// Creates a checkbox field builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="BooleanFieldBuilder"/> configured as a checkbox.</returns>
        public static BooleanFieldBuilder CheckboxFieldBuilder(this IHtmlHelper html)
        {
            return new BooleanFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a checkbox field builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="BooleanFieldBuilder"/> configured as a checkbox.</returns>
        public static BooleanFieldBuilder CheckboxFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new BooleanFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a checkbox field builder bound to a Boolean model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and value.</param>
        /// <returns>A configured <see cref="BooleanFieldBuilder"/> for the selected property.</returns>
        public static BooleanFieldBuilder CheckboxFieldBuilderFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression)
        {
            return CreateBooleanBuilderFor(html, expression, false);
        }

        /// <summary>
        /// Creates a switch field builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="BooleanFieldBuilder"/> configured as a Bootstrap switch.</returns>
        public static BooleanFieldBuilder SwitchFieldBuilder(this IHtmlHelper html)
        {
            return new BooleanFieldBuilder(html.ViewContext.Writer, html).AsSwitch();
        }

        /// <summary>
        /// Creates a switch field builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="BooleanFieldBuilder"/> configured as a Bootstrap switch.</returns>
        public static BooleanFieldBuilder SwitchFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new BooleanFieldBuilder(html.ViewContext.Writer, html).AsSwitch();
        }

        /// <summary>
        /// Creates a switch field builder bound to a Boolean model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and value.</param>
        /// <returns>A configured <see cref="BooleanFieldBuilder"/> for the selected property.</returns>
        public static BooleanFieldBuilder SwitchFieldBuilderFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression)
        {
            return CreateBooleanBuilderFor(html, expression, true);
        }

        private static BooleanFieldBuilder CreateBooleanBuilderFor<TModel>(IHtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, bool switchStyle)
        {
            if (expression.Body is not MemberExpression memberExpression)
            {
                throw new ArgumentException("Expression must be a member expression.", nameof(expression));
            }

            string propertyName = memberExpression.Member.Name;
            PropertyInfo? propertyInfo = typeof(TModel).GetProperty(propertyName);
            string label = propertyName;
            string description = string.Empty;
            bool value = false;

            if (html.ViewData.Model != null && propertyInfo != null && propertyInfo.GetValue(html.ViewData.Model) is bool modelValue)
            {
                value = modelValue;
            }

            if (propertyInfo != null)
            {
                DisplayAttribute? display = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                if (display != null)
                {
                    label = WebLocalizer.GetDataAnnotation(display.Name ?? propertyName);
                    description = FormFieldDisplayMetadata.ResolveDescription(display);
                }
            }

            return html.CheckboxFieldBuilder()
                .SetInput(propertyName.Replace(".", "_", StringComparison.Ordinal), propertyName)
                .SetLabel(label)
                .SetDescription(description)
                .SetValue(value)
                .AsSwitch(switchStyle);
        }
    }
}
