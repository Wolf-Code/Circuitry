using SharpLib2D.Info;
using SharpLib2D.Objects;

namespace SharpLib2D.UI
{
    public class Checkbox : Control, Interfaces.ITextControl
    {
        /// <summary>
        /// Returns whether or not the checkbox is checked.
        /// </summary>
        public bool Checked { private set; get; }
        protected readonly Label Label;

        /// <summary>
        /// Gets called whenever the checkbox is checked or unchecked.
        /// </summary>
        public event SharpLibUIEventHandler<Checkbox> OnCheckedChanged;

        public override BoundingRectangle VisibleRectangle
        {
            get { return new BoundingRectangle( this.TopLeft, Label.BottomRight ); }
        }

        public string Text
        {
            protected set { this.Label.SetText( value ); }
            get { return this.Label.Text; }
        }

        public Checkbox( string Text = "" )
        {
            SetSize( 15, 15 );
            Label = new Label( );
            Label.SetParent( this );
            Label.IgnoreMouseInput = false;
            Label.FontSize = 10;
            Label.VerticalAlignment = Directions.VerticalAlignment.Center;
            SetText( Text );

            OnLeftClick += LeftClick;
            Label.OnLeftClick += LeftClick;
        }

        private void LeftClick( Control Control )
        {
            SetChecked( !Checked );
        }

        public void SetText( string NewText )
        {
            Label.SetText( NewText );
            Label.SizeToContents( );
            Label.SetPosition( Width, 0 );
            Label.SetSize( Label.Width, Height );
        }

        public void SetChecked( bool NewChecked )
        {
            Checked = NewChecked;

            if ( OnCheckedChanged != null )
                OnCheckedChanged( this );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawCheckbox( this );
        }
    }
}
