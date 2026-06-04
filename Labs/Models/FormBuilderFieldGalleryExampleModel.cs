#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.ComponentModel.DataAnnotations;
using DMBFormBuilder.Resources;

#endregion

namespace DMBFormBuilderLabs.Models
{
    public enum FormBuilderSamplePriority
    {
        Low,
        Medium,
        High
    }

    public enum FormBuilderSampleVisibility
    {
        [Display(Name = "FormBuilder_Field_Public")]
        Public,

        [Display(Name = "FormBuilder_Field_Private")]
        Private,

        [Display(Name = "FormBuilder_Field_Internal")]
        Internal
    }

    [Flags]
    public enum FormBuilderSamplePermissions
    {
        None = 0,

        [Display(Name = "FormBuilder_Field_Read")]
        Read = 1,

        [Display(Name = "FormBuilder_Field_Write")]
        Write = 2,

        [Display(Name = "FormBuilder_Field_Execute")]
        Execute = 4,

        [Display(Name = "FormBuilder_Field_Admin")]
        Admin = 8
    }

    public sealed class FormBuilderTextAreaExampleModel
    {
        #region Static methods

        public static FormBuilderTextAreaExampleModel CreateDemo()
        {
            return new FormBuilderTextAreaExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [StringLength(80, MinimumLength = 20, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message_Prompt))]
        public string ConstrainedMessage { get; set; } = "This textarea shows length constraints.";

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Notes),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Notes_Prompt))]
        public string? DisabledNotes { get; set; } = "Readonly textarea content.";

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Notes),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Notes_Prompt))]
        public string? HiddenNotes { get; set; } = "Hidden label textarea content.";

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [StringLength(180, MinimumLength = 12, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message_Prompt))]
        public string InlineMessage { get; set; } = "Inline textarea content.";

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [StringLength(180, MinimumLength = 12, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message_Prompt))]
        public string Message { get; set; } = "Short message for the FormBuilder textarea.";

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message_Prompt))]
        public string? NoHtmlMessage { get; set; } = "Plain text only. Try typing <strong>HTML</strong>.";

        [StringLength(280, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Notes),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Notes_Prompt))]
        public string? Notes { get; set; } = "Floating textarea content.";

        #endregion
    }

    public sealed class FormBuilderBooleanExampleModel
    {
        #region Static methods

        public static FormBuilderBooleanExampleModel CreateDemo()
        {
            return new FormBuilderBooleanExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_AcceptTerms))]
        public bool AcceptTerms { get; set; } = true;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EnableNotifications))]
        public bool DangerSwitch { get; set; } = true;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EnableNotifications))]
        public bool DisabledCheckbox { get; set; } = true;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EnableNotifications))]
        public bool DisabledSwitch { get; set; } = true;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EnableNotifications))]
        public bool EnableNotifications { get; set; } = true;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_AcceptTerms))]
        public bool HiddenLabel { get; set; } = true;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_LeftLabel))]
        public bool LeftLabel { get; set; }

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_AcceptTerms))]
        public bool MustBeChecked { get; set; }

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EnableNotifications))]
        public bool MustBeUnchecked { get; set; } = true;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EnableNotifications))]
        public bool SuccessSwitch { get; set; } = true;

        #endregion
    }

    public sealed class FormBuilderDateInputExampleModel
    {
        #region Static methods

        public static FormBuilderDateInputExampleModel CreateDemo()
        {
            return new FormBuilderDateInputExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate_Prompt))]
        public DateTime DisabledDate { get; set; } = DateTime.Today.AddDays(21);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate_Prompt))]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate_Prompt))]
        public DateTime FloatingDate { get; set; } = DateTime.Today.AddDays(10);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate_Prompt))]
        public DateTime HiddenDate { get; set; } = DateTime.Today.AddDays(18);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate_Prompt))]
        public DateTime InlineDate { get; set; } = DateTime.Today.AddDays(14);

        #endregion
    }

    public sealed class FormBuilderDateTimeInputExampleModel
    {
        #region Static methods

        public static FormBuilderDateTimeInputExampleModel CreateDemo()
        {
            return new FormBuilderDateTimeInputExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime_Prompt))]
        public DateTime DisabledDateTime { get; set; } = DateTime.Today.AddDays(4).AddHours(11);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime_Prompt))]
        public DateTime DueDateTime { get; set; } = DateTime.Today.AddHours(14);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime_Prompt))]
        public DateTime FloatingDateTime { get; set; } = DateTime.Today.AddDays(1).AddHours(10);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime_Prompt))]
        public DateTime HiddenDateTime { get; set; } = DateTime.Today.AddDays(3).AddHours(9);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime_Prompt))]
        public DateTime InlineDateTime { get; set; } = DateTime.Today.AddDays(2).AddHours(16);

        #endregion
    }

    public sealed class FormBuilderTimeInputExampleModel
    {
        #region Static methods

        public static FormBuilderTimeInputExampleModel CreateDemo()
        {
            return new FormBuilderTimeInputExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Month),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Month_Prompt))]
        public DateTime DisabledMonth { get; set; } = DateTime.Today.AddMonths(1);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Time),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Time_Prompt))]
        public TimeSpan InlineTime { get; set; } = new TimeSpan(14, 45, 0);

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Month),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Month_Prompt))]
        public DateTime Month { get; set; } = DateTime.Today;

        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Time),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Time_Prompt))]
        public TimeSpan Time { get; set; } = new TimeSpan(9, 30, 0);

        #endregion
    }

    public sealed class FormBuilderColorInputExampleModel
    {
        #region Static methods

        public static FormBuilderColorInputExampleModel CreateDemo()
        {
            return new FormBuilderColorInputExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Color))]
        public string Color { get; set; } = "#2f6fed";

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Color))]
        public string DisabledColor { get; set; } = "#dc3545";

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Color))]
        public string FloatingColor { get; set; } = "#198754";

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Color))]
        public string GroupColor { get; set; } = "#fd7e14";

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Color))]
        public string HiddenColor { get; set; } = "#0dcaf0";

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Color))]
        public string InlineColor { get; set; } = "#6f42c1";

        #endregion
    }

    public sealed class FormBuilderSliderInputExampleModel
    {
        #region Static methods

        public static FormBuilderSliderInputExampleModel CreateDemo()
        {
            return new FormBuilderSliderInputExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int DisabledSlider { get; set; } = 40;

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int FineSlider { get; set; } = 42;

        [Range(10, 90, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int FloatingSlider { get; set; } = 25;

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int GroupSlider { get; set; } = 55;

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int InlineSlider { get; set; } = 75;

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int Slider { get; set; } = 50;

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int SuccessSlider { get; set; } = 65;

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider))]
        public int WarningSlider { get; set; } = 35;

        #endregion
    }

    public sealed class FormBuilderSelectExampleModel
    {
        #region Static methods

        public static FormBuilderSelectExampleModel CreateDemo()
        {
            return new FormBuilderSelectExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority))]
        public FormBuilderSamplePriority DisabledPriority { get; set; } = FormBuilderSamplePriority.Low;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority))]
        public FormBuilderSamplePriority FloatingPriority { get; set; } = FormBuilderSamplePriority.High;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority))]
        public FormBuilderSamplePriority GroupPriority { get; set; } = FormBuilderSamplePriority.Medium;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority))]
        public FormBuilderSamplePriority HiddenPriority { get; set; } = FormBuilderSamplePriority.High;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority))]
        public FormBuilderSamplePriority InlinePriority { get; set; } = FormBuilderSamplePriority.Low;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority))]
        public FormBuilderSamplePriority Priority { get; set; } = FormBuilderSamplePriority.Medium;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority))]
        public string RequiredPriority { get; set; } = string.Empty;

        #endregion
    }

    public sealed class FormBuilderTokenExampleModel
    {
        #region Static methods

        public static FormBuilderTokenExampleModel CreateDemo()
        {
            return new FormBuilderTokenExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MaxLength(64, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt")]
        public string ApiToken { get; set; } = "gdf_live_7a8b9c_form_builder_token";

        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt")]
        public string FloatingToken { get; set; } = "gdf_float_3f6e9a_token";

        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt")]
        public string HiddenToken { get; set; } = "gdf_hidden_4b8d22_token";

        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt")]
        public string InlineToken { get; set; } = "gdf_inline_9c2d1b_token";

        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt")]
        public string NoCopyToken { get; set; } = "gdf_no_copy_1a2b3c_token";

        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt")]
        public string NoRevealToken { get; set; } = "gdf_no_reveal_4d5e6f_token";

        [Display(Name = "FormBuilder_Field_ReadOnlyToken", Prompt = "FormBuilder_Field_ReadOnlyToken_Prompt")]
        public string ReadOnlyToken { get; set; } = "gdf_demo_readonly_token";

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MaxLength(64, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt")]
        public string RequiredToken { get; set; } = string.Empty;

        #endregion
    }

    public sealed class FormBuilderEnumRadioExampleModel
    {
        #region Static methods

        public static FormBuilderEnumRadioExampleModel CreateDemo()
        {
            return new FormBuilderEnumRadioExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(Name = "FormBuilder_Field_Visibility")]
        public FormBuilderSampleVisibility DisabledVisibility { get; set; } = FormBuilderSampleVisibility.Private;

        [Display(Name = "FormBuilder_Field_Visibility")]
        public FormBuilderSampleVisibility FloatingVisibility { get; set; } = FormBuilderSampleVisibility.Private;

        [Display(Name = "FormBuilder_Field_Visibility")]
        public FormBuilderSampleVisibility GroupVisibility { get; set; } = FormBuilderSampleVisibility.Internal;

        [Display(Name = "FormBuilder_Field_Visibility")]
        public FormBuilderSampleVisibility InlineVisibility { get; set; } = FormBuilderSampleVisibility.Public;

        [Display(Name = "FormBuilder_Field_Visibility")]
        public string ManualVisibility { get; set; } = nameof(FormBuilderSampleVisibility.Internal);

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Visibility")]
        public string RequiredVisibility { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Visibility")]
        public FormBuilderSampleVisibility Visibility { get; set; } = FormBuilderSampleVisibility.Internal;

        #endregion
    }

    public sealed class FormBuilderFlagExampleModel
    {
        #region Static methods

        public static FormBuilderFlagExampleModel CreateDemo()
        {
            return new FormBuilderFlagExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Display(Name = "FormBuilder_Field_Permissions")]
        public FormBuilderSamplePermissions DisabledPermissions { get; set; } = FormBuilderSamplePermissions.Read | FormBuilderSamplePermissions.Execute;

        [Display(Name = "FormBuilder_Field_PermissionsSelect")]
        public FormBuilderSamplePermissions DisabledPermissionsAsSelect { get; set; } = FormBuilderSamplePermissions.Write | FormBuilderSamplePermissions.Admin;

        [Display(Name = "FormBuilder_Field_Permissions")]
        public FormBuilderSamplePermissions FloatingPermissions { get; set; } = FormBuilderSamplePermissions.Read | FormBuilderSamplePermissions.Execute;

        [Display(Name = "FormBuilder_Field_Permissions")]
        public FormBuilderSamplePermissions GroupPermissions { get; set; } = FormBuilderSamplePermissions.Read | FormBuilderSamplePermissions.Admin;

        [Display(Name = "FormBuilder_Field_Permissions")]
        public FormBuilderSamplePermissions InlinePermissions { get; set; } = FormBuilderSamplePermissions.Write | FormBuilderSamplePermissions.Execute;

        [Display(Name = "FormBuilder_Field_Permissions")]
        public FormBuilderSamplePermissions Permissions { get; set; } = FormBuilderSamplePermissions.Read | FormBuilderSamplePermissions.Write;

        [Display(Name = "FormBuilder_Field_PermissionsSelect")]
        public FormBuilderSamplePermissions PermissionsAsSelect { get; set; } = FormBuilderSamplePermissions.Read | FormBuilderSamplePermissions.Admin;

        [Display(Name = "FormBuilder_Field_Permissions")]
        public FormBuilderSamplePermissions RequiredPermissions { get; set; } = FormBuilderSamplePermissions.None;

        #endregion
    }

    public sealed class FormBuilderCountryExampleModel
    {
        #region Constants

        private const string CountryCodeRegex = "^[A-Za-z]{2}$";

        #endregion

        #region Static methods

        public static FormBuilderCountryExampleModel CreateDemo()
        {
            return new FormBuilderCountryExampleModel();
        }

        #endregion

        #region Instance fields and properties

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Country", Prompt = "FormBuilder_Field_Country_Prompt")]
        public string Country { get; set; } = "FR";

        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Country", Prompt = "FormBuilder_Field_Country_Prompt")]
        public string DisabledCountry { get; set; } = "FR";

        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Country", Prompt = "FormBuilder_Field_Country_Prompt")]
        public string FloatingCountry { get; set; } = "FR";

        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Country", Prompt = "FormBuilder_Field_Country_Prompt")]
        public string GroupCountry { get; set; } = "FR";

        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Country", Prompt = "FormBuilder_Field_Country_Prompt")]
        public string HiddenCountry { get; set; } = "FR";

        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Country", Prompt = "FormBuilder_Field_Country_Prompt")]
        public string InlineCountry { get; set; } = "FR";

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(Name = "FormBuilder_Field_Country", Prompt = "FormBuilder_Field_Country_Prompt")]
        public string? RequiredCountry { get; set; }

        #endregion
    }
}