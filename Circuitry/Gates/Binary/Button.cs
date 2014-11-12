
using Circuitry.Components;
using Circuitry.Components.Circuits;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Graphics;
using SharpLib2D.Graphics.Objects;
using SharpLib2D.Resources;

namespace Circuitry.Gates.Binary
{
    public class Button : Gate
    {
        private static readonly Texture Off, On;

        public bool Down
        {
            private set;
            get;
        }

        static Button( )
        {
            Off = Loader.Get<Texture>( @"Resources\Textures\Components\Button\Normal\Button_Off.png" );
            On = Loader.Get<Texture>( @"Resources\Textures\Components\Button\Normal\Button_On.png" );
        }

        public Button( )
        {
            AddOutput( IONode.NodeType.Binary, "Value", "1 when the button is pressed, 0 otherwise." );
            Category = "Input";
        }

        public override void Draw( FrameEventArgs e )
        {
            DrawIOConnectors( );

            Color.Set( 1f, 1f, 1f );

            ( Down ? On : Off ).Bind( );

            DrawTexturedSelf( );
            base.Draw( e );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Circuit.CurrentState == Circuit.State.Active )
            {
                Down = true;
                SetOutput( "Value", true );
            }

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( Circuit.CurrentState == Circuit.State.Active )
            {
                Down = false;
                SetOutput( "Value", false );
            }

            base.OnButtonReleased( Button );
        }
    }
}
