using Gwen.Control;
using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;
using SharpLib2D.Info;

namespace Circuitry.Components
{
    public partial class Circuit : MouseEntityContainer
    {
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
