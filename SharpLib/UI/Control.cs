using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public event SharpLibUIEventHandler<Control> Removed; 

        public event SharpLibUIEventHandler<Control> OnLeftClick;
        public event SharpLibUIEventHandler<Control> OnRightClick; 
        #endregion

        #region Properties
        protected new IEnumerable<Control> Children
        {
            get { return GetChildren<Control>( ).Where( O => O.Visible ); }
        }

        /// <summary>
        /// Data which you can set to this control and retrieve later.
        /// </summary>
        public object UserData { set; get; }

        /// <summary>
        /// Prevents the control from being moved outside of its parent.
        /// </summary>
        public bool PreventLeavingParent { set; get; }

        /// <summary>
        /// Sends any mouse input it receives back to its parent.
        /// </summary>
        public bool IgnoreMouseInput { set; get; }

        /// <summary>
        /// Invisible controls don't receive any input and are invisible, and so are their children.
        /// They are, in fact, not even listed in the children list.
        /// </summary>
        public bool Visible { set; get; }

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

        /// <summary>
        /// The rectangle containing the area of the control which is actually visible.
        /// </summary>
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
            Visible = true;
            PreventLeavingParent = false;
            PreventDrawingOutsideVisibleArea = true;
            SetParent( ( ( UIState ) State.ActiveState ).Canvas );
        }

        protected Control( Control Parent )
        {
            Visible = true;
            PreventLeavingParent = false;
            PreventDrawingOutsideVisibleArea = true;
            SetParent( Parent );
        }

        #region Alignment

        /// <summary>
        /// Centers the control in its parent.
        /// </summary>
        public void Center( )
        {
            Vector2 Center = GetParent<Entity>( ).Size / 2f;

            SetPosition( Center.X - BoundingVolume.Width / 2, Center.Y - BoundingVolume.Height / 2 );
        }

        #region Move

        /// <summary>
        /// Moves the control below <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to move us below.</param>
        /// <param name="Offset">An optional additional offset to move it further down.</param>
        public void MoveBelow( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( this.LocalPosition.X, P.Y + C.Size.Y + Offset ) );
        }

        /// <summary>
        /// Moves the control above <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to move us above.</param>
        /// <param name="Offset">An optional additional offset to move it further up.</param>
        public void MoveAbove( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( this.LocalPosition.X, P.Y - this.Size.Y + Offset ) );
        }

        /// <summary>
        /// Moves the control to the right of <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to move us to the right of.</param>
        /// <param name="Offset">An optional additional offset to move it further to the right.</param>
        public void MoveRightOf( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( P.X + C.Size.X + Offset, this.LocalPosition.Y ) );
        }

        /// <summary>
        /// Moves the control to the left of <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to move us to the left of.</param>
        /// <param name="Offset">An optional additional offset to move it further to the left.</param>
        public void MoveLeftOf( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( P.X - this.Size.X + Offset, this.LocalPosition.Y ) );
        }

        #endregion Move

        #region Align

        /// <summary>
        /// Aligns the control's bottom with that of <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to align our bottom with.</param>
        /// <param name="Offset">An optional additional offset to move it further down.</param>
        public void AlignBottom( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( this.LocalPosition.X, P.Y + C.Size.Y - this.Size.Y + Offset ) );
        }

        /// <summary>
        /// Aligns the control's top with that of <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to align our top with.</param>
        /// <param name="Offset">An optional additional offset to move it further up.</param>
        public void AlignTop( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( this.LocalPosition.X, P.Y - Offset ) );
        }

        /// <summary>
        /// Aligns the control's right side with that of <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to align our right side with.</param>
        /// <param name="Offset">An optional additional offset to move it further to the right.</param>
        public void AlignRight( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( P.X + C.Size.X - this.Size.X + Offset, this.LocalPosition.Y ) );
        }

        /// <summary>
        /// Aligns the control's left side with that of <paramref name="C"/>.
        /// </summary>
        /// <param name="C">The control to align our left side with.</param>
        /// <param name="Offset">An optional additional offset to move it further to the left.</param>
        public void AlignLeft( Control C, float Offset = 0f )
        {
            Vector2 P = GetParent<Entity>( ).ToLocal( C.Position );
            SetPosition( new Vector2( P.X - Offset, this.LocalPosition.Y ) );
        }

        #endregion Align

        #endregion

        public override IEnumerable<T> GetAllChildrenAtPosition<T>( Vector2 CheckPosition )
        {
            List<T> Ents = new List<T>( );
            foreach ( T P in GetChildren<T>( ).Where( P => !( P is Control ) || ( P as Control ).Visible ) )
            {
                if ( P.ContainsPosition( CheckPosition ) )
                    Ents.Add( P );

                Ents.AddRange( P.GetAllChildrenAtPosition<T>( CheckPosition ) );
            }

            return Ents;
        }

        public override bool ContainsPosition( Vector2 WorldPosition )
        {
            return VisibleRectangle.Width > 0 && VisibleRectangle.Height > 0 &&
                   VisibleRectangle.Contains( WorldPosition );
        }

        public override MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            MouseEntity E = GetTopChildAt( CheckPosition ) as MouseEntity;
            return E != null ? E.GetTopChild( CheckPosition ) : ( IgnoreMouseInput ? Parent as MouseEntity : this );
        }

        #region Drawing

        /// <summary>
        /// Draws the control itself.
        /// </summary>
        protected virtual void DrawSelf( )
        {
            Canvas.Skin.DrawPanel( this );
        }

        public override void Draw( FrameEventArgs e )
        {
            if ( !this.Visible )
                return;

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

        /// <summary>
        /// Releases any resources used by the control, and clears all events.
        /// </summary>
        public virtual void Dispose( )
        {
            foreach ( Control C in Children )
                C.Dispose( );

            SizeChanged = null;
            OnLeftClick = null;
            OnRightClick = null;
            Removed = null;
        }

        protected override void OnRemove( )
        {
            base.OnRemove( );
            if ( Removed != null )
                Removed( this );

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
