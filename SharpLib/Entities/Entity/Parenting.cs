using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract partial class Entity
    {
        /// <summary>
        /// Gets all the children of type <paramref name="T"/> which contain <paramref name="CheckPosition"/>.
        /// </summary>
        /// <typeparam name="T">The type of children to check for.</typeparam>
        /// <param name="CheckPosition">The position to check for.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all children at position <paramref name="CheckPosition"/></returns>
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

        /// <summary>
        /// Returns a <see cref="List{T}"/> containing children, ordered by their Z-value.
        /// </summary>
        /// <typeparam name="T">The type of children to return.</typeparam>
        /// <returns>A <see cref="List{T}"/> containing all children of type <typeparamref name="T"/>, ordered by their Z-value.</returns>
        protected List<T> OrderedEntities<T>( ) where T : UpdatableEntity
        {
            return Children.OfType<T>( ).OrderByDescending( O => O.Z ).ToList( );
        }

        /// <summary>
        /// Gets the latest added child at <paramref name="WorldPosition"/>.
        /// </summary>
        /// <param name="WorldPosition">The world position to check for.</param>
        /// <returns>The top child, or null if there is none.</returns>
        public Entity GetTopChildAt( Vector2 WorldPosition )
        {
            IEnumerable<Entity> Ents = GetAllChildrenAtPosition<Entity>( WorldPosition );
            return Ents.OrderByDescending( O => O.Z ).FirstOrDefault( );
        }
    }
}
