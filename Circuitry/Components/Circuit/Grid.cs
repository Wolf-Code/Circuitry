using System;
using OpenTK;
using SharpLib2D.Entities.Camera;
using SharpLib2D.Graphics;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        /// <summary>
        /// The size of the grid.
        /// </summary>
        public int GridSize
        {
            set;
            get;
        }

        /// <summary>
        /// Whether or not to show the grid.
        /// </summary>
        public bool ShowGrid
        {
            set;
            get;
        }

        /// <summary>
        /// Whether or not to snap new gates to the grid.
        /// </summary>
        public bool SnapToGrid
        {
            set;
            get;
        }

        public Vector2 SnapPositionToGrid( Vector2 SnapPosition )
        {
            float X = ( float )Math.Floor( SnapPosition.X / GridSize ) * GridSize;
            float Y = ( float )Math.Floor( SnapPosition.Y / GridSize ) * GridSize;

            return new Vector2( X, Y );
        }

        private void DrawGrid( )
        {
            if ( !ShowGrid ) return;

            Color.Set( 1f, 1f, 1f, 0.2f );

            DefaultCamera Cam = SharpLib2D.States.State.ActiveState.Camera;
            Vector2 P = SnapPositionToGrid( Cam.TopLeft );
            float Off = GridSize / 6f;
            Vector2 S = new Vector2( Cam.Size.X + Off, Cam.Size.Y + Off );
            Vector2 LocalP = Cam.TopLeft - P;

            for ( float X = P.X; X < P.X + S.X + LocalP.X; X += GridSize )
            {
                for ( float Y = P.Y; Y < P.Y + S.Y + LocalP.Y; Y += GridSize )
                {
                    Line.Draw( new Vector2( X - Off, Y ), new Vector2( X + Off, Y ) );
                    Line.Draw( new Vector2( X, Y - Off ), new Vector2( X, Y + Off ) );
                }
            }
        }
    }
}
