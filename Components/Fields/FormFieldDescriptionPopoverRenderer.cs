#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FormFieldDescriptionPopoverRenderer.cs create at 2026/05/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Text.Encodings.Web;

#endregion

namespace DMBFormBuilder
{
    internal static class FormFieldDescriptionPopoverRenderer
    {
        internal static void Write(TextWriter writer, HtmlEncoder encoder, string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return;
            }

            writer.Write("<span");
            WriteAttribute(writer, encoder, "class", "btn btn-link btn-sm p-0 ms-1 dmb-form-field-description");
            WriteAttribute(writer, encoder, "role", "button");
            WriteAttribute(writer, encoder, "tabindex", "0");
            WriteAttribute(writer, encoder, "data-dmb-form-field-description", "true");
            WriteAttribute(writer, encoder, "data-bs-toggle", "popover");
            WriteAttribute(writer, encoder, "data-bs-trigger", "hover focus");
            WriteAttribute(writer, encoder, "data-bs-placement", "auto");
            WriteAttribute(writer, encoder, "data-bs-content", description);
            WriteAttribute(writer, encoder, "aria-label", "Information");
            writer.Write("><span class=\"bi bi-info-circle\" aria-hidden=\"true\"></span></span>");
        }

        private static void WriteAttribute(TextWriter writer, HtmlEncoder encoder, string name, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            writer.Write(' ');
            writer.Write(name);
            writer.Write("=\"");
            encoder.Encode(writer, value);
            writer.Write('"');
        }
    }
}
