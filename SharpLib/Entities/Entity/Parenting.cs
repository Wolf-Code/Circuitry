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

        private List<Entity> GetAllChildrenAtPosition( Vector2 CheckPosition )
        {
            List<Entity> Ents = new List<Entity>( );
            foreach ( Entity P in GetChildren<Entity>( ) )
            {
                if ( P.ContainsPosition( CheckPosition ) )
                    Ents.Add( P );

                Ents.AddRange( P.GetAllChildrenAtPosition( CheckPosition ) );
            }

            return Ents;
        }

        protected List<T> OrderedEntities<T>( ) where T : UpdatableEntity
        {
            return Children.OrderByDescending( O => O.Z ).OfType<T>( ).ToList( );
        }

        public Entity GetTopChildAt( Vector2 WorldPosition )
        {
            List<Entity> Ents = GetAllChildrenAtPosition( WorldPosition );
            return Ents.OrderByDescending( O => O.Z ).FirstOrDefault( );
        }
    }
}
