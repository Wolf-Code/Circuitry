using OpenTK;

namespace SharpLib2D.Entities
{
    public interface IPositionable
    {
        Vector2 Position { get; }
        float X { get; }
        float Y { get; }

        /// <summary>
        /// Sets the position of this object.
        /// </summary>
        /// <param name="NewPosition">The new position.</param>
        void SetPosition( Vector2 NewPosition );

        /// <summary>
        /// Sets the position of this object.
        /// </summary>
        /// <param name="NewX">The X-coordinate.</param>
        /// <param name="NewY">The Y-coordinate.</param>
        void SetPosition( float NewX, float NewY );
    }
}
