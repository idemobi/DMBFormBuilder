#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Collections.Generic;
using System.IO;
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
    ///     Builds a Bootstrap radio group for enum-like option selection.
    /// </summary>
    public sealed class EnumRadioFieldBuilder :
        HtmlBuilderBase<EnumRadioFieldBuilder>,
        ICanUseCustomClasses
    {
        #region Static methods

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

        #endregion

        #region Instance fields and properties

        private string _description = string.Empty;
        private bool _disabled;
        private bool _inline;
        private string _inputId = "EnumRadioField";
        private string _inputName = "EnumRadioField";
        private string _label = "Options";
        private IconStruct _labelIcon = IconStruct.Empty;

        private readonly List<EnumRadioOption> _options = new();
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private bool _required;
        private string _requiredMessage = string.Empty;
        private string? _value;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnumRadioFieldBuilder" /> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public EnumRadioFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "fieldset";
            this.AddClasses("dmb-form-field", "dmb-enum-radio-field", "mb-3");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds a radio option to the group.
        /// </summary>
        public EnumRadioFieldBuilder AddOption(string value, string text)
        {
            _options.Add(new EnumRadioOption(value, text));
            return this;
        }

        /// <inheritdoc />
        protected override EnumRadioFieldBuilder CreateInstance()
        {
            return new EnumRadioFieldBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc />
        protected override void InternalClone(EnumRadioFieldBuilder source)
        {
            base.InternalClone(source);
            _inputId = source._inputId;
            _inputName = source._inputName;
            _label = source._label;
            _description = source._description;
            _value = source._value;
            _requiredMessage = source._requiredMessage;
            _presentation = source._presentation;
            _disabled = source._disabled;
            _required = source._required;
            _labelIcon = source._labelIcon;
            _inline = source._inline;
            _options.Clear();
            _options.AddRange(source._options);
        }

        /// <summary>
        ///     Sets the optional description rendered as an information popover next to the field legend.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="EnumRadioFieldBuilder" /> instance for fluent chaining.</returns>
        public EnumRadioFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Enables or disables all rendered radio inputs.
        /// </summary>
        public new EnumRadioFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        ///     Renders radio options inline when enabled.
        /// </summary>
        public EnumRadioFieldBuilder SetInline(bool inline = true)
        {
            _inline = inline;
            return this;
        }

        /// <summary>
        ///     Sets the base radio identifier and shared model binding name.
        /// </summary>
        public EnumRadioFieldBuilder SetInput(string inputId, string inputName)
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
        ///     Sets the radio group label when a non-empty value is provided.
        /// </summary>
        public EnumRadioFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        ///     Sets the icon rendered with the radio group label.
        /// </summary>
        public EnumRadioFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        ///     Sets how the radio group label is positioned.
        /// </summary>
        public EnumRadioFieldBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        ///     Adds required validation metadata to the radio group.
        /// </summary>
        public EnumRadioFieldBuilder SetRequired(bool required = true, string? message = null)
        {
            _required = required;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _requiredMessage = message;
            }

            return this;
        }

        /// <summary>
        ///     Sets the selected radio option value.
        /// </summary>
        public EnumRadioFieldBuilder SetValue(string? value)
        {
            _value = value;
            return this;
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"form-control dmb-choice-floating-control\">");
            WriteLegend(writer, encoder, "dmb-choice-floating-label");
            WriteOptions(writer, encoder, inlineOptions: true);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        private void WriteGroup(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            WriteLegend(writer, encoder, "input-group-text dmb-choice-group-label");
            writer.Write("<div class=\"form-control dmb-choice-group-control\">");
            WriteOptions(writer, encoder, inlineOptions: true);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteInline(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"row g-2 align-items-start\">");
            writer.Write("<div class=\"col-sm-4\">");
            WriteLegend(writer, encoder, "col-form-label");
            writer.Write("</div><div class=\"col-sm-8\">");
            WriteOptions(writer, encoder, inlineOptions: true);
            writer.Write("</div>");
            writer.Write("<div class=\"offset-sm-4 col-sm-8\">");
            WriteValidation(writer, encoder);
            writer.Write("</div></div>");
        }

        private void WriteLabelIcon(TextWriter writer, HtmlEncoder encoder)
        {
            if (_labelIcon.IsEmpty)
            {
                return;
            }

            HtmlLayoutExtensions.IconBuilder(_htmlHelper, _labelIcon, "me-1").WriteTo(writer, encoder);
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

        private void WriteOptions(TextWriter writer, HtmlEncoder encoder, bool inlineOptions)
        {
            for (int index = 0; index < _options.Count; index++)
            {
                EnumRadioOption option = _options[index];
                string optionId = $"{_inputId}_{option.Value}";
                writer.Write("<div class=\"form-check");
                if (inlineOptions)
                {
                    writer.Write(" form-check-inline");
                }

                writer.Write("\">");
                writer.Write("<input class=\"form-check-input\" type=\"radio\"");
                WriteAttribute(writer, encoder, "id", optionId);
                WriteAttribute(writer, encoder, "name", _inputName);
                WriteAttribute(writer, encoder, "value", option.Value);
                WriteAttribute(writer, encoder, "data-val", "true");
                if (_required)
                {
                    WriteAttribute(writer, encoder, "required", "required");
                    WriteAttribute(writer, encoder, "data-val-required", _requiredMessage);
                }

                if (string.Equals(option.Value, _value, StringComparison.Ordinal))
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

        /// <summary>
        ///     Writes the complete radio group markup, including legend, radio inputs, and validation feedback.
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
                    WriteFloating(writer, encoder);
                break;
                case FormLabelPresentation.Inline:
                    WriteInline(writer, encoder);
                break;
                case FormLabelPresentation.Group:
                    WriteGroup(writer, encoder);
                break;
                case FormLabelPresentation.Hidden:
                    WriteLegend(writer, encoder, "visually-hidden");
                    WriteOptions(writer, encoder, inlineOptions: _inline);
                    WriteValidation(writer, encoder);
                break;
                case FormLabelPresentation.Normal:
                default:
                    WriteLegend(writer, encoder, "form-label fs-6");
                    WriteOptions(writer, encoder, inlineOptions: _inline);
                    WriteValidation(writer, encoder);
                break;
            }

            writer.Write($"</{_tag}>");
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

        #endregion

        #region Nested type: EnumRadioOption

        /// <summary>
        ///     Represents a radio option rendered by <see cref="EnumRadioFieldBuilder" />.
        /// </summary>
        /// <param name="Value">The option value submitted by the selected radio input.</param>
        /// <param name="Text">The option text rendered to the user.</param>
        public sealed record EnumRadioOption(string Value, string Text);

        #endregion
    }
}