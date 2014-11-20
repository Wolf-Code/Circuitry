
using System.Collections;

namespace SharpLib2D.Resources
{
    public class Config : Resource
    {
        private readonly Hashtable Table = new Hashtable( );

        public object this[ string Name ]
        {
            get { return Table[ Name ]; }
            set { Table[ Name ] = value; }
        }

        public override void Dispose( )
        {
            
        }
    }
}
