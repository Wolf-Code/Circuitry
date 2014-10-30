using System;
using OpenTK;
using OpenTK.Input;
using Mouse = SharpLib2D.Info.Mouse;

namespace SharpLib2D.UI
{
    public class Button : Control
    {
        public string Text { protected set; get; }
        public bool IsDown { private set; get; }
        protected Vector2 DownPosition;

        public EventHandler OnClick;

        public Button( string Text )
        {
            this.Text = Text;
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

        public override void Draw( FrameEventArgs e )
        {
            Canvas.Skin.DrawButton( this );
            DrawChildren( e );
        }
    }
}
