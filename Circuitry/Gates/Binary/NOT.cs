using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Circuitry.Gates.Binary
{
    public class NOT : Components.Gate
    {
        static SharpLib2D.Graphics.Texture T;

        static NOT( )
        {
            T = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\NOT.png" );
        }

        public NOT( )
        {
            this.Category = "Logic";
            this.SetSize( T.Width, T.Height );
            this.AddInput( Components.IONode.NodeType.Binary, "Input", "The input." );

            this.AddOutput( Components.IONode.NodeType.Binary, "Output", "Returns 1 if the input is 0, and 1 otherwise." );
        }

        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output", !this.GetInput( "Input" ).BinaryValue );
            
            base.OnInputChanged( I );
        }

        public override void Draw( FrameEventArgs e )
        {
            this.DefaultDraw( T );

            base.Draw( e );
        }
    }
}
