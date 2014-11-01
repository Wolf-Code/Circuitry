using OpenTK;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    class CircuitDragger : Dragger
    {
        private readonly Circuit Circuit;

        public CircuitDragger( Circuit C )
        {
            this.Circuit = C;
        }

        protected override Vector2 TransformPosition( Vector2 NewEntityPosition )
        {
            if ( Circuit.SnapToGrid )
                return Circuit.SnapPositionToGrid( NewEntityPosition - this.LocalGrabPoint + new Vector2(Circuit.GridSize / 2f) );
            
            return base.TransformPosition( NewEntityPosition );
        }
    }
}
