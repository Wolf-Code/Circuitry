using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Objects;
using SharpLib2D.UI;

namespace SharpLib2D.Entities
{
    public abstract class ObjectEntity : ParentableEntity
    {
        #region Properties

        protected List<T> OrderedEntities<T>( ) where T : UpdatableEntity
        {
            return Children.OrderByDescending( O => O.Z ).OfType<T>( ).ToList( );
        }


        protected Vector2 m_Position;
        public Vector2 Position
        {
            protected set
            {
                m_Position = value;
                OnPositionChanged( value );
            }
            get
            {
                return ( HasParent && Parent is ObjectEntity )
                    ? ( Parent as ObjectEntity ).ToWorld( m_Position )
                    : m_Position;
            }
        }

        protected Vector2 m_Size;
        public Vector2 Size
        {
            protected set
            {
                m_Size = value;
                OnResize( value );
            }
            get { return m_Size; }
        }

        public virtual float X
        {
            get { return Position.X; }
        }

        public virtual float Y
        {
            get { return Position.Y; }
        }

        public virtual float Width
        {
            get { return Size.X; }
        }

        public virtual float Height
        {
            get { return Size.Y; }
        }
        #endregion

        #region Size

        public virtual Vector2 TopLeft
        {
            get { return Position; }
        }

        public Vector2 TopRight
        {
            get
            {
                return new Vector2( this.TopLeft.X + this.Width, this.TopLeft.Y );
            }
        }

        public Vector2 BottomRight
        {
            get
            {
                return TopLeft + Size;
            }
        }

        public Vector2 BottomLeft
        {
            get
            {
                return new Vector2( this.TopLeft.X, this.BottomRight.Y );
            }
        }

        public virtual BoundingVolume BoundingVolume
        {
            get
            {
                return new BoundingRectangle( TopLeft, BottomRight );
            }
        }

        #region SetSize

        public void SetSize( float W, float H )
        {
            Size = new Vector2( W, H );
        }

        public void SetSize( Vector2 NewSize )
        {
            Size = NewSize;
        }

        #endregion

        protected virtual void OnResize( Vector2 NewSize )
        {
            
        }

        #endregion

        #region Position

        protected virtual void OnPositionChanged( Vector2 NewPosition )
        {
            
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

        #endregion

        #region Collection

        public bool ContainsPosition( Vector2 WorldPosition )
        {
            return this.BoundingVolume.Contains( WorldPosition );
        }

        public ObjectEntity GetChildAt( Vector2 WorldPosition )
        {
            List<ObjectEntity> Ents = OrderedEntities<ObjectEntity>( );
            return Ents.FirstOrDefault( T => T.ContainsPosition( WorldPosition ) );
        }

        #endregion

        #region ToLocal / ToWorld

        public virtual Vector2 ToLocal( Vector2 WorldPosition )
        {
            return WorldPosition - this.Position;
        }

        public virtual Vector2 ToWorld( Vector2 LocalPosition )
        {
            return this.Position + LocalPosition;
        }

        #endregion

        #region Update / Draw

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

        #endregion
    }
}
