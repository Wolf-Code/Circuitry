using System;

namespace SharpLib2D.Exceptions
{
    public class SharpException : Exception
    {
        public SharpException( string FormatMessage, params object[ ] Objects )
            : base( string.Format( FormatMessage, Objects ) )
        {

        }

        public SharpException( string Message )
            : base( Message )
        {

        }
    }
}
