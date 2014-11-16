using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using OpenTK;
using SharpLib2D.UI.Internal;
using SharpLib2D.UI.Internal.Scrollbar;

namespace SharpLib2D.UI
{
    public class ScrollablePanel : Panel
    {
        private InvisiblePanel Pnl;
        private HorizontalScrollbar HSBar;
        private VerticalScrollbar VSBar;
        public bool ShowHorizontalScrollbar { set; get; }
        public bool ShowVerticalScrollbar { set; get; }
        private readonly List<Control> Items = new List<Control>( );

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

        private Vector2 ContentSize
        {
            get
            {
                return new Vector2(
                    Items.Max( O => O.LocalPosition.X + O.Width ),
                    Items.Max( O => O.LocalPosition.Y + O.Height ) );
            }
        }

        private Vector2 ScrollValues = Vector2.Zero;

        public ScrollablePanel( )
        {
            this.ShowHorizontalScrollbar = false;
            this.ShowVerticalScrollbar = true;
            this.Pnl = new InvisiblePanel( );
            this.Pnl.SetParent( this );
            this.Pnl.SetSize( this.Size );
        }

        private void CheckScrollbarRequirements( )
        {
            if ( ShouldHorizontalScrollbar )
            {
                if ( HSBar == null )
                {
                    HSBar = new HorizontalScrollbar( );
                    HSBar.SetParent( this );
                    HSBar.OnValueChanged += HsBarOnOnValueChanged;
                    Pnl.SetHeight( this.Height - HSBar.Height );
                }

                HSBar.AlignBottom( this );
                HSBar.SetWidth( this.Width );
            }
            else
            {
                if ( HSBar != null )
                {
                    Pnl.SetHeight( this.Height );
                    HSBar.Remove( );
                    HSBar = null;
                    ScrollValues.X = 0;
                }
            }

            if ( ShouldVerticalScrollbar )
            {
                if ( VSBar == null )
                {
                    VSBar = new VerticalScrollbar( );
                    VSBar.SetParent( this );
                    VSBar.MinValue = 0;
                    VSBar.Value = 0;
                    VSBar.OnValueChanged += VsBarOnOnValueChanged;
                    Pnl.SetWidth( this.Width - VSBar.Width );
                }

                VSBar.AlignRight( this );
                VSBar.SetHeight( this.Height );
                VSBar.MaxValue =  ContentSize.Y - this.Height;
            }
            else
            {
                if ( VSBar != null )
                {
                    Pnl.SetWidth( this.Width );
                    VSBar.Remove( );
                    VSBar = null;
                    ScrollValues.Y = 0;
                }
            }
        }

        private void HsBarOnOnValueChanged( Scrollbar Control )
        {
            this.ScrollValues.X = ( float )Control.Value;
            this.ScrollInternalPanel(  );
        }

        private void VsBarOnOnValueChanged( Scrollbar Control )
        {
            this.ScrollValues.Y = ( float ) Control.Value;
            this.ScrollInternalPanel(  );
        }

        private void ScrollInternalPanel( )
        {
            this.Pnl.SetPosition( -ScrollValues );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            base.OnResize( OldSize, NewSize );

            CheckScrollbarRequirements( );
        }

        public void AddItem( Control C )
        {
            Items.Add( C );
            C.SetParent( this.Pnl );
            C.Removed += COnOnRemoved;
            OnItemAdded( C );
        }

        public void RemoveItem( Control C )
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
    }
}
