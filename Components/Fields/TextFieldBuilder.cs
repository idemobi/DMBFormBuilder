#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj TextFieldBuilder.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using DMBFormBuilder.Resources;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Builds a Bootstrap text input field with label, validation attributes, optional input group icon, and constraint badges.
    /// </summary>
    /// <remarks>
    /// Use this builder directly or through <see cref="TextFieldBuilderExtensions"/> and related specialized helpers
    /// to render text, number, decimal, ASCII, Unix-friendly, date-like, color, and range inputs.
    /// </remarks>
    public sealed class TextFieldBuilder :
        HtmlBuilderBase<TextFieldBuilder>,
        ICanUseCustomClasses
    {
        private sealed record FieldBadge(string Text, string Variant, string? CounterFor = null, int? CounterMax = null);

        private readonly Dictionary<string, string> _inputAttributes = new(StringComparer.OrdinalIgnoreCase);
        private readonly List<FieldBadge> _badges = new();

        private string _inputId = "TextField";
        private string _inputName = "TextField";
        private string _label = "Text field";
        private string _description = string.Empty;
        private string _placeholder = string.Empty;
        private string? _value;
        private string _inputType = "text";
        private string _requiredMessage = string.Empty;
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private bool _disabled;
        private bool _required;
        private bool _showConstraintBadges = true;
        private IconStruct _labelIcon = IconStruct.Empty;
        private string _groupIconCssClass = "bi bi-input-cursor-text";
        private VariantStyle _variant = VariantStyle.Primary;
        private bool _withTextField;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFieldBuilder"/> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public TextFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClasses("dmb-form-field", "dmb-text-field", "mb-3");
        }

        /// <summary>
        /// Sets the input identifier and model binding name used by the rendered <c>input</c>.
        /// </summary>
        /// <param name="inputId">The value rendered in the <c>id</c> attribute when not empty.</param>
        /// <param name="inputName">The value rendered in the <c>name</c> attribute when not empty.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetInput(string inputId, string inputName)
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
        /// Sets the field label text when a non-empty value is provided.
        /// </summary>
        /// <param name="label">The label text rendered for the field.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        /// Sets the optional description rendered as an information popover next to the field label.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Sets the placeholder text rendered on the input.
        /// </summary>
        /// <param name="placeholder">The placeholder text, or an empty string when <see langword="null"/>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetPlaceholder(string? placeholder)
        {
            _placeholder = placeholder ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Sets the raw value rendered in the input <c>value</c> attribute.
        /// </summary>
        /// <param name="value">The value to render; <see langword="null"/> omits the value.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetValue(string? value)
        {
            _value = value;
            return this;
        }

        /// <summary>
        /// Sets the HTML input type when a non-empty value is provided.
        /// </summary>
        /// <param name="inputType">The value rendered in the <c>type</c> attribute, such as <c>text</c>, <c>email</c>, or <c>number</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetInputType(string inputType)
        {
            if (!string.IsNullOrWhiteSpace(inputType))
            {
                _inputType = inputType;
            }

            return this;
        }

        /// <summary>
        /// Applies a semantic text-field profile by setting the input type and related validation attributes.
        /// </summary>
        /// <param name="kind">The text-field profile to apply.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetKind(TextFieldInputKind kind)
        {
            switch (kind)
            {
                case TextFieldInputKind.Numeric:
                    SetNumeric();
                    break;
                case TextFieldInputKind.Decimal:
                    SetFloat();
                    break;
                case TextFieldInputKind.Ascii:
                    SetAsciiOnly();
                    break;
                case TextFieldInputKind.Unix:
                    SetUnixText();
                    break;
                case TextFieldInputKind.Text:
                default:
                    SetInputType("text");
                    break;
            }

            return this;
        }

        /// <summary>
        /// Sets how the label is positioned or hidden around the input.
        /// </summary>
        /// <param name="presentation">The label presentation strategy.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        /// Sets the icon rendered with the label when the selected presentation supports it.
        /// </summary>
        /// <param name="icon">The icon descriptor to render.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        /// Enables or disables the rendered input.
        /// </summary>
        /// <param name="disabled"><see langword="true"/> to render the <c>disabled</c> attribute.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        /// Sets the CSS class used for the input group icon and mirrors it as the label icon when none is set.
        /// </summary>
        /// <param name="iconCssClass">The icon CSS class rendered inside the input group.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetGroupIcon(string? iconCssClass)
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
        /// Sets the Bootstrap variant used for group styling and badges.
        /// </summary>
        /// <param name="variant">The visual variant to apply.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }

        /// <summary>
        /// Toggles the extra text-field CSS marker used by FormBuilder styles.
        /// </summary>
        /// <param name="enabled"><see langword="true"/> to add the marker behavior.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetWithTextField(bool enabled = true)
        {
            _withTextField = enabled;
            return this;
        }

        /// <summary>
        /// Sets required validation on the input and optionally overrides the localized validation message.
        /// </summary>
        /// <param name="required"><see langword="true"/> to render required validation attributes.</param>
        /// <param name="message">An optional validation message rendered in <c>data-val-required</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetRequired(bool required = true, string? message = null)
        {
            _required = required;
            if (!string.IsNullOrWhiteSpace(message))
            {
                _requiredMessage = message;
            }

            return this;
        }

        /// <summary>
        /// Configures the field as an integer numeric input with numeric input mode and a step of <c>1</c>.
        /// </summary>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetNumeric()
        {
            return SetInputType("number")
                .SetInputAttribute("inputmode", "numeric")
                .SetInputAttribute("step", "1")
                .AddBadge(WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Badge_Numeric)), "secondary");
        }

        /// <summary>
        /// Configures the field as a decimal numeric input.
        /// </summary>
        /// <param name="step">The value rendered in the <c>step</c> attribute; defaults to <c>any</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetFloat(string step = "any")
        {
            return SetInputType("number")
                .SetInputAttribute("inputmode", "decimal")
                .SetInputAttribute("step", step)
                .AddBadge(WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Badge_Float)), "secondary");
        }

        /// <summary>
        /// Adds an ASCII-only regex constraint and cleaner marker.
        /// </summary>
        /// <param name="message">An optional regex validation message.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetAsciiOnly(string? message = null)
        {
            return SetPattern(@"[\x20-\x7E]*", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_AsciiInvalid)))
                .SetInputAttribute("data-formbuilder-cleaner", "ascii")
                .AddBadge(WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Badge_Ascii)), "secondary");
        }

        /// <summary>
        /// Adds a Unix-friendly regex constraint and cleaner marker for alphanumeric and underscore values.
        /// </summary>
        /// <param name="message">An optional regex validation message.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetUnixText(string? message = null)
        {
            return SetPattern(@"[a-zA-Z0-9_]*", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_UnixInvalid)))
                .SetInputAttribute("data-formbuilder-cleaner", "unix")
                .AddBadge(WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Badge_Unix)), "secondary");
        }

        /// <summary>
        /// Adds maximum length attributes, unobtrusive validation metadata, and a counter badge.
        /// </summary>
        /// <param name="maxLength">The maximum accepted character count; ignored when less than or equal to zero.</param>
        /// <param name="message">An optional validation message rendered in <c>data-val-maxlength</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetMaxLength(int maxLength, string? message = null)
        {
            if (maxLength > 0)
            {
                SetInputAttribute("maxlength", maxLength.ToString());
                SetInputAttribute("data-val-maxlength-max", maxLength.ToString());
                SetInputAttribute("data-val-maxlength", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength)));
                AddCounterBadge(maxLength);
            }

            return this;
        }

        /// <summary>
        /// Adds minimum length attributes and unobtrusive validation metadata.
        /// </summary>
        /// <param name="minLength">The minimum accepted character count; ignored when less than or equal to zero.</param>
        /// <param name="message">An optional validation message rendered in <c>data-val-minlength</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetMinLength(int minLength, string? message = null)
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
        /// Adds the minimum value constraint and range validation metadata.
        /// </summary>
        /// <param name="min">The minimum value rendered in the <c>min</c> attribute.</param>
        /// <param name="message">An optional validation message rendered in <c>data-val-range</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetMin(string min, string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(min))
            {
                SetInputAttribute("min", min);
                SetInputAttribute("data-val-range-min", min);
                SetInputAttribute("data-val-range", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range)));
                AddBadge($"≥ {min}", "primary");
            }

            return this;
        }

        /// <summary>
        /// Adds the maximum value constraint and range validation metadata.
        /// </summary>
        /// <param name="max">The maximum value rendered in the <c>max</c> attribute.</param>
        /// <param name="message">An optional validation message rendered in <c>data-val-range</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetMax(string max, string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(max))
            {
                SetInputAttribute("max", max);
                SetInputAttribute("data-val-range-max", max);
                SetInputAttribute("data-val-range", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range)));
                AddBadge($"≤ {max}", "primary");
            }

            return this;
        }

        /// <summary>
        /// Adds an HTML pattern constraint and unobtrusive regex validation metadata.
        /// </summary>
        /// <param name="pattern">The regular expression pattern rendered in the <c>pattern</c> attribute.</param>
        /// <param name="message">An optional validation message rendered in <c>data-val-regex</c>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetPattern(string pattern, string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(pattern))
            {
                SetInputAttribute("pattern", pattern);
                SetInputAttribute("data-val-regex-pattern", pattern);
                SetInputAttribute("data-val-regex", message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FormatInvalid)));
            }

            return this;
        }

        /// <summary>
        /// Adds a visual badge describing an active constraint or field hint.
        /// </summary>
        /// <param name="text">The badge text; empty values are ignored.</param>
        /// <param name="variant">The Bootstrap variant name used by the badge.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder AddBadge(string text, string variant = "primary")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _badges.Add(new FieldBadge(text, string.IsNullOrWhiteSpace(variant) ? "primary" : variant));
            }

            return this;
        }

        /// <summary>
        /// Shows or hides constraint badges generated by this builder.
        /// </summary>
        /// <param name="show"><see langword="true"/> to render badges; otherwise <see langword="false"/>.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder ShowConstraintBadges(bool show = true)
        {
            _showConstraintBadges = show;
            return this;
        }

        private TextFieldBuilder AddCounterBadge(int maxLength)
        {
            _badges.Add(new FieldBadge($"0 / {maxLength}", "primary", _inputId, maxLength));
            return this;
        }

        /// <summary>
        /// Sets or replaces an attribute on the rendered input.
        /// </summary>
        /// <param name="name">The attribute name; empty names are ignored.</param>
        /// <param name="value">The attribute value; <see langword="null"/> values are ignored.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetInputAttribute(string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                _inputAttributes[name] = value;
            }

            return this;
        }

        /// <summary>
        /// Sets or replaces multiple attributes on the rendered input.
        /// </summary>
        /// <param name="attributes">The attribute collection to copy into the input attribute bag.</param>
        /// <returns>The current <see cref="TextFieldBuilder"/> instance for fluent chaining.</returns>
        public TextFieldBuilder SetInputAttributes(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                SetInputAttribute(attribute.Key, attribute.Value);
            }

            return this;
        }

        /// <inheritdoc/>
        protected override TextFieldBuilder CreateInstance()
        {
            return new TextFieldBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc/>
        protected override void InternalClone(TextFieldBuilder source)
        {
            base.InternalClone(source);
            _inputId = source._inputId;
            _inputName = source._inputName;
            _label = source._label;
            _description = source._description;
            _placeholder = source._placeholder;
            _value = source._value;
            _inputType = source._inputType;
            _requiredMessage = source._requiredMessage;
            _presentation = source._presentation;
            _disabled = source._disabled;
            _required = source._required;
            _showConstraintBadges = source._showConstraintBadges;
            _labelIcon = source._labelIcon;
            _groupIconCssClass = source._groupIconCssClass;
            _variant = source._variant;
            _withTextField = source._withTextField;

            _inputAttributes.Clear();
            foreach (KeyValuePair<string, string> attribute in source._inputAttributes)
            {
                _inputAttributes[attribute.Key] = attribute.Value;
            }

            _badges.Clear();
            _badges.AddRange(source._badges);
        }

        /// <summary>
        /// Writes the complete Bootstrap text field markup, including label, input, validation message, and badges.
        /// </summary>
        /// <param name="writer">The writer receiving the generated markup.</param>
        /// <param name="encoder">The HTML encoder used by the rendering pipeline.</param>
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

        private void WriteNormal(TextWriter writer, HtmlEncoder encoder)
        {
            WriteLabel(writer, encoder, "form-label");
            writer.Write("<div class=\"dmb-form-field-control\">");
            WriteInputWithOptionalTextField(writer, encoder);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder)
        {
            if (IsRangeInput())
            {
                WriteRangeFloating(writer, encoder);
                return;
            }

            writer.Write("<div class=\"dmb-form-field-control\">");
            writer.Write("<div class=\"form-floating\">");
            WriteInput(writer, encoder);
            WriteLabel(writer, encoder, string.Empty);
            writer.Write("</div>");
            WriteBadges(writer, encoder);
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        private void WriteHiddenLabel(TextWriter writer, HtmlEncoder encoder)
        {
            WriteLabel(writer, encoder, "visually-hidden");
            writer.Write("<div class=\"dmb-form-field-control\">");
            WriteInputWithOptionalTextField(writer, encoder);
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
            WriteInputWithOptionalTextField(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("<div class=\"offset-sm-4 col-sm-8\">");
            WriteValidation(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WriteGroup(TextWriter writer, HtmlEncoder encoder)
        {
            if (IsRangeInput())
            {
                WriteRangeGroup(writer, encoder);
                return;
            }

            writer.Write("<div class=\"dmb-form-field-control\">");
            writer.Write("<div class=\"input-group has-validation\">");
            WriteLabel(writer, encoder, "input-group-text");
            WriteInput(writer, encoder);
            WriteValidation(writer, encoder);
            writer.Write("</div>");
            WriteBadges(writer, encoder);
            writer.Write("</div>");
        }

        private void WriteRangeFloating(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"dmb-form-field-control\">");
            writer.Write("<div class=\"form-control dmb-slider-floating-control\">");
            WriteLabel(writer, encoder, "dmb-slider-floating-label");
            WriteInputWithOptionalTextField(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
            WriteValidation(writer, encoder);
        }

        private void WriteRangeGroup(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"dmb-form-field-control\">");
            writer.Write("<div class=\"input-group has-validation\">");
            WriteLabel(writer, encoder, "input-group-text");
            writer.Write("<div class=\"form-control dmb-slider-group-control\">");
            WriteInputWithOptionalTextField(writer, encoder);
            writer.Write("</div>");
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

        private void WriteInputWithOptionalTextField(TextWriter writer, HtmlEncoder encoder)
        {
            if (!IsRangeInput() || !_withTextField)
            {
                WriteInput(writer, encoder);
                if (!IsRangeInput())
                {
                    WriteBadges(writer, encoder);
                }
                return;
            }

            writer.Write("<div class=\"dmb-slider-with-text-field\">");
            WriteInput(writer, encoder);
            writer.Write("<div class=\"dmb-slider-text-field-control\">");
            WriteSliderTextField(writer, encoder);
            WriteBadges(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WriteInput(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<input");
            WriteAttribute(writer, encoder, "class", GetInputCssClass());
            WriteAttribute(writer, encoder, "type", _inputType);
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "placeholder", string.IsNullOrWhiteSpace(_placeholder) ? _label : _placeholder);
            WriteAttribute(writer, encoder, "value", _value);
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
        }

        private void WriteSliderTextField(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<input class=\"form-control dmb-slider-text-field\"");
            WriteAttribute(writer, encoder, "type", "number");
            WriteAttribute(writer, encoder, "id", $"{_inputId}_Text");
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "value", _value);
            WriteAttribute(writer, encoder, "data-dmb-slider-text-for", _inputId);
            CopyInputAttribute(writer, encoder, "min");
            CopyInputAttribute(writer, encoder, "max");
            CopyInputAttribute(writer, encoder, "step");
            if (_disabled)
            {
                writer.Write(" disabled");
            }
            writer.Write(">");
        }

        private void CopyInputAttribute(TextWriter writer, HtmlEncoder encoder, string name)
        {
            if (_inputAttributes.TryGetValue(name, out string? value))
            {
                WriteAttribute(writer, encoder, name, value);
            }
        }

        private string GetInputCssClass()
        {
            if (string.Equals(_inputType, "range", StringComparison.OrdinalIgnoreCase))
            {
                return $"form-range dmb-range dmb-range-{_variant.GetVariantCss()}";
            }

            if (string.Equals(_inputType, "color", StringComparison.OrdinalIgnoreCase))
            {
                return "form-control form-control-color dmb-color-field-input";
            }

            return "form-control";
        }

        private bool IsRangeInput()
        {
            return string.Equals(_inputType, "range", StringComparison.OrdinalIgnoreCase);
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

        private static bool IsReservedInputAttribute(string key)
        {
            return string.Equals(key, "class", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "type", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "id", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "name", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "placeholder", StringComparison.OrdinalIgnoreCase)
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
    }
}
