using OpenTK;

namespace SharpLib2D.Objects
{
    public class BoundingRectangle : BoundingVolume
    {
        public override BoundingRectangle BoundingBox
        {
            get { return this; }
        }

        public override float Width
        {
            get { return Size.X; }
        }

        public override float Height
        {
            get { return Size.Y; }
        }

        public Vector2 Position;
        public Vector2 Size;

        public float Left
        {
            get { return Position.X; }
        }

        public float Right
        {
            get { return Position.X + Size.X; }
        }

        public float Top
        {
            get { return Position.Y; }
        }

        public float Bottom
        {
            get { return Position.Y + Size.Y; }
        }

        public BoundingRectangle( Vector2 TopLeft, Vector2 BottomRight )
        {
            this.Position = TopLeft;
            this.Size = BottomRight - TopLeft;
        }

        public BoundingRectangle( float X, float Y, float Width, float Height )
        {
            this.Position = new Vector2( X, Y );
            this.Size = new Vector2( Width, Height );
        }

        public override bool Contains( Vector2 Position )
        {
            return Position.X >= Left &&
                   Position.Y >= Top &&
                   Position.X <= Right &&
                   Position.Y <= Bottom;
        }

        public override string ToString( )
        {
            return string.Format( "X: {0}, Y: {1}, Width: {2}, Height: {3}", Position.X, Position.Y, Width, Height );
        }
    }
}
