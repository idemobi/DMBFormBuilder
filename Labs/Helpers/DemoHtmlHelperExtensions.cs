#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.IO;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace DMBFormBuilderLabs
{
    public static class DemoHtmlHelperExtensions
    {
        #region Constants

        private const string ExamplesPrefix = "Examples/";
        private const string MissingRawCodeMessage = "No raw code - run the PreBuilding raw examples generator.";
        private const string RawCodePrefix = "<div class=\"dmb-demo-code border rounded-3 overflow-hidden bg-body\"><div class=\"dmb-demo-code-header px-3 py-2 border-bottom bg-body-tertiary d-flex align-items-center\"><span class=\"badge text-bg-secondary font-monospace\">C#</span></div><pre class=\"language-csharp mb-0 border-0 rounded-0\"><code class=\"language-csharp\">";
        private const string RawCodeSuffix = "</code></pre></div>";
        private const string RawExamplesPrefix = "Examples_Raw/";

        #endregion

        #region Static methods

        private static IHtmlContent BuildRawCodeContent(string code)
        {
            return new HtmlString(RawCodePrefix + HtmlEncoder.Default.Encode(code) + RawCodeSuffix);
        }

        private static string ExtractClipboardCode(string rawContent)
        {
            string content = rawContent.TrimStart('\uFEFF');

            if (content.StartsWith(RawCodePrefix, StringComparison.Ordinal)
                && content.EndsWith(RawCodeSuffix, StringComparison.Ordinal))
            {
                return WebUtility.HtmlDecode(content[RawCodePrefix.Length..^RawCodeSuffix.Length]);
            }

            return WebUtility.HtmlDecode(content);
        }

        private static string GetRawPartialViewName(string partialViewName)
        {
            if (!partialViewName.StartsWith(ExamplesPrefix, StringComparison.Ordinal))
            {
                throw new ArgumentException(
                    $"Example partials must start with '{ExamplesPrefix}'.",
                    nameof(partialViewName));
            }

            return RawExamplesPrefix + partialViewName[ExamplesPrefix.Length..] + "_Raw";
        }

        public static async Task<IHtmlContent> RenderExamplePartialAsync(
            this IHtmlHelper htmlHelper,
            [AspMvcView] string partialViewName,
            string title = "Example",
            IconStruct icon = new IconStruct(),
            object? model = null,
            bool previewFirst = true
        )
        {
            ArgumentNullException.ThrowIfNull(htmlHelper);

            if (string.IsNullOrWhiteSpace(partialViewName))
            {
                throw new ArgumentException("The partial view name cannot be null or empty.", nameof(partialViewName));
            }

            string rawPartialViewName = GetRawPartialViewName(partialViewName);
            string demoId = HtmlIdGenerator.GenerateUniqueId(htmlHelper, "example_");

            IHtmlContent previewContent = await htmlHelper.PartialAsync(partialViewName, model);
            string rawHtml = MissingRawCodeMessage;
            IHtmlContent rawContent = BuildRawCodeContent(MissingRawCodeMessage);
            try
            {
                rawContent = await htmlHelper.PartialAsync(rawPartialViewName, model);
                rawHtml = ExtractClipboardCode(RenderHtmlContent(rawContent));
            }
            catch (Exception ex)
            {
                ILogger? logger = htmlHelper.ViewContext.HttpContext.RequestServices
                    .GetService<ILoggerFactory>()
                    ?.CreateLogger(typeof(DemoHtmlHelperExtensions).FullName ?? nameof(DemoHtmlHelperExtensions));

                logger?.LogWarning(
                    ex,
                    "Unable to render raw example partial '{RawPartialViewName}' for '{PartialViewName}'.",
                    rawPartialViewName,
                    partialViewName);
            }

            PageInformation page = PageRegistry.GetOrCreatePageInformation(htmlHelper.ViewContext.HttpContext);
            page.SetScriptFile("https://cdn.jsdelivr.net/npm/prismjs");
            page.SetScriptFile("https://cdn.jsdelivr.net/npm/prismjs/plugins/autoloader/prism-autoloader.min.js");
            page.SetStylesheet("https://cdn.jsdelivr.net/npm/prismjs/themes/prism.css");

            using StringWriter writer = new();
            writer.WriteLine($"""<div class="dmb-demo-partial border rounded-3 overflow-hidden mb-4" id="{demoId}"><div class="dmb-demo-partial-header px-3 py-2 border-bottom d-flex align-items-center">""");
            TitleBuilder Title = new TitleBuilder(writer, htmlHelper).SetTitle(title, TitleLevel.Four).SetIcon(icon);
            writer.WriteLine(Title.Render());
            ClipboardActionItem copyButton = new ClipboardActionItem().SetVariant(VariantStyle.Secondary).WithClipboardStartIcon(IconStruct.BootstrapEnum(BootStrapEnum.bi_clipboard)).WithClipboardEndIcon(IconStruct.BootstrapEnum(BootStrapEnum.bi_clipboard_check_fill)).WithClipboardValue(rawHtml);
            writer.WriteLine($"""<div class="dmb-demo-partial-toolbar d-flex ms-auto btn-group">""");
            writer.WriteLine($"""<button type="button" class="btn btn-sm {(previewFirst ? "btn-primary" : "btn-outline-primary")}" data-demo-target="{demoId}" data-demo-panel="preview"><span class="bi bi-eye"></span></button><button type="button" class="btn btn-sm {(!previewFirst ? "btn-primary" : "btn-outline-primary")}" data-demo-target="{demoId}" data-demo-panel="code"><span class="bi bi-code"></span></button>""");
            ButtonRender button = new ButtonRender(writer, htmlHelper, copyButton);
            writer.WriteLine(button.Render());
            writer.WriteLine($"""</div></div>""");
            writer.WriteLine($"""<div class="dmb-demo-partial-body"><div class="dmb-demo-panel {(previewFirst ? string.Empty : "d-none")}" data-demo-panel-content="preview"><div class="p-3">""");
            previewContent.WriteTo(writer, HtmlEncoder.Default);
            writer.WriteLine($"""</div></div><div class="dmb-demo-panel {(!previewFirst ? string.Empty : "d-none")}" data-demo-panel-content="code"><div class="p-3">""");
            rawContent.WriteTo(writer, HtmlEncoder.Default);
            writer.WriteLine($"""</div></div></div></div>""");
            return new HtmlString(writer.ToString());
        }

        private static string RenderHtmlContent(IHtmlContent content)
        {
            using var writer = new StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        #endregion
    }
}