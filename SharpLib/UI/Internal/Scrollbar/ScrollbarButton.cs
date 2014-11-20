using SharpLib2D.Info;

namespace SharpLib2D.UI.Internal.Scrollbar
{
    public class ScrollbarButton : Button
    {
        public Directions.Direction Direction { private set; get; }

        public ScrollbarButton( Scrollbar Bar, Directions.Direction Direction ) : base( Bar )
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
