using Circuitry.States;
using SharpLib2D.States;

namespace Circuitry.Gates.Binary
{
    public class ButtonToggle : Components.Gate
    {
        private static readonly SharpLib2D.Graphics.Texture Off, On;

        public bool Down
        {
            private set;
            get;
        }

        public override string Name
        {
            get
            {
                return "Toggleable Button";
            }
        }

        static ButtonToggle( )
        {
            Off = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\Button\\Toggle\\Button_Toggle_Off.png" );
            On = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\Button\\Toggle\\Button_Toggle_On.png" );
        }

        public ButtonToggle( )
        {
            this.AddOutput( Components.IONode.NodeType.Binary, "Value", "1 when the button is toggled, 0 otherwise." );
            this.SetSize( On.Width, On.Height );
            this.Category = "Input";
        }

        public override void Reset( )
        {
            base.Reset( );

            Down = false;
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            DrawIOConnectors( );

            SharpLib2D.Graphics.Color.Set( 1f, 1f, 1f, 1f );

            ( Down ? On : Off ).Bind( );

            DrawTexturedSelf( );
            base.Draw( e );
        }

        public override void OnButtonPressed( OpenTK.Input.MouseButton Button )
        {
            Gwen.Control.Base B = ( State.ActiveState as GwenState ).GetControlAt( ( int )SharpLib2D.Info.Mouse.Position.X, ( int )SharpLib2D.Info.Mouse.Position.Y );

            if ( Circuit.CurrentState == Components.Circuit.State.Active &&
                 B == ( State.ActiveState as GwenState ).GwenCanvas )
            {
                Down = !Down;
                SetOutput( "Value", Down );
            }

            base.OnButtonPressed( Button );
        }
    }
}
