using System;
using OpenTK;
using OpenTK.Input;

namespace SharpLib2D.UI.Internal.Scrollbar
{
    public class ScrollbarBarDragger : Button
    {
        public ScrollbarBar Bar { private set; get; }
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
        public ScrollbarBarDragger Dragger { private set; get; }
        public Scrollbar Scrollbar { private set; get; }

        internal float Length
        {
            get { return ( Scrollbar.LengthVector * Size ).Length; }
        }

        public ScrollbarBar( Scrollbar Scrollbar )
        {
            this.Scrollbar = Scrollbar;
            SetParent( this.Scrollbar );
            Dragger = new ScrollbarBarDragger( this );
            this.Scrollbar.OnValueChanged += Scrollbar_OnValueChanged;
        }

        void Scrollbar_OnValueChanged( Scrollbar Control )
        {
            if ( Canvas.Dragger.IsDragging<ScrollbarBarDragger>( ) && Canvas.Dragger.DraggingEntity == Dragger )
                return;

            if ( Control.Value <= 0 )
            {
                Dragger.SetPosition( 0, 0 );
                return;
            }

            double Div = Control.Value / ( Control.MinValue + Control.MaxValue );
            float MaxMovement = Length - Dragger.Length;

            Dragger.SetPosition( Scrollbar.LengthVector * ( float ) ( MaxMovement * Div ) );
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
                return Length;

            return ( float ) ( Length / Diff );
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
