using SharpLib2D.UI;
using SharpLib2D.UI.Skin;

namespace SharpLib2D.States
{
    public class UIState : State
    {
        public Canvas Canvas { protected set; get; }

        protected override void OnStart( )
        {
            Canvas = new Canvas( new DefaultSkin( ) );
            Canvas.SetSize( Info.Screen.Size );
            base.OnStart( );
        }

        protected override void OnResize( )
        {
            Canvas.SetSize( Info.Screen.Size );
            base.OnResize( );
        }

        public override void Dispose( )
        {
            
        }
    }
}
