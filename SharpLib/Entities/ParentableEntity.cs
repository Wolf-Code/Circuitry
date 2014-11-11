using System.Collections.Generic;
using System.Linq;
using SharpLib2D.States;

namespace SharpLib2D.Entities
{
    public class ParentableEntity : DrawableEntity
    {
        protected readonly List<ParentableEntity> Children = new List<ParentableEntity>( );

        public ParentableEntity Parent { protected set; get; }

        public bool HasParent
        {
            get { return Parent != null; }
        }

        public IEnumerable<T> GetChildren<T>( ) where T : ParentableEntity
        {
            return Children.OfType<T>( );
        }

        public void SetParent( ParentableEntity NewParent )
        {
            if ( this.HasParent )
            {
                this.Parent.Children.Remove( this );
                this.Parent.OnChildRemoved( this );
            }
            else if ( NewParent != null )
                Unlist( );


            ParentableEntity Old = this.Parent;
            this.Parent = NewParent;
            this.OnParentChanged( Old, NewParent );

            if ( this.Parent != null )
            {
                this.Parent.Children.Add( this );
                this.Parent.OnChildAdded( this );
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
                this.SetParent( null );
                Unlist( );
            }
            else
            {
                if ( State.ActiveState.Entities.Contains( this ) )
                    Unlist( );
            }
        }
    }
}
