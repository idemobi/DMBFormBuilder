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
    public sealed class FormBuilderPasswordFieldExampleModel
    {
        #region Constants

        private const int PasswordMaxLength = 128;
        private const int PasswordMinLength = 12;
        private const string PasswordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9\s]).+$";

        #endregion

        #region Static methods

        public static FormBuilderPasswordFieldExampleModel CreateDemo()
        {
            return new FormBuilderPasswordFieldExampleModel
            {
                DisabledPassword = "readonly-password",
                RevealLockedPassword = "reveal-locked-password",
                HiddenPassword = "hidden-password"
            };
        }

        #endregion

        #region Instance fields and properties

        [Compare(nameof(NewPassword), ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCompare))]
        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(PasswordMinLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [MaxLength(PasswordMaxLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(PasswordPattern, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordConfirm),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordConfirm_Prompt))]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(PasswordMinLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [MaxLength(PasswordMaxLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(PasswordPattern, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent_Prompt))]
        public string CurrentPassword { get; set; } = string.Empty;

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent_Prompt))]
        public string? DisabledPassword { get; set; }

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent_Prompt))]
        public string? HiddenPassword { get; set; }

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(PasswordMinLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [MaxLength(PasswordMaxLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(PasswordPattern, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew_Prompt))]
        public string NewPassword { get; set; } = string.Empty;

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordCurrent_Prompt))]
        public string? RevealLockedPassword { get; set; }

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(PasswordMinLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [MaxLength(PasswordMaxLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(PasswordPattern, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew_Prompt))]
        public string StrengthPasswordFloating { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(PasswordMinLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [MaxLength(PasswordMaxLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(PasswordPattern, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew_Prompt))]
        public string StrengthPasswordHidden { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(PasswordMinLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [MaxLength(PasswordMaxLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(PasswordPattern, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew_Prompt))]
        public string StrengthPasswordInline { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(PasswordMinLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [MaxLength(PasswordMaxLength, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(PasswordPattern, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew_Prompt))]
        public string StrengthPasswordNormal { get; set; } = string.Empty;

        #endregion
    }
}