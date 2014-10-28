﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Circuitry.Components;
using Gwen;
using Gwen.Control;

namespace Circuitry.States
{
    public class Game : GwenState
    {
        protected Circuit Circuit
        {
            private set;
            get;
        }

        protected override void OnStart( )
        {
            base.OnStart( );

            Circuit = new Circuit( );

            #region Control Window

            WindowControl ControlWindow = new WindowControl( GwenCanvas, "Controls" )
            {
                Height = 100,
                Dock = Pos.Bottom,
                IsClosable = false
            };
            ControlWindow.DisableResizing( );
            
            Button Mode = new Button( ControlWindow );
            Mode.SetText( "Set Active" );
            Mode.Clicked += sender =>
            {
                Mode.SetText( "Set " + Circuit.CurrentState );
                Circuit.ToggleState( );
            };

            LabeledCheckBox SnapToGrid = new LabeledCheckBox( ControlWindow )
            {
                Text = "Snap to grid",
                IsChecked = Circuit.SnapToGrid
            };
            SnapToGrid.CheckChanged += sender => { Circuit.SnapToGrid = SnapToGrid.IsChecked; };
            SnapToGrid.SetPosition( Mode.X, Mode.Y + Mode.Height + 5 );

            LabeledCheckBox ShowGrid = new LabeledCheckBox( ControlWindow )
            {
                Text = "Show grid",
                IsChecked = Circuit.ShowGrid
            };
            ShowGrid.CheckChanged += sender =>
            {
                Circuit.ShowGrid = ShowGrid.IsChecked;
            };
            ShowGrid.SetPosition( SnapToGrid.X, SnapToGrid.Y + SnapToGrid.Height + 5 );

            Label GridSliderLabel = new Label( ControlWindow );
            GridSliderLabel.SetText( "Grid size" );
            GridSliderLabel.SizeToContents( );
            GridSliderLabel.SetPosition( Mode.Right + 25, Mode.Y );

            HorizontalSlider GridSize = new HorizontalSlider( ControlWindow )
            {
                Min = 16,
                Max = 256,
                Value = Circuit.GridSize
            };
            GridSize.ValueChanged += Control => { Circuit.GridSize = ( int ) GridSize.Value; };
            GridSize.SetPosition( GridSliderLabel.X, GridSliderLabel.Bottom );
            GridSize.SetSize( 100, 20 );
            #endregion

            #region Gate Window

            WindowControl GateWindow = new WindowControl( GwenCanvas, "Gates" )
            {
                Width = 150,
                Dock = Pos.Left,
                IsClosable = false
            };
            GateWindow.DisableResizing( );

            CollapsibleList GateList = new CollapsibleList( GateWindow )
            {
                Dock = Pos.Fill
            };

            #region Adding Gates and Categories

            Dictionary<string, CollapsibleCategory> Categories = new Dictionary<string, CollapsibleCategory>( );

            foreach ( Type T in Assembly.GetExecutingAssembly( ).GetTypes( ) )
            {
                if ( !T.IsSubclassOf( typeof ( Gate ) ) ) continue;

                Gate G = Activator.CreateInstance( T ) as Gate;

                // ReSharper disable once PossibleNullReferenceException
                if ( !Categories.ContainsKey( G.Category ) )
                    Categories.Add( G.Category, GateList.Add( G.Category ) );

                Button GateButton = Categories[ G.Category ].Add( G.Name );
                GateButton.UserData = T;
                GateButton.Clicked += GateButton_Clicked;
                G.Remove( );
            }

            #endregion

            #endregion
        }

        void GateButton_Clicked( Base control )
        {
            if ( Circuit.CurrentState == Circuit.State.Active ) return;

            Circuit.StartGatePlacing( control.UserData as Type );
        }
    }
}
