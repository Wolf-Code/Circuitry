using OpenTK;

namespace SharpLib2D.Entities.Camera
{
    public class ScreenCamera : DefaultCamera
    {
        /// <summary>
        /// Returns the screen camera.
        /// </summary>
        public static ScreenCamera Get { private set; get; }

        public override Matrix4 View
        {
            get { return Matrix4.Identity; }
        }

        static ScreenCamera( )
        {
            Get = new ScreenCamera( );
        }
    }
}
