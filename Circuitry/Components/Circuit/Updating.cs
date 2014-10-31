using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using SharpLib2D.Info;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        private void Update( State S )
        {
            switch ( S )
            {
                case State.Active:
                    UpdateActive( );
                    break;

                case State.Build:
                    UpdateBuild( );
                    break;

                case State.Build_Placing:
                    UpdateBuildPlacing( );
                    break;
            }
        }

        private void UpdateActive( )
        {
            Signal[ ] Copy = Signals.ToArray( );
            Signals.Clear( );

            foreach ( Signal S in Copy )
            {
                S.In.SetValue( S.Out.Value );
                S.In.Gate.OnInputChanged( S.In );
            }
        }

        private void UpdateBuild( )
        {

        }

        private void UpdateBuildPlacing( )
        {
            Vector2 Pos = !SnapToGrid
                        ? ParentState.Camera.ToWorld( Mouse.Position )
                        : SnapPositionToGrid( ParentState.Camera.ToWorld( Mouse.Position + new Vector2( GridSize / 2f ) ) );

            GateToPlace.SetPosition( Pos );
        }
    }
}
