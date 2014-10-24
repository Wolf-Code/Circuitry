
using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    public class CircuitryEntity : MouseEntity
    {
        protected bool MouseCanSelect( )
        {
            return !UI.Manager.MouseInsideUI( );
        }
    }
}
