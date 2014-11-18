using Circuitry.Components.Circuits;
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
        private static readonly Texture Closed;
        private static readonly Texture Opened;

        static Bin( )
        {
            Closed = Loader.Get<Texture>( "Resources\\Textures\\UI\\bin_closed.png" );
            Opened = Loader.Get<Texture>( "Resources\\Textures\\UI\\bin_empty.png" );
        }

        public override BoundingVolume BoundingVolume
        {
            get
            {
                return new BoundingTriangle( 
                    TopLeft, 
                    new Vector2( BottomRight.X, TopLeft.Y ),
                    BottomRight );
            }
        }

        public Bin( Circuit C )
        {
            this.SetParent( C );
            this.Circuit.Dragger.OnDrop += ( Dragger, Entity ) =>
            {
                if ( this.BoundingVolume.Contains( Mouse.WorldPosition ) )
                    Entity.Remove( );
            };
        }

        public override void Update( FrameEventArgs e )
        {
            SetSize( Screen.Size.Y / 6, Screen.Size.Y / 6 );
            SetPosition(
                ParentState.Camera.ToWorld( new Vector2( Screen.Size.X - Size.X / 2, Size.Y / 2 ) ) );

            base.Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            Color.Set( Color4.Black );
            BoundingTriangle T = ( BoundingTriangle ) BoundingVolume;
            Triangle.Draw( T.Corner1, T.Corner2, T.Corner3 );

            if ( Circuit.Dragger.IsDragging<CircuitryEntity>( ) && this.BoundingVolume.Contains( Mouse.WorldPosition ) )
                Opened.Bind( );
            else
                Closed.Bind( );

            Color.Set( Color4.White );
            Rectangle.DrawTextured( TopLeft.X + Size.X / 2 - 5, TopLeft.Y + 5, Size.X / 2, Size.Y / 2 );

            base.Draw( e );
        }
    }
}
