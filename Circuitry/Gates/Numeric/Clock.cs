using System;
using Circuitry.Components;
using OpenTK;
using SharpLib2D.Graphics;

namespace Circuitry.Gates.Numeric
{
    class Clock : Gate
    {
        private int LastTime;

        public Clock( )
        {
            AddOutput( IONode.NodeType.Numeric, "Value", "The current OS time." );
            LastTime = GetTimeStamp( );

            Category = "Time";
            Texture = @"Resources\Textures\Components\Numeric\Clock.png";
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
                SetOutput( "Value", LastTime );
            }
            base.Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            DefaultTexturedDraw( );

            base.Draw( e );
        }
    }
}
