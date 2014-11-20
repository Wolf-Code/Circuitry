using SharpLib2D.UI.Internal;
using SharpLib2D.UI.Internal.Scrollbar;

namespace SharpLib2D.UI.Skin
{
    public abstract class Skin
    {
        /// <summary>
        /// Draws a <see cref="Panel"/>.
        /// </summary>
        /// <param name="P">The panel.</param>
        public abstract void DrawPanel( Control P );

        /// <summary>
        /// Draws a <see cref="Button"/>.
        /// </summary>
        /// <param name="B">The button.</param>
        public abstract void DrawButton( Button B );

        /// <summary>
        /// Draws a <see cref="WindowTitleBar"/>.
        /// </summary>
        /// <param name="B">The title bar.</param>
        public abstract void DrawWindowTitleBar( WindowTitleBar B );

        /// <summary>
        /// Draws a <see cref="WindowCloseButton"/>.
        /// </summary>
        /// <param name="B">The window close button.</param>
        public abstract void DrawWindowCloseButton( WindowCloseButton B );
        public abstract void DrawWindow( Window W );
        public abstract void DrawCheckbox( Checkbox C );
        public abstract void DrawScrollbarButton( ScrollbarButton B );
        public abstract void DrawScrollbarBarContainer( ScrollbarBar B );
        public abstract void DrawScrollbarBar( ScrollbarBarDragger D );
        public abstract void DrawCategoryHeader( CategoryHeader H );
        public abstract void DrawCategoryButton( CategoryHeader.CategoryButton B );
        public abstract void DrawSlider( Slider S );
        public abstract void DrawSliderGrip( Slider.SliderGrip G );
    }
}
