using Gwen.Control;
using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;
using SharpLib2D.Info;

namespace Circuitry.Components
{
    public partial class Circuit : MouseEntityContainer
    {
        private bool DraggingCamera;
        private Menu Menu;

        public Bin Bin { protected set; get; }

        public Circuit( )
        {
            CurrentState = State.Build;
            GridSize = 60;
            ShowGrid = true;
            SnapToGrid = true;

            Bin = new Bin( );
            Bin.SetParent( this );
        }

        #region Draw / Update

        public override void Update( FrameEventArgs e )
        {
            UpdateGateDragging( );
            if ( DraggingCamera )
                ParentState.Camera.SetPosition( ParentState.Camera.Position - Mouse.Delta );

            switch ( CurrentState )
            {
                case State.Active:
                    Signal[ ] Copy = Signals.ToArray( );
                    Signals.Clear( );

                    foreach ( Signal S in Copy )
                    {
                        S.In.SetValue( S.Out.Value );
                        S.In.Gate.OnInputChanged( S.In );
                    }
                    break;

                case State.Build_Placing:
                    Vector2 Pos = !SnapToGrid 
                        ? ParentState.Camera.ToWorld( Mouse.Position ) 
                        : SnapPositionToGrid( ParentState.Camera.ToWorld( Mouse.Position + new Vector2( GridSize / 2f ) ) );
                    
                    GateToPlace.SetPosition( Pos );
                        
                    break;
            }

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
