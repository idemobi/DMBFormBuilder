#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace DMBFormBuilderLabs.Models
{
    /// <summary>
    ///     Provides values for the FormBuilder submit-lock demonstration page.
    /// </summary>
    public sealed class FormBuilderSubmitLockExampleModel
    {
        #region Static methods

        /// <summary>
        ///     Creates a realistic initial model for the submit-lock demonstration.
        /// </summary>
        /// <returns>A populated <see cref="FormBuilderSubmitLockExampleModel" /> instance.</returns>
        public static FormBuilderSubmitLockExampleModel CreateDemo()
        {
            return new FormBuilderSubmitLockExampleModel
            {
                Title = "Notification preferences",
                Language = "fr",
                NotificationCenterEnabled = true,
                EmailEnabled = false,
                BrowserPushEnabled = true
            };
        }

        #endregion

        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets a value indicating whether browser push notifications are enabled.
        /// </summary>
        public bool BrowserPushEnabled { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether email notifications are enabled.
        /// </summary>
        public bool EmailEnabled { get; set; }

        /// <summary>
        ///     Gets or sets the language selected for notifications.
        /// </summary>
        [Required]
        [Display(Name = "Language")]
        public string Language { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets a value indicating whether notification-center delivery is enabled.
        /// </summary>
        public bool NotificationCenterEnabled { get; set; }

        /// <summary>
        ///     Gets or sets the preference set title.
        /// </summary>
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        #endregion
    }
}
