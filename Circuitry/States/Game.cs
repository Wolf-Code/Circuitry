using System;
using System.Collections.Generic;

namespace Circuitry.States
{
    public class Game : GwenState
    {
        protected Components.Circuit Circuit
        {
            private set;
            get;
        }

        protected override void OnStart( )
        {
            base.OnStart( );

            Circuit = new Components.Circuit( );

            Gwen.Control.WindowControl ControlWindow = new Gwen.Control.WindowControl( GwenCanvas, "Controls" )
            {
                Height = 100,
                Dock = Gwen.Pos.Bottom,
                IsClosable = false
            };
            ControlWindow.DisableResizing( );
            
            Gwen.Control.Button Mode = new Gwen.Control.Button( ControlWindow );
            Mode.SetText( Circuit.CurrentState.ToString( ) );
            Mode.Clicked += sender =>
            {
                Circuit.ToggleState( );
                Mode.SetText( Circuit.CurrentState.ToString( ) );
            };

            Gwen.Control.WindowControl GateWindow = new Gwen.Control.WindowControl( GwenCanvas, "Gates" )
            {
                Width = 100,
                Dock = Gwen.Pos.Left,
                IsClosable = false
            };
            GateWindow.DisableResizing( );

            Gwen.Control.CollapsibleList GateList = new Gwen.Control.CollapsibleList( GateWindow )
            {
                Dock = Gwen.Pos.Fill
            };

            Dictionary<string, Gwen.Control.CollapsibleCategory> Categories = new Dictionary<string, Gwen.Control.CollapsibleCategory>( );

            foreach ( Type T in System.Reflection.Assembly.GetExecutingAssembly( ).GetTypes( ) )
            {
                if ( !T.IsSubclassOf( typeof ( Components.Gate ) ) ) continue;

                Components.Gate G = Activator.CreateInstance( T ) as Components.Gate;

                // ReSharper disable once PossibleNullReferenceException
                if ( !Categories.ContainsKey( G.Category ) )
                    Categories.Add( G.Category, GateList.Add( G.Category ) );

                Gwen.Control.Button GateButton = Categories[ G.Category ].Add( G.Name );
                GateButton.UserData = T;
                GateButton.Clicked += GateButton_Clicked;
                G.Remove( );
            }
        }

        void GateButton_Clicked( Gwen.Control.Base control )
        {
            if ( Circuit.CurrentState == Components.Circuit.State.Active ) return;

            Circuit.StartGatePlacing( control.UserData as Type );
        }
    }
}
