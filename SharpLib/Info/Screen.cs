using OpenTK;

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
    }
}
