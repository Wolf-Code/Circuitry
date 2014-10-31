using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;
using SharpLib2D.Info;
using SharpLib2D.Objects;

namespace Circuitry.Components
{
    public class Bin : CircuitryEntity
    {
        private static Texture Closed, Opened;
        static Bin( )
        {
            Closed = Texture.Load( "Resources\\Textures\\UI\\bin_closed.png" );
            Opened = Texture.Load( "Resources\\Textures\\UI\\bin_empty.png" );
        }

        public override IBoundingVolume BoundingVolumne
        {
            get { return new BoundingTriangle( this.TopLeft, this.TopRight, this.BottomRight ); }
        }

        public override void Update( FrameEventArgs e )
        {
            this.SetPosition( ParentState.Camera.ToWorld( Screen.Size.X - this.Size.X / 2, this.Size.Y / 2 ) );
            base.Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            Color.Set( Color4.Black );
            Triangle.Draw( this.TopLeft, this.TopRight, this.BottomRight );

            Closed.Bind( );
            Color.Set( Color4.White );
            Rectangle.DrawTextured( this.TopLeft.X + Size.X / 2 - 5, this.TopLeft.Y + 5, this.Size.X / 2, this.Size.Y / 2 );

            base.Draw( e );
        }
    }
}
