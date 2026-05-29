#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

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
    ///     Builds a Bootstrap password input with optional visibility toggle, strength meter, and validation metadata.
    /// </summary>
    public sealed class PasswordFieldBuilder :
        HtmlBuilderBase<PasswordFieldBuilder>,
        ICanUseCustomClasses
    {
        #region Static methods

        private static bool IsReservedInputAttribute(string key)
        {
            return string.Equals(key, "class", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "type", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "id", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "name", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "placeholder", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "value", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "autocomplete", StringComparison.OrdinalIgnoreCase);
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

        #endregion

        #region Instance fields and properties

        private string _description = string.Empty;
        private bool _disabled;
        private readonly Dictionary<string, string> _inputAttributes = new(StringComparer.OrdinalIgnoreCase);

        private string _inputId = "PasswordField";
        private string _inputName = "PasswordField";
        private string _label = "Password";
        private IconStruct _labelIcon = IconStruct.Empty;
        private string _placeholder = string.Empty;
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private bool _required;
        private string _requiredMessage = string.Empty;
        private bool _showStrengthMeter;
        private bool _toggleEnabled = true;
        private string? _value;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PasswordFieldBuilder" /> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public PasswordFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClasses("dmb-form-field", "dmb-password-field", "mb-3");
        }

        #endregion

        #region Instance methods

        /// <inheritdoc />
        protected override PasswordFieldBuilder CreateInstance()
        {
            return new PasswordFieldBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc />
        protected override void InternalClone(PasswordFieldBuilder source)
        {
            base.InternalClone(source);
            _inputId = source._inputId;
            _inputName = source._inputName;
            _label = source._label;
            _description = source._description;
            _placeholder = source._placeholder;
            _value = source._value;
            _requiredMessage = source._requiredMessage;
            _presentation = source._presentation;
            _disabled = source._disabled;
            _required = source._required;
            _labelIcon = source._labelIcon;
            _showStrengthMeter = source._showStrengthMeter;
            _toggleEnabled = source._toggleEnabled;

            _inputAttributes.Clear();
            foreach (KeyValuePair<string, string> attribute in source._inputAttributes)
            {
                _inputAttributes[attribute.Key] = attribute.Value;
            }
        }

        /// <summary>
        ///     Adds equality validation metadata that compares this password with another input.
        /// </summary>
        public PasswordFieldBuilder SetCompareTo(string? otherInputName, string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(otherInputName))
            {
                SetInputAttribute("data-val-equalto-other", otherInputName);
                SetInputAttribute("data-val-equalto", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCompare)));
            }

            return this;
        }

        /// <summary>
        ///     Sets the optional description rendered as an information popover next to the field label.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="PasswordFieldBuilder" /> instance for fluent chaining.</returns>
        public PasswordFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Enables or disables the rendered password input.
        /// </summary>
        public new PasswordFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        ///     Sets the password input identifier and model binding name.
        /// </summary>
        public PasswordFieldBuilder SetInput(string inputId, string inputName)
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
        ///     Sets or replaces an attribute on the rendered password input.
        /// </summary>
        public PasswordFieldBuilder SetInputAttribute(string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                _inputAttributes[name] = value;
            }

            return this;
        }

        /// <summary>
        ///     Sets or replaces multiple attributes on the rendered password input.
        /// </summary>
        public PasswordFieldBuilder SetInputAttributes(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                SetInputAttribute(attribute.Key, attribute.Value);
            }

            return this;
        }

        /// <summary>
        ///     Sets the password label when a non-empty value is provided.
        /// </summary>
        public PasswordFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        ///     Sets the icon rendered with the password label.
        /// </summary>
        public PasswordFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        ///     Sets how the password label is positioned or hidden.
        /// </summary>
        public PasswordFieldBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        ///     Adds maximum length validation metadata to the password input.
        /// </summary>
        public PasswordFieldBuilder SetMaxLength(int maxLength, string? message = null)
        {
            if (maxLength > 0)
            {
                SetInputAttribute("maxlength", maxLength.ToString());
                SetInputAttribute("data-val-maxlength-max", maxLength.ToString());
                SetInputAttribute("data-val-maxlength", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength)));
            }

            return this;
        }

        /// <summary>
        ///     Adds minimum length validation metadata to the password input.
        /// </summary>
        public PasswordFieldBuilder SetMinLength(int minLength, string? message = null)
        {
            if (minLength > 0)
            {
                SetInputAttribute("minlength", minLength.ToString());
                SetInputAttribute("data-val-minlength-min", minLength.ToString());
                SetInputAttribute("data-val-minlength", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength)));
            }

            return this;
        }

        /// <summary>
        ///     Adds a regex pattern constraint to the password input.
        /// </summary>
        public PasswordFieldBuilder SetPattern(string? pattern, string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(pattern))
            {
                SetInputAttribute("pattern", pattern);
                SetInputAttribute("data-val-regex-pattern", pattern);
                SetInputAttribute("data-val-regex", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid)));
            }

            return this;
        }

        /// <summary>
        ///     Sets the password placeholder text.
        /// </summary>
        public PasswordFieldBuilder SetPlaceholder(string? placeholder)
        {
            _placeholder = placeholder ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Adds required validation metadata to the password input.
        /// </summary>
        public PasswordFieldBuilder SetRequired(bool required = true, string? message = null)
        {
            _required = required;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _requiredMessage = message;
            }

            return this;
        }

        /// <summary>
        ///     Enables or disables the password visibility toggle button.
        /// </summary>
        public PasswordFieldBuilder SetToggle(bool enabled = true)
        {
            _toggleEnabled = enabled;
            return this;
        }

        /// <summary>
        ///     Sets the sensitive password value rendered in the input.
        /// </summary>
        public PasswordFieldBuilder SetValue(string? value)
        {
            _value = value;
            return this;
        }

        /// <summary>
        ///     Shows or hides the password strength meter metadata and visual container.
        /// </summary>
        public PasswordFieldBuilder ShowStrengthMeter(bool show = true)
        {
            _showStrengthMeter = show;
            return this;
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            writer.Write("<div class=\"form-floating flex-grow-1\">");
            WriteInput(writer, encoder);
            WriteLabel(writer, encoder, string.Empty);
            writer.Write("</div>");
            WriteToggleButton(writer, encoder);
            WriteValidation(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteGroup(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            WriteLabel(writer, encoder, "input-group-text");
            WriteInput(writer, encoder);
            WriteToggleButton(writer, encoder);
            WriteValidation(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteHiddenLabel(TextWriter writer, HtmlEncoder encoder)
        {
            WriteLabel(writer, encoder, "visually-hidden");
            WriteInputGroup(writer, encoder);
        }

        private void WriteInline(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"row g-2 align-items-start\">");
            writer.Write("<div class=\"col-sm-4\">");
            WriteLabel(writer, encoder, "col-form-label");
            writer.Write("</div>");
            writer.Write("<div class=\"col-sm-8\">");
            WriteInputGroup(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WriteInput(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<input class=\"form-control\"");
            WriteAttribute(writer, encoder, "type", "password");
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "placeholder", string.IsNullOrWhiteSpace(_placeholder) ? _label : _placeholder);
            WriteAttribute(writer, encoder, "value", _value);
            string autocomplete = _inputAttributes.TryGetValue("autocomplete", out string? customAutocomplete)
                ? customAutocomplete
                : _showStrengthMeter
                    ? "new-password"
                    : "current-password";
            WriteAttribute(writer, encoder, "autocomplete", autocomplete);
            WriteAttribute(writer, encoder, "data-val", "true");
            WriteAttribute(writer, encoder, "data-dmb-password-input", "true");

            if (_required)
            {
                WriteAttribute(writer, encoder, "required", "required");
                WriteAttribute(writer, encoder, "data-val-required", _requiredMessage);
            }

            foreach (KeyValuePair<string, string> attribute in _inputAttributes)
            {
                if (IsReservedInputAttribute(attribute.Key))
                {
                    continue;
                }

                WriteAttribute(writer, encoder, attribute.Key, attribute.Value);
            }

            if (_disabled)
            {
                writer.Write(" readonly");
                WriteAttribute(writer, encoder, "aria-disabled", "true");
            }

            writer.Write(">");
        }

        private void WriteInputGroup(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            WriteInput(writer, encoder);
            WriteToggleButton(writer, encoder);
            WriteValidation(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteLabel(TextWriter writer, HtmlEncoder encoder, string cssClass)
        {
            bool writeDescriptionAfterLabel = cssClass.Contains("visually-hidden", StringComparison.Ordinal);
            writer.Write("<label");
            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                WriteAttribute(writer, encoder, "class", cssClass);
            }

            WriteAttribute(writer, encoder, "for", _inputId);
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

            if (!writeDescriptionAfterLabel)
            {
                FormFieldDescriptionPopoverRenderer.Write(writer, encoder, _description);
            }

            writer.Write("</label>");
            if (writeDescriptionAfterLabel)
            {
                FormFieldDescriptionPopoverRenderer.Write(writer, encoder, _description);
            }
        }

        private void WriteLabelIcon(TextWriter writer, HtmlEncoder encoder)
        {
            if (_labelIcon.IsEmpty)
            {
                return;
            }

            HtmlLayoutExtensions.IconBuilder(_htmlHelper, _labelIcon, "me-1").WriteTo(writer, encoder);
        }

        private void WriteNormal(TextWriter writer, HtmlEncoder encoder)
        {
            WriteLabel(writer, encoder, "form-label");
            WriteInputGroup(writer, encoder);
        }

        private void WriteStrengthMeter(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"progress mt-1 dmb-password-strength\" role=\"progressbar\" aria-label=\"");
            encoder.Encode(writer, WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordStrength)));
            writer.Write("\" aria-valuenow=\"5\" aria-valuemin=\"0\" aria-valuemax=\"100\" data-dmb-password-strength=\"");
            encoder.Encode(writer, _inputId);
            writer.Write("\"><div class=\"progress-bar bg-danger\" style=\"width:5%\"></div></div>");
        }

        /// <summary>
        ///     Writes the complete Bootstrap password field markup, including optional toggle and strength meter.
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
                case FormLabelPresentation.Hidden:
                    WriteHiddenLabel(writer, encoder);
                break;
                case FormLabelPresentation.Inline:
                    WriteInline(writer, encoder);
                break;
                case FormLabelPresentation.Group:
                    WriteGroup(writer, encoder);
                break;
                case FormLabelPresentation.Normal:
                default:
                    WriteNormal(writer, encoder);
                break;
            }

            if (_showStrengthMeter)
            {
                WriteStrengthMeter(writer, encoder);
            }

            writer.Write($"</{_tag}>");
        }

        private void WriteToggleButton(TextWriter writer, HtmlEncoder encoder)
        {
            if (!_toggleEnabled)
            {
                return;
            }

            writer.Write("<button class=\"btn btn-outline-secondary\" type=\"button\" data-dmb-password-toggle=\"");
            encoder.Encode(writer, _inputId);
            if (_disabled)
            {
                writer.Write("\" data-dmb-password-toggle-readonly=\"true");
            }

            writer.Write("\" data-dmb-password-show-label=\"");
            encoder.Encode(writer, WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordShow)));
            writer.Write("\" data-dmb-password-hide-label=\"");
            encoder.Encode(writer, WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordHide)));
            writer.Write("\" aria-label=\"");
            encoder.Encode(writer, WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordShow)));
            writer.Write("\"><span class=\"bi bi-eye\" aria-hidden=\"true\"></span></button>");
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
    }
}