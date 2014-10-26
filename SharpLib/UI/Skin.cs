using OpenTK.Graphics;

namespace SharpLib2D.UI
{
    public abstract class Skin
    {
        protected static void DrawControlRectangle( Control C, float ReduceSize = 0, int RoundSize = 8, int RoundQuality = 8 )
        {
            Graphics.Rectangle.DrawRounded( C.Position.X + ReduceSize / 2, C.Position.Y + ReduceSize / 2, C.Size.X - ReduceSize, C.Size.Y - ReduceSize, RoundSize, RoundQuality );
        }

        public virtual void DrawControl( Control C )
        {
            Graphics.Color.Set( Color4.Gray );
            DrawControlRectangle( C );

            Graphics.Color.Set( Color4.DarkGray );
            DrawControlRectangle( C, 10, 4 );
        }

        public virtual void DrawButton( Button B )
        {
            if ( B.IsDown )
            {
                Graphics.Color.Set( Color4.Gray );
                DrawControlRectangle( B );

                Graphics.Color.Set( Color4.LightGray );
                DrawControlRectangle( B, 10, 4 );
            }
            else
            {
                Graphics.Color.Set( Color4.DarkSlateGray );
                DrawControlRectangle( B );

                Graphics.Color.Set( Color4.Gray );
                DrawControlRectangle( B, 10, 4 );
            }
        }
    }
}
