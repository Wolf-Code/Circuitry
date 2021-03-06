﻿using System;
using Circuitry.UI;
using OpenTK.Input;

namespace Circuitry.Components.Circuits
{
    public partial class Circuit
    {
        public bool ShowLabels
        {
            internal set;
            get;
        }

        protected Gate GateToPlace
        {
            private set;
            get;
        }

        private void GateMouseInput( Gate G, MouseButtonEventArgs Args )
        {
            switch ( CurrentState )
            {
                case State.Build:
                    if ( Args.Button == MouseButton.Left )
                        if ( Args.IsPressed && !Manager.MouseInsideUI( ) )
                            Dragger.StartDragging( G );

                    if ( Args.Button == MouseButton.Right )
                        if ( Args.IsPressed )
                            G.ShowOptionsMenu( );
                    break;

                case State.Build_Placing:

                    break;
            }
        }

        public void StartGatePlacing( Type G )
        {
            if ( Connector.ConnectingNodes )
                return;

            if ( GateToPlace != null )
                GateToPlace.Remove( );

            GateToPlace = Activator.CreateInstance( G ) as Gate;
            CurrentState = State.Build_Placing;
        }

        public void AddGate( Gate G )
        {
            G.SetParent( this );
            G.Reset( );
        }
    }
}
