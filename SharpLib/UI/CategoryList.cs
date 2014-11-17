using System.Linq;
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI
{
    public class CategoryList : ScrollablePanel
    {
        public CategoryHeader AddCategory( string Title )
        {
            CategoryHeader Header = new CategoryHeader( Title );

            Header.SetParent( this.ContentPanel );
            Header.SetPosition( 0, Items.Count > 0 ? Items.Max( O => O.LocalPosition.Y + O.Height ) : 0 );

            this.AddItem( Header );
            return Header;
        }

        public void CategoryItemToggled( )
        {
            for ( int Q = 0; Q < Items.Count; Q++ )
            {
                Control Item = Items[ Q ];
                if ( Q > 0 )
                    Item.MoveBelow( Items[ Q - 1 ] );
                else
                    Item.SetPosition( 0, 0 );
            }

            this.OnResize( this.Size, this.Size );
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
