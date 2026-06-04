#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Defines the semantic constraint profile applied by <see cref="TextFieldBuilder.SetKind" />.
    /// </summary>
    public enum TextFieldInputKind
    {
        /// <summary>
        ///     Renders a regular text input without additional semantic constraints.
        /// </summary>
        Text,

        /// <summary>
        ///     Renders a numeric input constrained to integer-like values.
        /// </summary>
        Numeric,

        /// <summary>
        ///     Renders a numeric input constrained to decimal values.
        /// </summary>
        Decimal,

        /// <summary>
        ///     Renders a text input constrained to ASCII-compatible content.
        /// </summary>
        Ascii,

        /// <summary>
        ///     Renders a text input constrained to Unix-friendly text content.
        /// </summary>
        Unix
    }
}