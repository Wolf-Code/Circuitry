using System;
using OpenTK;
using OpenTK.Input;
using Mouse = SharpLib2D.Info.Mouse;

namespace SharpLib2D.Entities
{
    public class MouseEntityContainer : MouseEntity
    {
        public MouseEntity LastDownEntity { private set; get; }
        public MouseEntity HoverEntity { private set; get; }

        protected virtual Vector2 GetMousePosition( )
        {
            return ParentState.Camera.ToWorld( Mouse.Position );
        }

        public override void Update( FrameEventArgs e )
        {
            Vector2 MousePos = GetMousePosition( );

            MouseEntity Top = GetTopChild( MousePos );

            if ( Top != HoverEntity )
            {
                if ( HoverEntity != null )
                    HoverEntity.OnMouseExit( );

                Top.OnMouseEnter( );

                if ( HoverEntity != Top )
                {
                    if ( HoverEntity != null )
                        HoverEntity.OnRemoved -= HoverWasRemoved;

                    HoverEntity = Top;
                    HoverEntity.OnRemoved += HoverWasRemoved;
                }
            }

            foreach ( MouseButton B in Enum.GetValues( typeof( MouseButton ) ) )
            {
                if ( Mouse.IsPressed( B ) )
                {
                    Top.OnButtonPressed( B );
                    {
                        if ( LastDownEntity != Top )
                        {
                            if ( LastDownEntity != null )
                                LastDownEntity.OnRemoved -= LastDownWasRemoved;

                            LastDownEntity = Top;
                            LastDownEntity.OnRemoved += LastDownWasRemoved;
                        }
                    }
                }

                if ( !Mouse.IsReleased( B ) ) continue;

                if ( LastDownEntity != null )
                {
                    LastDownEntity.OnButtonReleased( B );
                    LastDownEntity.OnRemoved -= LastDownWasRemoved;
                    LastDownEntity = null;
                }
                else
                    Top.OnButtonReleased( B );
            }
            base.Update( e );
        }

        private void LastDownWasRemoved( object Sender, UpdatableEntity Entity )
        {
            LastDownEntity = null;
        }

        private void HoverWasRemoved( object Sender, UpdatableEntity Entity )
        {
            HoverEntity = null;
        }
    }
}
