using System;
using System.Globalization;
using System.IO;

namespace SharpLib2D.Resources.Loaders
{
    public class Config : ResourceLoader
    {
        public override string [ ] Extensions
        {
            get { return new [ ] { "ini" }; }
        }

        public override Type ResourceType
        {
            get { return typeof ( Resources.Config ); }
        }

        public override Resource Load( string Path )
        {
            using ( StreamReader R = new StreamReader( Path ) )
                return Load( R.BaseStream );
        }

        public override Resource Load( Stream Stream )
        {
            Resources.Config Conf = new Resources.Config( );
            using ( StreamReader Reader = new StreamReader( Stream ) )
            {
                while ( !Reader.EndOfStream )
                {
                    string Line = Reader.ReadLine( );
                    if ( Line.StartsWith( "//" ) )
                        continue;

                    if ( Line.StartsWith( "[", true, CultureInfo.InvariantCulture ) )
                    {
                        Line = Line.Trim( '[', ']' );
                    }
                    else
                    {
                        string [ ] Split = Line.Split( '=' );
                        string Name = Split[ 0 ].Trim( );
                        string Value = Split[ 1 ].Trim( );
                    }
                }
            }

            return Conf;
        }
    }
}
