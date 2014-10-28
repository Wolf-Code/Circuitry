using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Objects;

namespace SharpLib2D.Entities
{
    public abstract class Entity : DrawableEntity
    {
        protected Entity( )
        {
            SetSize( 100, 100 );
        }

        #region Size

        public Vector2 Size
        {
            protected set;
            get;
        }

        public virtual Vector2 TopLeft
        {
            get { return Position - Size / 2; }
        }

        public virtual Vector2 BottomRight
        {
            get
            {
                return Position + Size / 2;
            }
        }

        public virtual BoundingBox BoundingBox
        {
            get { return new BoundingBox( TopLeft, BottomRight ); }
        }

        #region SetSize

        public void SetSize( float Width, float Height )
        {
            Size = new Vector2( Width, Height );
        }

        public void SetSize( Vector2 NewSize )
        {
            Size = NewSize;
        }

        #endregion

        #endregion

        #region Position

        private Vector2 LPos;

        /// <summary>
        /// The entity's local position.
        /// </summary>
        public Vector2 Position
        {
            private set
            {
                LPos = value;
            }
            get
            {
                return HasParent ? Parent.ToWorld( LPos ) : LPos;
            }
        }

        #region SetPosition

        /// <summary>
        /// Sets the entity's position.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void SetPosition( float X, float Y )
        {
            Position = new Vector2( X, Y );
        }

        /// <summary>
        /// Sets the entity's position.
        /// </summary>
        /// <param name="NewPosition">The new position.</param>
        public void SetPosition( Vector2 NewPosition )
        {
            SetPosition( NewPosition.X, NewPosition.Y );
        }

        #endregion

        #region ToLocal / ToWorld

        public Vector2 ToLocal( float WorldX, float WorldY )
        {
            return ToLocal( new Vector2( WorldX, WorldY ) );
        }

        public virtual Vector2 ToLocal( Vector2 WorldPosition )
        {
            return WorldPosition - Position;
        }

        public Vector2 ToWorld( float LocalX, float LocalY )
        {
            return ToWorld( new Vector2( LocalX, LocalY ) );
        }

        public virtual Vector2 ToWorld( Vector2 LocalPosition )
        {
            return Position + LocalPosition;
        }

        #endregion

        #endregion

        #region Parenting

        public Entity Parent { private set; get; }
        protected List<Entity> Children = new List<Entity>( );

        protected List<Entity> OrderedEntities
        {
            get { return Children.OrderByDescending( O => O.Z ).ToList( ); }
        }

        public bool HasParent
        {
            get { return Parent != null; }
        }

        public void SetParent( Entity Ent )
        {
            if ( Parent == Ent )
                return;

            if ( HasParent )
            {
                Parent.OnChildRemoved( this );
                Parent.Children.Remove( this );
            }
            else
                Unlist( );

            Parent = Ent;
            if ( HasParent )
            {
                Parent.OnChildAdded( this );
                Parent.Children.Add( this );
                Z = Parent.Z + 1;
            }
            else
                Enlist( );

            OnParentChanged( Ent );
        }

        protected virtual void OnChildAdded( Entity Child )
        {
            
        }

        protected virtual void OnChildRemoved( Entity Child )
        {
            
        }

        protected virtual void OnParentChanged( Entity NewParent )
        {
            
        }

        #endregion

        #region Collection

        public bool ContainsPosition( Vector2 WorldPosition )
        {
            return WorldPosition.X >= TopLeft.X && 
                   WorldPosition.Y >= TopLeft.Y && 
                   WorldPosition.X <= BottomRight.X &&
                   WorldPosition.Y <= BottomRight.Y;
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

            while ( Children.Count > 0 )
                Children[ 0 ].Remove( );

            if ( this.HasParent )
                this.Parent.Children.Remove( this );
            else
                Unlist( );
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
