using System.Linq;
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI
{
    public class CategoryList : ScrollablePanel
    {
        /// <summary>
        /// Adds a category to the category list and returns the new header.
        /// </summary>
        /// <param name="Title">The category's title.</param>
        /// <returns>A new <see cref="CategoryHeader"/>.</returns>
        public CategoryHeader AddCategory( string Title )
        {
            CategoryHeader Header = new CategoryHeader( Title );

            Header.SetWidth( this.ContentPanel.Width );
            Header.SetParent( this.ContentPanel );
            Header.SetPosition( 0, Items.Count > 0 ? Items.Max( O => O.LocalPosition.Y + O.Height ) : 0 );

            this.AddItem( Header );
            return Header;
        }

        protected override void OnItemAdded( Control C )
        {
            base.OnItemAdded( C );

            this.OnResize( this.Size, this.Size );
        }

        protected override void OnItemRemoved( Control C )
        {
            int ID = this.Items.IndexOf( C );
            base.OnItemRemoved( C );

            this.RepositionItemsFromIndex( ID );
        }

        private void RepositionItemsFromIndex( int StartIndex = 0 )
        {
            for ( int Q = StartIndex; Q < Items.Count; Q++ )
            {
                Control Item = Items[ Q ];
                if ( Q > 0 )
                    Item.MoveBelow( Items[ Q - 1 ] );
                else
                    Item.SetPosition( 0, 0 );
            }

            this.OnResize( this.Size, this.Size );
        }

        /// <summary>
        /// Notifies the category list that a header inside it has been opened or closed.
        /// </summary>
        public void CategoryItemToggled( )
        {
            this.RepositionItemsFromIndex( );
        }

        protected override void ScrollbarChanged( bool Horizontal, bool ScrollbarVisible )
        {
            if ( Horizontal ) return;

            if ( ScrollbarVisible )
                this.ContentPanel.SetWidth( this.Width - this.VerticalScrollBar.Width );
            else
                this.ContentPanel.SetWidth( this.Width );

            foreach( Control C in this.Items )
                C.SetWidth( this.ContentPanel.Width );
        }
    }
}
