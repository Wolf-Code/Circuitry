using OpenTK;
using SharpLib2D.UI.Internal;

namespace SharpLib2D.UI
{
    public class Window : Control
    {
        /// <summary>
        /// The titlebar of the window.
        /// </summary>
        public WindowTitleBar TitleBar { private set; get; }

        /// <summary>
        /// Indicates if the window can be dragged.
        /// </summary>
        public bool IsDraggable
        {
            set { this.TitleBar.Draggable = value; }
            get { return this.TitleBar.Draggable; }
        }

        /// <summary>
        /// Whether to show the close button.
        /// </summary>
        public bool ShowCloseButton
        {
            set { this.TitleBar.Button.Visible = value; }
            get { return this.TitleBar.Visible; }
        }

        public Window( string Title, Canvas Cnv ) : base( null )
        {
            TitleBar = new WindowTitleBar( Title, this );
            TitleBar.SetParent( Cnv );
            TitleBar.PreventLeavingParent = true;
            SetParent( TitleBar );
            SetSize( 100, 100 );
            SetPosition( 0, 0 );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            NewSize.Y -= TitleBar.Size.Y;
            if ( NewSize.Y < 0 )
                NewSize.Y = 0;

            TitleBar.SetSize( NewSize.X, TitleBar.Size.Y );

            m_Size.Y = NewSize.Y;
        }

        protected override void OnReposition( Vector2 OldPosition, Vector2 NewPosition )
        {
            TitleBar.SetPosition( NewPosition );

            m_Position = new Vector2( 0, TitleBar.Height );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawWindow( this );
        }
    }
}
