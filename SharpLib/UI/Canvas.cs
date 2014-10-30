
using SharpLib2D.Entities;

namespace SharpLib2D.UI
{
    public class Canvas : MouseEntityContainer
    {
        public Skin Skin { private set; get; }

        public Canvas( Skin S )
        {
            Skin = S;
        }
    }
}
