using System;

namespace SharpLib2D.Exceptions
{
    public class StreamNotValidResourceException<T> : Exception
    {
        public StreamNotValidResourceException( Exception Original )
            : base( "Attempted to load " + typeof ( T ) + " from stream, but an error occured.", Original )
        {

        }
    }
}
