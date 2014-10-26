
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
