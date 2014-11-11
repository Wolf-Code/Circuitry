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
                return ( this.Bar.Scrollbar.LengthVector * this.Size ).Length;
            }
        }

        public ScrollbarBarDragger( ScrollbarBar Bar )
        {
            this.Bar = Bar;
            this.SetParent( Bar );
            this.PreventLeavingParent = true;
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Button == MouseButton.Left )
                Canvas.Dragger.StartDragging( this );

            base.OnButtonPressed( Button );
        }

        protected override void OnPositionChanged( Vector2 NewPosition )
        {
            this.Bar.OnDrag( );

            base.OnPositionChanged( NewPosition );
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

        private float Length
        {
            get { return ( this.Scrollbar.LengthVector * this.Size ).Length; }
        }

        public ScrollbarBar( Scrollbar Scrollbar )
        {
            this.Scrollbar = Scrollbar;
            this.SetParent( this.Scrollbar );
            this.Dragger = new ScrollbarBarDragger( this );
            this.Scrollbar.OnValueChanged += Scrollbar_OnValueChanged;
        }

        void Scrollbar_OnValueChanged( Scrollbar Control )
        {
            if ( Control.Value <= 0 )
            {
                Dragger.SetPosition( 0, 0 );
                return;
            }

            double Div = ( Control.MinValue + Control.MaxValue ) / Control.Value;
            float MaxMovement = this.Length - Dragger.Length;

            //this.Dragger.SetPosition( Scrollbar.LengthVector * ( float ) ( MaxMovement * Div ) );
        }

        internal void OnDrag( )
        {
            float MaxMovement = this.Length - Dragger.Length;
            float Movement = ( this.Dragger.LocalPosition * this.Scrollbar.LengthVector ).Length;
            Console.WriteLine( this.Dragger.LocalPosition + ", " + MaxMovement );
            this.Scrollbar.Value = ( Movement / MaxMovement ) * 
                                   ( this.Scrollbar.MaxValue - this.Scrollbar.MinValue ) +
                                   this.Scrollbar.MinValue;
        }

        private float DraggerMultiplier( )
        {
            double Diff = this.Scrollbar.MaxValue - this.Scrollbar.MinValue;
            if ( Diff <= 0 )
                return this.Length;

            return ( float ) ( this.Length / Diff );
        }

        private void ResizeDragger( )
        {
            float NewLength = this.Length * this.DraggerMultiplier( );
            if ( NewLength < this.Scrollbar.Thickness )
                NewLength = this.Scrollbar.Thickness;

            this.Dragger.SetSize( this.Scrollbar.ThicknessVector * this.Scrollbar.Thickness +
                                  this.Scrollbar.LengthVector * NewLength );
        }

        protected override void OnResize( Vector2 NewSize )
        {
            this.ResizeDragger( );
        }

        protected override void DrawSelf( )
        {
            Canvas.Skin.DrawScrollbarBarContainer( this );
        }
    }
}
