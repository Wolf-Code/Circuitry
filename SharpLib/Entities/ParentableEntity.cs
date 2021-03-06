﻿using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.States;

namespace SharpLib2D.Entities
{
    public class ParentableEntity : DrawableEntity
    {
        protected readonly List<ParentableEntity> Children = new List<ParentableEntity>( );

        public ParentableEntity Parent { private set; get; }

        public bool HasParent
        {
            get { return Parent != null; }
        }

        public bool IsParent<T>( ) where T : ParentableEntity
        {
            return Parent is T;
        }

        public T GetParent<T>( ) where T : ParentableEntity
        {
            return Parent as T;
        }

        public IEnumerable<T> GetChildren<T>( ) where T : ParentableEntity
        {
            return Children.OfType<T>( );
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

        public void SetParent( ParentableEntity NewParent )
        {
            if ( HasParent )
            {
                Parent.Children.Remove( this );
                Parent.OnChildRemoved( this );
            }
            else if ( NewParent != null )
                Unlist( );


            ParentableEntity Old = Parent;
            Parent = NewParent;
            OnParentChanged( Old, NewParent );

            if ( Parent != null )
            {
                Parent.Children.Add( this );
                Parent.OnChildAdded( this );
            }
            else
            {
                if ( Old != null )
                    Enlist( );
            }
        }

        protected virtual void OnParentChanged( ParentableEntity OldParent, ParentableEntity NewParent )
        {

        }

        protected virtual void OnChildAdded( ParentableEntity NewChild )
        {

        }

        protected virtual void OnChildRemoved( ParentableEntity OldChild )
        {

        }

        protected override void OnRemove( )
        {
            while ( Children.Count > 0 )
                Children[ 0 ].Remove( );

            if ( HasParent )
            {
                SetParent( null );
                Unlist( );
            }
            else if ( State.ActiveState.Entities.Contains( this ) )
                Unlist( );
        }

        public override void Update( FrameEventArgs e )
        {
            List<UpdatableEntity> Ents = OrderedEntities<UpdatableEntity>( );

            // ReSharper disable once ForCanBeConvertedToForeach
            for ( int Q = 0; Q < Ents.Count; Q++ )
                Ents[ Q ].Update( e );
        }
    }
}
