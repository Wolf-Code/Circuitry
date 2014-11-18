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

        /// <summary>
        /// Returns true if the cursor is not inside UI.
        /// </summary>
        /// <returns>True if the mouse is outside UI, false otherwise.</returns>
        protected bool MouseCanSelect( )
        {
            return !Manager.MouseInsideUI( );
        }
    }
}
