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
public sealed class TextFieldBuilderTests
{
    [Test]
    public void SetKindUnixAddsCleanerPatternAndNumericProfile()
    {
        IHtmlHelper html = TestHtmlHelperFactory.Create();
        TextFieldBuilder builder = new TextFieldBuilder(new StringWriter(), html)
            .SetInput("Slug", "Slug")
            .SetKind(TextFieldInputKind.Unix);

        string markup = builder.ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(markup, Does.Contain("pattern=\"[a-zA-Z0-9_]*\""));
            Assert.That(markup, Does.Contain("data-formbuilder-cleaner=\"unix\""));
            Assert.That(markup, Does.Contain("data-val-regex-pattern=\"[a-zA-Z0-9_]*\""));
        });
    }

    [Test]
    public void ToHtmlStringRendersInputValidationAndConstraintBadges()
    {
        IHtmlHelper html = TestHtmlHelperFactory.Create();
        TextFieldBuilder builder = new TextFieldBuilder(new StringWriter(), html)
            .SetInput("UserName", "UserName")
            .SetLabel("User name")
            .SetPlaceholder("Choose a user name")
            .SetValue("alice")
            .SetRequired(message: "User name is required.")
            .SetMaxLength(12, "User name is too long.")
            .SetMinLength(3, "User name is too short.")
            .SetPattern("[a-z]+", "Only lower-case letters.")
            .SetInputAttribute("data-test", "field")
            .SetInputAttribute("id", "IgnoredId");

        string markup = builder.ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(markup, Does.Contain("<label class=\"form-label\" for=\"UserName\">User name"));
            Assert.That(markup, Does.Contain("type=\"text\""));
            Assert.That(markup, Does.Contain("id=\"UserName\""));
            Assert.That(markup, Does.Contain("name=\"UserName\""));
            Assert.That(markup, Does.Contain("placeholder=\"Choose a user name\""));
            Assert.That(markup, Does.Contain("value=\"alice\""));
            Assert.That(markup, Does.Contain("required=\"required\""));
            Assert.That(markup, Does.Contain("data-val-required=\"User name is required.\""));
            Assert.That(markup, Does.Contain("maxlength=\"12\""));
            Assert.That(markup, Does.Contain("minlength=\"3\""));
            Assert.That(markup, Does.Contain("pattern=\"[a-z]&#x2B;\""));
            Assert.That(markup, Does.Contain("data-val-regex-pattern=\"[a-z]&#x2B;\""));
            Assert.That(markup, Does.Contain("data-test=\"field\""));
            Assert.That(markup, Does.Not.Contain("IgnoredId"));
            Assert.That(markup, Does.Contain("data-dmb-counter-max=\"12\""));
        });
    }
}