#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBFormBuilderLabs
{
    /// <summary>
    ///     BlockBuilder extensions for page-level layout: sections, sidebar, callouts, pillar cards, timelines.
    /// </summary>
    public static class LabsLayoutExtensions
    {
        #region Static methods

        #region SectionBuilder

        /// <summary>
        ///     Body background colour — used for spacer sections.
        /// </summary>
        public static SectionBuilder AsBodyBackground(this SectionBuilder builder)
            => builder.AddClass("bg-body");

        #endregion

        #endregion

        #region BlockBuilder — section regions

        /// <summary>
        ///     Centers text within a block.
        /// </summary>
        public static BlockBuilder AsTextCenter(this BlockBuilder builder)
            => builder.AddClass("text-center");

        /// <summary>
        ///     Standard section header layout: container-lg, horizontal padding, top/bottom spacing, flex row with space-between
        ///     alignment.
        /// </summary>
        public static BlockBuilder AsSectionHeader(this BlockBuilder builder)
            => builder.AddClass("container-lg px-4 pt-5 pb-3 d-flex justify-content-between align-items-start");

        /// <summary>
        ///     Standard section content layout: container-lg with horizontal padding and bottom spacing.
        /// </summary>
        public static BlockBuilder AsSectionContent(this BlockBuilder builder)
            => builder.AddClass("container-lg px-4 pb-5");

        /// <summary>
        ///     Section content block with top and bottom padding — variant of AsSectionContent including pt-4.
        /// </summary>
        public static BlockBuilder AsSectionContentTop(this BlockBuilder builder)
            => builder.AddClass("container-lg px-4 pt-4 pb-5");

        /// <summary>
        ///     Standard sub-label layout inside a section: container-lg with horizontal padding and bottom margin.
        /// </summary>
        public static BlockBuilder AsSectionSubLabel(this BlockBuilder builder, bool largeMargin = false)
            => builder.AddClass(largeMargin ? "container-lg px-4 mb-5" : "container-lg px-4 mb-3");

        #endregion

        #region BlockBuilder — sidebar

        /// <summary>
        ///     Sticky side panel with top offset matching the sticky navbar height.
        /// </summary>
        public static BlockBuilder AsStickyPanel(this BlockBuilder builder)
            => builder.AddClass("sticky-top").SetStyle("top", "130px");

        /// <summary>
        ///     Borderless card with shadow, rounded corners and hidden overflow — sidebar container.
        /// </summary>
        public static BlockBuilder AsSidebarCard(this BlockBuilder builder)
            => builder.AddClass("border-0 shadow-sm rounded-4 overflow-hidden");

        /// <summary>
        ///     Sidebar note block: top margin, padding, secondary background, rounded corners and shadow.
        /// </summary>
        public static BlockBuilder AsSidebarNote(this BlockBuilder builder)
            => builder.AddClass("mt-4 p-4 bg-body-secondary rounded-4 shadow-sm");

        /// <summary>
        ///     Horizontal separator row inside a sidebar list: top border and vertical padding.
        /// </summary>
        public static BlockBuilder AsSidebarRow(this BlockBuilder builder)
            => builder.AddClass("border-top py-2");

        /// <summary>
        ///     Section separator with top border and top padding — used inside Architecture sidebar tech stack.
        /// </summary>
        public static BlockBuilder AsBorderTopSection(this BlockBuilder builder)
            => builder.AddClass("border-top pt-3");

        /// <summary>
        ///     Small list item inside a sidebar section — bottom margin and small text size.
        /// </summary>
        public static BlockBuilder AsSidebarListItem(this BlockBuilder builder)
            => builder.AddClass("mb-2 small");

        #endregion

        #region BlockBuilder — documentation layout

        /// <summary>
        ///     Feature pillar card: padding, border, large rounded corners, full height, shadow.
        /// </summary>
        public static BlockBuilder AsPillarCard(this BlockBuilder builder)
            => builder.AddClass("p-3 border rounded-4 h-100 shadow-sm");

        /// <summary>
        ///     Architecture module card: border, large rounded corners, generous padding, full height.
        /// </summary>
        public static BlockBuilder AsArchCard(this BlockBuilder builder)
            => builder.AddClass("border rounded-4 p-4 h-100");

        /// <summary>
        ///     Benefit row: bottom margin, flex row with top-aligned items.
        /// </summary>
        public static BlockBuilder AsBenefitRow(this BlockBuilder builder)
            => builder.AddClass("mb-3 d-flex align-items-start");

        /// <summary>
        ///     Timeline step: relative positioning, left padding, left border (primary colour), bottom margin.
        /// </summary>
        public static BlockBuilder AsTimelineStep(this BlockBuilder builder)
            => builder.AddClass("position-relative ps-5 border-start border-2 border-primary mb-5");

        /// <summary>
        ///     Circular badge anchored to the left edge of a timeline step.
        /// </summary>
        public static BlockBuilder AsTimelineStepBadge(this BlockBuilder builder, string bgClass = "bg-primary")
            => builder
                .AddClass($"position-absolute top-0 start-0 translate-middle-x {bgClass} text-white rounded-circle d-flex align-items-center justify-content-center")
                .SetStyle("width", "40px")
                .SetStyle("height", "40px")
                .SetStyle("left", "-1px");

        /// <summary>
        ///     Info callout block — Bootstrap alert-info styling with rounded corners and shadow.
        /// </summary>
        public static BlockBuilder AsInfoCallout(this BlockBuilder builder)
            => builder.AddClass("alert alert-info border-0 shadow-sm rounded-4 p-4 my-4");

        /// <summary>
        ///     Icon column inside a callout — right margin to separate from text.
        /// </summary>
        public static BlockBuilder AsCalloutIcon(this BlockBuilder builder)
            => builder.AddClass("me-3");

        #endregion

        #region BlockBuilder — spacing utilities

        /// <summary>
        ///     Top margin spacer — mt-3, used for code preview wrappers and step spacers.
        /// </summary>
        public static BlockBuilder AsTopMargin(this BlockBuilder builder)
            => builder.AddClass("mt-3");

        /// <summary>
        ///     Small bottom margin — mb-2, used for provider list items.
        /// </summary>
        public static BlockBuilder AsBottomMarginSmall(this BlockBuilder builder)
            => builder.AddClass("mb-2");

        /// <summary>
        ///     Small text block.
        /// </summary>
        public static BlockBuilder AsSmallBlock(this BlockBuilder builder)
            => builder.AddClass("small");

        /// <summary>
        ///     Overflow-hidden marquee wrapper with generous vertical padding.
        /// </summary>
        public static BlockBuilder AsCarouselWrapper(this BlockBuilder builder)
            => builder.AddClass("py-5 overflow-hidden");

        /// <summary>
        /// Full-height flex column with centred content — for StagedReveal center zones and similar overlays.
        /// Uses gap-3 when <paramref name="largeGap"/> is true, gap-2 otherwise.
        /// </summary>
        public static BlockBuilder AsFlexCenterColumn(this BlockBuilder builder, bool largeGap = false)
            => builder
                .AddClass($"d-flex flex-column align-items-center justify-content-center {(largeGap ? "gap-3" : "gap-2")} text-center")
                .SetStyle("height", "100%");

        #endregion

        #region RowBuilder — grid layout

        /// <summary>
        ///     Pillar card grid — 4-unit gap with top margin, used in "Three Pillars" sections.
        /// </summary>
        public static RowBuilder AsPillarGrid(this RowBuilder builder)
            => builder.AddClass("g-4 mt-2");

        /// <summary>
        ///     Architecture card grid — 4-unit gap with vertical margin.
        /// </summary>
        public static RowBuilder AsArchGrid(this RowBuilder builder)
            => builder.AddClass("g-4 my-4");

        /// <summary>
        ///     Effect teaser grid — 3-unit gap with bottom margin.
        /// </summary>
        public static RowBuilder AsEffectGrid(this RowBuilder builder)
            => builder.AddClass("g-3 mb-4");

        /// <summary>
        ///     Row with bottom padding — used in full-page index layouts.
        /// </summary>
        public static RowBuilder AsBottomPadded(this RowBuilder builder)
            => builder.AddClass("pb-5");

        #endregion
    }
}