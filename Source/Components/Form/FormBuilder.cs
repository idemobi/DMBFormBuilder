#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBFormBuilder.Resources;
using DMBPageBuilder;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    ///     Builds a Bootstrap compatible HTML form while keeping PageBuilder attribute and CSS composition.
    /// </summary>
    public sealed class FormBuilder :
        HtmlTagBuilder<FormBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin,
        ICanUsePadding
    {
        #region Constants

        private const string ScriptPath = "/js/formbuilder/FormBuilder.js";
        private const string StylesheetPath = "/css/formbuilder/FormBuilder.css";

        #endregion

        #region Instance fields and properties

        private string? _requiredLegend;
        private bool _showRequiredLegend = true;

        private FormValidationMode _validationMode;

        /// <summary>
        ///     Gets the validation strategy currently applied to the form.
        /// </summary>
        /// <value>
        ///     The value controls the <c>data-validation-mode</c> attribute consumed by FormBuilder client behavior.
        /// </value>
        public FormValidationMode ValidationMode => _validationMode;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FormBuilder" /> class.
        /// </summary>
        /// <param name="writer">The output writer used by the current Razor view.</param>
        /// <param name="html">The HTML helper that supplies the current view context.</param>
        /// <remarks>
        ///     The builder renders a <c>form</c> element with Bootstrap validation classes and registers the default
        ///     FormBuilder CSS and JavaScript assets when rendering begins.
        /// </remarks>
        public FormBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "form";
            _validationMode = FormBuilderConfiguration.Default.ValidationMode;

            this.AddClasses("dmb-form-builder", "needs-validation");
            SetAttribute("method", "post");
            SetAttribute("autocomplete", "on");
            SetData("form-builder", "true");
            SetData("validation-mode", _validationMode.ToString().ToLowerInvariant());
        }

        #endregion

        #region Instance methods

        /// <inheritdoc />
        protected override FormBuilder CreateInstance()
        {
            return new FormBuilder(_textWriter, _htmlHelper);
        }

        /// <summary>
        ///     Adds the <c>novalidate</c> attribute to disable native browser validation.
        /// </summary>
        /// <param name="value">
        ///     <see langword="true" /> to render <c>novalidate</c>; <see langword="false" /> leaves the current attributes
        ///     unchanged.
        /// </param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder DisableBrowserValidation(bool value = true)
        {
            if (value)
            {
                SetAttribute("novalidate", "novalidate");
            }

            return this;
        }

        /// <summary>
        ///     Toggles the client-side marker used to enable submit buttons only when the form is valid.
        /// </summary>
        /// <param name="value"><see langword="true" /> to set the marker; <see langword="false" /> to remove it.</param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder EnableSubmitWhenValid(bool value = true)
        {
            SetData("submit-when-valid", value ? "true" : null);
            return this;
        }

        /// <summary>
        ///     Writes the required-field legend, when enabled, and closes the rendered form element.
        /// </summary>
        public override void End()
        {
            if (!_started)
            {
                return;
            }

            if (_showRequiredLegend)
            {
                string legend = string.IsNullOrWhiteSpace(_requiredLegend)
                    ? WebLocalizer.GetDataAnnotation(nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Form_RequiredLegend))
                    : _requiredLegend;

                _textWriter.Write("<div class=\"form-text dmb-form-required-legend\"><span class=\"text-danger\" aria-hidden=\"true\">*</span> ");
                _textWriter.Write(System.Text.Encodings.Web.HtmlEncoder.Default.Encode(legend));
                _textWriter.Write("</div>");
            }

            base.End();
        }

        /// <inheritdoc />
        protected override void InternalClone(FormBuilder source)
        {
            base.InternalClone(source);
            _validationMode = source._validationMode;
            _showRequiredLegend = source._showRequiredLegend;
            _requiredLegend = source._requiredLegend;
        }

        /// <summary>
        ///     Registers FormBuilder CSS and JavaScript assets with the current page information.
        /// </summary>
        protected override void OnBeginRendering()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(StylesheetPath);
            page.SetScriptFile(ScriptPath);
        }

        /// <summary>
        ///     Sets the form action URL when a non-empty value is provided.
        /// </summary>
        /// <param name="action">The URL or route path assigned to the rendered <c>action</c> attribute.</param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder SetAction(string? action)
        {
            if (!string.IsNullOrWhiteSpace(action))
            {
                SetAttribute("action", action);
            }

            return this;
        }

        /// <summary>
        ///     Sets an accessible label on the form when a non-empty value is provided.
        /// </summary>
        /// <param name="label">The text rendered in the <c>aria-label</c> attribute.</param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder SetAriaLabel(string? label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                SetAttribute("aria-label", label);
            }

            return this;
        }

        /// <summary>
        ///     Sets the form submission method rendered in the <c>method</c> attribute.
        /// </summary>
        /// <param name="method">The submission method to render.</param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder SetMethod(FormSubmissionMethod method)
        {
            SetAttribute("method", method.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        ///     Enables or disables multipart form submission.
        /// </summary>
        /// <param name="value">
        ///     <see langword="true" /> to set <c>enctype="multipart/form-data"</c>; <see langword="false" /> leaves the current
        ///     attributes unchanged.
        /// </param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder SetMultipart(bool value = true)
        {
            if (value)
            {
                SetAttribute("enctype", "multipart/form-data");
            }

            return this;
        }

        /// <summary>
        ///     Sets the text rendered in the required-field legend displayed before the closing form tag.
        /// </summary>
        /// <param name="legend">
        ///     The custom legend text; when <see langword="null" /> or empty, the localized default legend is used.
        /// </param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder SetRequiredLegend(string? legend)
        {
            _requiredLegend = legend;
            return this;
        }

        /// <summary>
        ///     Sets the validation mode advertised by the form data attributes.
        /// </summary>
        /// <param name="mode">The validation mode consumed by server-rendered markup and client scripts.</param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder SetValidationMode(FormValidationMode mode)
        {
            _validationMode = mode;
            SetData("validation-mode", mode.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        ///     Shows or hides the required-field legend rendered at the end of the form.
        /// </summary>
        /// <param name="show"><see langword="true" /> to render the legend; otherwise <see langword="false" />.</param>
        /// <returns>The current <see cref="FormBuilder" /> instance for fluent chaining.</returns>
        public FormBuilder ShowRequiredLegend(bool show = true)
        {
            _showRequiredLegend = show;
            return this;
        }

        #endregion
    }
}