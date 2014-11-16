using OpenTK;
using OpenTK.Graphics;
using SharpLib2D.Info;

namespace SharpLib2D.UI.Internal
{
    public class CategoryHeader : Control
    {
        #region Button

        public class CategoryButton : Button
        {

        }

        #endregion Button

        #region Title

        public class CategoryHeaderTitle : Button
        {
            public CategoryHeaderTitle( CategoryHeader Header )
            {
                this.SetParent( Header );
                this.OnClick += Control => Header.Opened = !Header.Opened;
                this.HorizontalAlignment = Directions.HorizontalAlignment.Center;
                this.VerticalAlignment = Directions.VerticalAlignment.Center;
            }

            protected override void DrawSelf( )
            {
                Graphics.Text.SetAlignments( HorizontalAlignment, VerticalAlignment );
                Graphics.Text.DrawString( this.Text, "Arial", 9f, this.Position + this.Size / 2f,
                    this.GetParent<CategoryHeader>( ).Opened ? Color4.Black : Color4.Gray );
            }
        }

        public class CategoryHeaderContainer : Panel
        {
            public CategoryHeaderContainer( CategoryHeader Header )
            {
                this.SetParent( Header );
            }

            protected override void DrawSelf( )
            {
                
            }
        }

        #endregion

        public bool Opened
        {
            private set; get;
        }

        public string Title
        {
            set { TitleBar.SetText( value ); }
            get { return TitleBar.Text; }
        }

        public CategoryHeaderTitle TitleBar { private set; get; }
        public new CategoryHeaderContainer Container { private set; get; }
        private const float HeaderHeight = 30;

        public CategoryHeader( string Title )
        {
            this.TitleBar = new CategoryHeaderTitle( this );
            this.Container = new CategoryHeaderContainer( this );
            this.SetHeight( HeaderHeight );

            this.Container.MoveBelow( this.TitleBar );
            this.Title = Title;
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            NewSize.Y = System.Math.Max( HeaderHeight, NewSize.Y );

            this.TitleBar.SetSize( NewSize.X, HeaderHeight );
            this.Container.SetSize( NewSize.X, NewSize.Y - HeaderHeight );

            base.OnResize( OldSize, NewSize );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawCategoryHeader( this );
        }
    }
}
