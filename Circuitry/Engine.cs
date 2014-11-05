using System;
using System.Drawing;
using Circuitry.States;
using Circuitry.UI;
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
            ClientRectangle = new Rectangle( 0, 0, 1024, 768 );

            Title = "Circuitry";

            GL.Enable( EnableCap.Blend );
            GL.BlendFunc( BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha );

            Manager.Initialize( this );

            SharpLib2D.Resources.Loader.CacheFolder( "Resources" );

            State.StartState<Game>( );

            base.OnLoad( e );
        }

        protected override void OnRenderFrame( FrameEventArgs e )
        {
            GL.ClearColor( Color4.CornflowerBlue );

            if ( Manager.Renderer.TextCacheSize > 256 )
                Manager.Renderer.FlushTextCache( );

            base.OnRenderFrame( e );
        }
    }
}
