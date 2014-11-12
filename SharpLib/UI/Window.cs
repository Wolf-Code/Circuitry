using OpenTK;
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI
{
    public class Window : Control
    {
        public WindowTitleBar TitleBar { private set; get; }

        public Window( string Title, Canvas Cnv )
        {
            this.TitleBar = new WindowTitleBar( Title, this );
            this.TitleBar.SetParent( Cnv );
            this.SetParent( TitleBar );
            this.SetSize( 100, 100 );
            this.SetPosition( 0, 0 );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            NewSize.Y -= this.TitleBar.Size.Y;
            if ( NewSize.Y < 0 )
                NewSize.Y = 0;

            TitleBar.SetSize( NewSize.X, TitleBar.Size.Y );

            m_Size.Y = NewSize.Y;
        }

        protected override void OnReposition( Vector2 OldPosition, Vector2 NewPosition )
        {
            this.TitleBar.SetPosition( NewPosition );

            m_Position = new Vector2( 0, this.TitleBar.Height );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindow( this );
        }
    }
}
