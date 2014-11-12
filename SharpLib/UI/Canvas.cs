using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Entities.Camera;
using SharpLib2D.Graphics;
using SharpLib2D.Info;

namespace SharpLib2D.UI
{
    public class Canvas : MouseEntityContainer
    {
        public Skin.Skin Skin { private set; get; }
        public Dragger Dragger { private set; get; }

        public override Vector2 TopLeft
        {
            get { return Position; }
        }

        public Canvas( Skin.Skin S )
        {
            Dragger = new Dragger( );
            Dragger.SetParent( this );
            SetSkin( S );
        }

        public void SetSkin( Skin.Skin S )
        {
            Skin = S;
        }

        protected override Vector2 GetMousePosition( )
        {
            return Mouse.Position;
        }

        public override void Draw( FrameEventArgs e )
        {
            Scissor.Enable( );
            {
                Renderer.RenderWithCamera( ScreenCamera.Get, ( ) => base.Draw( e ) );
            }
            Scissor.Disable( );
        }
    }
}
