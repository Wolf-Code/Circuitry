
namespace SharpLib2D.UI.Internal
{
    public class WindowCloseButton : Button
    {
        public WindowTitleBar TitleBar { private set; get; }

        public WindowCloseButton( WindowTitleBar Bar )
        {
            TitleBar = Bar;
            SetParent( TitleBar );
            SetSize( 21, 17 );
            OnClick += Control => TitleBar.Remove( );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindowCloseButton( this );
        }
    }
}
