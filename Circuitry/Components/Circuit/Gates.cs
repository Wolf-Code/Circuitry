using System;
using OpenTK;
using SharpLib2D.Info;

namespace Circuitry.Components
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

        private bool DraggingGate;
        protected Gate DragGate { private set; get; }

        private Vector2 LocalGateGrabPoint;

        #region Dragging

        protected void StartGateDragging( Gate G )
        {
            if ( DraggingGate )
                StopGateDragging( DragGate );

            DraggingGate = true;
            LocalGateGrabPoint = G.ToLocal( ParentState.Camera.ToWorld( Mouse.Position ) );
            DragGate = G;
        }

        private void UpdateGateDragging( )
        {
            if ( !DraggingGate ) return;

            Vector2 DPos = Mouse.Position - LocalGateGrabPoint;
            Vector2 Pos = !this.SnapToGrid
                ? ParentState.Camera.ToWorld( DPos )
                : this.SnapPositionToGrid( ParentState.Camera.ToWorld( DPos + new Vector2( this.GridSize / 2f ) ) );

            DragGate.SetPosition( Pos );
        }

        protected void StopGateDragging( Gate G )
        {
            DraggingGate = false;
        }

        #endregion

        public void StartGatePlacing( Type G )
        {
            if ( GateToPlace != null )
                GateToPlace.Remove( );

            GateToPlace = Activator.CreateInstance( G ) as Gate;
            CurrentState = State.Build_Placing;
        }

        public void AddGate( Gate G )
        {
            G.SetParent( this );
            G.Circuit = this;
            G.Reset( );
        }
    }
}
