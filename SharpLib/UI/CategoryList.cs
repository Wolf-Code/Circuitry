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
    }
}
