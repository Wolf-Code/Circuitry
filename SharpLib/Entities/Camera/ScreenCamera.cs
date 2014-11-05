using OpenTK;

namespace SharpLib2D.Entities.Camera
{
    public class ScreenCamera : DefaultCamera
    {
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
