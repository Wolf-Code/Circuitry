using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI
{
    public class CategoryList : ScrollablePanel
    {
        public CategoryList( )
        {

        }

        public CategoryHeader AddCategory( string Title )
        {
            CategoryHeader Header = new CategoryHeader( Title );
            this.AddItem( Header );

            return Header;
        }

        public void CategoryItemToggled( )
        {
            this.OnResize( this.Size, this.Size );
        }

        protected override void ScrollbarChanged( bool Horizontal, bool Visible )
        {
            if ( Horizontal ) return;

            if ( Visible )
                this.ContentPanel.SetWidth( this.Width - this.VerticalScrollBar.Width );
            else
                this.ContentPanel.SetWidth( this.Width );

            foreach( Control C in this.Items )
                C.SetWidth( this.ContentPanel.Width );
        }
    }
}
