using OpenTK.Input;
using SharpLib2D.UI.Interfaces;

namespace SharpLib2D.UI.Internal
{
    public abstract class Slider : DirectionalControl, IMinMaxValue
    {
        public class SliderGrip : Button
        {
            public SliderGrip( )
            {
                this.PreventLeavingParent = true;
            }

            public override void OnButtonPressed( MouseButton Button )
            {
                if ( Button == MouseButton.Left )
                    Canvas.Dragger.StartDragging( this );

                base.OnButtonPressed( Button );
            }
        }

        public double Value { set; get; }
        public double MinValue { set; get; }
        public double MaxValue { set; get; }

        protected Slider( bool Horizontal ) : base( Horizontal )
        {

        }
    }
}
