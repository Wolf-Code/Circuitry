using OpenTK;

namespace SharpLib2D.Objects
{
    public interface IBoundingVolume
    {
        bool Contains( Vector2 Position );
    }
}
