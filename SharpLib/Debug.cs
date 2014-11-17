using System;
using System.Collections.Generic;
using System.Text;

namespace SharpLib2D
{
    class Debug
    {
        public static void WriteLine( object Data )
        {
            Console.WriteLine( Data.ToString( ) );
        }

        public static void WriteLine( string Format, params object[ ] Args )
        {
            Console.WriteLine( Format, Args );
        }

        public static void PrintEnumerable<T>( IEnumerable<T> Collection )
        {
            StringBuilder Builder = new StringBuilder( );
            int C = 0;
            foreach ( T Item in Collection )
                Builder.AppendFormat( "{0}: {1}\n", C++, Item );

            WriteLine( Builder.ToString( ) );
        }
    }
}
