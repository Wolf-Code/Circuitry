﻿using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Graphics;

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
    }
}