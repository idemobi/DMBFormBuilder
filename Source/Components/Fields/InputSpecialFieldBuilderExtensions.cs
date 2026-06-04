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
    ///     Provides Razor helpers for HTML5 typed input fields built on <see cref="TextFieldBuilder" />.
    /// </summary>
    public static class InputSpecialFieldBuilderExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a color input bound to a string model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and the color value.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> configured with <c>type="color"</c>.</returns>
        public static TextFieldBuilder ColorFieldBuilderFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, string>> expression)
        {
            return CreateTypedInputBuilderFor(html, expression, "color", value => value?.ToString());
        }

        private static TextFieldBuilder CreateTypedInputBuilderFor<TModel, TProperty>(IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string inputType, Func<object?, string?> formatter)
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
                value = formatter(propertyInfo.GetValue(html.ViewData.Model));
            }

            TextFieldBuilder builder = html.TextFieldBuilder()
                .SetInput(propertyName.Replace(".", "_", StringComparison.Ordinal), propertyName)
                .SetInputType(inputType)
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

                RangeAttribute? range = propertyInfo.GetCustomAttribute<RangeAttribute>();
                if (range != null)
                {
                    builder.SetMin(FormatValue(range.Minimum));
                    builder.SetMax(FormatValue(range.Maximum));
                }
            }

            return builder
                .SetLabel(label)
                .SetDescription(description)
                .SetPlaceholder(placeholder);
        }

        /// <summary>
        ///     Creates a date input bound to a <see cref="DateTime" /> model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and the formatted date value.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> configured with <c>type="date"</c>.</returns>
        public static TextFieldBuilder DateFieldBuilderFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, DateTime>> expression)
        {
            return CreateTypedInputBuilderFor(html, expression, "date", value => value is DateTime date ? date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : null);
        }

        /// <summary>
        ///     Creates a local date-time input bound to a <see cref="DateTime" /> model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and the formatted date-time value.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> configured with <c>type="datetime-local"</c>.</returns>
        public static TextFieldBuilder DateTimeFieldBuilderFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, DateTime>> expression)
        {
            return CreateTypedInputBuilderFor(html, expression, "datetime-local", value => value is DateTime date ? date.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture) : null);
        }

        private static string FormatValue(object? value)
        {
            return value switch
            {
                null => string.Empty,
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty,
                _ => value.ToString() ?? string.Empty
            };
        }

        /// <summary>
        ///     Creates a month input bound to a <see cref="DateTime" /> model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and the formatted month value.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> configured with <c>type="month"</c>.</returns>
        public static TextFieldBuilder MonthFieldBuilderFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, DateTime>> expression)
        {
            return CreateTypedInputBuilderFor(html, expression, "month", value => value is DateTime date ? date.ToString("yyyy-MM", CultureInfo.InvariantCulture) : null);
        }

        /// <summary>
        ///     Creates a range slider input bound to a model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata, value, and range attributes.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> configured with <c>type="range"</c> and slider metadata.</returns>
        public static TextFieldBuilder SliderFieldBuilderFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return CreateTypedInputBuilderFor(html, expression, "range", value => value switch
                {
                    null => null,
                    IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture) ?? string.Empty,
                    _ => value.ToString() ?? string.Empty
                }).SetInputAttribute("data-dmb-slider", "true")
                .SetVariant(DMBPageBuilder.VariantStyle.Primary);
        }

        /// <summary>
        ///     Creates a time input bound to a <see cref="TimeSpan" /> model property.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and the formatted time value.</param>
        /// <returns>A <see cref="TextFieldBuilder" /> configured with <c>type="time"</c>.</returns>
        public static TextFieldBuilder TimeFieldBuilderFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TimeSpan>> expression)
        {
            return CreateTypedInputBuilderFor(html, expression, "time", value => value is TimeSpan time ? time.ToString(@"hh\:mm", CultureInfo.InvariantCulture) : null);
        }

        #endregion
    }
}