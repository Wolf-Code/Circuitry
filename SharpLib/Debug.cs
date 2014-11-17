using System;
using System.Collections.Generic;
using System.Text;

namespace SharpLib2D
{
    class Debug
    {
        /// <summary>
        /// Debugs an object into the console.
        /// </summary>
        /// <param name="Data">The object to write.</param>
        public static void WriteLine( object Data )
        {
            Console.WriteLine( Data.ToString( ) );
        }

        /// <summary>
        /// Debugs a formatted line into the console.
        /// </summary>
        /// <param name="Format">The formatting line.</param>
        /// <param name="Args">The values used in <paramref name="Format"/></param>
        public static void WriteLine( string Format, params object[ ] Args )
        {
            Console.WriteLine( Format, Args );
        }

        /// <summary>
        /// Debugs an enumerable into the console.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="Collection">The enumerable.</param>
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
