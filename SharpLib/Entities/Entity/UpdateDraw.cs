using System.Collections.Generic;
using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        public override void Update( FrameEventArgs e )
        {
            List<UpdatableEntity> Ents = OrderedEntities<UpdatableEntity>( ); ;
            // ReSharper disable once ForCanBeConvertedToForeach
            for ( int Q = 0; Q < Ents.Count; Q++ )
                Ents[ Q ].Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            List<DrawableEntity> Ents = OrderedEntities<DrawableEntity>( );
            foreach ( DrawableEntity T in Ents )
                T.Draw( e );
        }
    }
}
