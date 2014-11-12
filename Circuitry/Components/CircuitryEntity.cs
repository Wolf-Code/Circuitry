using Circuitry.Components.Circuits;
using Circuitry.UI;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    public class CircuitryEntity : MouseEntity
    {
        public Circuit Circuit
        {
            get
            {
                return this.IsParent<CircuitryEntity>( )
                    ? this.GetParent<CircuitryEntity>( ).Circuit
                    : this.GetParent<Circuit>( );
            }
        }

        protected bool MouseCanSelect( )
        {
            return !Manager.MouseInsideUI( );
        }
    }
}
