using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        public bool IsParent<T>( ) where T : ParentableEntity
        {
            return Parent is T;
        }

        public T GetParent<T>( ) where T : ParentableEntity
        {
            return Parent as T;
        }

        public virtual IEnumerable<T> GetAllChildrenAtPosition<T>( Vector2 CheckPosition ) where T : Entity
        {
            List<T> Ents = new List<T>( );
            foreach ( T P in GetChildren<T>( ) )
            {
                if ( P.ContainsPosition( CheckPosition ) )
                    Ents.Add( P );

                Ents.AddRange( P.GetAllChildrenAtPosition<T>( CheckPosition ) );
            }

            return Ents;
        }

        protected List<T> OrderedEntities<T>( ) where T : UpdatableEntity
        {
            return Children.OrderByDescending( O => O.Z ).OfType<T>( ).ToList( );
        }

        public Entity GetTopChildAt( Vector2 WorldPosition )
        {
            IEnumerable<Entity> Ents = GetAllChildrenAtPosition<Entity>( WorldPosition );
            return Ents.OrderByDescending( O => O.Z ).FirstOrDefault( );
        }
    }
}
