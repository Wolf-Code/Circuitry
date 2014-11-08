using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Info;

namespace SharpLib2D.UI
{
    public class Canvas : MouseEntityContainer
    {
        public Skin.Skin Skin { private set; get; }
        public Dragger Dragger { private set; get; }

        public Canvas( Skin.Skin S )
        {
            Dragger = new Dragger( );
            Dragger.SetParent( this );
            this.SetSkin( S );
        }

        public void SetSkin( Skin.Skin S )
        {
            this.Skin = S;
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
