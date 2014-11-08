using OpenTK;

namespace SharpLib2D.Objects
{
    public abstract class BoundingVolume
    {
        public abstract bool Contains( Vector2 Position );
        public abstract BoundingBox BoundingRectangle { get; }

        public virtual float Width
        {
            get { return BoundingRectangle.Width; }
        }

        public virtual float Height
        {
            get { return BoundingRectangle.Height; }
        }
    }
}