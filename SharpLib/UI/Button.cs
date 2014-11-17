using OpenTK.Input;
using SharpLib2D.Info;

namespace SharpLib2D.UI
{
    public class Button : Label
    {
        /// <summary>
        /// Returns whether the button is being held down.
        /// </summary>
        public bool IsDown { private set; get; }

        /// <summary>
        /// Gets called whenever the button is clicked and the cursor has not been moved.
        /// </summary>
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
