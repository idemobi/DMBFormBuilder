#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBComponentBuilder;
using DMBPageBuilder;

#endregion

namespace DMBFormBuilderLabs
{
    /// <summary>
    ///     Extension methods for text rendering: PB_ParagraphBuilder, TitleBuilder, BlockTitleBuilder, PB_SpanBuilder.
    /// </summary>
    public static class LabsTextExtensions
    {
        #region PB_ParagraphBuilder — text helpers

        /// <summary>
        ///     Muted text colour.
        /// </summary>
        public static PB_ParagraphBuilder AsMuted(this PB_ParagraphBuilder builder)
            => builder.AddClass("text-muted");

        /// <summary>
        ///     Lead paragraph style for hero subtitles.
        /// </summary>
        public static PB_ParagraphBuilder AsLead(this PB_ParagraphBuilder builder)
            => builder.AddClass("lead");

        /// <summary>
        ///     Small muted text — used for secondary descriptions.
        /// </summary>
        public static PB_ParagraphBuilder AsSmallMuted(this PB_ParagraphBuilder builder)
            => builder.AddClass("small text-muted");

        /// <summary>
        ///     Standard card description style: small text with bottom margin.
        /// </summary>
        public static PB_ParagraphBuilder AsCardDescription(this PB_ParagraphBuilder builder)
            => builder.AddClass("small mb-1");

        /// <summary>
        ///     Standard effect meta style: small muted italic text with no bottom margin.
        /// </summary>
        public static PB_ParagraphBuilder AsEffectMeta(this PB_ParagraphBuilder builder)
            => builder.AddClass("small text-muted mb-0 fst-italic");

        /// <summary>
        ///     Removes bottom margin — used in callouts and compact layouts.
        /// </summary>
        public static PB_ParagraphBuilder AsNoMargin(this PB_ParagraphBuilder builder)
            => builder.AddClass("mb-0");

        /// <summary>
        ///     Muted text with no bottom margin — combined to avoid chaining through AddClass.
        /// </summary>
        public static PB_ParagraphBuilder AsMutedNoMargin(this PB_ParagraphBuilder builder)
            => builder.AddClass("text-muted mb-0");

        /// <summary>
        ///     Small text size.
        /// </summary>
        public static PB_ParagraphBuilder AsSmall(this PB_ParagraphBuilder builder)
            => builder.AddClass("small");

        #endregion

        #region TitleBuilder — heading helpers

        /// <summary>
        ///     Semi-bold muted title with no bottom margin — sub-section style.
        /// </summary>
        public static TitleBuilder AsSubSectionTitle(this TitleBuilder builder)
            => builder.AddClass("fw-semibold text-muted mb-0");

        /// <summary>
        ///     Per-demo preview label: semi-bold, muted, small bottom margin.
        /// </summary>
        public static TitleBuilder AsPreviewLabel(this TitleBuilder builder)
            => builder.AddClass("fw-semibold text-muted mb-2");

        /// <summary>
        ///     Muted caption: normal weight, muted colour.
        /// </summary>
        public static TitleBuilder AsCaption(this TitleBuilder builder)
            => builder.AddClass("fw-normal text-muted");

        /// <summary>
        ///     Small muted caption: normal weight, small size, muted colour.
        /// </summary>
        public static TitleBuilder AsCaptionSmall(this TitleBuilder builder)
            => builder.AddClass("fw-normal small text-muted");

        /// <summary>
        ///     Caption with no bottom margin — fw-normal, muted, mb-0.
        /// </summary>
        public static TitleBuilder AsCaptionCompact(this TitleBuilder builder)
            => builder.AddClass("fw-normal text-muted mb-0");

        /// <summary>
        ///     Small caption with no bottom margin.
        /// </summary>
        public static TitleBuilder AsCaptionSmallCompact(this TitleBuilder builder)
            => builder.AddClass("fw-normal small text-muted mb-0");

        /// <summary>
        ///     Small caption with a small bottom margin — mb-2.
        /// </summary>
        public static TitleBuilder AsCaptionSmallMargin(this TitleBuilder builder)
            => builder.AddClass("fw-normal small text-muted mb-2");

        /// <summary>
        ///     First section heading on a documentation page — bottom margin only.
        /// </summary>
        public static TitleBuilder AsSectionHeading(this TitleBuilder builder)
            => builder.AddClass("mb-4");

        /// <summary>
        ///     Subsequent section heading — top margin to separate from previous section, bottom margin.
        /// </summary>
        public static TitleBuilder AsSubSectionHeading(this TitleBuilder builder)
            => builder.AddClass("mb-4 mt-5");

        /// <summary>
        ///     Follow-up heading with only top margin — e.g. "Key Benefits" after an intro block.
        /// </summary>
        public static TitleBuilder AsFollowUpTitle(this TitleBuilder builder)
            => builder.AddClass("mt-5");

