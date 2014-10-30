using System;

using OpenTK;
using OpenTK.Input;
using Mouse = SharpLib2D.Info.Mouse;

namespace SharpLib2D.Entities
{
    public class MouseEntityContainer : MouseEntity
    {
        protected MouseEntity LastDownEntity, HoverEntity;

        public override void Update( FrameEventArgs e )
        {
            Vector2 MousePos = ParentState.Camera.ToWorld( Mouse.Position );

            MouseEntity Top = GetTopChild( MousePos );

            if ( Top != HoverEntity )
            {
                if ( HoverEntity != null )
                    HoverEntity.OnMouseExit( );

                Top.OnMouseEnter( );

                HoverEntity = Top;
            }

            foreach ( MouseButton B in Enum.GetValues( typeof( MouseButton ) ) )
            {
                if ( Mouse.IsPressed( B ) )
                {
                    Top.OnButtonPressed( B );
                    LastDownEntity = Top;
                }

                if ( !Mouse.IsReleased( B ) ) continue;

                if ( LastDownEntity != null )
                {
                    LastDownEntity.OnButtonReleased( B );
                    LastDownEntity = null;
                }
                else
                    Top.OnButtonReleased( B );
            }
            base.Update( e );
        }
    }
}
