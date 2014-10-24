using Gwen.Control;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SharpLib2D.States;

namespace Circuitry.States
{
    public abstract class GwenState : State
    {
        public Canvas GwenCanvas { private set; get; }
        private Gwen.Input.OpenTK Input;

        protected override void OnStart( )
        {
            GwenCanvas = new Canvas( UI.Manager.Skin );
            Input = UI.Manager.CreateInput( GwenCanvas );


            base.OnStart( );
        }

        protected override void OnResume( )
        {
            AddEvents( );
            base.OnResume( );
        }

        protected override void OnPause( )
        {
            RemoveEvents( );
            base.OnPause( );
        }

        protected override void OnResize( )
        {
            GwenCanvas.SetSize( ( int ) SharpLib2D.Info.Screen.Size.X, ( int ) SharpLib2D.Info.Screen.Size.Y );
            base.OnResize( );
        }

        private void AddEvents( )
        {
            SharpLib2D.Info.Mouse.OnMouseMove += OnMouseMove;
            SharpLib2D.Info.Mouse.OnMouseButton += OnMouseButton;
        }

        private void RemoveEvents( )
        {
            // ReSharper disable DelegateSubtraction
            SharpLib2D.Info.Mouse.OnMouseMove -= OnMouseMove;
            SharpLib2D.Info.Mouse.OnMouseButton -= OnMouseButton;
            // ReSharper restore DelegateSubtraction
        }

        private void OnMouseButton( object Sender, MouseButtonEventArgs ButtonEventArgs )
        {
            Input.ProcessMouseMessage( ButtonEventArgs );
        }

        private void OnMouseMove( object Sender, MouseMoveEventArgs MouseMoveEventArgs )
        {
            Input.ProcessMouseMessage( MouseMoveEventArgs );
        }

        public override void Draw( FrameEventArgs e )
        {
            base.Draw( e );

            GL.MatrixMode( MatrixMode.Modelview );
            GL.PushMatrix( );
            {
                GL.LoadIdentity( );
                GwenCanvas.RenderCanvas( );
            }
            GL.PopMatrix( );
        }

        public Base GetControlAt( int X, int Y )
        {
            return GwenCanvas.GetControlAt( X, Y );
        }
    }
}
