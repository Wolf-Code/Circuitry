using OpenTK;
using OpenTK.Input;
using SharpLib2D.Objects;

namespace SharpLib2D.UI.Internal
{
    public class WindowTitleBar : Control
    {
        protected readonly Window Window;
        public readonly WindowCloseButton Button;
        protected readonly Label Label;

        public WindowTitleBar( string Title, Window W )
        {
            Window = W;
            Button = new WindowCloseButton( this );
            Label = new Label( );
            Label.SetParent( this );
            Label.SetText( Title );
            SetSize( 100, 23 );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            Button.SetPosition( Width - Button.Width - 4, 1 );
            Label.SetSize( Button.TopLeft.X, Height );

            base.OnResize( OldSize, NewSize );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Button == MouseButton.Left )
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
