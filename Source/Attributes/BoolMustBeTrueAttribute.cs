#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DMBServerHelper;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Specifies that a Boolean form field must be <see langword="true" />.
    /// </summary>
    /// <remarks>
    ///     <see cref="BooleanFieldBuilderExtensions.CheckboxFieldBuilderFor{TModel}" /> and
    ///     <see cref="BooleanFieldBuilderExtensions.SwitchFieldBuilderFor{TModel}" /> translate this attribute into
    ///     FormBuilder Boolean validation metadata.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class BoolMustBeTrueAttribute : ValidationAttribute
    {
        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BoolMustBeTrueAttribute" /> class.
        /// </summary>
        public BoolMustBeTrueAttribute()
            : base("The {0} field must be true.")
        {
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Formats and localizes the validation error message.
        /// </summary>
        /// <param name="name">The display name of the validated member.</param>
        /// <returns>The localized validation message.</returns>
        public override string FormatErrorMessage(string name)
        {
            string message = ErrorMessageString ?? "The {0} field must be true.";
            string localizedMessage = WebLocalizer.GetDataAnnotation(message);
            return string.Format(CultureInfo.CurrentCulture, localizedMessage, name);
        }

        /// <summary>
        ///     Validates whether the provided value is explicitly <see langword="true" />.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>
        ///     <see langword="true" /> when <paramref name="value" /> is a Boolean equal to <see langword="true" />;
        ///     otherwise, <see langword="false" />.
        /// </returns>
        public override bool IsValid(object? value)
        {
            return value is bool booleanValue && booleanValue == true;
        }

        #endregion
    }
}
