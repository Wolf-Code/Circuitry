﻿using System;
using OpenTK;
using OpenTK.Input;

namespace SharpLib2D.Entities
{
    public class MouseEntity : Entity
    {
        public MouseEntityContainer Container
        {
            get
            {
                if ( HasParent )
                    return ( ( MouseEntity ) Parent ).Container;
                
                if ( this is MouseEntityContainer )
                    return this as MouseEntityContainer;

                return null;
            }
        }

        public virtual MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            MouseEntity E = GetChildAt( CheckPosition ) as MouseEntity;

            return E != null ? E.GetTopChild( CheckPosition ) : this;
        }

        public virtual void OnButtonPressed( MouseButton Button )
        {

        }

        public virtual void OnButtonReleased( MouseButton Button )
        {

        }

        public virtual void OnMouseEnter( )
        {

        }

        public virtual void OnMouseExit( )
        {

        }
    }
}
