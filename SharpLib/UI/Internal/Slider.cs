﻿using OpenTK;
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

            public SliderGrip( Slider S )
            {
                this.PreventLeavingParent = true;
                this.SetParent( S );
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
            get
            {
                return m_Value;
            }
        }
        public SliderGrip Grip { private set; get; }
        private const float SliderThickness = 15;
        private const float GripThickness = 12;

        protected Slider( bool Horizontal ) : base( Horizontal )
        {
            this.Grip = new SliderGrip( this );
            this.Grip.SetSize( this.ThicknessVector * SliderThickness + this.LengthVector * GripThickness );
            this.OnMinValueChanged += Slider_OnMinValueChanged;
            this.OnMaxValueChanged += Slider_OnMaxValueChanged;
            this.OnValueChanged += Slider_OnValueChanged;
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
            double Div = Val / ( Min + Max );
            float MaxMovement = Length - this.Length;

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
