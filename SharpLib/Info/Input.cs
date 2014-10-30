using OpenTK;

namespace SharpLib2D.Info
{
    public static class Input
    {
        internal static GameWindow Game
        {
            private set;
            get;
        }

        internal static void Initialize( GameWindow Window )
        {
            Game = Window;
        }
    }
}
