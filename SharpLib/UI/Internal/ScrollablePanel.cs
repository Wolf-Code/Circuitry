using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Objects;

namespace SharpLib2D.UI.Internal
{
    public class ScrollablePanel : Panel
    {
        protected readonly InvisiblePanel ContentPanel;
        public readonly HorizontalScrollbar HorizontalScrollBar;
        public readonly VerticalScrollbar VerticalScrollBar;
        public bool ShowHorizontalScrollbar { set; get; }
        public bool ShowVerticalScrollbar { set; get; }
        protected readonly List<Control> Items = new List<Control>( );
        private Vector2 ScrollValues1;

        public event SharpLibUIEventHandler<ScrollablePanel> OnHorizontalScrollbarAdded;
        public event SharpLibUIEventHandler<ScrollablePanel> OnHorizontalScrollbarRemoved;

        public event SharpLibUIEventHandler<ScrollablePanel> OnVerticalScrollbarAdded;
        public event SharpLibUIEventHandler<ScrollablePanel> OnVerticalScrollbarRemoved;

        private bool ShouldHorizontalScrollbar
        {
            get { return ContentSize.X > this.Width && ShowHorizontalScrollbar; }
        }

        private bool ShouldVerticalScrollbar
        {
            get
            {
                return ContentSize.Y > this.Height && ShowVerticalScrollbar;
            }
        }

        protected Vector2 ContentSize
        {
            get
            {
                if ( Items.Count <= 0 )
                    return Vector2.Zero;

                return new Vector2(
                    Items.Max( O => O.LocalPosition.X + O.Width ),
                    Items.Max( O => O.LocalPosition.Y + O.Height ) );
            }
        }

        private Vector2 ScrollValues
        {
            set
            {
                ScrollValues1 = value;
                ScrollInternalPanel( );
            }
            get { return ScrollValues1; }
        }

        protected ScrollablePanel( )
        {
            this.ShowHorizontalScrollbar = false;
            this.ShowVerticalScrollbar = true;
            this.ContentPanel = new InvisiblePanel( );
            this.ContentPanel.SetParent( this );
            this.ContentPanel.SetSize( this.Size );
            this.ContentPanel.SetVisibleRegion( this.VisibleRectangle );

            this.HorizontalScrollBar = new HorizontalScrollbar
            {
                Value = 0,
                MinValue = 0,
                Visible = false,
            };
            this.HorizontalScrollBar.SetParent( this );
            this.HorizontalScrollBar.OnValueChanged += OnBarValueChanged;

            this.VerticalScrollBar = new VerticalScrollbar
            {
                Value = 0,
                MinValue = 0,
                Visible = false,
            };
            this.VerticalScrollBar.SetParent( this );
            this.VerticalScrollBar.OnValueChanged += OnBarValueChanged;

            ResizeScrollbars( );
        }

        private void CheckScrollbarRequirements( )
        {
            if ( HorizontalScrollBar.Visible != ShouldHorizontalScrollbar )
            {
                HorizontalScrollBar.Visible = ShouldHorizontalScrollbar;
                if ( !ShouldHorizontalScrollbar )
                {
                    ScrollValues = new Vector2( 0, ScrollValues.Y );
                    ScrollbarChanged( true, false );
                    if ( OnHorizontalScrollbarRemoved != null )
                        OnHorizontalScrollbarRemoved( this );
                }
                else
                {
                    ScrollbarChanged( true, true );
                    if ( OnHorizontalScrollbarAdded != null )
                        OnHorizontalScrollbarAdded( this );
                }
            }

            if ( ShouldHorizontalScrollbar )
                HorizontalScrollBar.MaxValue = ContentSize.X - this.Width + ( ShouldVerticalScrollbar ? this.VerticalScrollBar.Width : 0 );



            if ( this.VerticalScrollBar.Visible != ShouldVerticalScrollbar )
            {
                this.VerticalScrollBar.Visible = ShouldVerticalScrollbar;
                if ( !ShouldVerticalScrollbar )
                {
                    ScrollValues = new Vector2( ScrollValues.X, 0 );
                    ScrollbarChanged( false, false );
                    if ( OnVerticalScrollbarRemoved != null )
                        OnVerticalScrollbarRemoved( this );
                }
                else
                {
                    ScrollbarChanged( false, true );
                    if ( OnVerticalScrollbarAdded != null )
                        OnVerticalScrollbarAdded( this );
                }
            }

            if ( ShouldVerticalScrollbar )
                this.VerticalScrollBar.MaxValue = ContentSize.Y - this.Height + ( ShouldHorizontalScrollbar ? HorizontalScrollBar.Height : 0 );

            ResizeScrollbars( );
        }

        protected virtual void ScrollbarChanged( bool Horizontal, bool ScrollbarVisible )
        {

        }

        private void ResizeScrollbars( )
        {
            float W = this.Width;
            float H = this.Height;
            if ( ShouldHorizontalScrollbar && ShouldVerticalScrollbar )
            {
                W -= this.VerticalScrollBar.Width;
                H -= this.HorizontalScrollBar.Height;
            }

            HorizontalScrollBar.SetWidth( W );
            HorizontalScrollBar.AlignBottom( this );

            VerticalScrollBar.SetHeight( H );
            VerticalScrollBar.AlignRight( this );
        }

        private void OnBarValueChanged( Scrollbar.Scrollbar Control )
        {
            this.ScrollValues = Control == 
                HorizontalScrollBar 
                ? new Vector2( ( float ) Control.Value, ScrollValues.Y ) 
                : new Vector2( ScrollValues.X, ( float ) Control.Value );

            this.ScrollInternalPanel( );
        }

        private void ScrollInternalPanel( )
        {
            this.ContentPanel.SetPosition( -ScrollValues );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            base.OnResize( OldSize, NewSize );

            ResizeScrollbars( );
            CheckScrollbarRequirements( );
        }

        #region Items

        protected void AddItem( Control C )
        {
            Items.Add( C );
            C.SetParent( this.ContentPanel );
            C.Removed += COnOnRemoved;
            OnItemAdded( C );
        }

        protected void RemoveItem( Control C )
        {
            this.OnItemRemoved( C );
        }

        private void COnOnRemoved( Control Control )
        {
            this.OnItemRemoved( Control );
        }

        protected virtual void OnItemAdded( Control C )
        {
            this.ContentPanel.SetSize( this.ContentSize );
            this.CheckScrollbarRequirements( );
        }

        protected virtual void OnItemRemoved( Control C )
        {
            Items.Remove( C );
            C.Removed -= COnOnRemoved;

            this.ContentPanel.SetSize( this.ContentSize );
            this.CheckScrollbarRequirements( );
        }

        #endregion

        public override void Update( FrameEventArgs e )
        {
            float W = this.Width;
            if ( this.VerticalScrollBar.Visible )
                W -= this.VerticalScrollBar.Width;

            float H = this.Height;
            if ( HorizontalScrollBar.Visible )
                H -= HorizontalScrollBar.Height;

            this.ContentPanel.SetVisibleRegion( new BoundingRectangle( this.TopLeft.X, this.TopLeft.Y, W, H ) );

            base.Update( e );
        }
    }
}
