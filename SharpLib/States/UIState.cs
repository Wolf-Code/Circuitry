using SharpLib2D.UI;

namespace SharpLib2D.States
{
    public class UIState : State
    {
        public Canvas Canvas { protected set; get; }

        protected override void OnStart( )
        {
            Canvas = new Canvas( new DefaultSkin( ) );
            base.OnStart( );
        }
    }
}
