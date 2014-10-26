using System;
using Circuitry.Components;
using OpenTK;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Numeric
{
    class Clock : Gate
    {
        private static readonly Texture T;

        private int LastTime;

        static Clock( )
        {
            T = Texture.Load( @"Resources\Textures\Components\Numeric\Clock.png" );
        }

        public Clock( )
        {
            this.AddOutput( IONode.NodeType.Numeric, "Value", "The current OS time." );
            LastTime = GetTimeStamp( );

            this.Category = "Time";
        }

        private static int GetTimeStamp( )
        {
            return ( Int32 )( DateTime.UtcNow.Subtract( new DateTime( 1970, 1, 1 ) ) ).TotalSeconds;
        }

        public override void Update( FrameEventArgs e )
        {
            if ( !Active )
                return;

            int TS = GetTimeStamp( );
            if ( GetTimeStamp( ) != LastTime )
            {
                LastTime = TS;
                this.SetOutput( "Value", LastTime );
            }
            base.Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            this.DefaultDraw( T );

            base.Draw( e );
        }
    }
}
