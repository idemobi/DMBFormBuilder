#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj TextFieldInputKind.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Defines the semantic constraint profile applied by <see cref="TextFieldBuilder.SetKind"/>.
    /// </summary>
    public enum TextFieldInputKind
    {
        /// <summary>
        /// Renders a regular text input without additional semantic constraints.
        /// </summary>
        Text,

        /// <summary>
        /// Renders a numeric input constrained to integer-like values.
        /// </summary>
        Numeric,

        /// <summary>
        /// Renders a numeric input constrained to decimal values.
        /// </summary>
        Decimal,

        /// <summary>
        /// Renders a text input constrained to ASCII-compatible content.
        /// </summary>
        Ascii,

        /// <summary>
        /// Renders a text input constrained to Unix-friendly text content.
        /// </summary>
        Unix
    }
}
