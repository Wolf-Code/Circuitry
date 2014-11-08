using OpenTK;

namespace SharpLib2D.Objects
{
    public class BoundingBox : BoundingVolume
    {
        public override BoundingBox BoundingRectangle
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

        public Vector2 TopLeft { private set; get; }
        private Vector2 m_Size;

        public Vector2 Size
        {
            private set
            {
                m_Size = value;
                m_BottomRight = new Vector2( TopLeft.X + value.X, TopLeft.Y + value.Y );
            }
            get { return m_Size; }
        }

        private Vector2 m_BottomRight;

        public Vector2 BottomRight
        {
            private set
            {
                m_BottomRight = value;
                Size = new Vector2( value.X - TopLeft.X, value.Y - TopLeft.Y );
            }
            get { return m_BottomRight; }
        }

        public BoundingBox( Vector2 TopLeft, Vector2 BottomRight )
        {
            this.TopLeft = TopLeft;
            this.BottomRight = BottomRight;
        }

        public override bool Contains( Vector2 Position )
        {
            return Position.X >= TopLeft.X &&
                   Position.Y >= TopLeft.Y &&
                   Position.X <= BottomRight.X &&
                   Position.Y <= BottomRight.Y;
        }
    }
}
