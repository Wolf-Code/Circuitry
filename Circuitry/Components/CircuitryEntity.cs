using Circuitry.UI;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    public class CircuitryEntity : MouseEntity
    {
        protected bool MouseCanSelect( )
        {
            return !Manager.MouseInsideUI( );
        }
    }
}
