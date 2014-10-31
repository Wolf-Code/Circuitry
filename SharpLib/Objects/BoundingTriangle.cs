using OpenTK;

namespace SharpLib2D.Objects
{
    public class BoundingTriangle : IBoundingVolume
    {
        public Vector2 Corner1 { private set; get; }
        public Vector2 Corner2 { private set; get; }
        public Vector2 Corner3 { private set; get; }

        public BoundingTriangle( Vector2 Corner1, Vector2 Corner2, Vector2 Corner3 )
        {
            this.Corner1 = Corner1;
            this.Corner2 = Corner2;
            this.Corner3 = Corner3;
        }

        public bool Contains( Vector2 Position )
        {
            return false;
        }
    }
}