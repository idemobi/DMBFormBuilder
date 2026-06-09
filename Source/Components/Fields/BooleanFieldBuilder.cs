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
    ///     Builds a Bootstrap checkbox or switch field with model-binding support and optional expected-value validation.
    /// </summary>
    public sealed class BooleanFieldBuilder :
        HtmlBuilderBase<BooleanFieldBuilder>,
        ICanUseCustomClasses
    {
        #region Constants

        private const string ScriptPath = "/js/formbuilder/FormBuilder.js";

        #endregion

        #region Static methods

        private static bool IsReservedInputAttribute(string key)
        {
            return string.Equals(key, "class", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "type", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "id", StringComparison.OrdinalIgnoreCase)
                   || string.Equals(key, "name", StringComparison.OrdinalIgnoreCase)
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

        private string _constraintMessage = string.Empty;
        private string _description = string.Empty;
        private bool _disabled;
        private bool? _expectedValue;
        private bool _hideLabel;
        private readonly Dictionary<string, string> _inputAttributes = new(StringComparer.OrdinalIgnoreCase);

        private string _inputId = "BooleanField";
        private string _inputName = "BooleanField";
        private string _label = "Boolean field";
        private IconStruct _labelIcon = IconStruct.Empty;
        private bool _labelOnLeft;
        private bool _switchStyle;
        private bool _value;
        private VariantStyle _variant = VariantStyle.Primary;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BooleanFieldBuilder" /> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public BooleanFieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClasses("dmb-form-field", "dmb-boolean-field", "mb-3");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Renders the Boolean field as a Bootstrap switch instead of a regular checkbox.
        /// </summary>
        /// <param name="value"><see langword="true" /> to render switch styling.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder AsSwitch(bool value = true)
        {
            _switchStyle = value;
            return this;
        }

        /// <inheritdoc />
        protected override BooleanFieldBuilder CreateInstance()
        {
            return new BooleanFieldBuilder(_textWriter, _htmlHelper);
        }

        private void EnsureValidationAssets()
        {
            if (!_expectedValue.HasValue)
            {
                return;
            }

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetScriptFile(ScriptPath);
        }

        /// <summary>
        ///     Hides the label visually while keeping it available for assistive technologies.
        /// </summary>
        /// <param name="value"><see langword="true" /> to apply visually hidden label styling.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder HideLabel(bool value = true)
        {
            _hideLabel = value;
            return this;
        }

        /// <inheritdoc />
        protected override void InternalClone(BooleanFieldBuilder source)
        {
            base.InternalClone(source);
            _inputId = source._inputId;
            _inputName = source._inputName;
            _label = source._label;
            _description = source._description;
            _value = source._value;
            _disabled = source._disabled;
            _switchStyle = source._switchStyle;
            _labelOnLeft = source._labelOnLeft;
            _hideLabel = source._hideLabel;
            _expectedValue = source._expectedValue;
            _constraintMessage = source._constraintMessage;
            _labelIcon = source._labelIcon;
            _variant = source._variant;
            _inputAttributes.Clear();
            foreach (KeyValuePair<string, string> attribute in source._inputAttributes)
            {
                _inputAttributes[attribute.Key] = attribute.Value;
            }
        }

        /// <summary>
        ///     Places the label before the checkbox input.
        /// </summary>
        /// <param name="value"><see langword="true" /> to render the label on the left.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder LabelOnLeft(bool value = true)
        {
            _labelOnLeft = value;
            return this;
        }

        /// <inheritdoc />
        protected void OnBeginRendering()
        {
            EnsureValidationAssets();
        }

        /// <summary>
        ///     Sets the optional description rendered as an information popover next to the field label.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Enables or disables the rendered checkbox input.
        /// </summary>
        /// <param name="disabled"><see langword="true" /> to render the <c>disabled</c> attribute.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public new BooleanFieldBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        ///     Sets the checkbox identifier and model binding name.
        /// </summary>
        /// <param name="inputId">The value rendered in the <c>id</c> attribute when not empty.</param>
        /// <param name="inputName">The value rendered in the <c>name</c> attribute when not empty.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetInput(string inputId, string inputName)
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
        ///     Sets or replaces an attribute on the rendered checkbox input.
        /// </summary>
        /// <param name="name">The attribute name; empty names are ignored.</param>
        /// <param name="value">The attribute value; <see langword="null" /> values are ignored.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetInputAttribute(string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(name) && value != null)
            {
                _inputAttributes[name] = value;
            }

            return this;
        }

        /// <summary>
        ///     Sets the visible label text when a non-empty value is provided.
        /// </summary>
        /// <param name="label">The label text rendered next to the checkbox or switch.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        ///     Sets the icon rendered before the label text.
        /// </summary>
        /// <param name="icon">The icon descriptor to render.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        ///     Adds validation requiring the checkbox value to be checked.
        /// </summary>
        /// <param name="message">An optional validation message rendered in FormBuilder boolean validation metadata.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetMustBeChecked(string? message = null)
        {
            _expectedValue = true;
            _constraintMessage = message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_BoolMustBeTrue));
            return this;
        }

        /// <summary>
        ///     Adds validation requiring the checkbox value to remain unchecked.
        /// </summary>
        /// <param name="message">An optional validation message rendered in FormBuilder boolean validation metadata.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetMustBeUnchecked(string? message = null)
        {
            _expectedValue = false;
            _constraintMessage = message ?? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_BoolMustBeFalse));
            return this;
        }

        /// <summary>
        ///     Sets whether the checkbox is rendered as checked.
        /// </summary>
        /// <param name="value"><see langword="true" /> to render the <c>checked</c> attribute.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetValue(bool value)
        {
            _value = value;
            return this;
        }

        /// <summary>
        ///     Sets the Bootstrap variant used by checkbox or switch styling.
        /// </summary>
        /// <param name="variant">The visual variant to apply.</param>
        /// <returns>The current <see cref="BooleanFieldBuilder" /> instance for fluent chaining.</returns>
        public BooleanFieldBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }

        private void WriteInput(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<input class=\"form-check-input ");
            encoder.Encode(writer, BootstrapStyleHelper.GetSwitchStyleCss(_variant));
            writer.Write("\"");
            WriteAttribute(writer, encoder, "type", "checkbox");
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "value", "true");
            WriteAttribute(writer, encoder, "data-val", "true");
            if (_expectedValue.HasValue)
            {
                WriteAttribute(writer, encoder, "data-val-bool-expected", _expectedValue.Value ? "true" : "false");
                WriteAttribute(writer, encoder, "data-val-bool", _constraintMessage);
            }

            foreach (KeyValuePair<string, string> attribute in _inputAttributes)
            {
                if (IsReservedInputAttribute(attribute.Key))
                {
                    continue;
                }

                WriteAttribute(writer, encoder, attribute.Key, attribute.Value);
            }

            if (_value)
            {
                writer.Write(" checked");
            }

            if (_disabled)
            {
                writer.Write(" disabled");
            }

            writer.Write(">");
            writer.Write("<input type=\"hidden\"");
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "value", "false");
            WriteAttribute(writer, encoder, "data-formbuilder-ignore-validation", "true");
            writer.Write(">");
        }

        private void WriteLabel(TextWriter writer, HtmlEncoder encoder, string cssClass)
        {
            bool writeDescriptionAfterLabel = cssClass.Contains("visually-hidden", StringComparison.Ordinal);
            writer.Write("<label class=\"form-check-label");
            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                writer.Write(' ');
                encoder.Encode(writer, cssClass);
            }

            writer.Write("\"");
            WriteAttribute(writer, encoder, "for", _inputId);
            writer.Write(">");
            WriteLabelIcon(writer, encoder);
            encoder.Encode(writer, _label);
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

        /// <summary>
        ///     Writes the complete Bootstrap checkbox or switch markup, including hidden false value and validation metadata.
        /// </summary>
        /// <param name="writer">The writer receiving the generated markup.</param>
        /// <param name="encoder">The HTML encoder used by the rendering pipeline.</param>
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureValidationAssets();
            writer.Write($"<{_tag}{BuildAttributes()}>");
            writer.Write("<div class=\"form-check");
            if (_switchStyle)
            {
                writer.Write(" form-switch");
            }

            writer.Write("\">");

            if (_labelOnLeft && !_hideLabel)
            {
                WriteLabel(writer, encoder, "me-2");
            }

            WriteInput(writer, encoder);

            if (!_labelOnLeft)
            {
                WriteLabel(writer, encoder, _hideLabel ? "visually-hidden" : string.Empty);
            }

            writer.Write("</div>");
            WriteValidation(writer, encoder);
            writer.Write($"</{_tag}>");
        }

        private void WriteValidation(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"invalid-feedback\" data-valmsg-for=\"");
            encoder.Encode(writer, _inputName);
            writer.Write("\" data-valmsg-replace=\"true\">");
            if (_expectedValue.HasValue)
            {
                encoder.Encode(writer, _constraintMessage);
            }

            writer.Write("</div>");
        }

        #endregion
    }
}