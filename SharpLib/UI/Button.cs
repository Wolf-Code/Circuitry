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
        protected readonly Label Label;

        public EventHandler OnClick;

        public Button( string Text )
        {
            this.Label = new Label( );
            this.Label.SetParent( this );
        }

        public void SetText( string Text )
        {
            this.Label.SetText( Text );
            this.Label.SizeToContents( );
            this.Label.Center( );
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
                    OnClick( this, null );
            }

            base.OnButtonReleased( Button );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawButton( this );
        }
    }
}
