using System;
using OpenTK;
using OpenTK.Input;
using Mouse = SharpLib2D.Info.Mouse;

namespace SharpLib2D.UI
{
    public class Button : Control
    {
        public string Text
        {
            get { return this.Label.Text; }
        }

        public bool IsDown { private set; get; }
        protected Vector2 DownPosition;
        protected Label Label;

        public event EventHandler<Button> OnClick;

        public Button( string Text )
        {
            this.Label = new Label( );
            this.Label.SetParent( this );
            this.SetText( Text );
        }

        public void SetText( string NewText )
        {
            this.Label.SetText( NewText );
            this.Label.SizeToContents( );
            this.Label.Center( );
        }

        protected override void OnResize( Vector2 NewSize )
        {
            this.Label.Center( );
            base.OnResize( NewSize );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Button == MouseButton.Left )
            {
                IsDown = true;
                DownPosition = Mouse.Position;
            }

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( Button == MouseButton.Left )
            {
                IsDown = false;
                if ( DownPosition == Mouse.Position && OnClick != null )
                    OnClick( this, this );
            }

            base.OnButtonReleased( Button );
        }

        public override void Dispose( )
        {
            this.OnClick = null;

            base.Dispose( );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawButton( this );
        }
    }
}
