﻿
namespace Circuitry.Gates.Binary
{
    public class OR : Components.Gate
    {
        public OR( )
        {
            this.Category = "Logic";

            this.AddInput( Components.IONode.NodeType.Binary, "Input 1", "The first input." );
            this.AddInput( Components.IONode.NodeType.Binary, "Input 2", "The second input." );

            this.AddOutput( Components.IONode.NodeType.Binary, "Output", "Returns 1 if either input is 1 or if they're both 1, 0 otherwise." );
        }

        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output", this.GetInput( "Input 1" ).BinaryValue || this.GetInput( "Input 2" ).BinaryValue );
            
            base.OnInputChanged( I );
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            this.DefaultDraw( );

            base.Draw( e );
        }
    }
}
