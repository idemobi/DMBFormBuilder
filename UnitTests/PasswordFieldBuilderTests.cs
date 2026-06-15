#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.ComponentModel.DataAnnotations;
using DMBFormBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;
using NUnit.Framework;

#endregion

namespace DMBFormBuilderUnitTest;

[TestFixture]
public sealed class PasswordFieldBuilderTests
{
    #region Nested type: PasswordValidationModel

    private sealed class PasswordValidationModel
    {
        #region Instance fields and properties

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The '{0}' field is required.")]
        [MinLength(12, ErrorMessage = "The '{0}' field must be at least {1} characters long.")]
        [MaxLength(128, ErrorMessage = "The '{0}' field must be at most {1} characters long.")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[-_+!?=]).+$", ErrorMessage = "The '{0}' field must have one uppercase, one lowercase, one number and one special caracter (-_+!?=)")]
        public string Password { get; set; } = "weak";

        [Display(Name = "Password confirmation")]
        [Compare(nameof(Password), ErrorMessage = "The '{0}' field and the '{1}' field are different!")]
        public string PasswordConfirm { get; set; } = "other";

        #endregion
    }

    #endregion

    #region Instance methods

    [Test]
    public void PasswordFieldBuilderForFormatsValidationMessagesWithFieldLabel()
    {
        IHtmlHelper<PasswordValidationModel> html = TestHtmlHelperFactory.Create(new PasswordValidationModel());

        string markup = html.PasswordFieldBuilderFor(m => m.Password).ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(markup, Does.Contain("data-val-required=\"The &#x27;Password&#x27; field is required.\""));
            Assert.That(markup, Does.Contain("data-val-minlength=\"The &#x27;Password&#x27; field must be at least 12 characters long.\""));
            Assert.That(markup, Does.Contain("data-val-maxlength=\"The &#x27;Password&#x27; field must be at most 128 characters long.\""));
            Assert.That(markup, Does.Contain("data-val-regex=\"The &#x27;Password&#x27; field must have one uppercase, one lowercase, one number and one special caracter (-_&#x2B;!?=)\""));
            Assert.That(markup, Does.Not.Contain("{0}"));
            Assert.That(markup, Does.Not.Contain("{1}"));
        });
    }

    [Test]
    public void PasswordFieldBuilderForFormatsCompareMessageWithBothFieldLabels()
    {
        IHtmlHelper<PasswordValidationModel> html = TestHtmlHelperFactory.Create(new PasswordValidationModel());

        string markup = html.PasswordFieldBuilderFor(m => m.PasswordConfirm).ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(markup, Does.Contain("data-val-equalto=\"The &#x27;Password confirmation&#x27; field and the &#x27;Password&#x27; field are different!\""));
            Assert.That(markup, Does.Not.Contain("{0}"));
            Assert.That(markup, Does.Not.Contain("{1}"));
        });
    }

    #endregion
}
