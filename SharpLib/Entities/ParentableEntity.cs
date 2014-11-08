using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLib2D.States;

namespace SharpLib2D.Entities
{
    public class ParentableEntity : DrawableEntity
    {
        protected List<ParentableEntity> Children = new List<ParentableEntity>( );

        public ParentableEntity Parent { protected set; get; }

        public bool HasParent
        {
            get { return Parent != null; }
        }

        public void SetParent( ParentableEntity Parent )
        {
            if ( this.HasParent )
            {
                this.Parent.Children.Remove( this );
                this.Parent.OnChildRemoved( this );
            }

            this.OnParentChanged( this.Parent, Parent );
            this.Parent = Parent;
            if ( this.Parent != null )
            {
                this.Parent.Children.Add( this );
                this.Parent.OnChildAdded( this );
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
                this.SetParent( null );
            else
            {
                if ( State.ActiveState.Entities.Contains( this ) )
                    Unlist( );
            }
        }
    }
}
