using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace SharpLib2D.Entities
{
    public abstract class Entity : ObjectEntity
    {
        protected Entity( )
        {
            this.m_Size = new Vector2( 100, 100 );
        }

        #region Parenting

        protected List<Entity> OrderedEntities
        {
            get { return Children.OrderByDescending( O => O.Z ).OfType<Entity>( ).ToList( ); }
        }

        protected override void OnParentChanged( ParentableEntity OldParent, ParentableEntity NewParent )
        {
            if ( NewParent != null )
            {
                if ( OldParent == null )
                    Unlist( );
            }
            else if ( OldParent != null )
                Enlist( );
        }

        #endregion

        #region Collection

        public bool ContainsPosition( Vector2 WorldPosition )
        {
            return this.BoundingVolume.Contains( WorldPosition );
        }

        public Entity GetChildAt( Vector2 WorldPosition )
        {
            List<Entity> Ents = OrderedEntities;
            return Ents.FirstOrDefault( T => T.ContainsPosition( WorldPosition ) );
        }

        #endregion

        public override void Remove( )
        {
            OnRemove( );

            base.Remove( );
        }

        public override void Update( FrameEventArgs e )
        {
            List<Entity> Ents = OrderedEntities;
            // ReSharper disable once ForCanBeConvertedToForeach
            for ( int X = 0; X < Ents.Count; X++ )
                Ents[ X ].Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            List<Entity> Ents = OrderedEntities;
            foreach ( Entity T in Ents )
                T.Draw( e );
        }
    }
}
