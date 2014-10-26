using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.Entities.Camera;
using SharpLib2D.Graphics;
using SharpLib2D.Info;
using SharpLib2D.States;

namespace SharpLib2D
{
    public class Engine : GameWindow
    {
        private DefaultCamera Cam;

        public Engine( )
        {
            Input.Initialize( this );
        }

        protected override void OnLoad( EventArgs e )
        {
            Cam = new DefaultCamera( );

            base.OnLoad( e );
        }

        protected override void OnResize( EventArgs e )
        {
            GL.Viewport( 0, 0, Size.Width, Size.Height );
            Screen.Projection = Matrix4.CreateOrthographicOffCenter( 0, Size.Width, Size.Height, 0, -1, 1 );

            State.PerformEvent( State.Events.Resize );
            base.OnResize( e );
        }

        protected override void OnUpdateFrame( FrameEventArgs e )
        {
            Info.Mouse.Update( e );

            if( State.ActiveState != null )
                State.ActiveState.Update( e );

            base.OnUpdateFrame( e );
        }

        protected override void OnRenderFrame( FrameEventArgs e )
        {
            GL.Clear( ClearBufferMask.ColorBufferBit );

            Renderer.RenderWithCamera( State.ActiveState != null ? State.ActiveState.Camera : Cam,
                ( ) =>
                {
                    if ( State.ActiveState != null )
                        State.ActiveState.Draw( e );

                    base.OnRenderFrame( e );
                } );
            SwapBuffers( );
        }
    }
}
