using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Entities;

namespace SharpLib2D.UI
{
    public abstract class Control : MouseEntity, IDisposable
    {
        protected new List<Control> Children
        {
            get { return base.Children.Where( O => O is Control ) as List<Control>; }
        }

        protected Canvas Canvas 
        {
            get
            {
                MouseEntityContainer C = Container;
                return C is Canvas ? C as Canvas : null;
            }
        }

        public override Vector2 TopLeft
        {
            get { return Position; }
        }

        public override Vector2 BottomRight
        {
            get { return Position + Size; }
        }

        protected void DrawChildren( FrameEventArgs e )
        {
            base.Draw( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            Canvas.Skin.DrawControl( this );
            DrawChildren( e );
        }

        public virtual void Dispose( )
        {
            foreach ( Control C in this.Children )
                C.Dispose( );
        }
    }
}
