
using System;
using System.Linq;
using OpenTK;
using SharpLib2D.Graphics;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        private void DrawConnections( )
        {
            foreach ( Gate E in Children.OfType<Gate>( ) )
            {
                foreach ( Output O in E.Outputs )
                {
                    if ( !O.IsConnected || O.Direction != IONode.NodeDirection.Out ) continue;

                    Vector2 Diff = O.Connection.Position - O.Position;
                    Diff /= 2;

                    Color.Set( 0.2f, 0.2f, 0.2f );
                    Line.DrawCubicBezierCurve( O.Position, O.Connection.Position, O.Position + new Vector2( Math.Abs( Diff.X ), 0 ), O.Connection.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 6f );
                    //SharpLib2D.Graphics.Line.Draw( O.Position, O.Connection.Position, 6f );

                    if ( !O.BinaryValue )
                        Color.Set( 0.2f, 0.2f, 1f );
                    else
                        Color.Set( 0.2f, 1f, 0.2f );

                    Line.DrawCubicBezierCurve( O.Position, O.Connection.Position, O.Position + new Vector2( Math.Abs( Diff.X ), 0 ), O.Connection.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 4f );
                    //SharpLib2D.Graphics.Line.Draw( O.Position, O.Connection.Position, 4f );
                }
            }
        }
    }
}
