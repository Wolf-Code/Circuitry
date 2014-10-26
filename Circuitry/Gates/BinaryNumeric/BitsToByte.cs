using System.Collections;
using Circuitry.Components;
using OpenTK;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.BinaryNumeric
{
    class BitsToByte : Gate
    {
        private static readonly Texture T;
        private readonly BitArray BArray;

        static BitsToByte( )
        {
            T = Texture.Load( @"Resources\Textures\Components\BinaryNumeric\BitsToByte.png" );
        }

        public BitsToByte( )
        {
            this.SetGateSize( 1, 3 );

            this.AddOutput( IONode.NodeType.Numeric, "Value", "The byte made up from the 8 input bits." );
            BArray = new BitArray( 8 );

            for( int Q = 0; Q < BArray.Length; Q++ )
                this.AddInput( IONode.NodeType.Binary, "Bit " + ( Q + 1 ), "Bit #" + ( Q + 1 ) + " in the created byte." );

            this.Category = "Binary to Numeric";
        }

        public override void OnInputChanged( Input I )
        {
            int N = this.Inputs.IndexOf( I );
            BArray[ N ] = I.BinaryValue;

            byte [ ] B = new byte[ 1 ];
            BArray.CopyTo( B, 0 );

            this.SetOutput( "Value", B[ 0 ] );

            base.OnInputChanged( I );
        }

        public override void Draw( FrameEventArgs e )
        {
            this.DefaultDraw( T );

            base.Draw( e );
        }
    }
}
