
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI.Skin
{
    public abstract class Skin
    {
        public abstract void DrawPanel( Control P );
        public abstract void DrawButton( Button B );
        public abstract void DrawWindowTitleBar( WindowTitleBar B );
        public abstract void DrawWindow( Window W );
    }
}
