using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Entities;

namespace SharpLib2D.UI
{
    public abstract class Control : MouseEntity, IDisposable
    {
        protected new IEnumerable<Control> Children
        {
            get { return base.Children.Where( O => O is Control ) as IEnumerable<Control>; }
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

        protected virtual void DrawSelf( )
        {
            Canvas.Skin.DrawPanel( this );
        }

        public override void Draw( FrameEventArgs e )
        {
            DrawSelf( );
            base.Draw( e );
        }

        public virtual void Dispose( )
        {
            foreach ( Control C in this.Children )
                C.Dispose( );
        }
    }
}
