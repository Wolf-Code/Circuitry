using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract class Entity : ObjectEntity
    {
        protected Entity( )
        {
            this.m_Size = new Vector2( 100, 100 );
        }

        public override Vector2 TopLeft
        {
            get { return this.Position - this.Size / 2; }
        }
    }
}
