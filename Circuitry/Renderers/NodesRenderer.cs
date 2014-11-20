using System;
using System.Collections.Generic;
using Circuitry.Components;
using Circuitry.Components.Circuits;
using Circuitry.Components.Nodes;
using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;
using SharpLib2D.Info;

namespace Circuitry.Renderers
{
    public static class NodesRenderer
    {
        public static void DrawConnection( Circuit Circuit, IONode Start )
        {
            if ( !Start.HasNextNode && !Circuit.Connector.ConnectingNodes )
                return;

            IONode PreviousCur = Start;
            IONode Cur = Start;
            Color4 C = Cur.BinaryValue ? Color4.LimeGreen : Color4.Blue;
            List<Vector2> Positions = new List<Vector2>( );
            while ( Cur != null )
            {
                Positions.Add( Cur.Position );

                PreviousCur = Cur;
                Cur = Cur.NextNode;
            }

            if ( Circuit.Connector.ConnectingNodes )
            {
                if ( Circuit.Connector.ConnectionNode == PreviousCur )
                    Positions.Add( Mouse.WorldPosition );
                else if ( Circuit.Connector.ConnectionNode == Start )
                    Positions.Insert( 0, Mouse.WorldPosition );
            }

            Action<float> Drawer = Width =>
            {
                if ( Positions.Count > 2 )
                {
                    if ( Circuit.Connector.ConnectingNodes )
                        Line.DrawCubicBezierPath( Positions, 16, 0.3f, Width );
                    else
                    {
                        float StartDiff = Math.Abs( Positions[ 0 ].X - Positions[ 1 ].X );
                        float EndDiff = Math.Abs( Positions[ Positions.Count - 1 ].X - Positions[ Positions.Count - 2 ].X );
                        Line.DrawCubicBezierPath(
                            Start.ToWorld( new Vector2( StartDiff, 0 ) ),
                            Start.LastNode.ToWorld( new Vector2( -EndDiff, 0 ) ),
                            Positions, 16, 0.3f, Width );
                    }
                }
                else if ( Positions.Count > 1 )
                {
                    if ( Circuit.Connector.ConnectingNodes )
                        Line.Draw( Positions[ 0 ], Positions[ 1 ], Width );
                    else
                    {
                        float Diff = Math.Abs( Positions[ 0 ].Y - Positions[ 1 ].Y );
                        Vector2 CP1 = Start.ToWorld( new Vector2( Diff, 0 ) );
                        Vector2 CP2 = Start.NextNode.ToWorld( new Vector2( -Diff, 0 ) );

                        Line.DrawCubicBezierCurve( Positions[ 0 ], Positions[ 1 ], CP1,
                            CP2, 16, Width );
                    }
                }
            };

            Color.Set( Color4.DimGray );
            Drawer( 10f );
            Color.Set( C );
            Drawer( 5f );
        }
    }
}