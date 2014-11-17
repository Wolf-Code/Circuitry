using OpenTK;
using SharpLib2D.Objects;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity : ParentableEntity, IPositionable, ISizable
    {
        /// <summary>
        /// The bounding volume containing the entity in its entirety
        /// </summary>
        public virtual BoundingVolume BoundingVolume
        {
            get { return new BoundingRectangle( TopLeft, TopLeft + Size ); }
        }

        protected Entity( )
        {
            m_Size = new Vector2( 100, 100 );
        }
    }
}
