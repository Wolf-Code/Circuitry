
using OpenTK.Graphics;
using SharpLib2D.Graphics;
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI.Skin
{
    public class DefaultSkin : Skin
    {
        protected virtual Color4 OutlineColor
        {
            get
            {
                return new Color4( 34, 66, 94, 255 );
            }
        }

        protected virtual Color4 WindowColor
        {
            get
            {
                return new Color4( 52, 100, 142, 255 );
            }
        }

        protected static void DrawControlRectangle( Control C )
        {
            Rectangle.Draw( C.Position.X, C.Position.Y, C.Size.X, C.Size.Y );
        }

        protected static void DrawControlOutlineRectangle( Control C )
        {
            Rectangle.DrawOutlined( C.Position.X, C.Position.Y, C.Size.X, C.Size.Y, 2 );
        }

        public virtual void DrawControl( Control C )
        {
            Color.Set( WindowColor );
            DrawControlRectangle( C );

            Color.Set( OutlineColor );
            DrawControlOutlineRectangle( C );
        }

        public override void DrawPanel( Control P )
        {
            DrawControl( P );
        }

        public override void DrawButton( Button B )
        {
            if ( B.IsDown )
            {
                Color.Set( new Color4( 70, 135, 191, 255 ) );
                DrawControlRectangle( B );

                Color.Set( OutlineColor );
                DrawControlOutlineRectangle( B );
            }
            else
            {
                DrawControl( B );
            }
        }

        public override void DrawWindowTitleBar( WindowTitleBar B )
        {
            throw new System.NotImplementedException( );
        }

        public override void DrawWindowCloseButton( WindowCloseButton B )
        {
            throw new System.NotImplementedException( );
        }

        public override void DrawWindow( Window W )
        {
            DrawControl( W );
        }

        public override void DrawCheckbox( Checkbox C )
        {
            throw new System.NotImplementedException( );
        }
    }
}
