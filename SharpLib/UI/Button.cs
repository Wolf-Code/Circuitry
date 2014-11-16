using OpenTK.Input;
using SharpLib2D.Info;

namespace SharpLib2D.UI
{
    public class Button : Label
    {
        public bool IsDown { private set; get; }

        public event SharpLibUIEventHandler<Button> OnClick;

        public Button( string Text = "" )
        {
            SetText( Text );
            IgnoreMouseInput = false;
            HorizontalAlignment = Directions.HorizontalAlignment.Center;
            VerticalAlignment = Directions.VerticalAlignment.Center;
            OnLeftClick += Control => { if ( OnClick != null ) OnClick( this ); };
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
            OnClick = null;

            base.Dispose( );
        }

        protected void DrawLabel( )
        {
            base.DrawSelf( );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawButton( this );
            DrawLabel( );
        }
    }
}
