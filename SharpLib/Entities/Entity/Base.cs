using OpenTK;
using SharpLib2D.Objects;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity : ParentableEntity, IPositionable, ISizable
    {
        public virtual BoundingVolume BoundingVolume
        {
            get { return new BoundingRectangle( this.TopLeft, this.TopLeft + this.Size ); }
        }

        protected Entity( )
        {
            this.m_Size = new Vector2( 100, 100 );
        }
    }
}
