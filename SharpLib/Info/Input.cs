namespace SharpLib2D.Info
{
    public static class Input
    {
        internal static OpenTK.GameWindow Game
        {
            private set;
            get;
        }

        internal static void Initialize( OpenTK.GameWindow Window )
        {
            Game = Window;
        }
    }
}
