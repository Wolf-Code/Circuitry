using SharpLib2D.Info;
using SharpLib2D.Objects;

namespace SharpLib2D.UI
{
    public class Checkbox : Control
    {
        public bool Checked { private set; get; }
        protected readonly Label Label;
        public event SharpLibUIEventHandler<Checkbox> OnCheckedChanged;

        public override BoundingRectangle VisibleRectangle
        {
            get { return HasParent ? ( ( Control ) Parent ).VisibleRectangle : this.BoundingVolume.BoundingBox; }
        }

        public Checkbox( string Text = "" )
        {
            this.SetSize( 15, 15 );
            this.Label = new Label( );
            this.Label.SetParent( this );
            this.Label.IgnoreMouseInput = false;
            this.Label.FontSize = 10;
            this.Label.VerticalAlignment = Directions.VerticalAlignment.Center;
            this.SetText( Text );

            this.OnLeftClick += LeftClick;
            this.Label.OnLeftClick += LeftClick;
        }

        private void LeftClick( Control Control )
        {
            this.SetChecked( !this.Checked );
        }

        public void SetText( string Text )
        {
            this.Label.SetText( Text );
            this.Label.SizeToContents( );
            this.Label.SetPosition( this.Width, 0 );
            this.Label.SetSize( this.Label.Width, this.Height );
        }

        public void SetChecked( bool NewChecked )
        {
            this.Checked = NewChecked;

            if ( OnCheckedChanged != null )
                OnCheckedChanged( this );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawCheckbox( this );
        }
    }
}
