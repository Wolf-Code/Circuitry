
using System.Globalization;
using Circuitry.Components;
using OpenTK;
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
            this.Texture = "Resources\\Textures\\Components\\Screen\\Numeric.png";

            AddInput( IONode.NodeType.Numeric, "Value", "The value to display." );

            Category = "Output";
        }

        public override void Draw( FrameEventArgs e )
        {
            this.DefaultTexturedDraw( );

            string Val = GetInput( "Value" ).Value.ToString( CultureInfo.InstalledUICulture);

            Color.Set( 0f, 0f, 0f );
            Text.SetAlignments( Text.HorizontalAlignment.Center, Text.VerticalAlignment.Center );
            Text.DrawString( Val, Font, TextSize, Position );

            base.Draw( e );
        }
    }
}
