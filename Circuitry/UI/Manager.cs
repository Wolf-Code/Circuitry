using Circuitry.States;
using Gwen.Control;
using OpenTK;
using SharpLib2D.States;

namespace Circuitry.UI
{
    public static class Manager
    {
        public static Gwen.Renderer.OpenTK Renderer
        {
            private set;
            get;
        }

        public static Gwen.Skin.Base Skin
        {
            private set;
            get;
        }

        private static GameWindow Window;

        public static bool MouseInsideUI( )
        {
            Base B =
                ( ( GwenState ) State.ActiveState ).GwenCanvas.GetControlAt(
                    ( int ) SharpLib2D.Info.Mouse.Position.X, ( int ) SharpLib2D.Info.Mouse.Position.Y );
            return B != ( ( GwenState ) State.ActiveState ).GwenCanvas;
        }

        public static void Initialize( GameWindow Window )
        {
            Manager.Window = Window;
            Renderer = new Gwen.Renderer.OpenTK( );
            Skin = new Gwen.Skin.TexturedBase( Renderer, "Resources\\Textures\\UI\\DefaultSkin.png" );
        }

        public static Gwen.Input.OpenTK CreateInput( Canvas C )
        {
            Gwen.Input.OpenTK In = new Gwen.Input.OpenTK( Window );
            In.Initialize( C );

            return In;
        }
    }
}
