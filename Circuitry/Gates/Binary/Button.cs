using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuitry.Gates.Binary
{
    public class Button : Components.Gate
    {
        public bool Down
        {
            private set;
            get;
        }

        public Button( )
        {
            this.AddOutput( Components.IONode.NodeType.Binary, "Value", "1 when the button is pressed, 0 otherwise." );
            this.Category = "Input";
        }

        public override void Draw( OpenTK.FrameEventArgs e )
        {
            this.DrawIOConnectors( );

            if ( this.Down )
                SharpLib2D.Graphics.Color.Set( 0f, 1f, 0f, 1f );
            else
                SharpLib2D.Graphics.Color.Set( 1f, 0f, 0f, 1f );

            //DrawTexturedSelf( );
            SharpLib2D.Graphics.Rectangle.Draw( TopLeft.X, TopLeft.Y, Size.X, Size.Y );
            base.Draw( e );
        }

        public override void OnButtonPressed( OpenTK.Input.MouseButton Button )
        {
            if ( this.Circuit.CurrentState == Components.Circuit.State.Active )
            {
                this.Down = true;
                this.SetOutput( "Value", true );
            }

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( OpenTK.Input.MouseButton Button )
        {
            if ( this.Circuit.CurrentState == Components.Circuit.State.Active )
            {
                this.Down = false;
                this.SetOutput( "Value", false );
            }

            base.OnButtonReleased( Button );
        }
    }
}
