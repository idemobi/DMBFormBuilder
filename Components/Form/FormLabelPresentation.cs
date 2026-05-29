#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Defines how labels are rendered for form fields.
    /// </summary>
    public enum FormLabelPresentation
    {
        /// <summary>
        ///     Renders the label as a standard Bootstrap form label above or beside the field.
        /// </summary>
        Normal = 0,

        /// <summary>
        ///     Renders the label inside a Bootstrap floating label container.
        /// </summary>
        Floating = 1,

        /// <summary>
        ///     Keeps the label available to assistive technology while hiding it visually.
        /// </summary>
        Hidden = 2,

        /// <summary>
        ///     Renders the label inline with the field control.
        /// </summary>
        Inline = 3,

        /// <summary>
        ///     Renders the label as part of an input group structure.
        /// </summary>
        Group = 4
    }
}