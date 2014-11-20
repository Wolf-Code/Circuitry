using System.Collections.Generic;
using System.Linq;
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
            public readonly bool Even;

            public CategoryButton( CategoryHeader Header, bool Even ) : base( Header.Container )
            {
                this.Even = Even;
            }

            protected override void DrawSelf( )
            {
                Canvas.Skin.DrawCategoryButton( this );
                this.DrawLabel( );
            }
        }

        #endregion Button

        #region Title

        public class CategoryHeaderTitle : Button
        {
            public CategoryHeaderTitle( CategoryHeader Header ) : base( Header )
            {
                this.OnClick += Control => Header.Opened = !Header.Opened;
                this.HorizontalAlignment = Directions.HorizontalAlignment.Center;
                this.VerticalAlignment = Directions.VerticalAlignment.Center;
            }

            protected override void DrawSelf( )
            {
                Graphics.Text.SetAlignments( HorizontalAlignment, VerticalAlignment );
                Graphics.Text.DrawString( this.Text, "Arial", 10f, this.Position + this.Size / 2f,
                    this.GetParent<CategoryHeader>( ).Opened ? Color4.DimGray : Color4.DarkGray );
            }
        }

        #endregion

        #region Container

        public class CategoryHeaderContainer : Panel
        {
            public CategoryHeaderContainer( CategoryHeader Header ) : base( Header )
            {
                
            }

            protected override void DrawSelf( )
            {

            }

            protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
            {
                foreach ( CategoryButton B in this.GetChildren<CategoryButton>( ) )
                    B.SetWidth( this.Width );

                base.OnResize( OldSize, NewSize );
            }
        }

        #endregion

        private bool m_Opened;
        /// <summary>
        /// Indicates whether the category is open.
        /// </summary>
        public bool Opened
        {
            private set
            {
                if ( value )
                {
                    this.SetHeight( this.TitleBar.Height + ContentSize.Y );
                    this.Container.SetHeight( ContentSize.Y );
                }
                else
                {
                    this.SetHeight( this.TitleBar.Height );
                    this.Container.SetHeight( 0 );
                }

                m_Opened = value;
                this.Parent.GetParent<CategoryList>( ).CategoryItemToggled( );
            }
            get { return m_Opened; }
        }

        /// <summary>
        /// The title of the category header.
        /// </summary>
        public string Title
        {
            set { TitleBar.SetText( value ); }
            get { return TitleBar.Text; }
        }

        private Vector2 ContentSize
        {
            get
            {
                return new Vector2(
                    Buttons.Max( O => O.LocalPosition.X + O.Width ),
                    Buttons.Max( O => O.LocalPosition.Y + O.Height ) );
            }
        }

        public CategoryHeaderTitle TitleBar { private set; get; }
        public new CategoryHeaderContainer Container { private set; get; }
        private readonly List<CategoryButton> Buttons = new List<CategoryButton>( );
 
        private const float HeaderHeight = 30;

        public CategoryHeader( CategoryList List, string Title ) : base( List )
        {
            this.TitleBar = new CategoryHeaderTitle( this );
            this.Container = new CategoryHeaderContainer( this );
            this.SetHeight( HeaderHeight );

            this.Container.MoveBelow( this.TitleBar );
            this.Title = Title;
        }

        public CategoryButton AddButton( string Text )
        {
            CategoryButton B = new CategoryButton( this, Buttons.Count % 2 == 0 )
            {
                FontSize = 8f,
            };
            B.SetText( Text );
            B.SizeToContents( );
            B.SetWidth( this.Width );
            B.SetHeight( B.Height + 5 );
            B.SetPosition( 0, Buttons.Count > 0 ? Buttons.Max( O => O.LocalPosition.Y + O.Height ) : 0 );
            Buttons.Add( B );

            return B;
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
