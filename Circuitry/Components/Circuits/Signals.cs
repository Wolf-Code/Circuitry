using System.Collections.Generic;

namespace Circuitry.Components.Circuits
{
    public partial class Circuit
    {
        private readonly List<Signal> Signals = new List<Signal>( );

        public void PushSignal( Output O )
        {
            if ( CurrentState != State.Active || !O.HasNextNode )
                return;

            Signal S = new Signal( O, O.ConnectedInput );
            if ( !Signals.Contains( S ) )
                Signals.Add( S );
        }

        public void ClearSignals( )
        {
            Signals.Clear( );
        }
    }
}
