#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Defines how validation attributes and feedback are rendered.
    /// </summary>
    public enum FormValidationMode
    {
        /// <summary>
        ///     Disables FormBuilder-specific validation markup.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Renders markup intended for server-side validation feedback only.
        /// </summary>
        Server = 1,

        /// <summary>
        ///     Renders client validation attributes and behavior without requiring server feedback markup.
        /// </summary>
        Client = 2,

        /// <summary>
        ///     Renders client validation attributes together with server-side validation feedback hooks.
        /// </summary>
        ClientAndServer = 3
    }
}