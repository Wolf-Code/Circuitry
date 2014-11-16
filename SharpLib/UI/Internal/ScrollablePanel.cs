using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Objects;
using SharpLib2D.UI.Internal.Scrollbar;

namespace SharpLib2D.UI.Internal
{
    public class ScrollablePanel : Panel
    {
        private readonly InvisiblePanel Pnl;
        private readonly HorizontalScrollbar HSBar;
        private readonly VerticalScrollbar VSBar;
        public bool ShowHorizontalScrollbar { set; get; }
        public bool ShowVerticalScrollbar { set; get; }
        protected readonly List<Control> Items = new List<Control>( );
        private Vector2 ScrollValues1;

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

        public ScrollablePanel( )
        {
            this.ShowHorizontalScrollbar = false;
            this.ShowVerticalScrollbar = true;
            this.Pnl = new InvisiblePanel( );
            this.Pnl.SetParent( this );
            this.Pnl.SetSize( this.Size );
            this.Pnl.SetVisibleRegion( this.VisibleRectangle );

            this.HSBar = new HorizontalScrollbar
            {
                Value = 0,
                MinValue = 0,
                Visible = false,
            };
            this.HSBar.SetParent( this );
            this.HSBar.OnValueChanged += OnBarValueChanged;

            this.VSBar = new VerticalScrollbar
            {
                Value = 0,
                MinValue = 0,
                Visible = false,
            };
            this.VSBar.SetParent( this );
            this.VSBar.OnValueChanged += OnBarValueChanged;

            ResizeScrollbars( );
        }

        private void CheckScrollbarRequirements( )
        {
            if ( !HSBar.Visible == ShouldHorizontalScrollbar )
            {
                HSBar.Visible = ShouldHorizontalScrollbar;
                if ( !ShouldHorizontalScrollbar )
                    ScrollValues = new Vector2( 0, ScrollValues.Y );
            }

            if ( ShouldHorizontalScrollbar )
                HSBar.MaxValue = ContentSize.X - this.Width + ( VSBar != null ? VSBar.Width : 0 );



            if ( !VSBar.Visible == ShouldVerticalScrollbar )
            {
                VSBar.Visible = ShouldVerticalScrollbar;
                if ( !ShouldVerticalScrollbar )
                    ScrollValues = new Vector2( ScrollValues.X, 0 );
            }

            if ( ShouldVerticalScrollbar )
                VSBar.MaxValue = ContentSize.Y - this.Height + ( HSBar != null ? HSBar.Height : 0 );

            ResizeScrollbars( );
        }

        private void ResizeScrollbars( )
        {
            float W = this.Width;
            float H = this.Height;
            if ( ShouldHorizontalScrollbar && ShouldVerticalScrollbar )
            {
                W -= this.VSBar.Width;
                H -= this.HSBar.Height;
            }

            HSBar.SetWidth( W );
            HSBar.AlignBottom( this );

            VSBar.SetHeight( H );
            VSBar.AlignRight( this );
        }

        private void OnBarValueChanged( Scrollbar.Scrollbar Control )
        {
            this.ScrollValues = Control == 
                HSBar 
                ? new Vector2( ( float ) Control.Value, ScrollValues.Y ) 
                : new Vector2( ScrollValues.X, ( float ) Control.Value );

            this.ScrollInternalPanel( );
        }

        private void ScrollInternalPanel( )
        {
            this.Pnl.SetPosition( -ScrollValues );
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
            C.SetParent( this.Pnl );
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
            this.Pnl.SetSize( this.ContentSize );
            this.CheckScrollbarRequirements( );
        }

        protected virtual void OnItemRemoved( Control C )
        {
            Items.Remove( C );
            C.Removed -= COnOnRemoved;

            this.Pnl.SetSize( this.ContentSize );
            this.CheckScrollbarRequirements( );
        }

        #endregion

        public override void Update( FrameEventArgs e )
        {
            float W = this.Width;
            if ( VSBar.Visible )
                W -= VSBar.Width;

            float H = this.Height;
            if ( HSBar.Visible )
                H -= HSBar.Height;

            this.Pnl.SetVisibleRegion( new BoundingRectangle( this.TopLeft.X, this.TopLeft.Y, W, H ) );

            base.Update( e );
        }
    }
}
