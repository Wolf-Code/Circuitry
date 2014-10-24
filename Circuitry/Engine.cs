using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SharpLib2D.States;

namespace Circuitry
{
    class Engine : SharpLib2D.Engine
    {
        protected override void OnLoad( EventArgs e )
        {
            ClientRectangle = new System.Drawing.Rectangle( 0, 0, 1024, 768 );

            Title = "Circuitry";

            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha );

            UI.Manager.Initialize( this );

            State.StartState<States.Game>( );

            base.OnLoad( e );
        }

        protected override void OnRenderFrame( FrameEventArgs e )
        {
            GL.ClearColor( Color4.CornflowerBlue );

            if ( UI.Manager.Renderer.TextCacheSize > 256 )
                UI.Manager.Renderer.FlushTextCache( );

            base.OnRenderFrame( e );
        }
    }
}
