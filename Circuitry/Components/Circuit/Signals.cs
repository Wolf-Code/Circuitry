
using System.Collections.Generic;
using SharpLib2D.States;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        private readonly List<Signal> Signals = new List<Signal>( );

        public void PushSignal( Output O )
        {
            if ( CurrentState != State.Active || !O.IsConnected )
                return;

            Signal S = new Signal( O, O.Connection as Input );
            if ( !Signals.Contains( S ) )
                Signals.Add( S );
        }

        public void ClearSignals( )
        {
            Signals.Clear( );
        }
    }
}
