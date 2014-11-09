using OpenTK;
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI
{
    public class Window : Control
    {
        public WindowTitleBar TitleBar { private set; get; }

        public Window( Canvas Cnv )
        {
            this.TitleBar = new WindowTitleBar( this );
            this.TitleBar.SetParent( Cnv );
            this.SetParent( TitleBar );
            this.SetSize( 100, 100 );
            this.SetPosition( 0, 0 );
        }

        protected override Vector2 OnResize( Vector2 NewSize )
        {
            NewSize.Y -= this.TitleBar.Size.Y;
            if ( NewSize.Y < 0 )
                NewSize.Y = 0;

            TitleBar.SetSize( NewSize.X, TitleBar.Size.Y );

            return NewSize;
        }

        protected override Vector2 OnPositionChanged( Vector2 NewPosition )
        {
            this.TitleBar.SetPosition( NewPosition );

            return new Vector2( 0, this.TitleBar.Height );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindow( this );
        }
    }
}
