using System;
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
            public bool Even { internal set; get; }

            public CategoryButton( CategoryHeader Header )
            {
                this.SetParent( Header.Container );
            }

            protected override void DrawSelf( )
            {
                this.DrawLabel( );
            }
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

        private bool m_Opened;
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

        public CategoryHeader( string Title )
        {
            this.TitleBar = new CategoryHeaderTitle( this );
            this.Container = new CategoryHeaderContainer( this );
            this.SetHeight( HeaderHeight );

            this.Container.MoveBelow( this.TitleBar );
            this.Title = Title;
        }

        public CategoryButton AddButton( string Text )
        {
            CategoryButton B = new CategoryButton( this ) { Even = Buttons.Count % 2 == 0 };
            B.SetText( Text );
            B.SizeToContents( );
            B.SetWidth( this.Width );
            B.SetHeight( B.Height + 5 );

            Buttons.Add( B );
            B.SetPosition( 0, Buttons.Max( O => O.LocalPosition.Y + O.Height ) + this.Height );

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
