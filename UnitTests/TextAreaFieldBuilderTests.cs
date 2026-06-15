#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBFormBuilder;
using DMBFormBuilder.Resources;
using DMBServerHelper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using NUnit.Framework;

#endregion

namespace DMBFormBuilderUnitTest;

[TestFixture]
public sealed class TextAreaFieldBuilderTests
{
    private sealed class DictionaryStringLocalizer : IStringLocalizer
    {
        #region Instance fields and properties

        private readonly Dictionary<string, string> _values;

        #endregion

        #region Instance constructors and destructors

        public DictionaryStringLocalizer(Dictionary<string, string> values)
        {
            _values = values;
        }

        #endregion

        #region Instance methods

        #region From interface IStringLocalizer

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _values.Select(item => new LocalizedString(item.Key, item.Value, resourceNotFound: false));
        }

        #endregion

        #endregion

        #region Instance indexers

        public LocalizedString this[string name]
        {
            get
            {
                if (_values.TryGetValue(name, out string? value))
                {
                    return new LocalizedString(name, value, resourceNotFound: false);
                }

                return new LocalizedString(name, name, resourceNotFound: true);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];

        #endregion
    }

    [Test]
    public void SetNoHtmlUsesTranslatedBadgeText()
    {
        ICombinedStringLocalizer initialLocalizer = WebLocalizer.DataAnnotationLocalizer;
        CombinedStringLocalizer testLocalizer = new();
        testLocalizer.InjectResource("FormBuilderTextArea", new DictionaryStringLocalizer(new Dictionary<string, string>
        {
            [nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_NoHtmlInvalid)] = "No HTML not accepted.",
            [nameof(DMBFormBuilderDataAnnotationLocalization.FormBuilder_Field_Badge_NoHtml)] = "No HTML"
        }));

        WebLocalizer.DataAnnotationLocalizer = testLocalizer;

        try
        {
            IHtmlHelper html = TestHtmlHelperFactory.Create();
            TextAreaFieldBuilder builder = new TextAreaFieldBuilder(new StringWriter(), html)
                .SetInput("Message", "Message")
                .SetNoHtml();

            string markup = builder.ToHtmlString();

            Assert.That(markup, Does.Contain(">No HTML<"));
        }
        finally
        {
            WebLocalizer.DataAnnotationLocalizer = initialLocalizer;
        }
    }
}