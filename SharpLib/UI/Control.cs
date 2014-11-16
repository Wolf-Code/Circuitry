using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;
using SharpLib2D.Math;
using SharpLib2D.Objects;
using SharpLib2D.States;
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
            get { return GetChildren<Control>( ); }
        }

        public bool PreventLeavingParent { set; get; }
        public bool IgnoreMouseInput { set; get; }
        protected bool PreventDrawingOutsideVisibleArea { set; get; }

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
                if ( !HasParent ) return BoundingVolume.BoundingBox;

                BoundingRectangle ParentRect;
                if ( Parent is Control )
                    ParentRect = ( Parent as Control ).VisibleRectangle;
                else
                    ParentRect = ( ( Entity ) Parent ).BoundingVolume.BoundingBox;

                BoundingRectangle Clamped =
                    BoundingVolumes.IntersectionRectangle( ParentRect,
                        BoundingVolume.BoundingBox );

                return Clamped;
            }
        }

        #endregion

        protected Control( )
        {
            PreventLeavingParent = false;
            PreventDrawingOutsideVisibleArea = true;
            SetParent( ( ( UIState ) State.ActiveState ).Canvas );
        }

        protected Control( Control Parent )
        {
            PreventLeavingParent = false;
            PreventDrawingOutsideVisibleArea = true;
            SetParent( Parent );
        }

        #region Alignment

        public void Center( )
        {
            Vector2 Center = GetParent<Entity>( ).Size / 2f;

            SetPosition( Center.X - BoundingVolume.Width / 2, Center.Y - BoundingVolume.Height / 2 );
        }

        public void MoveBelow( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( this.LocalPosition.X, P.Y + C.Size.Y + Offset ) );
        }

        public void MoveAbove( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( this.LocalPosition.X, P.Y - this.Size.Y + Offset ) );
        }

        public void MoveRightOf( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( P.X + C.Size.X + Offset, this.LocalPosition.Y ) );
        }

        public void MoveLeftOf( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( P.X - this.Size.X + Offset, this.LocalPosition.Y ) );
        }

        #endregion

        public override MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            MouseEntity E = GetTopChildAt( CheckPosition ) as MouseEntity;
            return E != null ? E.GetTopChild( CheckPosition ) : ( IgnoreMouseInput ? Parent as MouseEntity : this );
        }

        #region Drawing

        protected virtual void DrawSelf( )
        {
            Canvas.Skin.DrawPanel( this );
        }

        public override void Draw( FrameEventArgs e )
        {
            BoundingRectangle Visible = VisibleRectangle;
            if ( Visible.Width <= 0 || Visible.Height <= 0 ) return;

            if ( !PreventDrawingOutsideVisibleArea )
            {
                Visible = this.GetParent<Control>( ).VisibleRectangle;
                if ( Visible.Width <= 0 || Visible.Height <= 0 ) return;
            }
            Scissor.SetScissorRectangle( Visible.Left, Visible.Top, Visible.Width, Visible.Height );
            DrawSelf( );

            base.Draw( e );
        }

        #endregion

        #region Removal

        public virtual void Dispose( )
        {
            foreach ( Control C in Children )
                C.Dispose( );

            SizeChanged = null;
            OnLeftClick = null;
            OnRightClick = null;
        }

        protected override void OnRemove( )
        {
            base.OnRemove( );

            Dispose( );
        }

        #endregion

        #region Events

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            if ( SizeChanged != null )
                SizeChanged( this );
        }

        protected override void OnReposition( Vector2 OldPosition, Vector2 NewPosition )
        {
            if ( !PreventLeavingParent || !HasParent || !( Parent is Entity ) ) return;

            Entity C = Parent as Entity;

            if ( NewPosition.X < 0 )
                NewPosition.X = 0;

            if ( NewPosition.Y < 0 )
                NewPosition.Y = 0;

            if ( NewPosition.X + BoundingVolume.Width > C.BoundingVolume.Width )
                NewPosition.X = C.BoundingVolume.Width - BoundingVolume.Width;

            if ( NewPosition.Y + BoundingVolume.Height > C.BoundingVolume.Height )
                NewPosition.Y = C.BoundingVolume.Height - BoundingVolume.Height;

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
