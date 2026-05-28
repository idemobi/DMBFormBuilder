#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FlagFieldBuilder.cs create at 2026/05/13
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBFormBuilder.Resources;
using DMBPageBuilder;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Builds a flags enum field as either a checkbox collection or a select control.
    /// </summary>
    public sealed class FlagFieldBuilder :
        HtmlBuilderBase<FlagFieldBuilder>,
        ICanUseCustomClasses
    {
        /// <summary>
        /// Represents one selectable flag option.
        /// </summary>
        /// <param name="Value">The numeric flag value submitted by the option.</param>
        /// <param name="Text">The option text rendered to the user.</param>
        /// <param name="Selected">Whether the flag is selected initially.</param>
        public sealed record FlagOption(long Value, string Text, bool Selected);

        private readonly List<FlagOption> _options = new();
        private string _inputId = "FlagField";
        private string _inputName = "FlagField";
        private string _label = "Flags";
        private string _description = string.Empty;
        private string _requiredMessage = string.Empty;
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private bool _disabled;
        private bool _required;
        private IconStruct _labelIcon = IconStruct.Empty;
        private bool _asSelect;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlagFieldBuilder"/> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public FlagFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "fieldset";
            this.AddClasses("dmb-form-field", "dmb-flag-field", "mb-3");
        }

        /// <summary>
        /// Sets the base input identifier and model binding name for flag values.
        /// </summary>
        public FlagFieldBuilder SetInput(string inputId, string inputName)
        {
            if (!string.IsNullOrWhiteSpace(inputId))
            {
                _inputId = inputId;
            }

            if (!string.IsNullOrWhiteSpace(inputName))
            {
                _inputName = inputName;
            }

            return this;
        }

        /// <summary>
        /// Sets the flags field label when a non-empty value is provided.
        /// </summary>
        public FlagFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        /// Sets the optional description rendered as an information popover next to the field legend.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="FlagFieldBuilder"/> instance for fluent chaining.</returns>
        public FlagFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Sets how the flags field label is positioned.
        /// </summary>
        public FlagFieldBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        /// Sets the icon rendered with the flags field label.
        /// </summary>
        public FlagFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        /// Adds a flag option to the field.
        /// </summary>
        public FlagFieldBuilder AddOption(long value, string text, bool selected)
        {
            _options.Add(new FlagOption(value, text, selected));
            return this;
        }

        /// <summary>
        /// Adds required validation metadata to the flags field.
        /// </summary>
        public FlagFieldBuilder SetRequired(bool required = true, string? message = null)
        {
            _required = required;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _requiredMessage = message;
            }

            return this;
        }

        /// <summary>
        /// Enables or disables all rendered flag inputs.
        /// </summary>
        public new FlagFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        /// Selects whether the flags are rendered as a select control or as checkboxes.
        /// </summary>
        public FlagFieldBuilder RenderAsSelect(bool asSelect = true)
        {
            _asSelect = asSelect;
            return this;
        }

        /// <inheritdoc/>
        protected override FlagFieldBuilder CreateInstance()
        {
            return new FlagFieldBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc/>
        protected override void InternalClone(FlagFieldBuilder source)
        {
            base.InternalClone(source);
            _inputId = source._inputId;
            _inputName = source._inputName;
            _label = source._label;
            _description = source._description;
            _requiredMessage = source._requiredMessage;
            _presentation = source._presentation;
            _disabled = source._disabled;
            _required = source._required;
            _labelIcon = source._labelIcon;
            _asSelect = source._asSelect;
            _options.Clear();
            _options.AddRange(source._options);
        }

        /// <summary>
        /// Writes the complete flags field markup in select or checkbox mode.
        /// </summary>
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            if (string.IsNullOrWhiteSpace(_requiredMessage))
            {
                _requiredMessage = WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required));
            }

            writer.Write($"<{_tag}{BuildAttributes()}>");
            switch (_presentation)
            {
                case FormLabelPresentation.Floating:
                    WriteHiddenInput(writer, encoder);
                    WriteFloating(writer, encoder);
                    break;
                case FormLabelPresentation.Inline:
                    WriteHiddenInput(writer, encoder);
                    WriteInline(writer, encoder);
                    break;
                case FormLabelPresentation.Group:
                    WriteHiddenInput(writer, encoder);
                    WriteGroup(writer, encoder);
                    break;
                case FormLabelPresentation.Hidden:
                    WriteLegend(writer, encoder, "visually-hidden");
                    WriteHiddenInput(writer, encoder);
                    WriteOptions(writer, encoder, inlineCheckboxes: false);
                    WriteValidation(writer, encoder);
                    break;
                case FormLabelPresentation.Normal:
                default:
                    WriteLegend(writer, encoder, "form-label fs-6");
                    WriteHiddenInput(writer, encoder);
                    WriteOptions(writer, encoder, inlineCheckboxes: false);
                    WriteValidation(writer, encoder);
                    break;
            }
            writer.Write($"</{_tag}>");
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"form-control dmb-choice-floating-control\">");
            WriteLegend(writer, encoder, "dmb-choice-floating-label");
            WriteOptions(writer, encoder, inlineCheckboxes: true);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        private void WriteInline(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"row g-2 align-items-start\">");
            writer.Write("<div class=\"col-sm-4\">");
            WriteLegend(writer, encoder, "col-form-label");
            writer.Write("</div><div class=\"col-sm-8\">");
            WriteOptions(writer, encoder, inlineCheckboxes: true);
            writer.Write("</div>");
            writer.Write("<div class=\"offset-sm-4 col-sm-8\">");
            WriteValidation(writer, encoder);
            writer.Write("</div></div>");
        }

        private void WriteGroup(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            WriteLegend(writer, encoder, "input-group-text dmb-choice-group-label");
            if (_asSelect)
            {
                WriteSelect(writer, encoder);
            }
            else
            {
                writer.Write("<div class=\"form-control dmb-choice-group-control\">");
                WriteCheckboxes(writer, encoder, inlineCheckboxes: true);
                writer.Write("</div>");
            }
            WriteValidation(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteLegend(TextWriter writer, HtmlEncoder encoder, string cssClass)
        {
            bool writeDescriptionAfterLegend = cssClass.Contains("visually-hidden", StringComparison.Ordinal);
            writer.Write("<legend");
            WriteAttribute(writer, encoder, "class", cssClass);
            writer.Write(">");
            WriteLabelIcon(writer, encoder);
            encoder.Encode(writer, _label);
            if (_required)
            {
                writer.Write("<span class=\"text-danger ms-1\" aria-hidden=\"true\">*</span>");
                writer.Write("<span class=\"visually-hidden\"> ");
                encoder.Encode(writer, _requiredMessage);
                writer.Write("</span>");
            }
            if (!writeDescriptionAfterLegend)
            {
                FormFieldDescriptionPopoverRenderer.Write(writer, encoder, _description);
            }
            writer.Write("</legend>");
            if (writeDescriptionAfterLegend)
            {
                FormFieldDescriptionPopoverRenderer.Write(writer, encoder, _description);
            }
        }

        private void WriteHiddenInput(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<input type=\"hidden\"");
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "value", CombinedValue().ToString());
            WriteAttribute(writer, encoder, "data-dmb-flag-hidden", _inputId);
            if (_required)
            {
                WriteAttribute(writer, encoder, "required", "required");
                WriteAttribute(writer, encoder, "data-val", "true");
                WriteAttribute(writer, encoder, "data-val-required", _requiredMessage);
                WriteAttribute(writer, encoder, "data-val-flag-required", _requiredMessage);
            }
            writer.Write(">");
        }

        private void WriteOptions(TextWriter writer, HtmlEncoder encoder, bool inlineCheckboxes)
        {
            writer.Write("<div class=\"dmb-flag-field-options\">");
            if (_asSelect)
            {
                WriteSelect(writer, encoder);
            }
            else
            {
                WriteCheckboxes(writer, encoder, inlineCheckboxes);
            }
            writer.Write("</div>");
        }

        private void WriteValidation(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"invalid-feedback\" data-valmsg-for=\"");
            encoder.Encode(writer, _inputName);
            writer.Write("\" data-valmsg-replace=\"true\">");
            if (_required)
            {
                encoder.Encode(writer, _requiredMessage);
            }
            writer.Write("</div>");
        }

        private void WriteLabelIcon(TextWriter writer, HtmlEncoder encoder)
        {
            if (_labelIcon.IsEmpty)
            {
                return;
            }

            HtmlLayoutExtensions.IconBuilder(_htmlHelper, _labelIcon, "me-1").WriteTo(writer, encoder);
        }

        private void WriteCheckboxes(TextWriter writer, HtmlEncoder encoder, bool inlineCheckboxes)
        {
            foreach (FlagOption option in _options)
            {
                string optionId = $"{_inputId}_{option.Value}";
                writer.Write("<div class=\"form-check");
                if (inlineCheckboxes)
                {
                    writer.Write(" form-check-inline");
                }
                writer.Write("\">");
                writer.Write("<input class=\"form-check-input\" type=\"checkbox\"");
                WriteAttribute(writer, encoder, "id", optionId);
                WriteAttribute(writer, encoder, "value", option.Value.ToString());
                WriteAttribute(writer, encoder, "data-dmb-flag-for", _inputId);
                if (option.Selected)
                {
                    writer.Write(" checked");
                }
                if (_disabled)
                {
                    writer.Write(" disabled");
                }
                writer.Write(">");
                writer.Write("<label class=\"form-check-label\"");
                WriteAttribute(writer, encoder, "for", optionId);
                writer.Write(">");
                encoder.Encode(writer, option.Text);
                writer.Write("</label></div>");
            }
        }

        private void WriteSelect(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<select class=\"form-select\" multiple");
            WriteAttribute(writer, encoder, "data-dmb-flag-for", _inputId);
            if (_disabled)
            {
                writer.Write(" disabled");
            }
            writer.Write(">");

            foreach (FlagOption option in _options)
            {
                writer.Write("<option");
                WriteAttribute(writer, encoder, "value", option.Value.ToString());
                if (option.Selected)
                {
                    writer.Write(" selected");
                }
                writer.Write(">");
                encoder.Encode(writer, option.Text);
                writer.Write("</option>");
            }
            writer.Write("</select>");
        }

        private long CombinedValue()
        {
            long result = 0;
            foreach (FlagOption option in _options.Where(option => option.Selected))
            {
                result |= option.Value;
            }

            return result;
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
