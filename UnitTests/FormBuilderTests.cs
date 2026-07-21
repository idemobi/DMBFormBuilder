#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBFormBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;
using NUnit.Framework;

#endregion

namespace DMBFormBuilderUnitTest;

[TestFixture]
public sealed class FormBuilderTests
{
    [Test]
    public void BuildAttributesContainsConfiguredFormMetadata()
    {
        IHtmlHelper html = TestHtmlHelperFactory.Create();
        FormBuilder builder = new FormBuilder(new StringWriter(), html)
            .SetAction("/account/save")
            .SetMethod(FormSubmissionMethod.Get)
            .SetValidationMode(FormValidationMode.Client)
            .SetMultipart()
            .DisableBrowserValidation()
            .EnableSubmitWhenValid()
            .EnableSubmitWhenChanged()
            .SetAriaLabel("Account form");

        string attributes = builder.BuildAttributes();

        Assert.Multiple(() =>
        {
            Assert.That(builder.ValidationMode, Is.EqualTo(FormValidationMode.Client));
            Assert.That(attributes, Does.Contain("dmb-form-builder"));
            Assert.That(attributes, Does.Contain("needs-validation"));
            Assert.That(attributes, Does.Contain("method=\"get\""));
            Assert.That(attributes, Does.Contain("action=\"/account/save\""));
            Assert.That(attributes, Does.Contain("enctype=\"multipart/form-data\""));
            Assert.That(attributes, Does.Contain("novalidate"));
            Assert.That(attributes, Does.Contain("aria-label=\"Account form\""));
            Assert.That(attributes, Does.Contain("data-form-builder=\"true\""));
            Assert.That(attributes, Does.Contain("data-validation-mode=\"client\""));
            Assert.That(attributes, Does.Contain("data-submit-when-valid=\"true\""));
            Assert.That(attributes, Does.Contain("data-submit-when-changed=\"true\""));
        });
    }
}
