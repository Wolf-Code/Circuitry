using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.Graphics.Objects;

namespace SharpLib2D.Graphics
{
    public static class Triangle
    {
        public static void Draw( Vector2 P1, Vector2 P2, Vector2 P3 )
        {
            Polygon.Draw( PrimitiveType.TriangleFan,
                new Vertex
                {
                    Color = Color.ActiveColor,
                    Position = P1,
                    UV = Vector2.Zero
                },
                new Vertex
                {
                    Color = Color.ActiveColor,
                    Position = P2,
                    UV = Vector2.Zero
                },
                new Vertex
                {
                    Color = Color.ActiveColor,
                    Position = P3,
                    UV = Vector2.Zero
                } );
        }

        public static void DrawOutlined( Vector2 P1, Vector2 P2, Vector2 P3, Color4 OutlineColor, Color4 InsideColor,
            float Outline )
        {
            Vector2 Center = ( P1 + P2 + P3 ) / 3;

            Color.Set( OutlineColor );
            Draw( P1, P2, P3 );

            Vector2 P1Dir = P1 - Center;
            Vector2 P2Dir = P2 - Center;
            Vector2 P3Dir = P3 - Center;

            float Dist1 = P1Dir.Length - Outline;
            float Dist2 = P2Dir.Length - Outline;
            float Dist3 = P3Dir.Length - Outline;

            Color.Set( InsideColor );
            Draw( Center + P1Dir.Normalized( ) * Dist1,
                Center + P2Dir.Normalized( ) * Dist2,
                Center + P3Dir.Normalized( ) * Dist3 );
        }
    }
}
