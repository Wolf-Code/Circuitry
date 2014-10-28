
namespace Circuitry.Gates.Binary
{
    public class NOR : OR
    {
        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output",
                !( this.GetInput( "Input 1" ).BinaryValue || this.GetInput( "Input 2" ).BinaryValue ) );
        }
    }
}
