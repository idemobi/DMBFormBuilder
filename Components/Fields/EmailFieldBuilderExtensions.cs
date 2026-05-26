#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj EmailFieldBuilderExtensions.cs create at 2026/05/12
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
    /// Provides Razor helper entry points for email-specialized <see cref="TextFieldBuilder"/> instances.
    /// </summary>
    public static class EmailFieldBuilderExtensions
    {
        /// <summary>
        /// Creates an email input builder for a non-generic Razor view.
        /// </summary>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="TextFieldBuilder"/> configured with email input attributes.</returns>
        public static TextFieldBuilder EmailFieldBuilder(this IHtmlHelper html)
        {
            return ConfigureEmailField(html.TextFieldBuilder(), null);
        }

        /// <summary>
        /// Creates an email input builder for a strongly typed Razor view.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <param name="html">The HTML helper that supplies the current view writer and context.</param>
        /// <returns>A <see cref="TextFieldBuilder"/> configured with email input attributes.</returns>
        public static TextFieldBuilder EmailFieldBuilder<TModel>(this IHtmlHelper<TModel> html)
        {
            return ConfigureEmailField(html.TextFieldBuilder(), null);
        }

        /// <summary>
        /// Creates an email input builder bound to a model property expression.
        /// </summary>
        /// <typeparam name="TModel">The Razor view model type.</typeparam>
        /// <typeparam name="TProperty">The bound property type.</typeparam>
        /// <param name="html">The strongly typed HTML helper.</param>
        /// <param name="expression">A member expression used to derive field metadata and optional <see cref="EmailAddressAttribute"/> messages.</param>
        /// <returns>A configured <see cref="TextFieldBuilder"/> with email validation metadata.</returns>
        public static TextFieldBuilder EmailFieldBuilderFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            TextFieldBuilder builder = html.TextFieldBuilderFor(expression);
            return ConfigureEmailField(builder, ResolveEmailMessage<TModel, TProperty>(expression));
        }

        private static TextFieldBuilder ConfigureEmailField(TextFieldBuilder builder, string? invalidEmailMessage)
        {
            return builder
                .SetInputType("email")
                .SetInputAttribute("autocomplete", "email")
                .SetInputAttribute("inputmode", "email")
                .SetInputAttribute("data-val-email", invalidEmailMessage ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid)));
        }

        private static string? ResolveEmailMessage<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body is not MemberExpression memberExpression)
            {
                return null;
            }

            PropertyInfo? propertyInfo = typeof(TModel).GetProperty(memberExpression.Member.Name);
            EmailAddressAttribute? emailAddress = propertyInfo?.GetCustomAttribute<EmailAddressAttribute>();
            if (!string.IsNullOrWhiteSpace(emailAddress?.ErrorMessage))
            {
                return WebLocalizer.GetDataAnnotation(emailAddress.ErrorMessage);
            }

            return null;
        }
    }
}
