using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Entities;
using SharpLib2D.Entities.Camera;
using SharpLib2D.Graphics;
using SharpLib2D.Info;

namespace SharpLib2D.UI
{
    public class Canvas : MouseEntityContainer
    {
        /// <summary>
        /// The canvas' skin.
        /// </summary>
        public Skin.Skin Skin { private set; get; }

        /// <summary>
        /// The dragger entity used to drag UI controls.
        /// </summary>
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

        /// <summary>
        /// Sets the skin with which to draw the canvas' children.
        /// </summary>
        /// <param name="S">The new skin.</param>
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
            Color.Set( Color4.White );
            Scissor.Enable( );
            {
                Renderer.RenderWithCamera( ScreenCamera.Get, ( ) => base.Draw( e ) );
            }
            Scissor.Disable( );
        }
    }
}
