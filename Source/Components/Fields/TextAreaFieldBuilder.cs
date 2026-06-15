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
    ///     Builds a Bootstrap textarea field with label presentation, validation metadata, and optional constraint badges.
    /// </summary>
    public sealed class TextAreaFieldBuilder :
        HtmlBuilderBase<TextAreaFieldBuilder>,
        ICanUseCustomClasses
    {
        #region Static methods

        private static bool IsReservedInputAttribute(string key)
        {
            return string.Equals(key, "class", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "id", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "name", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "placeholder", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "rows", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "value", StringComparison.OrdinalIgnoreCase);
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

        private readonly List<FieldBadge> _badges = new();
        private string _description = string.Empty;
        private bool _disabled;

        private readonly Dictionary<string, string> _inputAttributes = new(StringComparer.OrdinalIgnoreCase);

        private string _inputId = "TextAreaField";
        private string _inputName = "TextAreaField";
        private string _label = "Text area";
        private IconStruct _labelIcon = IconStruct.Empty;
        private string _placeholder = string.Empty;
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private bool _required;
        private string _requiredMessage = string.Empty;
        private int _rows = 4;
        private bool _showConstraintBadges = true;
        private string? _value;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextAreaFieldBuilder" /> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public TextAreaFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClasses("dmb-form-field", "dmb-textarea-field", "mb-3");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds a visual badge describing an active textarea constraint or hint.
        /// </summary>
        public TextAreaFieldBuilder AddBadge(string text, string variant = "primary")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _badges.Add(new FieldBadge(text, string.IsNullOrWhiteSpace(variant) ? "primary" : variant));
            }

            return this;
        }

        /// <inheritdoc />
        protected override TextAreaFieldBuilder CreateInstance()
        {
            return new TextAreaFieldBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc />
        protected override void InternalClone(TextAreaFieldBuilder source)
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
            _showConstraintBadges = source._showConstraintBadges;
            _labelIcon = source._labelIcon;
            _rows = source._rows;
            _inputAttributes.Clear();
            foreach (KeyValuePair<string, string> attribute in source._inputAttributes)
            {
                _inputAttributes[attribute.Key] = attribute.Value;
            }

            _badges.Clear();
            _badges.AddRange(source._badges);
        }

        /// <summary>
        ///     Sets the optional description rendered as an information popover next to the field label.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="TextAreaFieldBuilder" /> instance for fluent chaining.</returns>
        public TextAreaFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Enables or disables the rendered textarea.
        /// </summary>
        public new TextAreaFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        ///     Sets the textarea identifier and model binding name.
        /// </summary>
        public TextAreaFieldBuilder SetInput(string inputId, string inputName)
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
        ///     Sets or replaces an attribute on the rendered textarea.
        /// </summary>
        public TextAreaFieldBuilder SetInputAttribute(string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                _inputAttributes[name] = value;
            }

            return this;
        }

        /// <summary>
        ///     Sets the textarea label when a non-empty value is provided.
        /// </summary>
        public TextAreaFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        ///     Sets the icon rendered with the textarea label.
        /// </summary>
        public TextAreaFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        ///     Sets how the textarea label is positioned or hidden.
        /// </summary>
        public TextAreaFieldBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        ///     Adds maximum length validation metadata and a counter badge.
        /// </summary>
        public TextAreaFieldBuilder SetMaxLength(int maxLength, string? message = null)
        {
            if (maxLength > 0)
            {
                SetInputAttribute("maxlength", maxLength.ToString());
                SetInputAttribute("data-val-maxlength-max", maxLength.ToString());
                SetInputAttribute("data-val-maxlength", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength)));
                _badges.Add(new FieldBadge($"0 / {maxLength}", "primary", _inputId, maxLength));
            }

            return this;
        }

        /// <summary>
        ///     Adds minimum length validation metadata.
        /// </summary>
        public TextAreaFieldBuilder SetMinLength(int minLength, string? message = null)
        {
            if (minLength > 0)
            {
                SetInputAttribute("minlength", minLength.ToString());
                SetInputAttribute("data-val-minlength-min", minLength.ToString());
                SetInputAttribute("data-val-minlength", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength)));
                AddBadge($"≥ {minLength}", "primary");
            }

            return this;
        }

        /// <summary>
        ///     Adds FormBuilder no-HTML validation metadata and a matching constraint badge.
        /// </summary>
        public TextAreaFieldBuilder SetNoHtml(string? message = null)
        {
            SetInputAttribute("data-val-nohtml", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_NoHtmlInvalid)));
            AddBadge(WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Badge_NoHtml)), "secondary");
            return this;
        }

        /// <summary>
        ///     Sets the textarea placeholder text.
        /// </summary>
        public TextAreaFieldBuilder SetPlaceholder(string? placeholder)
        {
            _placeholder = placeholder ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Adds required validation metadata to the textarea.
        /// </summary>
        public TextAreaFieldBuilder SetRequired(bool required = true, string? message = null)
        {
            _required = required;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _requiredMessage = message;
            }

            return this;
        }

        /// <summary>
        ///     Sets the number of textarea rows when the value is greater than zero.
        /// </summary>
        public TextAreaFieldBuilder SetRows(int rows)
        {
            if (rows > 0)
            {
                _rows = rows;
            }

            return this;
        }

        /// <summary>
        ///     Sets the textarea content value.
        /// </summary>
        public TextAreaFieldBuilder SetValue(string? value)
        {
            _value = value;
            return this;
        }

        /// <summary>
        ///     Shows or hides constraint badges generated by this builder.
        /// </summary>
        public TextAreaFieldBuilder ShowConstraintBadges(bool show = true)
        {
            _showConstraintBadges = show;
            return this;
        }

        private void WriteBadges(TextWriter writer, HtmlEncoder encoder)
        {
            if (!_showConstraintBadges || _badges.Count == 0)
            {
                return;
            }

            writer.Write("<div class=\"form-badge dmb-form-field-badges\">");
            foreach (FieldBadge badge in _badges)
            {
                writer.Write("<span class=\"badge rounded-pill shadow-sm bg-");
                encoder.Encode(writer, badge.Variant);
                writer.Write(" text-bg-");
                encoder.Encode(writer, badge.Variant);
                writer.Write("\"");
                if (!string.IsNullOrWhiteSpace(badge.CounterFor))
                {
                    WriteAttribute(writer, encoder, "data-dmb-counter-for", badge.CounterFor);
                }

                if (badge.CounterMax.HasValue)
                {
                    WriteAttribute(writer, encoder, "data-dmb-counter-max", badge.CounterMax.Value.ToString());
                }

                writer.Write(">");
                encoder.Encode(writer, badge.Text);
                writer.Write("</span>");
            }

            writer.Write("</div>");
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"dmb-form-field-control\">");
            writer.Write("<div class=\"form-floating\">");
            WriteInput(writer, encoder);
            WriteLabel(writer, encoder, string.Empty);
            writer.Write("</div>");
            WriteBadges(writer, encoder);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        private void WriteGroup(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"dmb-form-field-control\">");
            writer.Write("<div class=\"input-group has-validation\">");
            WriteLabel(writer, encoder, "input-group-text");
            WriteInput(writer, encoder);
            WriteValidation(writer, encoder);
            writer.Write("</div>");
            WriteBadges(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteHiddenLabel(TextWriter writer, HtmlEncoder encoder)
        {
            WriteLabel(writer, encoder, "visually-hidden");
            writer.Write("<div class=\"dmb-form-field-control\">");
            WriteInput(writer, encoder);
            WriteBadges(writer, encoder);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        private void WriteInline(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"row g-2 align-items-start\">");
            writer.Write("<div class=\"col-sm-4\">");
            WriteLabel(writer, encoder, "col-form-label");
            writer.Write("</div>");
            writer.Write("<div class=\"col-sm-8\">");
            writer.Write("<div class=\"dmb-form-field-control\">");
            WriteInput(writer, encoder);
            WriteBadges(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("<div class=\"offset-sm-4 col-sm-8\">");
            WriteValidation(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WriteInput(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<textarea class=\"form-control\"");
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "placeholder", string.IsNullOrWhiteSpace(_placeholder) ? _label : _placeholder);
            WriteAttribute(writer, encoder, "rows", _rows.ToString());
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
            encoder.Encode(writer, _value ?? string.Empty);
            writer.Write("</textarea>");
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
            writer.Write("<div class=\"dmb-form-field-control\">");
            WriteInput(writer, encoder);
            WriteBadges(writer, encoder);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        /// <summary>
        ///     Writes the complete Bootstrap textarea markup, including label, validation message, and badges.
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

        #region Nested type: FieldBadge

        private sealed record FieldBadge(string Text, string Variant, string? CounterFor = null, int? CounterMax = null);

        #endregion
    }
}