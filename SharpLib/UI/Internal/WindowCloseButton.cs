
namespace SharpLib2D.UI.Internal
{
    public class WindowCloseButton : Button
    {
        public WindowTitleBar TitleBar { private set; get; }

        public WindowCloseButton( WindowTitleBar Bar )
        {
            this.TitleBar = Bar;
            this.SetParent( this.TitleBar );
            this.SetSize( 21, 17 );
            this.OnClick += Control => this.TitleBar.Remove( );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindowCloseButton( this );
        }
    }
}
