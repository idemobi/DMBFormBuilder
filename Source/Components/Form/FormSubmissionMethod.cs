#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Defines the HTTP submission method rendered on a <see cref="FormBuilder" /> element.
    /// </summary>
    public enum FormSubmissionMethod
    {
        /// <summary>
        ///     Renders a form submitted with the HTTP GET method.
        /// </summary>
        Get = 0,

        /// <summary>
        ///     Renders a form submitted with the HTTP POST method.
        /// </summary>
        Post = 1,

        /// <summary>
        ///     Renders the HTML dialog submission method used by dialog-contained forms.
        /// </summary>
        Dialog = 2
    }
}