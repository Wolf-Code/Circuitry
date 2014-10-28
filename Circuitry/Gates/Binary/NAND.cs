namespace Circuitry.Gates.Binary
{
    public class NAND : AND
    {
        public NAND( )
        {
            this.GetOutput( "Output" ).Description = "Returns 0 if both input 1 and 2 are 1, 1 otherwise.";
        }

        public override void OnInputChanged( Components.Input I )
        {
            this.SetOutput( "Output", !( this.GetInput( "Input 1" ).BinaryValue && this.GetInput( "Input 2" ).BinaryValue ) );
        }

    }
}
