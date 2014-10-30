using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.Graphics.Objects;

namespace SharpLib2D.Graphics
{
    public static class Polygon
    {
        public static void Draw( PrimitiveType Type, params Vertex [ ] Vertices )
        {
            PrimitiveBatch.Begin( );
            {
                foreach ( Vertex V in Vertices )
                {
                    PrimitiveBatch.AddVertex( V.Position, V.Color, V.UV, Type );
                }
            }
            PrimitiveBatch.End( );
        }
    }
}
