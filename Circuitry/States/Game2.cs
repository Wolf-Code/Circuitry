using System;
using System.Collections.Generic;
using System.Reflection;
using Circuitry.Components;
using Circuitry.Components.Circuits;
using SharpLib2D.Resources;
using SharpLib2D.States;
using SharpLib2D.UI;
using SharpLib2D.UI.Internal;
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

        protected Window ControlWindow { private set; get; }
        protected Window GateWindow { private set; get; }
        protected CategoryList GateList { private set; get; }

        protected override void OnStart( )
        {
            base.OnStart( );

            Circuit = new Circuit( );
            this.Canvas.SetSkin( new GwenTextureSkin( Loader.Get<Texture>( "Resources\\Textures\\UI\\DefaultSkin.png" ) ) );

            #region Control Window

            ControlWindow = new Window( "Controls", Canvas )
            {
                Height = 230,
                ShowCloseButton = false
            };
            
            Button Mode = new Button( ControlWindow, "Set Active" );
            Mode.SetPosition( 10,10 );
            
            Mode.OnClick += sender =>
            {
                Mode.SetText( "Set " + Circuit.CurrentState );
                Circuit.ToggleState( );
            };

            Checkbox SnapToGrid = new Checkbox( ControlWindow ) { Checked = Circuit.SnapToGrid };
            SnapToGrid.SetText( "Snap to grid" );
            SnapToGrid.OnCheckedChanged += sender => { Circuit.SnapToGrid = SnapToGrid.Checked; };
            SnapToGrid.AlignLeft( Mode );
            SnapToGrid.MoveBelow( Mode, 5 );

            Checkbox ShowGrid = new Checkbox( ControlWindow ) { Checked = Circuit.ShowGrid };
            ShowGrid.OnCheckedChanged += sender =>
            {
                Circuit.ShowGrid = ShowGrid.Checked;
            };
            ShowGrid.AlignLeft( SnapToGrid );
            ShowGrid.MoveBelow( SnapToGrid, 5 );
            ShowGrid.SetText( "Show grid" );

            Checkbox ShowGateLabels = new Checkbox( ControlWindow ) { Checked = Circuit.ShowLabels };
            ShowGateLabels.OnCheckedChanged += sender =>
            {
                Circuit.ShowLabels = ShowGateLabels.Checked;
            };
            ShowGateLabels.SetText( "Show gate labels" );
            ShowGateLabels.AlignLeft( ShowGrid );
            ShowGateLabels.MoveBelow( ShowGrid, 5 );

            Label GridSliderLabel = new Label( ControlWindow );
            GridSliderLabel.SetText( "Grid size: " + Circuit.GridSize );
            GridSliderLabel.SizeToContents( );
            GridSliderLabel.MoveRightOf( Mode, 40 );

            HorizontalSlider GridSize = new HorizontalSlider( ControlWindow )
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

            #endregion
            
            #region Gate Window

            GateWindow = new Window( "Gates", Canvas )
            {
                Width = 150,
                ShowCloseButton = false
            };

            GateList = new CategoryList( GateWindow );
            GateList.SetSize( GateWindow.Size );

            #region Adding Gates and Categories

            Dictionary<string, CategoryHeader> Categories = new Dictionary<string, CategoryHeader>( );

            foreach ( Type T in Assembly.GetExecutingAssembly( ).GetTypes( ) )
            {
                if ( !T.IsSubclassOf( typeof ( Gate ) ) ) continue;

                Gate G = Activator.CreateInstance( T ) as Gate;

                // ReSharper disable once PossibleNullReferenceException
                if ( !Categories.ContainsKey( G.Category ) )
                    Categories.Add( G.Category, GateList.AddCategory( G.Category ) );

                Button GateButton = Categories[ G.Category ].AddButton( G.Name );
                GateButton.UserData = T;
                GateButton.OnClick += GateButton_Clicked;
                G.Remove( );
            }

            #endregion

            #endregion

            LayoutWindows( );
        }

        private void LayoutWindows( )
        {
            ControlWindow.SetWidth( this.Canvas.Width );
            ControlWindow.SetPosition( 0, this.Canvas.Height - ControlWindow.Height );

            GateWindow.SetHeight( this.Canvas.Height - ControlWindow.TitleBar.BoundingVolume.Height );
            GateList.SetSize( GateWindow.Size );
        }

        protected override void OnResize( )
        {
            base.OnResize( );

            LayoutWindows( );
        }

        void GateButton_Clicked( Button control )
        {
            if ( Circuit.CurrentState == Circuit.State.Active ) return;

            Circuit.StartGatePlacing( control.UserData as Type );
        }
    }
}
