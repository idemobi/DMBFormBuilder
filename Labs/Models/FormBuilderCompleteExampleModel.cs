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
    public enum FormBuilderCompleteExampleKind
    {
        Normal,
        Floating,
        Inline,
        Grouped,
        Locked
    }

    public sealed class FormBuilderCompleteExamplePageModel
    {
        #region Instance fields and properties

        public FormBuilderCompleteExampleModel Form { get; set; } = FormBuilderCompleteExampleModel.CreateDemo();
        public FormBuilderCompleteExampleKind Kind { get; set; }

        public string PartialName => Kind switch
        {
            FormBuilderCompleteExampleKind.Floating => "Examples/Form/Complete/_B002FloatingLabelForm",
            FormBuilderCompleteExampleKind.Inline => "Examples/Form/Complete/_B003InlineLabelForm",
            FormBuilderCompleteExampleKind.Grouped => "Examples/Form/Complete/_B004GroupedLabelForm",
            FormBuilderCompleteExampleKind.Locked => "Examples/Form/Complete/_B005LockedFieldForm",
            _ => "Examples/Form/Complete/_B001NormalLabelForm"
        };

        public bool ShowSummary { get; set; }

        public string Title => Kind switch
        {
            FormBuilderCompleteExampleKind.Floating => "Floating label",
            FormBuilderCompleteExampleKind.Inline => "Inline label + field",
            FormBuilderCompleteExampleKind.Grouped => "Grouped label + field",
            FormBuilderCompleteExampleKind.Locked => "Locked field",
            _ => "Normal label + field"
        };

        #endregion
    }

    public sealed class FormBuilderCompleteExampleModel
    {
        #region Constants

        private const string CountryCodeRegex = "^[A-Za-z]{2}$";

        #endregion

        #region Static methods

        public static FormBuilderCompleteExampleModel CreateDemo()
        {
            return new FormBuilderCompleteExampleModel
            {
                FirstName = "Jean",
                Email = "jean@example.com",
                Password = "ExamplePass42",
                Message = "This complete example validates a full FormBuilder journey.",
                Priority = FormBuilderSamplePriority.Medium,
                Visibility = FormBuilderSampleVisibility.Internal,
                Permissions = FormBuilderSamplePermissions.Read | FormBuilderSamplePermissions.Write,
                ApiToken = "gdf_demo_full_form_token",
                DueDate = DateTime.Today.AddDays(7),
                DueDateTime = DateTime.Today.AddDays(7).AddHours(10),
                ReminderTime = new TimeSpan(9, 30, 0),
                Color = "#2f6fed",
                Completion = 60,
                Country = "FR",
                AcceptTerms = true
            };
        }

        #endregion

        #region Instance fields and properties

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_AcceptTerms), Description = "Boolean fields render the same information popover beside the check label.")]
        public bool AcceptTerms { get; set; }

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MaxLength(64, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(Name = "FormBuilder_Field_ApiToken", Prompt = "FormBuilder_Field_ApiToken_Prompt", Description = "Token actions stay next to the input while the description remains on the label.")]
        public string ApiToken { get; set; } = string.Empty;

        [Display(Name = "Captcha", Description = "Captcha labels can also expose DisplayAttribute descriptions.")]
        public string CaptchaValue { get; set; } = string.Empty;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Color), Description = "Pick the accent color used by the example item.")]
        public string Color { get; set; } = "#2f6fed";

        [Range(0, 100, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Range))]
        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Slider), Description = "Move the slider to update the completion percentage.")]
        public int Completion { get; set; } = 60;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [RegularExpression(CountryCodeRegex, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(
            Name = "FormBuilder_Field_Country",
            Prompt = "FormBuilder_Field_Country_Prompt",
            Description = "The quick action can fill the current country when available.")]
        public string Country { get; set; } = "FR";

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDate_Prompt),
            Description = "Select the target calendar day for this item.")]
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_DueDateTime_Prompt),
            Description = "Use local date and time when a precise reminder is needed.")]
        public DateTime DueDateTime { get; set; } = DateTime.Today.AddDays(7).AddHours(10);

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [EmailAddress(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_EmailInvalid))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Email),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Email_Prompt),
            Description = "Use a reachable address for account and notification messages.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FirstName),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_FirstName_Prompt),
            Description = "Shown on every complete form presentation as a DisplayAttribute description popover.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [StringLength(180, MinimumLength = 12, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MaxLength))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Message_Prompt),
            Description = "Add enough context for the review team to understand the request.")]
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [MinLength(12, ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_MinLength))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_PasswordNew_Prompt),
            Description = "Choose a long password; the strength meter reacts while typing.")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "FormBuilder_Field_Permissions", Description = "Permissions combine several flags into a single submitted value.")]
        public FormBuilderSamplePermissions Permissions { get; set; } = FormBuilderSamplePermissions.Read | FormBuilderSamplePermissions.Write;

        [Display(Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Priority), Description = "Priority controls how quickly the item should be handled.")]
        public FormBuilderSamplePriority Priority { get; set; } = FormBuilderSamplePriority.Medium;

        [Required(ErrorMessage = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Required))]
        [Display(
            Name = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Time),
            Prompt = nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Time_Prompt),
            Description = "The time input keeps the description available in all label layouts.")]
        public TimeSpan ReminderTime { get; set; } = new TimeSpan(9, 30, 0);

        [Display(Name = "FormBuilder_Field_Visibility", Description = "Visibility defines who can discover this item after saving.")]
        public FormBuilderSampleVisibility Visibility { get; set; } = FormBuilderSampleVisibility.Internal;

        #endregion
    }
}