
namespace Circuitry.Gates.Binary
{
    public class Button : Components.Gate
    {
        private static readonly SharpLib2D.Graphics.Texture Off, On;

        public bool Down
        {
            private set;
            get;
        }

        static Button( )
        {
            Off = SharpLib2D.Graphics.Texture.Load( @"Resources\Textures\Components\Button\Normal\Button_Off.png" );
            On = SharpLib2D.Graphics.Texture.Load( @"Resources\Textures\Components\Button\Normal\Button_On.png" );
        }

        public Button( )
        {
            this.AddOutput( Components.IONode.NodeType.Binary, "Value", "1 when the button is pressed, 0 otherwise." );
            this.Category = "Input";
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            DrawIOConnectors( );

            SharpLib2D.Graphics.Color.Set( 1f, 1f, 1f );

            ( Down ? On : Off ).Bind( );

            DrawTexturedSelf( );
            base.Draw( e );
        }

        public override void OnButtonPressed( OpenTK.Input.MouseButton Button )
        {
            if ( this.Circuit.CurrentState == Components.Circuit.State.Active )
            {
                this.Down = true;
                this.SetOutput( "Value", true );
            }

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( OpenTK.Input.MouseButton Button )
        {
            if ( this.Circuit.CurrentState == Components.Circuit.State.Active )
            {
                this.Down = false;
                this.SetOutput( "Value", false );
            }

            base.OnButtonReleased( Button );
        }
    }
}
