using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        protected bool IsParent<T>( ) where T : ParentableEntity
        {
            return Parent is T;
        }

        protected T GetParent<T>( ) where T : ParentableEntity
        {
            return Parent as T;
        }

        protected List<T> OrderedEntities<T>( ) where T : UpdatableEntity
        {
            return Children.OrderByDescending( O => O.Z ).OfType<T>( ).ToList( );
        }

        public bool ContainsPosition( Vector2 WorldPosition )
        {
            return this.BoundingVolume.Contains( WorldPosition );
        }

        public Entity GetChildAt( Vector2 WorldPosition )
        {
            List<Entity> Ents = OrderedEntities<Entity>( );
            return Ents.FirstOrDefault( T => T.ContainsPosition( WorldPosition ) );
        }
    }
}
