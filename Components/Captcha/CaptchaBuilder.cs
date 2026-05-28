#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj CaptchaBuilder.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using DMBFormBuilder.Resources;
using DMBServerHelper;
using DMBServerWebHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Builds a captcha input group backed by FormBuilder captcha generation and refresh behavior.
    /// </summary>
    public sealed class CaptchaBuilder :
        HtmlBuilderBase<CaptchaBuilder>,
        ICanUseCustomClasses
    {
        private const string ScriptPath = "/js/formbuilder/CaptchaBuilder.js";
        private const string CryptoJsPath = "https://cdnjs.cloudflare.com/ajax/libs/crypto-js/4.1.1/crypto-js.min.js";

        private readonly Dictionary<string, string> _inputAttributes = new(StringComparer.OrdinalIgnoreCase);

        private string _inputId = "CaptchaValue";
        private string _inputName = "CaptchaValue";
        private string _label = "Captcha";
        private string _description = string.Empty;
        private string _placeholder = "Captcha";
        private string _refreshUrl = "/Captcha/RefreshCaptcha";
        private string _alt = "Captcha";
        private int _imageHeight = 30;
        private bool _disabled;
        private bool _required = true;
        private FormLabelPresentation _presentation = FormBuilderConfiguration.Default.LabelPresentation;
        private IconStruct _labelIcon = IconStruct.Empty;
        private VariantStyle _refreshVariant = VariantStyle.Primary;
        private ButtonVariantMode _refreshVariantMode = ButtonVariantMode.Filled;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaBuilder"/> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        public CaptchaBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClasses("dmb-form-field", "dmb-captcha", "mb-3");
            SetData("dmb-captcha", "true");
        }

        /// <summary>
        /// Sets the captcha input identifier and model binding name.
        /// </summary>
        public CaptchaBuilder SetInput(string inputId, string inputName)
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
        /// Sets the captcha label when a non-empty value is provided.
        /// </summary>
        public CaptchaBuilder SetLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                _label = label;
            }

            return this;
        }

        /// <summary>
        /// Sets the optional description rendered as an information popover next to the captcha label.
        /// </summary>
        /// <param name="description">The popover content, or an empty value to omit the information trigger.</param>
        /// <returns>The current <see cref="CaptchaBuilder"/> instance for fluent chaining.</returns>
        public CaptchaBuilder SetDescription(string? description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Sets how the captcha label is positioned or hidden.
        /// </summary>
        public CaptchaBuilder SetLabelPresentation(FormLabelPresentation presentation)
        {
            _presentation = presentation;
            return this;
        }

        /// <summary>
        /// Sets the icon rendered with the captcha label.
        /// </summary>
        public CaptchaBuilder SetLabelIcon(IconStruct icon)
        {
            _labelIcon = icon;
            return this;
        }

        /// <summary>
        /// Sets the captcha input placeholder when a non-empty value is provided.
        /// </summary>
        public CaptchaBuilder SetPlaceholder(string? placeholder)
        {
            if (!string.IsNullOrWhiteSpace(placeholder))
            {
                _placeholder = placeholder;
            }

            return this;
        }

        /// <summary>
        /// Sets the endpoint URL used by the captcha refresh button.
        /// </summary>
        public CaptchaBuilder SetRefreshUrl(string? refreshUrl)
        {
            if (!string.IsNullOrWhiteSpace(refreshUrl))
            {
                _refreshUrl = refreshUrl;
            }

            return this;
        }

        /// <summary>
        /// Sets the captcha image height when the value is greater than zero.
        /// </summary>
        public CaptchaBuilder SetImageHeight(int imageHeight)
        {
            if (imageHeight > 0)
            {
                _imageHeight = imageHeight;
            }

            return this;
        }

        /// <summary>
        /// Sets the alternative text rendered on the captcha image.
        /// </summary>
        public CaptchaBuilder SetAlt(string? alt)
        {
            if (!string.IsNullOrWhiteSpace(alt))
            {
                _alt = alt;
            }

            return this;
        }

        /// <summary>
        /// Enables or disables the captcha input and refresh control.
        /// </summary>
        public new CaptchaBuilder SetDisabled(bool disabled = true)
        {
            _disabled = disabled;
            return this;
        }

        /// <summary>
        /// Enables or disables required validation for the captcha input.
        /// </summary>
        public CaptchaBuilder SetRequired(bool required = true)
        {
            _required = required;
            return this;
        }

        /// <summary>
        /// Sets the Bootstrap variant used by the captcha refresh button.
        /// </summary>
        public CaptchaBuilder SetRefreshVariant(VariantStyle variant, ButtonVariantMode variantMode = ButtonVariantMode.Filled)
        {
            _refreshVariant = variant;
            _refreshVariantMode = variantMode;
            return this;
        }

        /// <summary>
        /// Sets or replaces an attribute on the rendered captcha input.
        /// </summary>
        public CaptchaBuilder SetInputAttribute(string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
            {
                _inputAttributes[name] = value;
            }

            return this;
        }

        /// <summary>
        /// Sets or replaces multiple attributes on the rendered captcha input.
        /// </summary>
        public CaptchaBuilder SetInputAttributes(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                SetInputAttribute(attribute.Key, attribute.Value);
            }

            return this;
        }

        /// <inheritdoc/>
        protected override CaptchaBuilder CreateInstance()
        {
            return new CaptchaBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc/>
        protected override void InternalClone(CaptchaBuilder source)
        {
            base.InternalClone(source);
            _inputId = source._inputId;
            _inputName = source._inputName;
            _label = source._label;
            _description = source._description;
            _placeholder = source._placeholder;
            _refreshUrl = source._refreshUrl;
            _alt = source._alt;
            _imageHeight = source._imageHeight;
            _disabled = source._disabled;
            _required = source._required;
            _presentation = source._presentation;
            _labelIcon = source._labelIcon;
            _refreshVariant = source._refreshVariant;
            _refreshVariantMode = source._refreshVariantMode;

            _inputAttributes.Clear();
            foreach (KeyValuePair<string, string> attribute in source._inputAttributes)
            {
                _inputAttributes[attribute.Key] = attribute.Value;
            }
        }

        /// <summary>
        /// Writes the complete captcha field markup, creates the captcha image payload, and registers client refresh scripts.
        /// </summary>
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            string imageBase64 = CaptchaFactory.RandomCaptchaToImage(_htmlHelper.ViewContext.HttpContext, ServerWebHelperConfiguration.Config.CaptchaParameters);
            string captcha = CaptchaFactory.GetStoredCaptcha(_htmlHelper.ViewContext.HttpContext);
            string hash = SecurityHashTools.GenerateSha256(captcha);
            string value = string.Empty;
            string invalidMessage = WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Captcha_Invalid));
            string requiredMessage = WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Captcha_Required));
            string imageErrorMessage = WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Captcha_Image_Error));

