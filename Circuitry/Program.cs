using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
