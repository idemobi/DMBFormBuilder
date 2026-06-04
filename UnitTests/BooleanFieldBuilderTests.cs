#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.IO;
using DMBFormBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;
using NUnit.Framework;

#endregion

namespace DMBFormBuilderUnitTest;

[TestFixture]
public sealed class BooleanFieldBuilderTests
{
    public sealed class BooleanAnnotationModel
    {
        #region Instance fields and properties

        [BoolMustBeTrue]
        public bool AcceptTerms { get; set; }

        [BoolMustBeFalse]
        public bool RefuseTracking { get; set; }

        #endregion
    }

    [Test]
    public void ToHtmlStringRendersCheckedSwitchAndHiddenFalseValue()
    {
        IHtmlHelper html = TestHtmlHelperFactory.Create();
        BooleanFieldBuilder builder = new BooleanFieldBuilder(new StringWriter(), html)
            .SetInput("AcceptTerms", "AcceptTerms")
            .SetLabel("Accept terms")
            .SetValue(true)
            .AsSwitch()
            .SetMustBeChecked("Terms must be accepted.")
            .SetInputAttribute("data-test", "boolean")
            .SetInputAttribute("value", "IgnoredValue");

        string markup = builder.ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(markup, Does.Contain("class=\"form-check form-switch\""));
            Assert.That(markup, Does.Contain("type=\"checkbox\""));
            Assert.That(markup, Does.Contain("id=\"AcceptTerms\""));
            Assert.That(markup, Does.Contain("name=\"AcceptTerms\""));
            Assert.That(markup, Does.Contain("value=\"true\""));
            Assert.That(markup, Does.Contain("checked"));
            Assert.That(markup, Does.Contain("data-val-bool-expected=\"true\""));
            Assert.That(markup, Does.Contain("data-val-bool=\"Terms must be accepted.\""));
            Assert.That(markup, Does.Contain("<input type=\"hidden\" name=\"AcceptTerms\" value=\"false\""));
            Assert.That(markup, Does.Contain("data-formbuilder-ignore-validation=\"true\""));
            Assert.That(markup, Does.Contain("data-test=\"boolean\""));
            Assert.That(markup, Does.Not.Contain("IgnoredValue"));
        });
    }

    [Test]
    public void CheckboxFieldBuilderForAppliesBooleanValidationAttributes()
    {
        IHtmlHelper<BooleanAnnotationModel> html = TestHtmlHelperFactory.Create(new BooleanAnnotationModel());

        string acceptMarkup = html.CheckboxFieldBuilderFor(model => model.AcceptTerms).ToHtmlString();
        string refuseMarkup = html.CheckboxFieldBuilderFor(model => model.RefuseTracking).ToHtmlString();

        Assert.Multiple(() =>
        {
            Assert.That(acceptMarkup, Does.Contain("data-val-bool-expected=\"true\""));
            Assert.That(acceptMarkup, Does.Contain("data-val-bool=\"The AcceptTerms field must be true.\""));
            Assert.That(refuseMarkup, Does.Contain("data-val-bool-expected=\"false\""));
            Assert.That(refuseMarkup, Does.Contain("data-val-bool=\"The RefuseTracking field must be false.\""));
        });
    }
}
