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
    public sealed class FormBuilderTextFieldExampleModel
    {
        #region Static methods

        public static FormBuilderTextFieldExampleModel CreateDemo()
        {
            return new FormBuilderTextFieldExampleModel
            {
                Nickname = "Pou",
                GroupCode = "AB-123",
                HiddenLabelSearch = "forms",
                DisabledValue = "Server value",
                UnixCode = "project_01",
                AsciiCode = "ASCII-ONLY"
            };
        }

        #endregion

        #region Instance fields and properties

        [MaxLength(12, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_AsciiCode),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_AsciiCode_Prompt))]
        public string? AsciiCode { get; set; }

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [StringLength(32, MinimumLength = 3)]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Company),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Company_Prompt))]
        public string Company { get; set; } = string.Empty;

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Disabled),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Disabled_Prompt))]
        public string DisabledValue { get; set; } = "Readonly example";

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FirstName),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FirstName_Prompt))]
        public string FirstName { get; set; } = string.Empty;

        [Range(0.5, 99.9, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FloatValue),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FloatValue_Prompt))]
        public decimal FloatValue { get; set; } = 12.5m;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(
            Name = "FormBuilder_Field_GroupCode",
            Prompt = "FormBuilder_Field_GroupCode_Prompt")]
        public string GroupCode { get; set; } = string.Empty;

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_HiddenLabel),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_HiddenLabel_Prompt))]
        public string? HiddenLabelSearch { get; set; }

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MaxLength(8)]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_InlineCode),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_InlineCode_Prompt))]
        public string InlineCode { get; set; } = string.Empty;

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Nickname),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Nickname_Prompt))]
        public string? Nickname { get; set; }

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_NumericValue),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_NumericValue_Prompt))]
        public int NumericValue { get; set; } = 42;

        [MaxLength(16, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [RegularExpression(@"[a-zA-Z0-9_]*", ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_UnixInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_UnixCode),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_UnixCode_Prompt))]
        public string? UnixCode { get; set; }

        #endregion
    }
}