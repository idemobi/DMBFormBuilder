#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FormFieldDisplayMetadata.cs create at 2026/05/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.ComponentModel.DataAnnotations;
using DMBServerHelper;

#endregion

namespace DMBFormBuilder
{
    internal static class FormFieldDisplayMetadata
    {
        internal static string ResolveDescription(DisplayAttribute? display)
        {
            string? description = display?.GetDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return string.Empty;
            }

            return WebLocalizer.GetDataAnnotation(description);
        }
    }
}
