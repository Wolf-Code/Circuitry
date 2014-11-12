using OpenTK;
using OpenTK.Input;

namespace SharpLib2D.Entities
{
    public class MouseEntity : Entity
    {
        public MouseEntityContainer Container
        {
            get
            {
                if ( IsParent<MouseEntity>( ) )
                    return GetParent<MouseEntity>( ).Container;

                return this as MouseEntityContainer;
            }
        }

        public bool IsMouseOn { private set; get; }

        public virtual MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            MouseEntity E = GetTopChildAt( CheckPosition ) as MouseEntity;
            return E != null ? E.GetTopChild( CheckPosition ) : this;
        }

        public virtual void OnButtonPressed( MouseButton Button )
        {

        }

        public virtual void OnButtonReleased( MouseButton Button )
        {

        }

        public virtual void OnMouseEnter( )
        {
            this.IsMouseOn = true;
        }

        public virtual void OnMouseExit( )
        {
            this.IsMouseOn = false;
        }
    }
}
