using OpenTK.Graphics;
using SharpLib2D.Graphics;

namespace SharpLib2D.UI
{
    public abstract class Skin
    {
        protected static void DrawControlRectangle( Control C, float ReduceSize = 0, int RoundSize = 8, int RoundQuality = 8 )
        {
            Rectangle.DrawRounded( C.Position.X + ReduceSize / 2, C.Position.Y + ReduceSize / 2, C.Size.X - ReduceSize, C.Size.Y - ReduceSize, RoundSize, RoundQuality );
        }

        public virtual void DrawControl( Control C )
        {
            Color.Set( Color4.Gray );
            DrawControlRectangle( C );

            Color.Set( Color4.DarkGray );
            DrawControlRectangle( C, 10, 4 );
        }

        public virtual void DrawButton( Button B )
        {
            if ( B.IsDown )
            {
                Color.Set( Color4.Gray );
                DrawControlRectangle( B );

                Color.Set( Color4.LightGray );
                DrawControlRectangle( B, 10, 4 );
            }
            else
            {
                Color.Set( Color4.DarkSlateGray );
                DrawControlRectangle( B );

                Color.Set( Color4.Gray );
                DrawControlRectangle( B, 10, 4 );
            }
        }
    }
}
