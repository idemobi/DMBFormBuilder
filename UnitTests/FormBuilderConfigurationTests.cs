#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBFormBuilder;
using NUnit.Framework;

#endregion

namespace DMBFormBuilderUnitTest;

[TestFixture]
public sealed class FormBuilderConfigurationTests
{
    [Test]
    public void DefaultConfigurationUsesNormalLabelsAndClientServerValidation()
    {
        FormBuilderConfiguration configuration = FormBuilderConfiguration.Default;

        Assert.Multiple(() =>
        {
            Assert.That(configuration.LabelPresentation, Is.EqualTo(FormLabelPresentation.Normal));
            Assert.That(configuration.ValidationMode, Is.EqualTo(FormValidationMode.ClientAndServer));
            Assert.That(configuration.ApiDescription(), Is.False);
            Assert.That(configuration.NeedsConfigFileOrAppSettings(), Is.False);
        });
    }
}