
namespace Circuitry.Gates.Binary
{
    public class LED : Components.Gate
    {
        static readonly SharpLib2D.Graphics.Texture On, Off;

        static LED( )
        {
            On = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\LED\\LED_On.png" );
            Off = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\LED\\LED_Off.png" );
        }

        public LED( )
        {
            this.AddInput( Components.IONode.NodeType.Binary, "Enabled", "Emits light when 1, is off otherwise." );

            this.Category = "Output";
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            this.DrawIOConnectors( );

            SharpLib2D.Graphics.Color.Set( 1f, 1f, 1f, 1f );
            if ( this.GetIO<Components.Input>( "Enabled" ).BinaryValue )
                On.Bind( );
            else
                Off.Bind( );

            DrawTexturedSelf( );
            base.Draw( e );
        }
    }
}
