
using OpenTK;

namespace SharpLib2D.UI.Internal
{
    public class WindowCloseButton : Button
    {
        public WindowTitleBar TitleBar { private set; get; }

        public WindowCloseButton( WindowTitleBar Bar ) : base( "" )
        {
            this.TitleBar = Bar;
            this.SetParent( this.TitleBar );
            this.Label.Remove( );
            this.Label = null;
            this.SetSize( 21, 17 );
            Bar.OnSizeChanged += ( Sender, Control ) => this.SetPosition( Control.Width - this.Width - 4, 1 );
            this.OnClick += ( Sender, Control ) => this.TitleBar.Remove( );
        }

        protected override void OnResize( Vector2 NewSize )
        {
            
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindowCloseButton( this );
        }
    }
}
