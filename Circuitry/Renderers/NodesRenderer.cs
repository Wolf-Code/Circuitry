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
        public static void DrawLine( Color4 Col, Vector2 Start, Vector2 End )
        {
            Color.Set( 0.2f, 0.2f, 0.2f );
            /*Line.DrawCubicBezierCurve( Cur.Position, Cur.NextNode.Position,
                Cur.Position + new Vector2( Math.Abs( Diff.X ), 0 ),
                Cur.NextNode.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 6f );*/
            Line.Draw( Start, End, 6f );

            Color.Set( Col );

            /*Line.DrawCubicBezierCurve( Cur.Position, Cur.NextNode.Position,
                Cur.Position + new Vector2( Math.Abs( Diff.X ), 0 ),
                Cur.NextNode.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 4f );*/
            Line.Draw( Start, End, 4f );
        }

        public static void DrawConnection( Circuit Circuit, IONode Start )
        {
            IONode Cur = Start;
            IONode Next = Start.NextNode;
            Color4 C = Cur.BinaryValue ? Color4.LimeGreen : Color4.Blue;

            while ( Next != null )
            {
                DrawLine( C, Cur.Position, Next.Position );

                Cur = Next;
                Next = Next.NextNode;
            }

            if ( !Circuit.Connector.ConnectingNodes ) return;

            if ( Cur == Circuit.Connector.ConnectionNode )
                DrawLine( C, Cur.Position, Mouse.WorldPosition );
            else if ( Start == Circuit.Connector.ConnectionNode )
                DrawLine( C, Start.Position, Mouse.WorldPosition );

            IONode.DrawNode( Mouse.WorldPosition );
        }
    }
}
