using OpenTK.Graphics;

namespace SharpLib2D.UI
{
    public abstract class Skin
    {
        protected void DrawControlRectangle( Control C, float ReduceSize = 0 )
        {
            Graphics.Rectangle.Draw( C.Position.X + ReduceSize / 2, C.Position.Y + ReduceSize / 2, C.Size.X - ReduceSize, C.Size.Y - ReduceSize );
        }

        public virtual void DrawControl( Control C )
        {
            Graphics.Color.Set( Color4.Gray );
            DrawControlRectangle( C, 0 );

            Graphics.Color.Set( Color4.DarkGray );
            DrawControlRectangle( C, 10 );
        }

        public virtual void DrawButton( Button B )
        {
            if ( B.IsDown )
            {
                Graphics.Color.Set( Color4.Gray );
                DrawControlRectangle( B, 0 );

                Graphics.Color.Set( Color4.LightGray );
                DrawControlRectangle( B, 10 );
            }
            else
            {
                Graphics.Color.Set( Color4.DarkSlateGray );
                DrawControlRectangle( B, 0 );

                Graphics.Color.Set( Color4.Gray );
                DrawControlRectangle( B, 10 );
            }
        }
    }
}
