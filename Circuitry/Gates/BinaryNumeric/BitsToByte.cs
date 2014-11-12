using System.Collections;
using Circuitry.Components;
using Circuitry.Components.Nodes;
using OpenTK;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.BinaryNumeric
{
    class BitsToByte : Gate
    {
        private readonly BitArray BArray;

        public BitsToByte( )
        {
            SetGateSize( 1, 3 );
            Texture = @"Resources\Textures\Components\BinaryNumeric\BitsToByte.png";
            AddOutput( IONode.NodeType.Numeric, "Value", "The byte made up from the 8 input bits." );
            BArray = new BitArray( 8 );

            for( int Q = 0; Q < BArray.Length; Q++ )
                AddInput( IONode.NodeType.Binary, "Bit " + ( Q + 1 ), "Bit #" + ( Q + 1 ) + " in the created byte." );

            Category = "Binary to Numeric";
        }

        public override void OnInputChanged( Input I )
        {
            int N = Inputs.IndexOf( I );
            BArray[ N ] = I.BinaryValue;

            byte [ ] B = new byte[ 1 ];
            BArray.CopyTo( B, 0 );

            SetOutput( "Value", B[ 0 ] );

            base.OnInputChanged( I );
        }

        public override void Draw( FrameEventArgs e )
        {
            DefaultTexturedDraw(  );

            base.Draw( e );
        }
    }
}
