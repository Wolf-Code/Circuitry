
using System.Linq;

namespace Circuitry.Components
{
    public partial class Circuit
    {/// <summary>
        /// The possible game states.
        /// </summary>
        public enum State
        {
            Build,
            Build_Placing,
            Active,
            Paused
        }

        /// <summary>
        /// The current game state.
        /// </summary>
        public State CurrentState
        {
            private set;
            get;
        }

        /// <summary>
        /// Toggles the game's state.
        /// </summary>
        public void ToggleState( )
        {
            if ( CurrentState != State.Build )
            {
                CurrentState = State.Build;
                // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
                ClearSignals( );

                foreach ( Gate M in Children.Where( O => O is Gate ) )
                    M.Reset( );
            }
            else
            {
                if ( ConnectingNodes )
                    CancelConnecting( );
                CurrentState = State.Active;
            }
        }

    }
}
