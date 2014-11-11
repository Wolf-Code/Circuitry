using OpenTK.Input;

namespace SharpLib2D.UI
{
    public class Button : Label
    {
        public bool IsDown { private set; get; }

        public event SharpLibUIEventHandler<Button> OnClick;

        public Button( string Text = "" )
        {
            this.SetText( Text );
            this.IgnoreMouseInput = false;
            this.HorizontalAlignment = Graphics.Text.HorizontalAlignment.Center;
            this.VerticalAlignment = Graphics.Text.VerticalAlignment.Center;
            this.OnLeftClick += Control => { if ( OnClick != null ) OnClick( this ); };
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Button == MouseButton.Left )
                IsDown = true;

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( Button == MouseButton.Left )
                IsDown = false;

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
            base.DrawSelf( );
        }
    }
}
