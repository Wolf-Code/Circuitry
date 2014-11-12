using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SharpLib2D.Resources
{
    public class Font : Resource
    {
        public FontFamily Family { private set; get; }
        private readonly Dictionary<float, System.Drawing.Font> LoadedFonts = new Dictionary<float, System.Drawing.Font>( ); 

        public Font( FontFamily Fam )
        {
            Family = Fam;
        }

        public System.Drawing.Font Get( float Size )
        {
            if ( LoadedFonts.ContainsKey( Size ) )
                return LoadedFonts[ Size ];

            System.Drawing.Font F = new System.Drawing.Font( Family, Size );
            LoadedFonts.Add( Size, F );

            return F;
        }

        public override void Dispose( )
        {
            Family.Dispose( );
            foreach ( System.Drawing.Font F in LoadedFonts.Select( O => O.Value ) )
                F.Dispose( );

            LoadedFonts.Clear( );
        }
    }
}
