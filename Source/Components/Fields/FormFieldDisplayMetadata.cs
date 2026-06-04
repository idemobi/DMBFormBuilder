#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.ComponentModel.DataAnnotations;
using DMBServerHelper;

#endregion

namespace DMBFormBuilder
{
    internal static class FormFieldDisplayMetadata
    {
        #region Static methods

        internal static string ResolveDescription(DisplayAttribute? display)
        {
            string? description = display?.GetDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return string.Empty;
            }

            return WebLocalizer.GetDataAnnotation(description);
        }

        #endregion
    }
}