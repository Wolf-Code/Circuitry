using System;
using System.Collections.Generic;
using System.Reflection;
using Circuitry.Components;
using Circuitry.Components.Circuits;
using SharpLib2D.Resources;
using SharpLib2D.States;
using SharpLib2D.UI;
using SharpLib2D.UI.Skin;

namespace Circuitry.States
{
    public class Game2 : UIState
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
            this.Canvas.SetSkin( new GwenTextureSkin( Loader.Get<Texture>( "Resources\\Textures\\UI\\DefaultSkin.png" ) ) );

            #region Control Window

            Window ControlWindow = new Window( "Controls", Canvas )
            {
                Height = 150,
                Width = 600,
                ShowCloseButton = false,
                PreventLeavingParent = true
            };
            
            Button Mode = new Button( "Set Active" );
            Mode.SetParent( ControlWindow );
            Mode.SetPosition( 10,10 );
            
            Mode.OnClick += sender =>
            {
                Mode.SetText( "Set " + Circuit.CurrentState );
                Circuit.ToggleState( );
            };

            Checkbox SnapToGrid = new Checkbox { Checked = Circuit.SnapToGrid };
            SnapToGrid.SetParent( ControlWindow );
            SnapToGrid.SetText( "Snap to grid" );
            SnapToGrid.OnCheckedChanged += sender => { Circuit.SnapToGrid = SnapToGrid.Checked; };
            SnapToGrid.AlignLeft( Mode );
            SnapToGrid.MoveBelow( Mode, 5 );

            Checkbox ShowGrid = new Checkbox { Checked = Circuit.ShowGrid };
            ShowGrid.OnCheckedChanged += sender =>
            {
                Circuit.ShowGrid = ShowGrid.Checked;
            };
            ShowGrid.SetParent( ControlWindow );
            ShowGrid.AlignLeft( SnapToGrid );
            ShowGrid.MoveBelow( SnapToGrid, 5 );
            ShowGrid.SetText( "Show grid" );

            Checkbox ShowGateLabels = new Checkbox { Checked = Circuit.ShowLabels };
            ShowGateLabels.SetParent( ControlWindow );
            ShowGateLabels.OnCheckedChanged += sender =>
            {
                Circuit.ShowLabels = ShowGateLabels.Checked;
            };
            ShowGateLabels.SetText( "Show gate labels" );
            ShowGateLabels.AlignLeft( ShowGrid );
            ShowGateLabels.MoveBelow( ShowGrid, 5 );

            Label GridSliderLabel = new Label( );
            GridSliderLabel.SetParent( ControlWindow );
            GridSliderLabel.SetText( "Grid size: " + Circuit.GridSize );
            GridSliderLabel.SizeToContents( );
            GridSliderLabel.MoveRightOf( Mode, 40 );

            HorizontalSlider GridSize = new HorizontalSlider
            {
                MinValue = 3,
                MaxValue = 7,
                Value = Math.Log( Circuit.GridSize, 2 ),
            };
            GridSize.OnValueChanged += Control =>
            {
                Circuit.GridSize = ( int )Math.Pow( 2, ( int ) GridSize.Value );
                GridSliderLabel.SetText( "Grid size: " + Circuit.GridSize );
                GridSliderLabel.SizeToContents( );
            };
            GridSize.AlignLeft( GridSliderLabel );
            GridSize.MoveBelow( GridSliderLabel );
            GridSize.SetSize( 150, 20 );
            GridSize.SetParent( ControlWindow );

            #endregion
            /*
            #region Gate Window

            WindowControl GateWindow = new WindowControl( GwenCanvas, "Gates" )
            {
                Width = 150,
                Dock = Pos.Left,
                IsClosable = false
            };

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

            #endregion*/
        }
        /*
        void GateButton_Clicked( Base control )
        {
            if ( Circuit.CurrentState == Circuit.State.Active ) return;

            Circuit.StartGatePlacing( control.UserData as Type );
        }*/
    }
}
