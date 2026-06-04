#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DMBFormBuilder.Resources;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Provides Razor helpers for country selection fields based on <see cref="SelectFieldBuilder" />.
    /// </summary>
    public static class CountryFieldBuilderExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a localized country selector bound to a model property expression.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive input metadata, value, label, and validation attributes.</param>
        /// <returns>A <see cref="SelectFieldBuilder" /> populated with ISO region options and a current-country quick action.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="expression" /> is not a member expression.</exception>
        public static SelectFieldBuilder CountryFieldBuilderFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
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
            string currentCountry = DMBServerWebHelper.CountryWebTools.GetCountryString(html.ViewContext.HttpContext);
            if (string.Equals(currentCountry, "None", StringComparison.OrdinalIgnoreCase))
            {
                currentCountry = string.Empty;
            }

            if (html.ViewData.Model != null && propertyInfo != null)
            {
                object? rawValue = propertyInfo.GetValue(html.ViewData.Model);
                value = NormalizeCountryCode(rawValue);
            }

            SelectFieldBuilder builder = html.SelectFieldBuilder()
                .SetInput(propertyName.Replace(".", "_", StringComparison.Ordinal), propertyName)
                .SetValue(value)
                .SetInputAttribute("data-dmb-current-country", currentCountry)
                .SetQuickSelectAction(currentCountry, "bi bi-geo-alt", WebLocalizer.GetDataAnnotation("FormBuilder_Field_Country_Prompt"));

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

            builder.AddPlaceholderOption(WebLocalizer.GetDataAnnotation("FormBuilder_Field_Country_Prompt"));
            foreach (KeyValuePair<string, string> country in GetCountries())
            {
                if (string.Equals(country.Key, "NONE", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                builder.AddOption(country.Key, WebLocalizer.GetDataAnnotation(country.Value));
            }

            return builder
                .SetLabel(label)
                .SetDescription(description);
        }

        private static IEnumerable<KeyValuePair<string, string>> GetCountries()
        {
            HashSet<string> seen = new(StringComparer.OrdinalIgnoreCase);
            List<KeyValuePair<string, string>> countries = [];

            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                try
                {
                    RegionInfo region = new(culture.Name);
                    string code = region.TwoLetterISORegionName.ToUpperInvariant();
                    if (!seen.Add(code))
                    {
                        continue;
                    }

                    countries.Add(new KeyValuePair<string, string>(code, region.EnglishName));
                }
                catch (ArgumentException)
                {
                }
            }

            return countries.OrderBy(x => x.Value, StringComparer.OrdinalIgnoreCase);
        }

        private static string? NormalizeCountryCode(object? rawValue)
        {
            if (rawValue == null)
            {
                return null;
            }

            if (rawValue is Enum enumValue)
            {
                string? enumCode = NormalizeEnumCountryCode(enumValue);
                if (string.IsNullOrWhiteSpace(enumCode) == false)
                {
                    return enumCode;
                }
            }

            string? rawText = rawValue.ToString();
            if (string.IsNullOrWhiteSpace(rawText))
            {
                return null;
            }

            string code = rawText.Trim();
            if (string.Equals(code, "None", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return code.ToUpperInvariant();
        }

        private static string? NormalizeEnumCountryCode(Enum enumValue)
        {
            string enumName = enumValue.ToString() ?? string.Empty;
            if (enumName.Length == 2)
            {
                return enumName.ToUpperInvariant();
            }

            Type enumType = enumValue.GetType();
            long enumIntegralValue = Convert.ToInt64(enumValue, CultureInfo.InvariantCulture);
            foreach (string name in Enum.GetNames(enumType))
            {
                object parsedValue = Enum.Parse(enumType, name);
                long parsedIntegralValue = Convert.ToInt64(parsedValue, CultureInfo.InvariantCulture);
                if (parsedIntegralValue == enumIntegralValue && name.Length == 2)
                {
                    return name.ToUpperInvariant();
                }
            }

            return null;
        }

        #endregion
    }
}