using OpenTK;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    class CircuitDragger : Dragger
    {
        private readonly Circuit Circuit;

        public CircuitDragger( Circuit C )
        {
            Circuit = C;
        }

        protected override Vector2 TransformPosition( Vector2 NewEntityPosition )
        {
            if ( Circuit.SnapToGrid )
                return Circuit.SnapPositionToGrid( NewEntityPosition - LocalGrabPoint + new Vector2(Circuit.GridSize / 2f) );
            
            return base.TransformPosition( NewEntityPosition );
        }
    }
}
