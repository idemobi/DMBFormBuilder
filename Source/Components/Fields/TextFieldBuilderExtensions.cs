#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using DMBFormBuilder.Resources;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Provides Razor helper entry points for <see cref="TextFieldBuilder" />.
    /// </summary>
    public static class TextFieldBuilderExtensions
    {
        #region Static methods

        private static string FormatValue(object? value)
        {
            return value switch
            {
                null => string.Empty,
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty,
                _ => value.ToString() ?? string.Empty
            };
        }

        private static bool IsDecimalType(Type type)
        {
            return type == typeof(float)
                   || type == typeof(double)
                   || type == typeof(decimal);
        }

        private static bool IsNumericType(Type type)
        {
            return type == typeof(byte)
                   || type == typeof(sbyte)
                   || type == typeof(short)
                   || type == typeof(ushort)
                   || type == typeof(int)
                   || type == typeof(uint)
                   || type == typeof(long)
                   || type == typeof(ulong)
                   || type == typeof(float)
                   || type == typeof(double)
                   || type == typeof(decimal);
        }

        private static string ResolveMessage(string? key, string fallbackKey)
        {
            return WebLocalizer.GetDataAnnotation(string.IsNullOrWhiteSpace(key) ? fallbackKey : key);
        }

        private static string ResolveRequiredMessage(RequiredAttribute required, string label)
        {
            if (!string.IsNullOrWhiteSpace(required.ErrorMessage))
            {
                return WebLocalizer.GetDataAnnotation(required.ErrorMessage, label);
            }

            return WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required));
        }

        /// <summary>
        ///     Creates a text field builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> writing to the current view output.</returns>
        public static TextFieldBuilder TextFieldBuilder(this IHtmlHelper html)
        {
            return new TextFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a text field builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> writing to the current view output.</returns>
        public static TextFieldBuilder TextFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return new TextFieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a text field builder bound to a model property expression.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">
        ///     A member expression used to derive input name, identifier, value, label, prompt, and
        ///     validation metadata.
        /// </param>
        /// <returns>A configured <see cref="TextFieldBuilder" /> for the selected model property.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expression" /> is not a member expression.</exception>
        public static TextFieldBuilder TextFieldBuilderFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
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
                value = FormatValue(propertyInfo.GetValue(html.ViewData.Model));
            }

            TextFieldBuilder builder = html.TextFieldBuilder()
                .SetInput(inputId, inputName)
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
                    builder.SetRequired(true, ResolveRequiredMessage(required, label));
                }

                MaxLengthAttribute? maxLength = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
                if (maxLength != null)
                {
                    builder.SetMaxLength(maxLength.Length, ResolveMessage(maxLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength)));
                }

                MinLengthAttribute? minLength = propertyInfo.GetCustomAttribute<MinLengthAttribute>();
                if (minLength != null)
                {
                    builder.SetMinLength(minLength.Length, ResolveMessage(minLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength)));
                }

                StringLengthAttribute? stringLength = propertyInfo.GetCustomAttribute<StringLengthAttribute>();
                if (stringLength != null)
                {
                    builder.SetMaxLength(stringLength.MaximumLength, ResolveMessage(stringLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength)));
                    if (stringLength.MinimumLength > 0)
                    {
                        builder.SetMinLength(stringLength.MinimumLength, ResolveMessage(stringLength.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength)));
                    }
                }

                RangeAttribute? range = propertyInfo.GetCustomAttribute<RangeAttribute>();
                if (range != null)
                {
                    builder.SetMin(FormatValue(range.Minimum), ResolveMessage(range.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range)));
                    builder.SetMax(FormatValue(range.Maximum), ResolveMessage(range.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range)));
                }

                RegularExpressionAttribute? regex = propertyInfo.GetCustomAttribute<RegularExpressionAttribute>();
                if (regex != null)
                {
                    builder.SetPattern(regex.Pattern, ResolveMessage(regex.ErrorMessage, nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FormatInvalid)));
                }

                Type realType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                if (IsNumericType(realType))
                {
                    if (IsDecimalType(realType))
                    {
                        builder.SetFloat();
                    }
                    else
                    {
                        builder.SetNumeric();
                    }
                }
            }

            return builder
                .SetLabel(label)
                .SetDescription(description)
                .SetPlaceholder(placeholder);
        }

        #endregion
    }
}