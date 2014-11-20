using Circuitry.States;
using Gwen.Skin;
using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Info;
using SharpLib2D.States;
using SharpLib2D.UI;

namespace Circuitry.UI
{
    public static class Manager
    {
        public static Gwen.Renderer.OpenTK Renderer
        {
            private set;
            get;
        }

        public static Base Skin
        {
            private set;
            get;
        }

        private static GameWindow Window;

        /// <summary>
        /// Returns true if the mouse is on any UI element.
        /// </summary>
        /// <returns></returns>
        public static bool MouseInsideUI( )
        {
            if ( State.IsActiveState<UIState>( ) )
            {
                MouseEntity C = State.GetActiveState<UIState>( ).Canvas.GetTopChild( Mouse.Position );

                return !( C is Canvas );
            }

            Gwen.Control.Base B =
                ( ( GwenState ) State.ActiveState ).GwenCanvas.GetControlAt(
                    ( int ) Mouse.Position.X, ( int ) Mouse.Position.Y );

            return B != ( ( GwenState ) State.ActiveState ).GwenCanvas;

        }

        public static void Initialize( GameWindow Window )
        {
            Manager.Window = Window;
            Renderer = new Gwen.Renderer.OpenTK( );
            Skin = new TexturedBase( Renderer, "Resources\\Textures\\UI\\DefaultSkin.png" );
        }

        public static Gwen.Input.OpenTK CreateInput( Gwen.Control.Canvas C )
        {
            Gwen.Input.OpenTK In = new Gwen.Input.OpenTK( Window );
            In.Initialize( C );

            return In;
        }
    }
}
