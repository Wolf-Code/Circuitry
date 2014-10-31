﻿using Circuitry.UI;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    public class CircuitryEntity : MouseEntity
    {
        public Circuit Circuit
        {
            set;
            get;
        }

        protected bool MouseCanSelect( )
        {
            return !Manager.MouseInsideUI( );
        }
    }
}
