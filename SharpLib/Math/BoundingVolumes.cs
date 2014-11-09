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
        public static BoundingRectangle ClampRectangle( BoundingRectangle Container, BoundingRectangle ToClamp )
        {
            BoundingRectangle New = new BoundingRectangle( ToClamp.Left, ToClamp.Top, ToClamp.Width, ToClamp.Height );

            if ( New.Left < Container.Left )
                New.Position.X = Container.Left;

            if ( New.Top < Container.Top )
                New.Position.Y = Container.Top;

            if ( New.Right > Container.Right )
                New.Size.X -= ( New.Right - Container.Right );

            if ( New.Bottom > Container.Bottom )
                New.Size.Y -= ( New.Bottom - Container.Bottom );

            return New;
        }
    }
}
