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
public sealed class EmailFieldBuilderTests
{
    #region Nested type: EmailValidationModel

    private sealed class EmailValidationModel
    {
        #region Instance fields and properties

        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "The '{0}' field is not a valid email address.")]
        public string Email { get; set; } = "not-an-email";

        #endregion
    }

    #endregion

    #region Instance methods

    [Test]
    public void EmailFieldBuilderForFormatsEmailAddressMessageWithFieldLabel()
    {
        IHtmlHelper<EmailValidationModel> html = TestHtmlHelperFactory.Create(new EmailValidationModel());

        string markup = html.EmailFieldBuilderFor(m => m.Email).ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(markup, Does.Contain("data-val-email=\"The &#x27;Email address&#x27; field is not a valid email address.\""));
            Assert.That(markup, Does.Not.Contain("{0}"));
        });
    }

    #endregion
}