#if DEBUG
            value = captcha;
#endif

            SetData("dmb-captcha-refresh-url", _refreshUrl);

            writer.Write($"<{_tag}{BuildAttributes()}>");
            switch (_presentation)
            {
                case FormLabelPresentation.Floating:
                    WriteFloating(writer, encoder, imageBase64, imageErrorMessage, hash, invalidMessage, requiredMessage, value);
                    break;
                case FormLabelPresentation.Hidden:
                    WriteFieldLabel(writer, encoder, "visually-hidden");
                    WriteInputGroup(writer, encoder, imageBase64, imageErrorMessage, hash, invalidMessage, requiredMessage, value, includeLabelInGroup: false);
                    break;
                case FormLabelPresentation.Inline:
                    WriteInline(writer, encoder, imageBase64, imageErrorMessage, hash, invalidMessage, requiredMessage, value);
                    break;
                case FormLabelPresentation.Group:
                    WriteInputGroup(writer, encoder, imageBase64, imageErrorMessage, hash, invalidMessage, requiredMessage, value, includeLabelInGroup: true);
                    break;
                case FormLabelPresentation.Normal:
                default:
                    WriteFieldLabel(writer, encoder, "form-label");
                    WriteInputGroup(writer, encoder, imageBase64, imageErrorMessage, hash, invalidMessage, requiredMessage, value, includeLabelInGroup: false);
                    break;
            }
            writer.Write($"</{_tag}>");
        }

        private void WriteFloating(TextWriter writer, HtmlEncoder encoder, string imageBase64, string imageErrorMessage, string hash, string invalidMessage, string requiredMessage, string value)
        {
            writer.Write("<div class=\"form-control dmb-captcha-floating-control\">");
            WriteFieldLabel(writer, encoder, "dmb-captcha-floating-label");
            WriteInputGroup(writer, encoder, imageBase64, imageErrorMessage, hash, invalidMessage, requiredMessage, value, includeLabelInGroup: false);
            writer.Write("</div>");
        }

        private void WriteInline(TextWriter writer, HtmlEncoder encoder, string imageBase64, string imageErrorMessage, string hash, string invalidMessage, string requiredMessage, string value)
        {
            writer.Write("<div class=\"row g-2 align-items-start\">");
            writer.Write("<div class=\"col-sm-4\">");
            WriteFieldLabel(writer, encoder, "col-form-label");
            writer.Write("</div><div class=\"col-sm-8\">");
            WriteInputGroup(writer, encoder, imageBase64, imageErrorMessage, hash, invalidMessage, requiredMessage, value, includeLabelInGroup: false);
            writer.Write("</div></div>");
        }

        private void WriteInputGroup(TextWriter writer, HtmlEncoder encoder, string imageBase64, string imageErrorMessage, string hash, string invalidMessage, string requiredMessage, string value, bool includeLabelInGroup)
        {
            writer.Write("<div class=\"input-group has-validation\">");
            if (includeLabelInGroup)
            {
                WriteFieldLabel(writer, encoder, "input-group-text dmb-captcha-group-label");
            }
            WriteImageLabel(writer, encoder, imageBase64, imageErrorMessage);
            WriteInput(writer, encoder,
                hash,
                invalidMessage,
                requiredMessage,
                value);
            WriteRefreshButton(writer, encoder);
            WriteFeedback(writer, encoder, requiredMessage);
            writer.Write("</div>");
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetScriptFile(CryptoJsPath, order: -10);
            page.SetScriptFile(ScriptPath);
        }

        private void WriteFieldLabel(TextWriter writer, HtmlEncoder encoder, string cssClass)
        {
            bool writeDescriptionAfterLabel = cssClass.Contains("visually-hidden", StringComparison.Ordinal);
            writer.Write("<label");
            WriteAttribute(writer, encoder, "class", cssClass);
            WriteAttribute(writer, encoder, "for", _inputId);
            writer.Write(">");
            WriteLabelIcon(writer, encoder);
            encoder.Encode(writer, _label);
            if (_required)
            {
                writer.Write("<span class=\"text-danger ms-1\" aria-hidden=\"true\">*</span>");
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

        private void WriteImageLabel(TextWriter writer, HtmlEncoder encoder, string imageBase64, string imageErrorMessage)
        {
            writer.Write("<span class=\"input-group-text dmb-captcha-image\">");

            if (string.IsNullOrEmpty(imageBase64))
            {
                writer.Write("<span><span class=\"bi bi-exclamation-triangle\"></span> ");
                encoder.Encode(writer, imageErrorMessage);
                writer.Write("</span>");
            }
            else
            {
                writer.Write("<img");
                WriteAttribute(writer, encoder, "id", $"{_inputId}-img-captcha");
                WriteAttribute(writer, encoder, "src", $"data:image/png;base64,{imageBase64}");
                WriteAttribute(writer, encoder, "height", _imageHeight.ToString());
                WriteAttribute(writer, encoder, "alt", _alt);
                writer.Write(">");
            }

            writer.Write("</span>");
        }

        private void WriteInput(TextWriter writer, HtmlEncoder encoder, string hash, string invalidMessage, string requiredMessage, string value)
        {
            writer.Write("<input class=\"form-control\" type=\"text\"");
            WriteAttribute(writer, encoder, "id", _inputId);
            WriteAttribute(writer, encoder, "name", _inputName);
            WriteAttribute(writer, encoder, "placeholder", _placeholder);
            WriteAttribute(writer, encoder, "data-hash", hash);
            WriteAttribute(writer, encoder, "data-val", "true");
            WriteAttribute(writer, encoder, "data-val-captchahash", invalidMessage);
            WriteAttribute(writer, encoder, "autocomplete", "off");
            WriteAttribute(writer, encoder, "value", value);

            if (_required)
            {
                WriteAttribute(writer, encoder, "required", "required");
                WriteAttribute(writer, encoder, "data-val-required", requiredMessage);
            }

            foreach (KeyValuePair<string, string> attribute in _inputAttributes)
            {
                if (string.Equals(attribute.Key, "class", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(attribute.Key, "type", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(attribute.Key, "id", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(attribute.Key, "name", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(attribute.Key, "value", StringComparison.OrdinalIgnoreCase))
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

        private void WriteRefreshButton(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<button type=\"button\"");
            WriteAttribute(writer, encoder, "class", $"input-group-text btn {_refreshVariant.GetButtonVariantCss(_refreshVariantMode)}");
            writer.Write(" data-dmb-captcha-refresh");

            if (_disabled)
            {
                writer.Write(" disabled");
            }

            writer.Write("><span class=\"bi bi-arrow-repeat\"></span></button>");
        }

        private static void WriteFeedback(TextWriter writer, HtmlEncoder encoder, string requiredMessage)
        {
            writer.Write("<div class=\"invalid-feedback\">");
            encoder.Encode(writer, requiredMessage);
            writer.Write("</div>");
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
