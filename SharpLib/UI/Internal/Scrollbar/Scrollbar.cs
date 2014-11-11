using OpenTK;
using SharpLib2D.Info;

namespace SharpLib2D.UI.Internal.Scrollbar
{
    public abstract class Scrollbar : Control
    {
        public event SharpLibUIEventHandler<Scrollbar> OnValueChanged; 

        public double MinValue { set; get; }
        public double MaxValue { set; get; }

        private double m_Value;

        public double Value
        {
            set
            {
                if ( m_Value != value )
                {
                    m_Value = value;
                    if ( OnValueChanged != null )
                        OnValueChanged( this );
                }
            }
            get { return m_Value; }
        }

        private readonly ScrollbarButton [ ] Buttons = new ScrollbarButton[ 2 ];
        private readonly ScrollbarBar Bar;

        protected bool Horizontal { set; get; }

        private float RequiredThickness
        {
            get { return this.Buttons[ 0 ].Size.X; }
        }

        internal protected Vector2 LengthVector
        {
            get { return Horizontal ? Vector2.UnitX : Vector2.UnitY; }
        }

        internal protected Vector2 ThicknessVector
        {
            get
            {
                return Horizontal ? Vector2.UnitY : Vector2.UnitX;
            }
        }

        internal protected float Length
        {
            get { return Horizontal ? this.Width : this.Height; }
        }

        internal protected float Thickness
        {
            get { return Horizontal ? this.Height : this.Width; }
        }

        protected Scrollbar( bool Horizontal )
        {
            this.MinValue = 0;
            this.MaxValue = 100;

            this.Horizontal = Horizontal;
            if ( Horizontal )
                Buttons = new [ ]
                { new ScrollbarButton( Directions.Direction.Left ), new ScrollbarButton( Directions.Direction.Right ) };
            else
                Buttons = new [ ]
                { new ScrollbarButton( Directions.Direction.Up ), new ScrollbarButton( Directions.Direction.Down ) };

            foreach ( ScrollbarButton B in this.Buttons )
                B.SetParent( this );

            Bar = new ScrollbarBar( this );
        }

        private Vector2 GetOffsetPosition( float Distance )
        {
            return LengthVector * Distance;
        }

        protected override void OnResize( Vector2 NewSize )
        {
            if ( ( int ) this.Thickness != ( int ) RequiredThickness )
                this.SetSize( this.LengthVector * this.Length + this.ThicknessVector * RequiredThickness );

            this.Buttons[ 0 ].SetPosition( this.GetOffsetPosition( 0 ) );
            this.Buttons[ 1 ].SetPosition( this.GetOffsetPosition( this.Length - RequiredThickness ) );
            Bar.SetPosition( this.GetOffsetPosition( RequiredThickness ) );
            Bar.SetSize( this.LengthVector * ( Length - RequiredThickness * 2 ) + this.ThicknessVector * this.Thickness );

            base.OnResize( NewSize );
        }
    }
}
