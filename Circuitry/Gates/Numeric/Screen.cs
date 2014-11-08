
using System.Globalization;
using Circuitry.Components;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Numeric
{
    public class Screen : Gate
    {
        public override string Name
        {
            get { return "Numerical Screen"; }
        }

        private const string Font = "Arial";
        private const float TextSize = 10;

        public Screen( )
        {
            SetGateSize( 2, 1 );
            Texture = "Resources\\Textures\\Components\\Screen\\Numeric.png";

            AddInput( IONode.NodeType.Numeric, "Value", "The value to display." );

            Category = "Output";
        }

        public override void Draw( FrameEventArgs e )
        {
            DefaultTexturedDraw( );

            string Val = GetInput( "Value" ).Value.ToString( CultureInfo.InstalledUICulture);

            Text.SetAlignments( Text.HorizontalAlignment.Center, Text.VerticalAlignment.Center );
            Text.DrawString( Val, Font, TextSize, Position, Color4.Black );

            base.Draw( e );
        }
    }
}
