using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.Entities.Camera;

namespace SharpLib2D.Graphics
{
    public static class Renderer
    {
        /// <summary>
        /// Performs everything inside a given action with a given camera's projection and view matrix.
        /// </summary>
        /// <param name="Cam">The camera to use the projection and view matrix of.</param>
        /// <param name="Func">The function where the projection and view matrix used are the one inside the camera.</param>
        public static void RenderWithCamera( DefaultCamera Cam, Action Func )
        {
            Matrix4 P = Cam.Projection;
            Matrix4 V = Cam.View;

            GL.MatrixMode( MatrixMode.Projection );
            {
                GL.PushMatrix( );
                {
                    GL.LoadMatrix( ref P );
                    GL.MatrixMode( MatrixMode.Modelview );
                    {
                        GL.PushMatrix( );
                        {
                            GL.LoadMatrix( ref V );
                            Func( );
                        }
                        GL.PopMatrix( );
                    }
                    GL.MatrixMode( MatrixMode.Projection );
                }
                GL.PopMatrix( );
            }
        }

    }
}
