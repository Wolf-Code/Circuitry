using Gwen.Control;
using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;

namespace Circuitry.Components
{
    public partial class Circuit : MouseEntityContainer
    {
        private Menu Menu;

        public Bin Bin { protected set; get; }

        private CircuitDragger Dragger;

        public Circuit( )
        {
            CurrentState = State.Build;
            GridSize = 64;
            ShowGrid = true;
            SnapToGrid = true;

            Bin = new Bin( );
            Bin.SetParent( this );

            Dragger = new CircuitDragger( this );
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
