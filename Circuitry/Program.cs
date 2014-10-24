
namespace Circuitry
{
    class Program
    {
        static void Main( string[ ] args )
        {
            using ( Engine E = new Engine( ) )
            {
                E.Run( 60 );
            }
        }
    }
}
