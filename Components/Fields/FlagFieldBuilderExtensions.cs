#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FlagFieldBuilderExtensions.cs create at 2026/05/13
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
    /// Provides Razor helper entry points for flags enum fields.
    /// </summary>
    public static class FlagFieldBuilderExtensions
    {
        /// <summary>
        /// Creates a flags field builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="FlagFieldBuilder"/> writing to the current view output.</returns>
        public static FlagFieldBuilder FlagFieldBuilder(this IHtmlHelper html)
        {
            return new FlagFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a flags field builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="FlagFieldBuilder"/> writing to the current view output.</returns>
        public static FlagFieldBuilder FlagFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new FlagFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a checkbox flags field bound to an enum model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TEnum">The flags enum type used to populate options.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata, selected flags, and validation attributes.</param>
        /// <returns>A configured <see cref="FlagFieldBuilder"/> rendered as a checkbox list.</returns>
        public static FlagFieldBuilder FlagCheckboxFieldBuilderFor<TModel, TEnum>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
            where TEnum : struct, Enum
        {
            return CreateFlagFieldBuilderFor(html, expression, false);
        }

        /// <summary>
        /// Creates a select flags field bound to an enum model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TEnum">The flags enum type used to populate options.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata, selected flags, and validation attributes.</param>
        /// <returns>A configured <see cref="FlagFieldBuilder"/> rendered as a select control.</returns>
        public static FlagFieldBuilder FlagSelectFieldBuilderFor<TModel, TEnum>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
            where TEnum : struct, Enum
        {
            return CreateFlagFieldBuilderFor(html, expression, true);
        }

        private static FlagFieldBuilder CreateFlagFieldBuilderFor<TModel, TEnum>(IHtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression, bool asSelect)
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
            long currentValue = 0;

            if (html.ViewData.Model != null && propertyInfo != null)
            {
                object? rawValue = propertyInfo.GetValue(html.ViewData.Model);
                if (rawValue != null)
                {
                    currentValue = Convert.ToInt64(rawValue);
                }
            }

            FlagFieldBuilder builder = html.FlagFieldBuilder()
                .SetInput(propertyName.Replace(".", "_", StringComparison.Ordinal), propertyName)
                .RenderAsSelect(asSelect);

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
                long optionValue = Convert.ToInt64(option);
                if (optionValue == 0)
                {
                    continue;
                }

                bool selected = (currentValue & optionValue) == optionValue;
                builder.AddOption(optionValue, ResolveEnumDisplay(option), selected);
            }

            return builder
                .SetLabel(label)
                .SetDescription(description);
        }

        private static string ResolveEnumDisplay<TEnum>(TEnum option)
            where TEnum : struct, Enum
        {
            string optionName = option.ToString() ?? string.Empty;
            MemberInfo? member = typeof(TEnum).GetMember(optionName).FirstOrDefault();
            DisplayAttribute? display = member?.GetCustomAttribute<DisplayAttribute>();
            return WebLocalizer.GetDataAnnotation(display?.Name ?? optionName);
        }
    }
}
