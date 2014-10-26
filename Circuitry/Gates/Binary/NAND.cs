namespace Circuitry.Gates.Binary
{
    public class NAND : Components.Gate
    {
        static readonly SharpLib2D.Graphics.Texture T;

        static NAND( )
        {
            T = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\NAND.png" );
        }

        public NAND( )
        {
            this.SetSize( T.Width, T.Height );

            this.Category = "Logic";
            this.AddInput( Components.IONode.NodeType.Binary, "Input 1", "The first input." );
            this.AddInput( Components.IONode.NodeType.Binary, "Input 2", "The second input." );

            this.AddOutput( Components.IONode.NodeType.Binary, "Output", "Returns 0 if both input 1 and 2 are 1, 1 otherwise." );
        }

        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output", !( this.GetInput( "Input 1" ).BinaryValue && this.GetInput( "Input 2" ).BinaryValue ) );
            
            base.OnInputChanged( I );
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            this.DefaultDraw( T );

            base.Draw( e );
        }
    }
}
