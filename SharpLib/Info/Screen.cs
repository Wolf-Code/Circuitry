using OpenTK;
using SharpLib2D.States;

namespace SharpLib2D.Info
{
    public static class Screen
    {
        /// <summary>
        /// The default projection matrix.
        /// </summary>
        public static Matrix4 Projection { internal set; get; }

        /// <summary>
        /// The size of the game's screen.
        /// </summary>
        public static Vector2 Size
        {
            get
            {
                return new Vector2( Input.Game.ClientSize.Width, Input.Game.ClientSize.Height );
            }
        }

        /// <summary>
        /// Checks to see if a position is visible with the currently active camera.
        /// </summary>
        /// <param name="WorldPosition"></param>
        /// <returns></returns>
        public static bool IsVisible( Vector2 WorldPosition )
        {
            return State.ActiveState.Camera.ContainsPosition( WorldPosition );
        }
    }
}
