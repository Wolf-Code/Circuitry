using System;
using Circuitry.States;
using Gwen.Control;
using Gwen.Skin;
using OpenTK;
using SharpLib2D.Info;
using SharpLib2D.States;
using Base = Gwen.Skin.Base;

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

        public static bool MouseInsideUI( )
        {
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

        public static Gwen.Input.OpenTK CreateInput( Canvas C )
        {
            Gwen.Input.OpenTK In = new Gwen.Input.OpenTK( Window );
            In.Initialize( C );

            return In;
        }
    }
}
