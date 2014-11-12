
using Circuitry.Components;
using OpenTK;
using SharpLib2D.Graphics;
using SharpLib2D.Graphics.Objects;
using SharpLib2D.Resources;

namespace Circuitry.Gates.Binary
{
    public class LED : Gate
    {
        static readonly Texture On, Off;

        static LED( )
        {
            On = Loader.Get<Texture>( "Resources\\Textures\\Components\\LED\\LED_On.png" );
            Off = Loader.Get<Texture>( "Resources\\Textures\\Components\\LED\\LED_Off.png" );
        }

        public LED( )
        {
            AddInput( IONode.NodeType.Binary, "Enabled", "Emits light when 1, is off otherwise." );

            Category = "Output";
        }

        public override void Draw( FrameEventArgs e )
        {
            DrawIOConnectors( );

            Color.Set( 1f, 1f, 1f, 1f );
            if ( GetIO<Input>( "Enabled" ).BinaryValue )
                On.Bind( );
            else
                Off.Bind( );

            DrawTexturedSelf( );
            base.Draw( e );
        }
    }
}
