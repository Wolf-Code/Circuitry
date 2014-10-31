using System.Collections.Generic;
using System.Linq;
using Circuitry.UI;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        internal bool OnChildMouseAction( CircuitryEntity Entity, MouseButtonEventArgs Args )
        {
            Gate G = Entity as Gate;
            if ( G != null )
            {
                GateMouseInput( G, Args );
                return true;
            }

            IONode N = Entity as IONode;
            if ( N != null )
            {
                NodeMouseInput( N, Args );
                return true;
            }

            return false;
        }

        public override MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            List<Entity> Ents = OrderedEntities;
            Entity E = Ents.FirstOrDefault( O => O.GetChildAt( CheckPosition ) != null );
            if ( E == null )
                return ( MouseEntity )Ents.FirstOrDefault( O => O.ContainsPosition( CheckPosition ) ) ?? this;

            Entity E2 = E.GetChildAt( CheckPosition );
            while ( E2 != null )
            {
                E = E2;
                E2 = E.GetChildAt( CheckPosition );
            }

            return ( MouseEntity )E;
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
            NodePathClick( Button );

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
