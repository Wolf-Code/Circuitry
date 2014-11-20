using OpenTK;
using OpenTK.Input;

namespace SharpLib2D.UI.Internal.Scrollbar
{
    public class ScrollbarBarDragger : Button
    {
        private ScrollbarBar Bar { set; get; }
        public float Length
        {
            get
            {
                return ( Bar.Scrollbar.LengthVector * Size ).Length;
            }
        }

        public ScrollbarBarDragger( ScrollbarBar Bar )
        {
            this.Bar = Bar;
            SetParent( Bar );
            PreventLeavingParent = true;
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

            if ( Canvas.Dragger.IsDragging<ScrollbarBarDragger>( ) && Canvas.Dragger.DraggingEntity == this )
                Bar.OnDrag( );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawScrollbarBar( this );
        }
    }

    public class ScrollbarBar : Control
    {
        private ScrollbarBarDragger Dragger { set; get; }
        public Scrollbar Scrollbar { private set; get; }

        private float Length
        {
            get { return ( Scrollbar.LengthVector * Size ).Length; }
        }

        public ScrollbarBar( Scrollbar Scrollbar )
        {
            this.Scrollbar = Scrollbar;
            SetParent( this.Scrollbar );
            Dragger = new ScrollbarBarDragger( this );
            this.Scrollbar.OnValueChanged += Scrollbar_OnValueChanged;
            SharpLibUIEventHandler<Scrollbar> MinMaxChange = Scrollbar1 =>
            {
                ResizeDragger( );
                MoveDragger( Scrollbar1.MinValue, Scrollbar1.MaxValue, Scrollbar1.Value );
            };

            this.Scrollbar.OnMinValueChanged += MinMaxChange;
            this.Scrollbar.OnMaxValueChanged += MinMaxChange;
        }

        void Scrollbar_OnValueChanged( Scrollbar Control )
        {
            if ( Canvas.Dragger.IsDragging<ScrollbarBarDragger>( ) && Canvas.Dragger.DraggingEntity == Dragger )
                return;

            if ( Control.Value <= this.Scrollbar.MinValue )
            {
                Dragger.SetPosition( 0, 0 );
                return;
            }

            this.MoveDragger( Control.MinValue, Control.MaxValue, Control.Value );
        }

        private void MoveDragger( double Min, double Max, double Val )
        {
            double Div = Val / ( Min + Max );
            float MaxMovement = Length - Dragger.Length;

            Dragger.SetPosition( Scrollbar.LengthVector * ( float )( MaxMovement * Div ) );
        }

        internal void OnDrag( )
        {
            float MaxMovement = Length - Dragger.Length;
            float Movement = ( ToLocal( Dragger.Position ) * Scrollbar.LengthVector ).Length;

            Scrollbar.Value = ( Movement / MaxMovement ) * 
                                   ( Scrollbar.MaxValue - Scrollbar.MinValue ) +
                                   Scrollbar.MinValue;
        }

        internal float DraggerMultiplier( )
        {
            double Diff = Scrollbar.MaxValue - Scrollbar.MinValue;
            if ( Diff <= 0 )
                return 1f;

            return ( float ) ( Length / ( Length + Diff ) );
        }

        private void ResizeDragger( )
        {
            float NewLength = Length * DraggerMultiplier( );
            if ( NewLength < Scrollbar.Thickness )
                NewLength = Scrollbar.Thickness;

            Dragger.SetSize( Scrollbar.ThicknessVector * Scrollbar.Thickness +
                                  Scrollbar.LengthVector * NewLength );
        }

        protected override void OnResize( Vector2 OldSize, Vector2 NewSize )
        {
            ResizeDragger( );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawScrollbarBarContainer( this );
        }
    }
}
