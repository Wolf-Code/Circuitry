using OpenTK;
using SharpLib2D.Info;
using SharpLib2D.Math;

namespace SharpLib2D.Entities.Camera
{
    public class DefaultCamera : Entity
    {
        /// <summary>
        /// The projection matrix.
        /// </summary>
        public virtual Matrix4 Projection
        {
            get { return Screen.Projection; }
        }

        /// <summary>
        /// The view matrix.
        /// </summary>
        public virtual Matrix4 View
        {
            get { return Matrix4.CreateTranslation( -Position.X + Size.X / 2, -Position.Y + Size.Y / 2, 0 ); }
        }

        /// <summary>
        /// Transforms world coordinates into screen coordinates.
        /// </summary>
        /// <param name="WorldPosition"></param>
        /// <returns></returns>
        public override Vector2 ToLocal( Vector2 WorldPosition )
        {
            return Vector.Transform( WorldPosition, View );
        }

        /// <summary>
        /// Transforms screen coordinates into world coordinates.
        /// </summary>
        /// <param name="ScreenPosition"></param>
        /// <returns></returns>
        public override Vector2 ToWorld( Vector2 ScreenPosition )
        {
            return Vector.Transform( ScreenPosition, View.Inverted( ) );
        }
    }
}
