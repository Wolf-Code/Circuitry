using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLib2D.Objects;

namespace SharpLib2D.Math
{
    public static class BoundingVolumes
    {
        public static BoundingRectangle IntersectionRectangle( BoundingRectangle Container, BoundingRectangle Intersector )
        {
            BoundingRectangle New = new BoundingRectangle( Intersector.Left, Intersector.Top, Intersector.Width, Intersector.Height );

            float Diff = 0;
            if ( New.Left < Container.Left )
            {
                Diff = Container.Left - New.Left;
                New.Position.X += Diff;
                New.Size.X -= Diff;
            }

            if ( New.Top < Container.Top )
            {
                Diff = Container.Top - New.Top;
                New.Position.Y += Diff;
                New.Size.Y -= Diff;
            }

            if ( New.Right > Container.Right )
                New.Size.X -= ( New.Right - Container.Right );

            if ( New.Bottom > Container.Bottom )
                New.Size.Y -= ( New.Bottom - Container.Bottom );

            return New;
        }
    }
}
