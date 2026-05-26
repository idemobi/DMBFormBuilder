#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FormValidationMode.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Defines how validation attributes and feedback are rendered.
    /// </summary>
    public enum FormValidationMode
    {
        /// <summary>
        /// Disables FormBuilder-specific validation markup.
        /// </summary>
        None = 0,

        /// <summary>
        /// Renders markup intended for server-side validation feedback only.
        /// </summary>
        Server = 1,

        /// <summary>
        /// Renders client validation attributes and behavior without requiring server feedback markup.
        /// </summary>
        Client = 2,

        /// <summary>
        /// Renders client validation attributes together with server-side validation feedback hooks.
        /// </summary>
        ClientAndServer = 3
    }
}
