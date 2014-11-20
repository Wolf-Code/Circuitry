using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.Graphics.Objects;

namespace SharpLib2D.Graphics
{
    
    static class PrimitiveBatch
    {
        private static readonly Dictionary<PrimitiveType, List<Vertex>> Vertices = new Dictionary<PrimitiveType, List<Vertex>>( ); 

        private static bool HasBegun;

        public static void Begin( )
        {
            if ( HasBegun )
                throw new Exception( "PrimitiveBatch.End has to be called before we can Begin again." );

            HasBegun = true;
        }

        public static void End( )
        {
            if ( !HasBegun )
                throw new Exception( "PrimitiveBatch.Begin has to be called before End can be called." );

            Flush( );

            HasBegun = false;
        }

        public static void AddVertex( Vector2 Position, Color4 Color, Vector2 UV, PrimitiveType PrimitiveType = PrimitiveType.TriangleStrip )
        {
            if ( !HasBegun )
                throw new InvalidOperationException( "PrimitiveBatch.Begin has to be called before anything else can be called." );

            if ( !Vertices.ContainsKey( PrimitiveType ) )
                Vertices.Add( PrimitiveType, new List<Vertex>( ) );

            Vertices[ PrimitiveType ].Add( new Vertex { Position = Position, Color = Color, UV = UV } );
        }

        private static void Flush( )
        {
            foreach ( var Pair in Vertices )
            {
                //if ( Pair.Value.Any( O => Info.Screen.IsVisible( O.Position ) ) )
                //{
                    GL.Begin( Pair.Key );
                    {
                        foreach ( Vertex V in Pair.Value )
                        {
                            GL.TexCoord2( V.UV );
                            GL.Color4( V.Color );
                            GL.Vertex2( V.Position );
                        }
                    }
                    GL.End( );
                //}
                Pair.Value.Clear( );
            }
        }

        /*
        private static void FlushTriangles( )
        {
            if ( TriangleVerticesCount < 3 )
                return;
            Texture.EnableTextures( false );

            //int TriangleCount = TriangleVerticesCount / 3;
            GL.Begin( PrimitiveType.TriangleStrip );
            {
                for ( int Q = 0; Q < TriangleVerticesCount; Q++ )
                {
                    GL.Color4( TriangleVertices[ Q ].Color );
                    GL.TexCoord2( TriangleVertices[ Q ].UV );
                    GL.Vertex2( TriangleVertices[ Q ].Position );
                }
            }
            GL.End( );
            //TriangleVerticesCount -= TriangleCount;
            TriangleVerticesCount = 0;
        }

        private static void FlushLines( )
        {
            if ( LineVerticesCount < 2 )
                return;
            Texture.EnableTextures( false );
            //int LineCount = LineVerticesCount / 2;
            GL.Begin( PrimitiveType.LineStrip );
            {
                for ( int Q = 0; Q < LineVerticesCount; Q++ )
                {
                    GL.Color4( LineVertices[ Q ].Color );
                    GL.TexCoord2( LineVertices[ Q ].UV );
                    GL.Vertex2( LineVertices[ Q ].Position );
                }
            }
            GL.End( );
            //LineVerticesCount -= LineCount;
            LineVerticesCount = 0;
        }*/
    }
}
