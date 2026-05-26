#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FormSubmissionMethod.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Defines the HTTP submission method rendered on a <see cref="FormBuilder"/> element.
    /// </summary>
    public enum FormSubmissionMethod
    {
        /// <summary>
        /// Renders a form submitted with the HTTP GET method.
        /// </summary>
        Get = 0,

        /// <summary>
        /// Renders a form submitted with the HTTP POST method.
        /// </summary>
        Post = 1,

        /// <summary>
        /// Renders the HTML dialog submission method used by dialog-contained forms.
        /// </summary>
        Dialog = 2
    }
}
