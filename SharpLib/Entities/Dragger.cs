using System;
using OpenTK;
using OpenTK.Input;
using Mouse = SharpLib2D.Info.Mouse;

namespace SharpLib2D.Entities
{
    public class Dragger : ParentableEntity
    {
        public Entity DraggingEntity { private set; get; }
        public bool Dragging { private set; get; }
        public Vector2 LocalGrabPoint { private set; get; }

        public delegate void DraggerDropEventHandler( Dragger Dragger, Entity DroppedEntity );

        public event DraggerDropEventHandler OnDrop;

        public Dragger( )
        {
            Mouse.OnMouseButton += OnMouseButton;
        }

        protected override void OnRemove( )
        {
            Mouse.OnMouseButton -= OnMouseButton;
            OnDrop = null;
            base.OnRemove( );
        }

        private void OnMouseButton( object Sender, MouseButtonEventArgs MouseButtonEventArgs )
        {
            if ( MouseButtonEventArgs.Button == MouseButton.Left && !MouseButtonEventArgs.IsPressed )
                StopDragging( );
        }

        public void StartDragging( Entity E )
        {
            if ( Dragging )
                StopDragging( );

            Dragging = true;
            DraggingEntity = E;
            LocalGrabPoint = E.ToLocal( Mouse.WorldPosition );
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

            Vector2 DPos = Mouse.Position - LocalGrabPoint;
            Vector2 Pos = ParentState.Camera.ToWorld( DPos );

            DraggingEntity.SetPosition(
                TransformPosition( ( DraggingEntity.HasParent && DraggingEntity.Parent is Entity )
                    ? ( DraggingEntity.Parent as Entity ).ToLocal( Pos )
                    : Pos ) );

            base.Update( e );
        }

        private void StopDragging( )
        {
            if ( !Dragging )
                return;

            if ( OnDrop != null )
                OnDrop( this, DraggingEntity );

            Dragging = false;
            DraggingEntity = null;
            LocalGrabPoint = Vector2.Zero;
        }
    }
}
