using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpLib2D.Exceptions;

namespace SharpLib2D.Resources.Loaders
{
    public class Font : ResourceLoader
    {
        public override Type ResourceType
        {
            get { return typeof ( Resources.Font ); }
        }

        public override string [ ] Extensions
        {
            get { return new [ ] { "ttf" }; }
        }

        public override Resource Load( string Path )
        {
            if ( CachedResources.ContainsKey( Path ) )
                return CachedResources[ Path ];

            FontFamily Fam = FontFamily.Families.FirstOrDefault( O => O.Name == Path );

            if ( Fam != null )
            {
                Resources.Font F = new Resources.Font( Fam );
                CachedResources.TryAdd( Path, F );
                return F;
            }

            if ( !File.Exists( Path ) ) throw new StreamNotValidResourceException<Resources.Font>( new Exception( ) );
            StreamReader R = new StreamReader( Path );
            try
            {
                Resources.Font F = Load( R.BaseStream ) as Resources.Font;
                CachedResources.TryAdd( Path, F );
                return F;
            }
            finally
            {
                R.Dispose( );
            }
        }

        public override Resource Load( Stream Stream )
        {
            byte [ ] Buffer = new byte[ Stream.Length ];
            Stream.Read( Buffer, 0, Buffer.Length );
            PrivateFontCollection C = new PrivateFontCollection( );

            GCHandle Hndl = GCHandle.Alloc( Buffer, GCHandleType.Pinned );
            try
            {
                IntPtr Ptr = Marshal.UnsafeAddrOfPinnedArrayElement( Buffer, 0 );
                C.AddMemoryFont( Ptr, Buffer.Length );
                FontFamily F = C.Families[ 0 ];

                return new Resources.Font( F );
            }
            finally
            {
                Hndl.Free( );
                C.Dispose( );
            }
        }
    }
}
