using OpenTK;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    public class CircuitDragger : Dragger
    {
        private readonly Circuits.Circuit Circuit;

        public CircuitDragger( Circuits.Circuit C )
        {
            Circuit = C;
            this.SetParent( C );
        }

        protected override Vector2 TransformPosition( Vector2 NewEntityPosition )
        {
            if ( Circuit.SnapToGrid )
                return Circuit.SnapPositionToGrid( NewEntityPosition - LocalGrabPoint + new Vector2(Circuit.GridSize / 2f) );
            
            return base.TransformPosition( NewEntityPosition );
        }
    }
}
