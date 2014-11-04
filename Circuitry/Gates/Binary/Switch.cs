using Circuitry.Components;
using Circuitry.States;
using Gwen.Control;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Graphics;
using SharpLib2D.Graphics.Objects;
using SharpLib2D.Resources;
using SharpLib2D.States;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.Gates.Binary
{
    public class Switch : Gate
    {
        private static readonly Texture Off, On;

        public bool Down
        {
            private set;
            get;
        }

        public override string Name
        {
            get
            {
                return "Switch";
            }
        }

        static Switch( )
        {
            Off = SharpLib2D.Resources.Loader.Get<Texture>( "Resources\\Textures\\Components\\Button\\Toggle\\Button_Toggle_Off.png" );
            On = SharpLib2D.Resources.Loader.Get<Texture>( "Resources\\Textures\\Components\\Button\\Toggle\\Button_Toggle_On.png" );
        }

        public Switch( )
        {
            AddOutput( IONode.NodeType.Binary, "Value", "1 when the button is toggled, 0 otherwise." );
            Category = "Input";
        }

        public override void Reset( )
        {
            base.Reset( );

            Down = false;
        }

        public override void Draw( FrameEventArgs e )
        {
            DrawIOConnectors( );

            Color.Set( 1f, 1f, 1f, 1f );

            ( Down ? On : Off ).Bind( );

            DrawTexturedSelf( );
            base.Draw( e );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            Base B = ( State.ActiveState as GwenState ).GetControlAt( ( int )Mouse.Position.X, ( int )Mouse.Position.Y );

            if ( Circuit.CurrentState == Circuit.State.Active &&
                 B == ( State.ActiveState as GwenState ).GwenCanvas )
            {
                Down = !Down;
                SetOutput( "Value", Down );
            }

            base.OnButtonPressed( Button );
        }
    }
}
