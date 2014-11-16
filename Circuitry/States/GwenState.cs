using Circuitry.UI;
using Gwen.Control;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SharpLib2D.Info;
using SharpLib2D.States;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.States
{
    public abstract class GwenState : State
    {
        public Canvas GwenCanvas { private set; get; }
        private Gwen.Input.OpenTK Input;

        protected override void OnStart( )
        {
            GwenCanvas = new Canvas( Manager.Skin );
            Input = Manager.CreateInput( GwenCanvas );

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

        public override void Dispose( )
        {
            GwenCanvas.Dispose( );
        }

        protected override void OnResize( )
        {
            GwenCanvas.SetSize( ( int ) Screen.Size.X, ( int ) Screen.Size.Y );
            base.OnResize( );
        }

        private void AddEvents( )
        {
            Mouse.OnMouseMove += OnMouseMove;
            Mouse.OnMouseButton += OnMouseButton;
        }

        private void RemoveEvents( )
        {
            // ReSharper disable DelegateSubtraction
            Mouse.OnMouseMove -= OnMouseMove;
            Mouse.OnMouseButton -= OnMouseButton;
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
