using System;
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
            get { return Buttons[ 0 ].Size.X; }
        }

        protected internal Vector2 LengthVector
        {
            get { return Horizontal ? Vector2.UnitX : Vector2.UnitY; }
        }

        protected internal Vector2 ThicknessVector
        {
            get { return Horizontal ? Vector2.UnitY : Vector2.UnitX; }
        }

        protected internal float Length
        {
            get { return Horizontal ? Width : Height; }
        }

        protected internal float Thickness
        {
            get { return Horizontal ? Height : Width; }
        }

        protected Scrollbar( bool Horizontal )
        {
            MinValue = 0;
            MaxValue = 100;

            this.Horizontal = Horizontal;
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
    }
}