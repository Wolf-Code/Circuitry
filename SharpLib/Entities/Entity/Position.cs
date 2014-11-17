using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        #region Properties

        protected Vector2 m_Position;

        /// <summary>
        /// The entity's position.
        /// </summary>
        public Vector2 Position
        {
            set { SetPosition( value ); }
            get
            {
                return ( HasParent && IsParent<Entity>( ) )
                    ? GetParent<Entity>( ).ToWorld( m_Position )
                    : LocalPosition;
            }
        }

        /// <summary>
        /// The entity's position relative to its parent.
        /// </summary>
        /// <remarks> 
        /// If there is no parent, this returns the entity's actual position. 
        /// </remarks>
        public Vector2 LocalPosition
        {
            get { return m_Position; }
        }

        /// <summary>
        /// The entity's top left.
        /// </summary>
        public virtual Vector2 TopLeft
        {
            get { return Position - Size / 2f; }
        }

        /// <summary>
        /// The entity's bottom right.
        /// </summary>
        public Vector2 BottomRight
        {
            get { return TopLeft + Size; }
        }

        /// <summary>
        /// The entity's X-coordinate.
        /// </summary>
        public float X
        {
            set { SetPosition( value, Y ); }
            get { return Position.X; }
        }

        /// <summary>
        /// The entity's Y-coordinate.
        /// </summary>
        public float Y
        {
            set { SetPosition( X, value ); }
            get { return Position.Y; }
        }

        #endregion

        /// <summary>
        /// Gets called whenever the entity's position is changed.
        /// </summary>
        /// <param name="Old">The entity's old position.</param>
        /// <param name="New">The entity's new position.</param>
        protected virtual void OnReposition( Vector2 Old, Vector2 New )
        {

        }

        public void SetPosition( float NewX, float NewY )
        {
            Vector2 Old = Position;

            m_Position.X = NewX;
            m_Position.Y = NewY;

            OnReposition( Old, new Vector2( NewX, NewY ) );
        }

        public void SetPosition( Vector2 NewPosition )
        {
            SetPosition( NewPosition.X, NewPosition.Y );
        }

        public virtual bool ContainsPosition( Vector2 WorldPosition )
        {
            return BoundingVolume.Contains( WorldPosition );
        }

        #region ToLocal / ToWorld

        public virtual Vector2 ToLocal( Vector2 WorldPosition )
        {
            return WorldPosition - Position;
        }

        public virtual Vector2 ToWorld( Vector2 LocalPosition )
        {
            return Position + LocalPosition;
        }

        #endregion
    }
}
