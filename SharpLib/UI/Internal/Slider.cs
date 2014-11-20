using OpenTK;
using OpenTK.Input;
using SharpLib2D.UI.Interfaces;

namespace SharpLib2D.UI.Internal
{
    public abstract class Slider : DirectionalControl, IMinMaxValue
    {
        public class SliderGrip : Button
        {
            public Slider Slider
            {
                get { return this.GetParent<Slider>( ); }
            }

            public SliderGrip( Slider S ) : base( S )
            {
                this.PreventLeavingParent = true;
            }

            public override void OnButtonPressed( MouseButton Button )
            {
                if ( Button == MouseButton.Left )
                    Canvas.Dragger.StartDragging( this );

                base.OnButtonPressed( Button );
            }

            protected override void OnReposition( Vector2 OldPosition, Vector2 NewPosition )
            {
                base.OnReposition( OldPosition, NewPosition );
                
                float MaxDist = this.Slider.Length - ( this.Size * this.Slider.LengthVector ).Length;
                this.Slider.Value = this.Slider.MinValue +
                                    ( this.Slider.MaxValue - this.Slider.MinValue ) *
                                    ( this.LocalPosition * this.Slider.LengthVector ).Length / MaxDist;
            }

            protected override void DrawSelf( )
            {
                Canvas.Skin.DrawSliderGrip( this );
            }
        }

        public event SharpLibUIEventHandler<Slider> OnValueChanged;
        public event SharpLibUIEventHandler<Slider> OnMinValueChanged;
        public event SharpLibUIEventHandler<Slider> OnMaxValueChanged;

        private double MinValue1, MaxValue1;

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
            get
            {
                return MinValue1;
            }
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
            get
            {
                return MaxValue1;
            }
        }

        private double m_Value;
        private int RoundDigits1;

        public double Value
        {
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                double Val =
                    System.Math.Round( System.Math.Min( this.MaxValue, System.Math.Max( this.MinValue, value ) ),
                        RoundDigits );

                if ( m_Value == Val ) return;

                m_Value = Val;
                if ( OnValueChanged != null )
                    OnValueChanged( this );
            }
            get
            {
                return m_Value;
            }
        }

        public int RoundDigits
        {
            set
            {
                RoundDigits1 = value;
                Slider_OnValueChanged( this );
            }
            get { return RoundDigits1; }
        }

        public SliderGrip Grip { private set; get; }
        private const float SliderThickness = 15;
        private const float GripThickness = 12;

        protected Slider( Control Parent, bool Horizontal ) : base( Parent, Horizontal )
        {
            this.Grip = new SliderGrip( this );
            this.Grip.SetSize( this.ThicknessVector * SliderThickness + this.LengthVector * GripThickness );
            this.MinValue = 0;
            this.MaxValue = 100;
            this.Value = 50;
            this.OnMinValueChanged += Slider_OnMinValueChanged;
            this.OnMaxValueChanged += Slider_OnMaxValueChanged;
            this.OnValueChanged += Slider_OnValueChanged;
            Canvas.Dragger.OnDrop += ( Dragger, Entity ) =>
            {
                if( Entity == this.Grip )
                    Slider_OnValueChanged( this );
            };
        }

        private void Slider_OnValueChanged( Slider Control )
        {
            if ( Canvas.Dragger.IsDragging( Grip ) )
                return;

            if ( Control.Value <= MinValue )
            {
                Grip.SetPosition( 0, 0 );
                return;
            }

            this.MoveGrip( Control.MinValue, Control.MaxValue, Control.Value );
        }

        private void Slider_OnMaxValueChanged( Slider Control )
        {
            this.MoveGrip( this.MinValue, this.MaxValue, this.Value );
        }

        void Slider_OnMinValueChanged( Slider Control )
        {
            this.MoveGrip( this.MinValue, this.MaxValue, this.Value );
        }

        private void MoveGrip( double Min, double Max, double Val )
        {
            double Div = ( Val - Min ) / ( Max - Min );
            float MaxMovement = Length - ( this.Grip.Size * this.LengthVector ).Length;

            Grip.SetPosition( this.LengthVector * ( float )( MaxMovement * Div ) );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            if ( ( int )Thickness != ( int )SliderThickness )
                SetSize( LengthVector * Length + ThicknessVector * SliderThickness );

            base.OnResize( OldSize, NewSize );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawSlider( this );
        }
    }
}