        /// <summary>
        ///     Overrides heading size to h4.
        /// </summary>
        public static TitleBuilder AsH4(this TitleBuilder builder)
            => builder.AddClass("h4");

        /// <summary>
        ///     Overrides heading size to h5.
        /// </summary>
        public static TitleBuilder AsH5(this TitleBuilder builder)
            => builder.AddClass("h5");

        /// <summary>
        ///     Removes bottom margin — compact title inside a card or sidebar.
        /// </summary>
        public static TitleBuilder AsCompact(this TitleBuilder builder)
            => builder.AddClass("mb-0");

        /// <summary>
        ///     Bootstrap alert heading style — used as the title inside an info callout.
        /// </summary>
        public static TitleBuilder AsAlertHeading(this TitleBuilder builder)
            => builder.AddClass("alert-heading");

        /// <summary>
        ///     Bootstrap card-title style — used for sidebar card headers.
        /// </summary>
        public static TitleBuilder AsCardTitle(this TitleBuilder builder)
            => builder.AddClass("card-title");

        /// <summary>
        ///     Step title inside a pipeline or getting-started section — small top margin.
        /// </summary>
        public static TitleBuilder AsStepTitle(this TitleBuilder builder)
            => builder.AddClass("mt-3");

        /// <summary>
        ///     Primary text colour — used for effect family headings.
        /// </summary>
        public static TitleBuilder AsTextPrimary(this TitleBuilder builder)
            => builder.AddClass("text-primary");

        /// <summary>
        ///     Success text colour — used for effect family headings.
        /// </summary>
        public static TitleBuilder AsTextSuccess(this TitleBuilder builder)
            => builder.AddClass("text-success");

        #endregion

        #region BlockTitleBuilder — heading helpers

        /// <summary>
        ///     Section heading — bottom margin.
        /// </summary>
        public static BlockTitleBuilder AsSectionHeading(this BlockTitleBuilder builder)
            => builder.AddClass("mb-4");

        /// <summary>
        ///     Spacing between loop items — small bottom margin.
        /// </summary>
        public static BlockTitleBuilder AsItemSpaced(this BlockTitleBuilder builder)
            => builder.AddClass("mb-3");

        #endregion

        #region PB_SpanBuilder — span helpers

        /// <summary>
        ///     Standard card overlay label: white, semi-bold, small text.
        /// </summary>
        public static PB_SpanBuilder AsCardLabel(this PB_SpanBuilder builder)
            => builder.AddClass("text-white fw-semibold small");

        /// <summary>
        ///     Monospace font — for inline code snippets.
        /// </summary>
        public static PB_SpanBuilder AsMonospace(this PB_SpanBuilder builder)
            => builder.AddClass("font-monospace");

        /// <summary>
        ///     Monospace font at small size — for compact inline code.
        /// </summary>
        public static PB_SpanBuilder AsMonospaceSmall(this PB_SpanBuilder builder)
            => builder.AddClass("font-monospace small");

        /// <summary>
        ///     Block-level code preview — secondary background, padding, rounded, small monospace font.
        /// </summary>
        public static PB_SpanBuilder AsCodePreview(this PB_SpanBuilder builder)
            => builder.AddClass("bg-body-secondary p-2 rounded d-block small font-monospace");

        /// <summary>
        ///     Scrollable block-level code preview — same as AsCodePreview with horizontal scroll.
        /// </summary>
        public static PB_SpanBuilder AsCodePreviewScrollable(this PB_SpanBuilder builder)
            => builder.AddClass("bg-body-secondary p-2 rounded d-block small font-monospace overflow-x-auto");

        /// <summary>
        ///     Bold text span.
        /// </summary>
        public static PB_SpanBuilder AsBold(this PB_SpanBuilder builder)
            => builder.AddClass("fw-bold");

        /// <summary>
        ///     Semi-bold text span.
        /// </summary>
        public static PB_SpanBuilder AsSemiBold(this PB_SpanBuilder builder)
            => builder.AddClass("fw-semibold");

        /// <summary>
        ///     Italic text span.
        /// </summary>
        public static PB_SpanBuilder AsItalic(this PB_SpanBuilder builder)
            => builder.AddClass("fst-italic");

        /// <summary>
        ///     White semi-bold label — for mock-up layer text.
        /// </summary>
        public static PB_SpanBuilder AsWhiteLabel(this PB_SpanBuilder builder)
            => builder.AddClass("text-white fw-semibold");

        /// <summary>
        ///     Hero display text — large, bold, body colour, tight line-height.
        /// </summary>
        public static PB_SpanBuilder AsDisplayHero(this PB_SpanBuilder builder)
            => builder.AddClass("display-1 fw-bold text-body lh-1");

        #endregion
    }
}