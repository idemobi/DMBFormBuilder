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
    ///     Builds a token input field with password-style masking, optional visibility toggle, and optional copy action.
    /// </summary>
    public sealed class TokenFieldBuilder :
        HtmlBuilderBase<TokenFieldBuilder>,
        ICanUseCustomClasses
    {
        #region Constants

        private const string ScriptPath = "/js/formbuilder/FormBuilder.js";
        private const string StylesheetPath = "/css/formbuilder/FormBuilder.css";

        #endregion

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

        private bool _copyEnabled = true;
        private string _description = string.Empty;
        private bool _disabled;
        private readonly Dictionary<string, string> _inputAttributes = new(StringComparer.OrdinalIgnoreCase);

        private string _inputId = "TokenField";
        private string _inputName = "TokenField";
        private string _label = "Token";
        private IconStruct _labelIcon = IconStruct.Empty;
        private string _placeholder = string.Empty;
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private bool _required;
        private string _requiredMessage = string.Empty;
        private bool _toggleEnabled = true;
        private string? _value;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TokenFieldBuilder" /> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public TokenFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClasses("dmb-form-field", "dmb-token-field", "mb-3");
        }

        #endregion

        #region Instance methods

        /// <inheritdoc />
        protected override TokenFieldBuilder CreateInstance()
        {
            return new TokenFieldBuilder(_textWriter, _htmlHelper);
        }

        /// <summary>
        ///     Registers FormBuilder CSS and JavaScript assets required by token actions.
        /// </summary>
        private void EnsureValidationAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(StylesheetPath);
            page.SetScriptFile(ScriptPath);
        }

        /// <summary>
        ///     Enables or disables token copy behavior.
        /// </summary>
        public TokenFieldBuilder EnableCopy(bool enabled = true)
        {
            return SetCopy(enabled);
        }

        /// <inheritdoc />
        protected override void InternalClone(TokenFieldBuilder source)
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
            _toggleEnabled = source._toggleEnabled;
            _copyEnabled = source._copyEnabled;

            _inputAttributes.Clear();
            foreach (KeyValuePair<string, string> attribute in source._inputAttributes)
            {
                _inputAttributes[attribute.Key] = attribute.Value;
            }
        }

        /// <summary>
        ///     Enables or disables the copy button for the token input.
        /// </summary>
        public TokenFieldBuilder SetCopy(bool enabled = true)
        {
            _copyEnabled = enabled;
            return this;
        }

        /// <summary>
        ///     Sets the optional description rendered as an information popover next to the field label.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="TokenFieldBuilder" /> instance for fluent chaining.</returns>
        public TokenFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Enables or disables the rendered token input.
        /// </summary>
        public new TokenFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        ///     Sets the token input identifier and model binding name.
        /// </summary>
        public TokenFieldBuilder SetInput(string inputId, string inputName)
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
        ///     Sets or replaces an attribute on the rendered token input.
        /// </summary>
        public TokenFieldBuilder SetInputAttribute(string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                _inputAttributes[name] = value;
            }

            return this;
        }

        /// <summary>
        ///     Sets the token label when a non-empty value is provided.
        /// </summary>
        public TokenFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        ///     Sets the icon rendered with the token label.
        /// </summary>
        public TokenFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        ///     Sets how the token label is positioned or hidden.
        /// </summary>
        public TokenFieldBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        ///     Sets the token input placeholder text.
        /// </summary>
        public TokenFieldBuilder SetPlaceholder(string? placeholder)
        {
            _placeholder = placeholder ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Adds required validation metadata to the token input.
        /// </summary>
        public TokenFieldBuilder SetRequired(bool required = true, string? message = null)
        {
            _required = required;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _requiredMessage = message;
            }

            return this;
        }

        /// <summary>
        ///     Enables or disables the visibility toggle button for the token input.
        /// </summary>
        public TokenFieldBuilder SetToggle(bool enabled = true)
        {
            _toggleEnabled = enabled;
            return this;
        }

        /// <summary>
        ///     Sets the sensitive token value rendered in the input.
        /// </summary>
        public TokenFieldBuilder SetValue(string? value)
        {
            _value = value;
            return this;
        }

        private void WriteCopyButton(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<button class=\"btn btn-outline-primary\" type=\"button\"");
            WriteAttribute(writer, encoder, "data-dmb-token-copy", _inputId);
            WriteAttribute(writer, encoder, "aria-label", WebLocalizer.GetDataAnnotation("FormBuilder_Field_TokenCopy"));
            writer.Write("><span class=\"bi bi-clipboard\" aria-hidden=\"true\"></span></button>");
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder)
        {
            WriteInputGroup(writer, encoder, floating: true);
        }

        private void WriteGroup(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            WriteLabel(writer, encoder, "input-group-text");
            WriteInput(writer, encoder);
            if (_toggleEnabled)
            {
                WriteToggleButton(writer, encoder);
            }

            if (_copyEnabled)
            {
                WriteCopyButton(writer, encoder);
            }

            WriteValidation(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteHiddenLabel(TextWriter writer, HtmlEncoder encoder)
        {
            WriteLabel(writer, encoder, "visually-hidden");
            WriteInputGroup(writer, encoder, floating: false);
        }

        private void WriteInline(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"row g-2 align-items-start\"><div class=\"col-sm-4\">");
            WriteLabel(writer, encoder, "col-form-label");
            writer.Write("</div><div class=\"col-sm-8\">");
            WriteInputGroup(writer, encoder, floating: false);
            writer.Write("</div></div>");
        }

        private void WriteInput(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<input class=\"form-control\"");
            WriteAttribute(writer, encoder, "type", "password");
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "placeholder", string.IsNullOrWhiteSpace(_placeholder) ? _label : _placeholder);
            WriteAttribute(writer, encoder, "value", _value);
            WriteAttribute(writer, encoder, "autocomplete", "off");
            WriteAttribute(writer, encoder, "data-val", "true");
            WriteAttribute(writer, encoder, "data-dmb-token-input", "true");

            if (_required)
            {
                WriteAttribute(writer, encoder, "required", "required");
                WriteAttribute(writer, encoder, "data-val-required", _requiredMessage);
            }

            foreach (KeyValuePair<string, string> attribute in _inputAttributes)
            {
                if (!IsReservedInputAttribute(attribute.Key))
                {
                    WriteAttribute(writer, encoder, attribute.Key, attribute.Value);
                }
            }

            if (_disabled)
            {
                writer.Write(" readonly");
                WriteAttribute(writer, encoder, "aria-disabled", "true");
            }

            writer.Write(">");
        }

        private void WriteInputGroup(TextWriter writer, HtmlEncoder encoder, bool floating)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            if (floating)
            {
                writer.Write("<div class=\"form-floating\">");
                WriteInput(writer, encoder);
                WriteLabel(writer, encoder, string.Empty);
                writer.Write("</div>");
            }
            else
            {
                WriteInput(writer, encoder);
            }

            if (_toggleEnabled)
            {
                WriteToggleButton(writer, encoder);
            }

            if (_copyEnabled)
            {
                WriteCopyButton(writer, encoder);
            }

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
            WriteInputGroup(writer, encoder, floating: false);
        }

        /// <summary>
        ///     Writes the complete Bootstrap token field markup, including toggle and copy buttons when enabled.
        /// </summary>
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            
            EnsureValidationAssets();
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

            writer.Write($"</{_tag}>");
        }

        private void WriteToggleButton(TextWriter writer, HtmlEncoder encoder)
        {
            string showLabel = WebLocalizer.GetDataAnnotation("FormBuilder_Field_TokenShow");
            string hideLabel = WebLocalizer.GetDataAnnotation("FormBuilder_Field_TokenHide");

            writer.Write("<button class=\"btn btn-outline-secondary\" type=\"button\"");
            WriteAttribute(writer, encoder, "data-dmb-token-toggle", _inputId);
            WriteAttribute(writer, encoder, "data-dmb-token-show-label", showLabel);
            WriteAttribute(writer, encoder, "data-dmb-token-hide-label", hideLabel);
            WriteAttribute(writer, encoder, "aria-label", showLabel);
            writer.Write("><span class=\"bi bi-eye\" aria-hidden=\"true\"></span></button>");
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
