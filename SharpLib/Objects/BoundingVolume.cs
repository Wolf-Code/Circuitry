using OpenTK;

namespace SharpLib2D.Objects
{
    public abstract class BoundingVolume
    {
        public abstract bool Contains( Vector2 Position );
        public abstract BoundingRectangle BoundingBox { get; }

        public virtual float Width
        {
            get { return BoundingBox.Width; }
        }

        public virtual float Height
        {
            get { return BoundingBox.Height; }
        }
    }
}