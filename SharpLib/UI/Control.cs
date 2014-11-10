using System;
using System.Collections.Generic;
using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;
using SharpLib2D.Objects;

namespace SharpLib2D.UI
{
    public abstract class Control : MouseEntity, IDisposable
    {
        #region Properties
        protected new IEnumerable<Control> Children
        {
            get { return this.GetChildren<Control>( ); }
        }

        public bool PreventLeavingParent { set; get; }
        public bool IgnoreMouseInput { protected set; get; }

        #region Events

        public event EventHandler<Control> OnSizeChanged;
        #endregion

        protected Canvas Canvas 
        {
            get
            {
                MouseEntityContainer C = Container;
                return C is Canvas ? C as Canvas : null;
            }
        }

        public override Vector2 TopLeft
        {
            get { return Position; }
        }

        protected BoundingRectangle VisibleRectangle
        {
            get
            {
                if ( !this.HasParent ) return BoundingVolume.BoundingBox;

                BoundingRectangle ParentRect;
                if ( this.Parent is Control )
                    ParentRect = ( this.Parent as Control ).VisibleRectangle;
                else
                    ParentRect = ( ( ObjectEntity ) this.Parent ).BoundingVolume.BoundingBox;

                BoundingRectangle Clamped =
                    Math.BoundingVolumes.IntersectionRectangle( ParentRect,
                        BoundingVolume.BoundingBox );

                return Clamped;
            }
        }

        #endregion

        protected Control( )
        {
            this.PreventLeavingParent = false;
        }

        protected override void OnPositionChanged( Vector2 NewPosition )
        {
            if ( !PreventLeavingParent || !HasParent || !( Parent is ObjectEntity ) ) return;

            ObjectEntity C = Parent as ObjectEntity;

            if ( NewPosition.X < 0 )
                NewPosition.X = 0;

            if ( NewPosition.Y < 0 )
                NewPosition.Y = 0;

            if ( NewPosition.X + this.BoundingVolume.Width > C.BoundingVolume.Width )
                NewPosition.X = C.BoundingVolume.Width - this.BoundingVolume.Width;

            if ( NewPosition.Y + this.BoundingVolume.Height > C.BoundingVolume.Height )
                NewPosition.Y = C.BoundingVolume.Height - this.BoundingVolume.Height;

            m_Position = NewPosition;
        }

        public void Center( )
        {
            Vector2 Center;
            if ( HasParent )
            {
                ObjectEntity C = ( ObjectEntity ) Parent;
                Center = C.Size / 2f;
            }
            else
                Center = Canvas.Size / 2f;

            this.SetPosition( Center.X - this.BoundingVolume.Width / 2, Center.Y - this.BoundingVolume.Height / 2 );
        }

        public override MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            MouseEntity E = GetChildAt( CheckPosition ) as MouseEntity;
            return E != null ? E.GetTopChild( CheckPosition ) : ( this.IgnoreMouseInput ? this.Parent as MouseEntity : this );
        }

        protected virtual void DrawSelf( )
        {
            Canvas.Skin.DrawPanel( this );
        }

        public override void Draw( FrameEventArgs e )
        {
            BoundingRectangle Visible = this.VisibleRectangle;
            if ( Visible.Width <= 0 || Visible.Height <= 0 ) return;

            Scissor.SetScissorRectangle( Visible.Left, Visible.Top, Visible.Width, Visible.Height );
            DrawSelf( );

            base.Draw( e );
        }

        public virtual void Dispose( )
        {
            foreach ( Control C in this.Children )
                C.Dispose( );

            this.OnSizeChanged = null;
        }

        protected override void OnRemove( )
        {
            base.OnRemove( );

            this.Dispose( );
        }

        #region Events

        protected override void OnResize( Vector2 NewSize )
        {
            if ( this.OnSizeChanged != null )
                this.OnSizeChanged( this, this );
        }

        #endregion
    }
}
