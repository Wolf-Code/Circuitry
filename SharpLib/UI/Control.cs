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
            get { return base.Children.OfType<Control>(  ); }
        }

        public bool PreventLeavingParent { protected set; get; }

        protected Control( )
        {
            this.PreventLeavingParent = true;
        }

        protected Canvas Canvas 
        {
            get
            {
                MouseEntityContainer C = Container;
                return C is Canvas ? C as Canvas : null;
            }
        }

        protected virtual void DrawSelf( )
        {
            Canvas.Skin.DrawPanel( this );
        }

        protected override Vector2 OnPositionChanged( Vector2 NewPosition )
        {
            if ( PreventLeavingParent && HasParent && ( Parent is ObjectEntity ) )
            {
                ObjectEntity C = Parent as ObjectEntity;

                if ( NewPosition.X < 0 )
                    NewPosition.X = 0;

                if ( NewPosition.Y < 0 )
                    NewPosition.Y = 0;

                if ( NewPosition.X + this.BoundingVolume.Width > C.BoundingVolume.Width )
                    NewPosition.X = C.BoundingVolume.Width - this.BoundingVolume.Width;

                if ( NewPosition.Y + this.BoundingVolume.Height > C.BoundingVolume.Height )
                    NewPosition.Y = C.BoundingVolume.Height - this.BoundingVolume.Height;
            }

            return NewPosition;
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
