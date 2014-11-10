using OpenTK;
using SharpLib2D.States;

namespace SharpLib2D.Entities
{
    public abstract class UpdatableEntity
    {
        protected State ParentState { private set; get; }

        public float Z { internal set; get; }

        protected UpdatableEntity( )
        {
            ParentState = State.ActiveState;
            Enlist( );
        }

        /// <summary>
        /// Removes the entity from its state.
        /// </summary>
        public void Remove( )
        {
            OnRemove( );
        }

        /// <summary>
        /// Removes the entity from its state's entity list.
        /// </summary>
        public void Unlist( )
        {
            ParentState.RemoveEntity( this );
        }

        /// <summary>
        /// Enlists the entity to its state's entity list.
        /// </summary>
        public void Enlist( )
        {
            ParentState.AddEntity( this );
        }

        /// <summary>
        /// Gets called right before the entity is removed.
        /// </summary>
        protected virtual void OnRemove( )
        {
            Unlist( );
        }

        public virtual void Update( FrameEventArgs e )
        {

        }
    }
}
