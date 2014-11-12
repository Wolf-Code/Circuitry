using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        protected Vector2 m_Size;

        public Vector2 Size
        {
            get { return m_Size; }
            set { SetSize( value ); }
        }

        public float Width
        {
            get { return m_Size.X; }
            set { SetSize( value, Height ); }
        }

        public float Height
        {
            get { return m_Size.Y; }
            set { SetSize( Width, value ); }
        }

        protected virtual void OnResize( Vector2 OldSize, Vector2 NewSize )
        {

        }

        public void SetWidth( float NewWidth )
        {
            SetSize( NewWidth, Height );
        }

        public void SetHeight( float NewHeight )
        {
            SetSize( Width, NewHeight );
        }

        public void SetSize( float NewWidth, float NewHeight )
        {
            Vector2 OldSize = Size;

            m_Size.X = NewWidth;
            m_Size.Y = NewHeight;

            OnResize( OldSize, m_Size );
        }

        public void SetSize( Vector2 NewSize )
        {
            SetSize( NewSize.X, NewSize.Y );
        }
    }
}
