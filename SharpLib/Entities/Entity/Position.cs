using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        #region Properties

        protected Vector2 m_Position;

        public Vector2 Position
        {
            set { SetPosition( value ); }
            get
            {
                return ( HasParent && IsParent<Entity>(  ) )
                    ? GetParent<Entity>(  ).ToWorld( m_Position )
                    : m_Position;
            }
        }

        public Vector2 LocalPosition
        {
            get { return m_Position; }
        }

        public virtual Vector2 TopLeft
        {
            get { return Position; }
        }

        public float X
        {
            set { SetPosition( value, this.Y ); }
            get { return m_Position.X; }
        }

        public float Y
        {
            set { SetPosition( this.X, value ); }
            get { return m_Position.Y; }
        }

        #endregion

        protected virtual void OnReposition( Vector2 Old, Vector2 New )
        {

        }

        public void SetPosition( float NewX, float NewY )
        {
            Vector2 Old = this.Position;

            m_Position.X = NewX;
            m_Position.Y = NewY;

            OnReposition( Old, new Vector2( NewX, NewY ) );
        }

        public void SetPosition( Vector2 NewPosition )
        {
            SetPosition( NewPosition.X, NewPosition.Y );
        }

        #region ToLocal / ToWorld

        public virtual Vector2 ToLocal( Vector2 WorldPosition )
        {
            return WorldPosition - this.Position;
        }

        public virtual Vector2 ToWorld( Vector2 LocalPosition )
        {
            return this.Position + LocalPosition;
        }

        #endregion

    }
}
