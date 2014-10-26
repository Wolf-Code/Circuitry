
namespace Circuitry.Gates.Binary
{
    public class AND : Components.Gate
    {
        static SharpLib2D.Graphics.Texture T;

        static AND( )
        {
            T = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\AND.png" );
        }

        public AND( )
        {
            this.Category = "Logic";
            this.SetSize( T.Width, T.Height );

            this.AddInput( Components.IONode.NodeType.Binary, "Input 1", "The first input." );
            this.AddInput( Components.IONode.NodeType.Binary, "Input 2", "The second input." );

            this.AddOutput( Components.IONode.NodeType.Binary, "Output", "Returns 1 if both input 1 and 2 are 1, 0 otherwise." );
        }

        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output", this.GetInput( "Input 1" ).BinaryValue && this.GetInput( "Input 2" ).BinaryValue );
            
            base.OnInputChanged( I );
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            this.DrawIOConnectors( );

            SharpLib2D.Graphics.Color.Set( 1f, 1f, 1f, 1f );
            T.Bind( );

            DrawTexturedSelf( );

            base.Draw( e );
        }
    }
}
