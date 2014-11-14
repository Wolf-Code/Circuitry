using System;
using System.Collections.Generic;
using Circuitry.Components;
using Circuitry.Components.Circuits;
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
                    Line.DrawCubicBezierPath( Positions, 8, 0.3f, Width );
                else if ( Positions.Count > 1 )
                    Line.Draw( Positions[ 0 ], Positions[ 1 ], Width );
            };

            Color.Set( Color4.DimGray );
            Drawer( 10f );
            Color.Set( C );
            Drawer( 5f );
        }
    }
}