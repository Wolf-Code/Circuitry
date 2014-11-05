using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Info;

namespace SharpLib2D.UI
{
    public class Canvas : MouseEntityContainer
    {
        public Skin Skin { private set; get; }

        public Canvas( Skin S )
        {
            Skin = S;
        }

        protected override Vector2 GetMousePosition( )
        {
            return Mouse.Position;
        }

        public override void Draw( FrameEventArgs e )
        {
            Graphics.Renderer.RenderWithCamera( Entities.Camera.ScreenCamera.Get, ( ) => base.Draw( e ) );
        }
    }
}
