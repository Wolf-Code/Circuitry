
namespace Circuitry.Gates.Binary
{
    public class XOR : OR
    {
        public XOR( )
        {
            this.GetOutput( "Output" ).Description = "Returns 1 if either input is one, but not both.";
        }

        public override void OnInputChanged( Components.Input I )
        {
            bool B1 = this.GetInput( "Input 1" ).BinaryValue, B2 = this.GetInput( "Input 2" ).BinaryValue;
            this.SetOutput( "Output", B1 != B2 );
        }
    }
}
