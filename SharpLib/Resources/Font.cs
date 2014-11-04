using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLib2D.Resources
{
    public class Font : Resource
    {
        public FontFamily Family { private set; get; }
        private Dictionary<float, System.Drawing.Font> LoadedFonts = new Dictionary<float, System.Drawing.Font>( ); 

        public Font( FontFamily Fam )
        {
            this.Family = Fam;
        }

        public System.Drawing.Font Get( float Size )
        {
            if ( LoadedFonts.ContainsKey( Size ) )
                return LoadedFonts[ Size ];

            System.Drawing.Font F = new System.Drawing.Font( this.Family, Size );
            LoadedFonts.Add( Size, F );

            return F;
        }
    }
}
