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
    ///     BlockBuilder extensions for card structure, preview wrappers, and effect teaser cards.
    /// </summary>
    public static class LabsCardExtensions
    {
        #region BlockBuilder — inner content

        /// <summary>
        ///     Centered inner content padding — used inside effect demo wrappers.
        /// </summary>
        public static BlockBuilder AsInnerContentBlock(this BlockBuilder builder, bool largePadding = false)
            => builder.AddClass(largePadding ? "py-5 px-4 text-center" : "py-4 px-4 text-center");

        /// <summary>
        ///     Clips content overflow — standalone overflow-hidden without rounding.
        /// </summary>
        public static BlockBuilder AsOverflowHidden(this BlockBuilder builder)
            => builder.AddClass("overflow-hidden");

        #endregion

        #region BlockBuilder — preview wrappers

        /// <summary>
        ///     Rounded corners and hidden overflow — for demo/preview wrappers without shadow.
        /// </summary>
        public static BlockBuilder AsPreviewWrapper(this BlockBuilder builder)
            => builder.AddClass("rounded-3 overflow-hidden");

        /// <summary>
        ///     Preview wrapper with inner padding — rounded corners, overflow hidden, padding.
        /// </summary>
        public static BlockBuilder AsPreviewWrapperPadded(this BlockBuilder builder)
            => builder.AddClass("rounded-3 overflow-hidden p-2");

        /// <summary>
        ///     Standard card preview wrapper: rounded corners, hidden overflow, shadow, relative positioning.
        /// </summary>
        public static BlockBuilder AsCardPreview(this BlockBuilder builder)
            => builder.AddClass("rounded-3 overflow-hidden shadow-sm position-relative");

        #endregion

        #region BlockBuilder — card footer

        /// <summary>
        ///     Standard card overlay footer: absolute positioning at the bottom with a fade-to-black gradient background.
        /// </summary>
        public static BlockBuilder AsCardOverlayFooter(this BlockBuilder builder, int zIndex = 10)
            => builder
                .AddClass("position-absolute bottom-0 start-0 end-0 d-flex justify-content-between align-items-center px-3 py-2")
                .SetStyle("background", "linear-gradient(transparent,rgba(0,0,0,.72))")
                .SetStyle("z-index", zIndex.ToString());

        /// <summary>
        ///     Standard card footer row below a preview: top/horizontal padding, flex row with space-between alignment.
        /// </summary>
        public static BlockBuilder AsCardFooterRow(this BlockBuilder builder)
            => builder.AddClass("pt-2 px-1 d-flex justify-content-between align-items-start");

        #endregion

        #region BlockBuilder — padded blocks

        /// <summary>
        ///     Standard uniform padding block — p-4.
        /// </summary>
        public static BlockBuilder AsPaddedBlock(this BlockBuilder builder)
            => builder.AddClass("p-4");

        /// <summary>
        ///     Centered padded block — p-4 text-center. Used in pipeline sidebar highlights.
        /// </summary>
        public static BlockBuilder AsCenteredPaddedBlock(this BlockBuilder builder)
            => builder.AddClass("p-4 text-center");

        /// <summary>
        ///     Example preview wrapper for Index pages — border, rounded corners, padding, bottom margin.
        /// </summary>
        public static BlockBuilder AsExamplePreview(this BlockBuilder builder)
            => builder.AddClass("border rounded-3 p-4 mb-3");

        /// <summary>
        ///     Effect teaser card — full height, borderless, secondary background, rounded, padded.
        /// </summary>
        public static BlockBuilder AsEffectTeaserCard(this BlockBuilder builder)
            => builder.AddClass("h-100 border-0 shadow-sm bg-body-secondary rounded-3 p-3");

        #endregion
    }
}