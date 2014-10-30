using OpenTK;

namespace SharpLib2D.Objects
{
    public class BoundingBox
    {
        public Vector2 TopLeft { private set; get; }

        public Vector2 Size { private set; get; }

        public Vector2 BottomRight { private set; get; }

        public BoundingBox( Vector2 TopLeft, float Width, float Height )
        {
            this.TopLeft = TopLeft;
            BottomRight = new Vector2( TopLeft.X + Width, this.TopLeft.Y + Height );
            Size = new Vector2( Width, Height );
        }

        public BoundingBox( Vector2 TopLeft, Vector2 BottomRight )
        {
            this.TopLeft = TopLeft;
            this.BottomRight = BottomRight;
            Size = new Vector2( BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y );
        }
    }
}
