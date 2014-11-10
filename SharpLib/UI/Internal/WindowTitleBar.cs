using OpenTK;
using OpenTK.Input;
using SharpLib2D.Objects;

namespace SharpLib2D.UI.Internal
{
    public class WindowTitleBar : Control
    {
        protected readonly Window Window;
        protected readonly WindowCloseButton Button;

        public WindowTitleBar( Window W )
        {
            this.Window = W;
            this.SetSize( 100, 23 );
            this.Button = new WindowCloseButton( this );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Button == MouseButton.Left )
                Canvas.Dragger.StartDragging( this );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindowTitleBar( this );
        }

        public override BoundingVolume BoundingVolume
        {
            get { return new BoundingRectangle( this.TopLeft, this.BottomRight + new Vector2( 0, this.Window.Height ) ); }
        }
    }
}
