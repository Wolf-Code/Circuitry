using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace SharpLib2D.UI
{
    public class Canvas : Entities.MouseEntityContainer
    {
        public Skin Skin { private set; get; }

        public Canvas( Skin S )
        {
            Skin = S;
        }
    }
}
