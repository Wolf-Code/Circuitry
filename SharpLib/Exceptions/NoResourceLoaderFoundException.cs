using System;
using System.IO;

namespace SharpLib2D.Exceptions
{
    public class NoResourceLoaderFoundException : Exception
    {
        public NoResourceLoaderFoundException( FileSystemInfo File )
            : base( "Attempted to load file '" + File.Name + "', but there was no appropriate loader found for extension '" + File.Extension + "'" )
        {

        }
    }
}
