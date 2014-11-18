using OpenTK;
using SharpLib2D.Info;
using SharpLib2D.UI.Interfaces;

namespace SharpLib2D.UI.Internal.Scrollbar
{
    public abstract class Scrollbar : DirectionalControl, IMinMaxValue
    {
        public event SharpLibUIEventHandler<Scrollbar> OnValueChanged;
        public event SharpLibUIEventHandler<Scrollbar> OnMinValueChanged;
        public event SharpLibUIEventHandler<Scrollbar> OnMaxValueChanged;

        public double MinValue
        {
            set
            {
                MinValue1 = value;
                if ( OnMinValueChanged != null )
                    OnMinValueChanged( this );

                if ( Value < MinValue )
                    Value = MinValue;
            }
            get { return MinValue1; }
        }

        public double MaxValue
        {
            set
            {
                MaxValue1 = value;
                if ( OnMaxValueChanged != null )
                    OnMaxValueChanged( this );

                if ( Value > MaxValue )
                    Value = MaxValue;
            }
            get { return MaxValue1; }
        }

        private double m_Value;

        public double Value
        {
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                double Val = System.Math.Min( this.MaxValue, System.Math.Max( this.MinValue, value ) );
                if ( m_Value == Val ) return;

                m_Value = Val;
                if ( OnValueChanged != null )
                    OnValueChanged( this );
            }
            get { return m_Value; }
        }

        private readonly ScrollbarButton [ ] Buttons = new ScrollbarButton[ 2 ];
        private readonly ScrollbarBar Bar;
        private double MinValue1;
        private double MaxValue1;

        private float RequiredThickness
        {
            get { return Buttons[ 0 ].Size.X; }
        }

        protected Scrollbar( bool Horizontal ) : base( Horizontal )
        {
            MinValue = 0;
            MaxValue = 100;

            if ( Horizontal )
                Buttons = new [ ]
                { new ScrollbarButton( Directions.Direction.Left ), new ScrollbarButton( Directions.Direction.Right ) };
            else
                Buttons = new [ ]
                { new ScrollbarButton( Directions.Direction.Up ), new ScrollbarButton( Directions.Direction.Down ) };

            for ( int Index = 0; Index < Buttons.Length; Index++ )
            {
                ScrollbarButton B = Buttons[ Index ];
                if ( Index <= 0 )
                    B.OnClick += OnClickBack;
                else
                    B.OnClick += OnClickForward;

                B.SetParent( this );
            }

            Bar = new ScrollbarBar( this );
            this.SetSize( this.Size );
        }

        private void OnClickForward( Button Control )
        {
            this.Value = System.Math.Min( MaxValue,
                this.Value + ( this.MaxValue - this.MinValue ) * this.Bar.DraggerMultiplier( ) );
        }

        private void OnClickBack( Button Control )
        {
            this.Value = System.Math.Max( MinValue,
                this.Value - ( this.MaxValue - this.MinValue ) * this.Bar.DraggerMultiplier( ) );
        }

        private Vector2 GetOffsetPosition( float Distance )
        {
            return LengthVector * Distance;
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            if ( ( int ) Thickness != ( int ) RequiredThickness )
                SetSize( LengthVector * Length + ThicknessVector * RequiredThickness );

            Buttons[ 0 ].SetPosition( GetOffsetPosition( 0 ) );
            Buttons[ 1 ].SetPosition( GetOffsetPosition( Length - RequiredThickness ) );
            Bar.SetPosition( GetOffsetPosition( RequiredThickness ) );
            Bar.SetSize( LengthVector * ( Length - RequiredThickness * 2 ) + ThicknessVector * Thickness );

            base.OnResize( OldSize, NewSize );
        }

        public override void Dispose( )
        {
            this.OnValueChanged = null;
            this.OnMinValueChanged = null;
            this.OnMaxValueChanged = null;
            base.Dispose( );
        }
    }
}