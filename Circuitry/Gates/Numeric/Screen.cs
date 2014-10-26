
using System.Globalization;
using OpenTK;

namespace Circuitry.Gates.Numeric
{
    public class Screen : Components.Gate
    {
        static readonly SharpLib2D.Graphics.Texture T;

        public override string Name
        {
            get { return "Numerical Screen"; }
        }

        private const string Font = "Arial";
        private readonly float TextSize = 10;

        static Screen( )
        {
            T = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\Screen\\Numeric.png" );
        }

        public Screen( )
        {
            this.SetGateSize( 2, 1 );
            this.AddInput( Components.IONode.NodeType.Numeric, "Value", "The value to display." );

            this.Category = "Output";
        }

        public override void Draw( FrameEventArgs e )
        {
            this.DrawIOConnectors( );

            SharpLib2D.Graphics.Color.Set( 1f, 1f, 1f );
            T.Bind( );

            DrawTexturedSelf( );

            string Val = this.GetInput( "Value" ).Value.ToString( CultureInfo.InstalledUICulture);
            Vector2 S = SharpLib2D.Graphics.Text.MeasureString( Val, Font, TextSize );
            
            SharpLib2D.Graphics.Color.Set( 0f, 0f, 0f );
            SharpLib2D.Graphics.Text.DrawString( Val, Font, TextSize,
                this.Position - S / 2 );

            base.Draw( e );
        }
    }
}
