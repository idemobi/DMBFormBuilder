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
internal sealed class ValidationAttributeTests
{
    [Test]
    public void BoolMustBeFalseAttributeAcceptsOnlyFalseBoolean()
    {
        BoolMustBeFalseAttribute attribute = new BoolMustBeFalseAttribute();

        Assert.Multiple(() =>
        {
            Assert.That(attribute.IsValid(false), Is.True);
            Assert.That(attribute.IsValid(true), Is.False);
            Assert.That(attribute.IsValid(null), Is.False);
            Assert.That(attribute.IsValid("false"), Is.False);
        });
    }

    [Test]
    public void BoolMustBeTrueAttributeAcceptsOnlyTrueBoolean()
    {
        BoolMustBeTrueAttribute attribute = new BoolMustBeTrueAttribute();

        Assert.Multiple(() =>
        {
            Assert.That(attribute.IsValid(true), Is.True);
            Assert.That(attribute.IsValid(false), Is.False);
            Assert.That(attribute.IsValid(null), Is.False);
            Assert.That(attribute.IsValid("true"), Is.False);
        });
    }
}