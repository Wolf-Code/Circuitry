using SharpLib2D.Info;

namespace SharpLib2D.UI.Internal.Scrollbar
{
    public class ScrollbarButton : Button
    {
        public Directions.Direction Direction { private set; get; }

        public ScrollbarButton( Directions.Direction Direction )
        {
            this.Direction = Direction;
            SetSize( 15, 15 );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawScrollbarButton( this );
        }
    }
}
