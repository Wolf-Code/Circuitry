﻿using Circuitry.Components.Circuits.Components;
using Gwen.Control;
using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Resources;

namespace Circuitry.Components.Circuits
{
    public partial class Circuit : MouseEntityContainer
    {
        private Menu Menu;

        public Bin Bin { protected set; get; }
        public readonly NodeConnector Connector;

        public readonly CircuitDragger Dragger;

        public Circuit( )
        {
            CurrentState = State.Build;
            GridSize = 1 << 6;
            ShowGrid = true;
            SnapToGrid = true;

            Connector = new NodeConnector( this );

            Dragger = new CircuitDragger( this );

            Bin = new Bin( this );
        }

        #region Draw / Update

        public override void Update( FrameEventArgs e )
        {
            UpdateCameraDragging( );

            Update( CurrentState );

            base.Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            Texture.EnableTextures( false );

            DrawGrid( );
            DrawConnections( );
            
            base.Draw( e );
        }

        #endregion
    }
}
