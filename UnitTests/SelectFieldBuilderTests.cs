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
public sealed class SelectFieldBuilderTests
{
    [Test]
    public void SelectOptionStoresDisabledAndHiddenFlags()
    {
        SelectFieldBuilder.SelectOption option = new("value", "Text", Disabled: true, Hidden: true);

        Assert.Multiple(() =>
        {
            Assert.That(option.Value, Is.EqualTo("value"));
            Assert.That(option.Text, Is.EqualTo("Text"));
            Assert.That(option.Disabled, Is.True);
            Assert.That(option.Hidden, Is.True);
        });
    }

    [Test]
    public void ToHtmlStringRendersPlaceholderSelectionAndQuickAction()
    {
        IHtmlHelper html = TestHtmlHelperFactory.Create();
        SelectFieldBuilder builder = new SelectFieldBuilder(new StringWriter(), html)
            .SetInput("Country", "Country")
            .SetLabel("Country")
            .AddPlaceholderOption("Choose...", hidden: true)
            .AddOption("FR", "France")
            .AddOption("US", "United States")
            .SetValue("FR")
            .SetRequired(message: "Country is required.")
            .SetQuickSelectAction("FR", title: "Use current country")
            .SetInputAttribute("data-test", "select")
            .SetInputAttribute("name", "IgnoredName");

        string markup = builder.ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(markup, Does.Contain("<label class=\"form-label\" for=\"Country\">Country"));
            Assert.That(markup, Does.Contain("<select class=\"form-select\" id=\"Country\" name=\"Country\""));
            Assert.That(markup, Does.Contain("<option value=\"\" disabled hidden>Choose...</option>"));
            Assert.That(markup, Does.Contain("<option value=\"FR\" selected>France</option>"));
            Assert.That(markup, Does.Contain("<option value=\"US\">United States</option>"));
            Assert.That(markup, Does.Contain("required=\"required\""));
            Assert.That(markup, Does.Contain("data-val-required=\"Country is required.\""));
            Assert.That(markup, Does.Contain("data-dmb-select-value-for=\"Country\""));
            Assert.That(markup, Does.Contain("data-dmb-select-value=\"FR\""));
            Assert.That(markup, Does.Contain("title=\"Use current country\""));
            Assert.That(markup, Does.Contain("data-test=\"select\""));
            Assert.That(markup, Does.Not.Contain("IgnoredName"));
        });
    }
}