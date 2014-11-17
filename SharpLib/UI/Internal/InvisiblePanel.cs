using SharpLib2D.Objects;

namespace SharpLib2D.UI.Internal
{
    /// <summary>
    /// A panel which is invisible and requires you to set the visible region.
    /// </summary>
    public class InvisiblePanel : Panel
    {
        private BoundingRectangle VisRect;

        public override BoundingRectangle VisibleRectangle
        {
            get { return VisRect; }
        }

        public void SetVisibleRegion( BoundingRectangle Size )
        {
            this.VisRect = Size;
        }

        protected override void DrawSelf( )
        {

        }
    }
}
