using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;
using SharpLib2D.Info;
using SharpLib2D.Objects;
using SharpLib2D.Resources;

namespace Circuitry.Components
{
    public class Bin : CircuitryEntity
    {
        private static Texture Closed, Opened;
        static Bin( )
        {
            Closed = Loader.Get<Texture>( "Resources\\Textures\\UI\\bin_closed.png" );
            Opened = Loader.Get<Texture>( "Resources\\Textures\\UI\\bin_empty.png" );
        }

        public override BoundingVolume BoundingVolume
        {
            get
            {
                return new BoundingTriangle( this.TopLeft, new Vector2( this.BottomRight.X, this.TopLeft.Y ),
                    this.BottomRight );
            }
        }

        public override void Update( FrameEventArgs e )
        {
            this.SetSize( Screen.Size.Y / 6, Screen.Size.Y / 6 );
            this.SetPosition(
                ParentState.Camera.ToWorld( new Vector2( Screen.Size.X - this.Size.X / 2, this.Size.Y / 2 ) ) );
            base.Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            Color.Set( Color4.Black );
            BoundingTriangle T = ( BoundingTriangle ) this.BoundingVolume;
            Triangle.Draw( T.Corner1, T.Corner2, T.Corner3 );

            Closed.Bind( );
            Color.Set( Color4.White );
            Rectangle.DrawTextured( this.TopLeft.X + Size.X / 2 - 5, this.TopLeft.Y + 5, this.Size.X / 2, this.Size.Y / 2 );

            base.Draw( e );
        }
    }
}
