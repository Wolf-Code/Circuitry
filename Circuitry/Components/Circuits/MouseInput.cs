using System.Collections.Generic;
using System.Linq;
using Circuitry.UI;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Entities;

namespace Circuitry.Components.Circuits
{
    public partial class Circuit
    {
        internal bool OnChildMouseAction( CircuitryEntity Entity, MouseButtonEventArgs Args )
        {
            Gate G = Entity as Gate;
            if ( G == null ) return false;

            GateMouseInput( G, Args );
            return true;
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            switch ( Button )
            {
                case MouseButton.Left:
                    switch ( CurrentState )
                    {
                        case State.Build_Placing:
                            if ( !Manager.MouseInsideUI( ) )
                            {
                                AddGate( GateToPlace );
                                GateToPlace.Active = true;
                                GateToPlace = null;
                                CurrentState = State.Build;
                            }
                            break;

                        default:
                            StartCameraDragging( );
                            break;
                    }
                    break;

                case MouseButton.Right:
                    switch ( CurrentState )
                    {
                        case State.Build_Placing:
                            if ( !Manager.MouseInsideUI( ) )
                            {
                                GateToPlace.Remove( );
                                GateToPlace = null;
                                CurrentState = State.Build;
                            }
                            break;
                    }
                    break;
            }

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            switch ( Button )
            {
                case MouseButton.Left:
                    StopCameraDragging( );
                    break;
            }
            base.OnButtonReleased( Button );
        }
    }
}
