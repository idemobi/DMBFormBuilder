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
    ///     Builds a Bootstrap select field with label presentation, validation metadata, options, and optional quick-select
    ///     action.
    /// </summary>
    public sealed class SelectFieldBuilder :
        HtmlBuilderBase<SelectFieldBuilder>,
        ICanUseCustomClasses
    {
        #region Static methods

        private static bool IsReservedInputAttribute(string key)
        {
            return string.Equals(key, "class", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "id", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "name", StringComparison.OrdinalIgnoreCase);
        }

        private static void WriteAttribute(TextWriter writer, HtmlEncoder encoder, string name, string? value, bool writeEmptyValue = false)
        {
            if (value == null || writeEmptyValue == false && string.IsNullOrWhiteSpace(value))
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
        private string _groupIconCssClass = "bi bi-menu-button-wide";
        private readonly Dictionary<string, string> _inputAttributes = new(StringComparer.OrdinalIgnoreCase);

        private string _inputId = "SelectField";
        private string _inputName = "SelectField";
        private string _label = "Select field";
        private IconStruct _labelIcon = IconStruct.Empty;

        private readonly List<SelectOption> _options = new();
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private string _quickSelectIconCssClass = "bi bi-geo-alt";
        private string _quickSelectTitle = string.Empty;
        private string? _quickSelectValue;
        private VariantStyle _quickSelectVariant = VariantStyle.Primary;
        private ButtonVariantMode _quickSelectVariantMode = ButtonVariantMode.Outline;
        private bool _required;
        private string _requiredMessage = string.Empty;
        private string? _value;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectFieldBuilder" /> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public SelectFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClasses("dmb-form-field", "dmb-select-field", "mb-3");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds a selectable option to the control.
        /// </summary>
        public SelectFieldBuilder AddOption(string value, string text)
        {
            _options.Add(new SelectOption(value, text));
            return this;
        }

        /// <summary>
        ///     Adds a disabled placeholder option with an empty value.
        /// </summary>
        public SelectFieldBuilder AddPlaceholderOption(string text, bool hidden = false)
        {
            _options.Add(new SelectOption(string.Empty, text, Disabled: true, Hidden: hidden));
            return this;
        }

        /// <inheritdoc />
        protected override SelectFieldBuilder CreateInstance()
        {
            return new SelectFieldBuilder(_textWriter, _htmlHelper);
        }

        private bool HasQuickSelectAction()
        {
            return !string.IsNullOrWhiteSpace(_quickSelectValue);
        }

        /// <inheritdoc />
        protected override void InternalClone(SelectFieldBuilder source)
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
            _groupIconCssClass = source._groupIconCssClass;
            _quickSelectValue = source._quickSelectValue;
            _quickSelectIconCssClass = source._quickSelectIconCssClass;
            _quickSelectTitle = source._quickSelectTitle;
            _quickSelectVariant = source._quickSelectVariant;
            _quickSelectVariantMode = source._quickSelectVariantMode;
            _options.Clear();
            _options.AddRange(source._options);
            _inputAttributes.Clear();
            foreach (KeyValuePair<string, string> attribute in source._inputAttributes)
            {
                _inputAttributes[attribute.Key] = attribute.Value;
            }
        }

        /// <summary>
        ///     Sets the optional description rendered as an information popover next to the field label.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="SelectFieldBuilder" /> instance for fluent chaining.</returns>
        public SelectFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Enables or disables the rendered select control.
        /// </summary>
        public new SelectFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        ///     Sets the CSS icon class rendered in group presentation.
        /// </summary>
        public SelectFieldBuilder SetGroupIcon(string? iconCssClass)
        {
            if (!string.IsNullOrWhiteSpace(iconCssClass))
            {
                _groupIconCssClass = iconCssClass;
                if (_labelIcon.IsEmpty)
                {
                    _labelIcon = IconStruct.Parse(iconCssClass);
                }
            }

            return this;
        }

        /// <summary>
        ///     Sets the select identifier and model binding name.
        /// </summary>
        public SelectFieldBuilder SetInput(string inputId, string inputName)
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
        ///     Sets or replaces an attribute on the rendered select control.
        /// </summary>
        public SelectFieldBuilder SetInputAttribute(string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                _inputAttributes[name] = value;
            }

            return this;
        }

        /// <summary>
        ///     Sets the select label when a non-empty value is provided.
        /// </summary>
        public SelectFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        ///     Sets the icon rendered with the select label.
        /// </summary>
        public SelectFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        ///     Sets how the select label is positioned or hidden.
        /// </summary>
        public SelectFieldBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        ///     Configures an optional action button that assigns a predefined select value.
        /// </summary>
        public SelectFieldBuilder SetQuickSelectAction(
            string? value,
            string iconCssClass = "bi bi-geo-alt",
            string? title = null,
            VariantStyle variant = VariantStyle.Primary,
            ButtonVariantMode variantMode = ButtonVariantMode.Outline
        )
        {
            _quickSelectValue = value;
            if (!string.IsNullOrWhiteSpace(iconCssClass))
            {
                _quickSelectIconCssClass = iconCssClass;
            }

            _quickSelectTitle = title ?? string.Empty;
            _quickSelectVariant = variant;
            _quickSelectVariantMode = variantMode;
            return this;
        }

        /// <summary>
        ///     Sets the Bootstrap variant used by the quick-select action button.
        /// </summary>
        public SelectFieldBuilder SetQuickSelectVariant(VariantStyle variant, ButtonVariantMode variantMode = ButtonVariantMode.Outline)
        {
            _quickSelectVariant = variant;
            _quickSelectVariantMode = variantMode;
            return this;
        }

        /// <summary>
        ///     Adds required validation metadata to the select control.
        /// </summary>
        public SelectFieldBuilder SetRequired(bool required = true, string? message = null)
        {
            _required = required;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _requiredMessage = message;
            }

            return this;
        }

        /// <summary>
        ///     Sets the currently selected option value.
        /// </summary>
        public SelectFieldBuilder SetValue(string? value)
        {
            _value = value;
            return this;
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder)
        {
            if (HasQuickSelectAction())
            {
                writer.Write("<div class=\"input-group has-validation dmb-select-quick-group\">");
                writer.Write("<div class=\"form-floating\">");
                WriteSelect(writer, encoder);
                WriteLabel(writer, encoder, string.Empty);
                writer.Write("</div>");
                WriteQuickSelectAction(writer, encoder);
                WriteValidation(writer, encoder);
                writer.Write("</div>");
            }
            else
            {
                writer.Write("<div class=\"form-floating\">");
                WriteSelect(writer, encoder);
                WriteLabel(writer, encoder, string.Empty);
                writer.Write("</div>");
                WriteValidation(writer, encoder);
            }
        }

        private void WriteGroup(TextWriter writer, HtmlEncoder encoder)
        {
            WriteSelectInputGroup(writer, encoder, includeLeadingIcon: true, includeValidation: true);
        }

        private void WriteHiddenLabel(TextWriter writer, HtmlEncoder encoder)
        {
            WriteLabel(writer, encoder, "visually-hidden");
            if (HasQuickSelectAction())
            {
                WriteSelectInputGroup(writer, encoder, includeLeadingIcon: false, includeValidation: true);
            }
            else
            {
                WriteSelect(writer, encoder);
                WriteValidation(writer, encoder);
            }
        }

        private void WriteInline(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"row g-2 align-items-start\">");
            writer.Write("<div class=\"col-sm-4\">");
            WriteLabel(writer, encoder, "col-form-label");
            writer.Write("</div>");
            writer.Write("<div class=\"col-sm-8\">");
            if (HasQuickSelectAction())
            {
                WriteSelectInputGroup(writer, encoder, includeLeadingIcon: false, includeValidation: false);
            }
            else
            {
                WriteSelect(writer, encoder);
            }

            writer.Write("</div>");
            writer.Write("<div class=\"offset-sm-4 col-sm-8\">");
            WriteValidation(writer, encoder);
            writer.Write("</div>");
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
            if (HasQuickSelectAction())
            {
                WriteSelectInputGroup(writer, encoder, includeLeadingIcon: false, includeValidation: true);
            }
            else
            {
                WriteSelect(writer, encoder);
                WriteValidation(writer, encoder);
            }
        }

        private void WriteQuickSelectAction(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<button");
            WriteAttribute(writer, encoder, "class", $"input-group-text btn {_quickSelectVariant.GetButtonVariantCss(_quickSelectVariantMode)}");
            writer.Write(" type=\"button\"");
            WriteAttribute(writer, encoder, "data-dmb-select-value-for", _inputId);
            WriteAttribute(writer, encoder, "data-dmb-select-value", _quickSelectValue);
            if (!string.IsNullOrWhiteSpace(_quickSelectTitle))
            {
                WriteAttribute(writer, encoder, "title", _quickSelectTitle);
                WriteAttribute(writer, encoder, "aria-label", _quickSelectTitle);
            }

            if (_disabled)
            {
                writer.Write(" disabled");
            }

            writer.Write("><span");
            WriteAttribute(writer, encoder, "class", _quickSelectIconCssClass);
            writer.Write(" aria-hidden=\"true\"></span></button>");
        }

        private void WriteSelect(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<select class=\"form-select\"");
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "data-val", "true");

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
                writer.Write(" disabled");
            }

            writer.Write(">");
            foreach (SelectOption option in _options)
            {
                writer.Write("<option");
                WriteAttribute(writer, encoder, "value", option.Value, writeEmptyValue: true);
                if (option.Disabled)
                {
                    writer.Write(" disabled");
                }

                if (option.Hidden)
                {
                    writer.Write(" hidden");
                }

                if (string.Equals(option.Value, _value, StringComparison.Ordinal))
                {
                    writer.Write(" selected");
                }

                writer.Write(">");
                encoder.Encode(writer, option.Text);
                writer.Write("</option>");
            }

            writer.Write("</select>");
        }

        private void WriteSelectInputGroup(TextWriter writer, HtmlEncoder encoder, bool includeLeadingIcon, bool includeValidation)
        {
            writer.Write("<div class=\"input-group has-validation");
            if (HasQuickSelectAction())
            {
                writer.Write(" dmb-select-quick-group");
            }

            writer.Write("\">");
            if (includeLeadingIcon)
            {
                WriteLabel(writer, encoder, "input-group-text");
            }

            WriteSelect(writer, encoder);
            if (HasQuickSelectAction())
            {
                WriteQuickSelectAction(writer, encoder);
            }

            if (includeValidation)
            {
                WriteValidation(writer, encoder);
            }

            writer.Write("</div>");
        }

        /// <summary>
        ///     Writes the complete Bootstrap select field markup, including options and validation feedback.
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

        #region Nested type: SelectOption

        /// <summary>
        ///     Represents an option rendered by <see cref="SelectFieldBuilder" />.
        /// </summary>
        /// <param name="Value">The option value submitted by the select control.</param>
        /// <param name="Text">The option text rendered to the user.</param>
        /// <param name="Disabled">Whether the option is disabled.</param>
        /// <param name="Hidden">Whether the option is hidden.</param>
        public sealed record SelectOption(string Value, string Text, bool Disabled = false, bool Hidden = false);

        #endregion
    }
}