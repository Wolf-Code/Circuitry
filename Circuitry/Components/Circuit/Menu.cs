using System;
using Circuitry.States;
using Gwen.Control;
using SharpLib2D.Info;

namespace Circuitry.Components
{
    public class MenuEntry
    {
        public string Text
        {
            private set;
            get;
        }
        public Base.GwenEventHandler Handler
        {
            private set;
            get;
        }

        public MenuEntry( string Text, Base.GwenEventHandler Handler )
        {
            this.Text = Text;
            this.Handler = Handler;
        }
    }

    public partial class Circuit
    {
        public void ShowMenu( params MenuEntry[ ] Items )
        {
            if ( Menu != null )
            {
                Menu.DelayedDelete( );
                Menu = null;
            }

            Menu = new Menu( ( ParentState as GwenState ).GwenCanvas );
            Menu.SetPosition( Mouse.Position.X, Mouse.Position.Y );
            foreach ( MenuEntry Entry in Items )
            {
                MenuItem I = Menu.AddItem( Entry.Text );
                I.Clicked += Entry.Handler;
            }
        }
    }
}
