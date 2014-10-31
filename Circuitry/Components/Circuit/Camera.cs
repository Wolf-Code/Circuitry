using Circuitry.UI;
using SharpLib2D.Info;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        private bool DraggingCamera;

        private void StartCameraDragging( )
        {
            if ( !Manager.MouseInsideUI( ) )
                DraggingCamera = true;
        }

        private void UpdateCameraDragging( )
        {
            if ( DraggingCamera )
                ParentState.Camera.SetPosition( ParentState.Camera.Position - Mouse.Delta );
        }

        private void StopCameraDragging( )
        {
            if ( DraggingCamera )
                DraggingCamera = false;
        }
    }
}
