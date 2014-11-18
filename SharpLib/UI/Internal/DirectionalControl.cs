using OpenTK;

namespace SharpLib2D.UI.Internal
{
    public abstract class DirectionalControl : Control
    {
        protected bool Horizontal
        {
            private set;
            get;
        }

        protected internal Vector2 LengthVector
        {
            get
            {
                return Horizontal ? Vector2.UnitX : Vector2.UnitY;
            }
        }

        protected internal Vector2 ThicknessVector
        {
            get
            {
                return Horizontal ? Vector2.UnitY : Vector2.UnitX;
            }
        }

        protected float Length
        {
            get
            {
                return Horizontal ? Width : Height;
            }
        }

        protected internal float Thickness
        {
            get
            {
                return Horizontal ? Height : Width;
            }
        }

        protected DirectionalControl( bool Horizontal )
        {
            this.Horizontal = Horizontal;
        }
    }
}
