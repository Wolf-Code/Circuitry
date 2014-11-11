
using SharpLib2D.UI.Internal;
using SharpLib2D.UI.Internal.Scrollbar;

namespace SharpLib2D.UI.Skin
{
    public abstract class Skin
    {
        public abstract void DrawPanel( Control P );
        public abstract void DrawButton( Button B );
        public abstract void DrawWindowTitleBar( WindowTitleBar B );
        public abstract void DrawWindowCloseButton( WindowCloseButton B );
        public abstract void DrawWindow( Window W );
        public abstract void DrawCheckbox( Checkbox C );
        public abstract void DrawScrollbarButton( ScrollbarButton B );
        public abstract void DrawScrollbarBarContainer( ScrollbarBar B );
        public abstract void DrawScrollbarBar( ScrollbarBarDragger D );
    }
}
