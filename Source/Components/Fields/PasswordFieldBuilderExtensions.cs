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
    ///     Provides Razor helper entry points for password fields.
    /// </summary>
    public static class PasswordFieldBuilderExtensions
    {
        #region Static methods

        private static PasswordFieldBuilder CreatePasswordFieldBuilderFor<TModel, TProperty>(IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, bool withStrengthMeter)
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
            string? value = null;

            if (html.ViewData.Model != null && propertyInfo != null)
            {
                value = propertyInfo.GetValue(html.ViewData.Model)?.ToString();
            }

            PasswordFieldBuilder builder = html.PasswordFieldBuilder()
                .SetInput(inputId, inputName)
                .SetValue(value)
                .ShowStrengthMeter(withStrengthMeter);

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
                    builder.SetRequired(true, ResolveRequiredMessage(required, label));
                }

                MaxLengthAttribute? maxLength = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLength != null)
                {
                    builder.SetMaxLength(maxLength.Length, ResolveMessage(maxLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength), label, maxLength.Length));
                }

                MinLengthAttribute? minLength = propertyInfo.GetCustomAttribute<MinLengthAttribute>();
                if (minLength != null)
                {
                    builder.SetMinLength(minLength.Length, ResolveMessage(minLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength), label, minLength.Length));
                }

                StringLengthAttribute? stringLength = propertyInfo.GetCustomAttribute<StringLengthAttribute>();
                if (stringLength != null)
                {
                    builder.SetMaxLength(stringLength.MaximumLength, ResolveMessage(stringLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength), label, stringLength.MaximumLength));
                    if (stringLength.MinimumLength > 0)
                    {
                        builder.SetMinLength(stringLength.MinimumLength, ResolveMessage(stringLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength), label, stringLength.MinimumLength));
                    }
                }

                RegularExpressionAttribute? regex = propertyInfo.GetCustomAttribute<RegularExpressionAttribute>();
                if (regex != null)
                {
                    builder.SetPattern(regex.Pattern, ResolveMessage(regex.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid), label));
                }

                CompareAttribute? compare = propertyInfo.GetCustomAttribute<CompareAttribute>();
                if (compare != null)
                {
                    builder.SetCompareTo(compare.OtherProperty, ResolveMessage(compare.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCompare), label, ResolveCompareLabel<TModel>(compare)));
                }
            }

            return builder
                .SetLabel(label)
                .SetDescription(description)
                .SetPlaceholder(placeholder);
        }

        /// <summary>
        ///     Creates a password field builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="PasswordFieldBuilder" /> writing to the current view output.</returns>
        public static PasswordFieldBuilder PasswordFieldBuilder(this IHtmlHelper html)
        {
            return new PasswordFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a password field builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="PasswordFieldBuilder" /> writing to the current view output.</returns>
        public static PasswordFieldBuilder PasswordFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new PasswordFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a password field builder bound to a model property expression.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">
        ///     A member expression used to derive field metadata, value, label, prompt, and validation
        ///     attributes.
        /// </param>
        /// <returns>A configured <see cref="PasswordFieldBuilder" /> for the selected property.</returns>
        public static PasswordFieldBuilder PasswordFieldBuilderFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return CreatePasswordFieldBuilderFor(html, expression, false);
        }

        /// <summary>
        ///     Creates a password field builder with password strength metadata enabled.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">
        ///     A member expression used to derive field metadata, value, label, prompt, and validation
        ///     attributes.
        /// </param>
        /// <returns>A configured <see cref="PasswordFieldBuilder" /> with strength meter rendering enabled.</returns>
        public static PasswordFieldBuilder PasswordFieldBuilderWithStrengthFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return CreatePasswordFieldBuilderFor(html, expression, true);
        }

        private static string ResolveCompareLabel<TModel>(CompareAttribute compare)
        {
            PropertyInfo? propertyInfo = typeof(TModel).GetProperty(compare.OtherProperty);
            DisplayAttribute? display = propertyInfo?.GetCustomAttribute<DisplayAttribute>();
            if (display != null)
            {
                return WebLocalizer.GetDataAnnotation(display.Name ?? compare.OtherProperty);
            }

            return compare.OtherProperty;
        }

        private static string ResolveMessage(string? key, string fallbackKey, params object[] args)
        {
            string resolvedKey = string.IsNullOrWhiteSpace(key) ? fallbackKey : key;
            return args.Length == 0
                ? WebLocalizer.GetDataAnnotation(resolvedKey)
                : WebLocalizer.GetDataAnnotation(resolvedKey, args);
        }

        private static string ResolveRequiredMessage(RequiredAttribute required, string label)
        {
            if (!string.IsNullOrWhiteSpace(required.ErrorMessage))
            {
                return WebLocalizer.GetDataAnnotation(required.ErrorMessage, label);
            }

            return WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required));
        }

        #endregion
    }
}
