using OpenTK;
using OpenTK.Input;
using SharpLib2D.Info;
using SharpLib2D.Objects;

namespace SharpLib2D.UI.Internal
{
    public class WindowTitleBar : Control
    {
        protected readonly Window Window;
        public readonly WindowCloseButton Button;
        protected readonly Label Label;
        public bool Draggable { set; get; }

        public WindowTitleBar( string Title, Window W ) : base( null )
        {
            Window = W;
            Button = new WindowCloseButton( this );
            Label = new Label( this );
            Label.SetParent( this );
            Label.SetText( Title );
            Label.VerticalAlignment = Directions.VerticalAlignment.Center;
            SetSize( W.Width, 23 );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            Button.SetPosition( Width - Button.Width - 4, 1 );
            Label.SetSize( Button.TopLeft.X, Height );

            base.OnResize( OldSize, NewSize );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Button == MouseButton.Left && Draggable )
                Canvas.Dragger.StartDragging( this );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindowTitleBar( this );
        }

        public override BoundingVolume BoundingVolume
        {
            get { return new BoundingRectangle( TopLeft, TopLeft + new Vector2( Size.X, Size.Y + Window.Height ) ); }
        }
    }
}
