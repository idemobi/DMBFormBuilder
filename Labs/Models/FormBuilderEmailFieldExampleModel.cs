#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.ComponentModel.DataAnnotations;
using DMBFormBuilder.Resources;

#endregion

namespace DMBFormBuilderLabs.Models
{
    public sealed class FormBuilderEmailFieldExampleModel
    {
        #region Static methods

        public static FormBuilderEmailFieldExampleModel CreateDemo()
        {
            return new FormBuilderEmailFieldExampleModel
            {
                OptionalEmail = "contact@example.com",
                GroupEmail = "team@example.com",
                DisabledEmail = "readonly@example.com",
                HiddenEmail = "hidden@example.com"
            };
        }

        #endregion

        #region Instance fields and properties

        [EmailAddress(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailOptional),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailOptional_Prompt))]
        public string? DisabledEmail { get; set; }

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [EmailAddress(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Email),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Email_Prompt))]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [EmailAddress(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Email),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Email_Prompt))]
        public string GroupEmail { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_HiddenEmail),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_HiddenEmail_Prompt))]
        public string? HiddenEmail { get; set; }

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [EmailAddress(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_InlineEmail),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_InlineEmail_Prompt))]
        public string InlineEmail { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailOptional),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailOptional_Prompt))]
        public string? OptionalEmail { get; set; }

        #endregion
    }
}