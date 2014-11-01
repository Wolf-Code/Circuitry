using System;
using OpenTK;
using OpenTK.Input;

namespace SharpLib2D.Entities
{
    public class Dragger : Entity
    {
        public Entity DraggingEntity { private set; get; }
        public bool Dragging { private set; get; }
        public Vector2 LocalGrabPoint { private set; get; }

        public Dragger( )
        {
            Info.Mouse.OnMouseButton += OnMouseButton;
        }

        protected override void OnRemove( )
        {
            Info.Mouse.OnMouseButton -= OnMouseButton;
            base.OnRemove( );
        }

        private void OnMouseButton( object Sender, MouseButtonEventArgs MouseButtonEventArgs )
        {
            if ( MouseButtonEventArgs.Button == MouseButton.Left && !MouseButtonEventArgs.IsPressed )
                this.StopDragging( );
        }

        public void StartDragging( Entity E )
        {
            if ( Dragging )
                StopDragging( );

            Dragging = true;
            DraggingEntity = E;
            LocalGrabPoint = E.ToLocal( ParentState.Camera.ToWorld( Info.Mouse.Position ) );
        }

        /// <summary>
        /// Returns true if we're dragging, and the entity we're dragging is of type T.
        /// </summary>
        /// <typeparam name="T">The type to check for.</typeparam>
        /// <returns></returns>
        public bool IsDragging<T>( ) where T : Entity
        {
            return Dragging && DraggingEntity is T;
        }

        protected virtual Vector2 TransformPosition( Vector2 NewEntityPosition )
        {
            return NewEntityPosition;
        }

        public override void Update( FrameEventArgs e )
        {
            if ( !Dragging ) return;

            Vector2 DPos = Info.Mouse.Position - LocalGrabPoint;
            Vector2 Pos = ParentState.Camera.ToWorld( DPos );

            DraggingEntity.SetPosition( TransformPosition( DraggingEntity.Parent.ToLocal( Pos ) ) );

            base.Update( e );
        }

        private void StopDragging( )
        {
            if ( !Dragging )
                return;

            Dragging = false;
            DraggingEntity = null;
            LocalGrabPoint = Vector2.Zero;
        }
    }
}
