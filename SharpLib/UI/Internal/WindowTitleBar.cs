using OpenTK;
using OpenTK.Input;
using SharpLib2D.Objects;

namespace SharpLib2D.UI.Internal
{
    public class WindowTitleBar : Control
    {
        protected readonly Window Window;
        protected readonly WindowCloseButton Button;
        protected readonly Label Label;

        public WindowTitleBar( string Title, Window W )
        {
            this.Window = W;
            this.Button = new WindowCloseButton( this );
            this.Label = new Label( );
            this.Label.SetParent( this );
            this.Label.SetText( Title );
            this.SetSize( 100, 23 );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            this.Button.SetPosition( this.Width - this.Button.Width - 4, 1 );
            this.Label.SetSize( this.Button.TopLeft.X, this.Height );

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
            get { return new BoundingRectangle( this.TopLeft, this.TopLeft + new Vector2( this.Size.X, this.Size.Y + this.Window.Height ) ); }
        }
    }
}
