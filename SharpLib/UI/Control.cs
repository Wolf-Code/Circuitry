using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;
using SharpLib2D.Objects;
using Mouse = SharpLib2D.Info.Mouse;

namespace SharpLib2D.UI
{
    public abstract class Control : MouseEntity, IDisposable
    {
        /// <summary>
        /// Handles UI events.
        /// </summary>
        /// <param name="Control">The control for which the event occured.</param>
        public delegate void SharpLibUIEventHandler<in T>( T Control ) where T : Control;

        private readonly Hashtable ClickPositions = new Hashtable( );

        #region Events

        /// <summary>
        /// Gets raised whenever the control is resized.
        /// </summary>
        public event SharpLibUIEventHandler<Control> SizeChanged;

        public event SharpLibUIEventHandler<Control> OnLeftClick;
        public event SharpLibUIEventHandler<Control> OnRightClick; 
        #endregion

        #region Properties
        protected new IEnumerable<Control> Children
        {
            get { return this.GetChildren<Control>( ); }
        }

        public bool PreventLeavingParent { set; get; }
        public bool IgnoreMouseInput { set; get; }

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

        public virtual BoundingRectangle VisibleRectangle
        {
            get
            {
                if ( !this.HasParent ) return BoundingVolume.BoundingBox;

                BoundingRectangle ParentRect;
                if ( this.Parent is Control )
                    ParentRect = ( this.Parent as Control ).VisibleRectangle;
                else
                    ParentRect = ( ( Entity ) this.Parent ).BoundingVolume.BoundingBox;

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

        #region Alignment

        public void Center( )
        {
            Vector2 Center;
            if ( HasParent )
            {
                Entity C = ( Entity ) Parent;
                Center = C.Size / 2f;
            }
            else
                Center = Canvas.Size / 2f;

            this.SetPosition( Center.X - this.BoundingVolume.Width / 2, Center.Y - this.BoundingVolume.Height / 2 );
        }

        public void MoveBelow( Control C, float Offset = 0f )
        {
            Vector2 P;
            if ( HasParent )
                P = ( Parent as Entity ).ToLocal( C.Position );
        }

        #endregion

        public override MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            MouseEntity E = GetChildAt( CheckPosition ) as MouseEntity;
            return E != null ? E.GetTopChild( CheckPosition ) : ( this.IgnoreMouseInput ? this.Parent as MouseEntity : this );
        }

        #region Drawing

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

        #endregion

        #region Removal

        public virtual void Dispose( )
        {
            foreach ( Control C in this.Children )
                C.Dispose( );

            this.SizeChanged = null;
            this.OnLeftClick = null;
            this.OnRightClick = null;
        }

        protected override void OnRemove( )
        {
            base.OnRemove( );

            this.Dispose( );
        }

        #endregion

        #region Events

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            if ( this.SizeChanged != null )
                this.SizeChanged( this );
        }

        protected override void OnReposition( Vector2 OldPosition, Vector2 NewPosition )
        {
            if ( !PreventLeavingParent || !HasParent || !( Parent is Entity ) ) return;

            Entity C = Parent as Entity;

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

        #region Mouse Input

        public override void OnButtonPressed( MouseButton Button )
        {
            ClickPositions[ Button ] = Mouse.Position;
            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( ClickPositions[ Button ] != null && ( Vector2 )ClickPositions[ Button ] == Mouse.Position )
            {
                switch ( Button )
                {
                    case MouseButton.Left:
                        if ( OnLeftClick != null )
                            OnLeftClick( this );
                        break;

                    case MouseButton.Right:
                        if ( OnRightClick != null )
                            OnRightClick( this );
                        break;
                }
            }
            base.OnButtonReleased( Button );
        }

        #endregion

        #endregion
    }
}
