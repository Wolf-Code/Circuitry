using System.Collections.Generic;
using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        public override void Draw( FrameEventArgs e )
        {
            List<DrawableEntity> Ents = OrderedEntities<DrawableEntity>( );
            foreach ( DrawableEntity T in Ents )
                T.Draw( e );
        }
    }
}
