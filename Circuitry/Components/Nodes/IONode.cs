using System;
using Circuitry.Components.Nodes;
using Circuitry.States;
using Circuitry.UI;
using Gwen;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SharpLib2D.Graphics;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.Components
{
    public class IONode : CircuitryEntity
    {
        #region Enums

        public enum NodeType
        {
            Binary,
            Numeric
        }

        public enum NodeDirection
        {
            In,
            Out
        }

        #endregion

        #region Properties

        public NodeDirection Direction
        {
            protected set;
            get;
        }

        public NodeType Type
        {
            protected set;
            get;
        }

        public IONode NextNode { set; get; }
        public IONode PreviousNode { set; get; }

        /// <summary>
        /// Returns true if the node has a previous node.
        /// </summary>
        public bool HasPreviousNode
        {
            get { return PreviousNode != null; }
        }

        /// <summary>
        /// Returns true if the node has a next node.
        /// </summary>
        public bool HasNextNode
        {
            get { return NextNode != null; }
        }

        /// <summary>
        /// The node's identifying name.
        /// </summary>
        public string Name
        {
            protected set;
            get;
        }

        /// <summary>
        /// A description of this node's purpose.
        /// </summary>
        public string Description
        {
            internal set;
            get;
        }

        /// <summary>
        /// The node's value.
        /// </summary>
        public double Value
        {
            private set;
            get;
        }

        /// <summary>
        /// The node's binary value.
        /// </summary>
        public bool BinaryValue
        {
            get
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                // If the value is equal to 1f, then we're one. Zero otherwise.
                return Value == 1.0;
            }
        }

        /// <summary>
        /// The size each node has.
        /// </summary>
        public const int NodeSize = Gate.SizeUnit / 4;

        /// <summary>
        /// The gate this node belongs to.
        /// </summary>
        public Gate Gate
        {
            get
            {
                return Parent as Gate;
            }
        }

        public virtual bool IsInput
        {
            get { return false; }
        }

        public virtual bool IsOutput
        {
            get { return false; }
        }

        public bool IsLink
        {
            get { return !IsInput && !IsOutput; }
        }

        #endregion

        public IONode FirstNode
        {
            get
            {
                IONode Prev = PreviousNode ?? this;

                while ( Prev != null && Prev.HasPreviousNode )
                {
                    Prev = Prev.PreviousNode;
                }

                return Prev;
            }
        }

        public IONode LastNode
        {
            get
            {
                IONode Next = NextNode ?? this;

                while ( Next != null && Next.HasNextNode )
                {
                    Next = Next.NextNode;
                }

                return Next;
            }
        }

        public IONode( NodeType Type, NodeDirection Dir )
        {
            this.Type = Type;
            Value = 0f;
            SetSize( NodeSize, NodeSize );
            Direction = Dir;
        }

        protected IONode( NodeType Type, NodeDirection Direction, string Name, string Description ) : this( Type, Direction )
        {
            this.Name = Name;
            this.Description = Description;
        }

        /// <summary>
        /// Sets the node's value.
        /// </summary>
        /// <param name="NewValue">The new value.</param>
        public void SetValue( double NewValue )
        {
            Value = NewValue;
        }

        /// <summary>
        /// Sets the node's binary value.
        /// </summary>
        /// <param name="NewValue">The new value.</param>
        public void SetValue( bool NewValue )
        {
            Value = NewValue ? 1.0 : 0.0;
        }

        public static void DrawNode( Vector2 Pos )
        {
            Circle.DrawOutlined( Pos.X, Pos.Y, NodeSize / 2f, 1f, Color4.Black, Color4.White );
        }

        public override void Draw( FrameEventArgs e )
        {
            DrawNode( Position );

            base.Draw( e );
        }

        public override void OnMouseEnter( )
        {
            if ( this.IsInput || this.IsOutput )
            {
                ( ParentState as GwenState ).GwenCanvas.SetToolTipText( this.Name + ": " + this.Description );
                ToolTip.Enable( ( ParentState as GwenState ).GwenCanvas );
            }

            base.OnMouseEnter( );
        }

        public override void OnMouseExit( )
        {
            if ( this.IsInput || this.IsOutput )
                ToolTip.Disable( ( ParentState as GwenState ).GwenCanvas );
            
            base.OnMouseExit( );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Manager.MouseInsideUI( ) || Gate == null )
                return;

            if ( Gate.Circuit.OnChildMouseAction( this,
                new MouseButtonEventArgs( ( int ) Mouse.Position.X, ( int ) Mouse.Position.Y, Button, true ) ) )
                return;

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( Manager.MouseInsideUI( ) || Gate == null )
                return;

            if ( Gate.Circuit.OnChildMouseAction( this,
                new MouseButtonEventArgs( ( int ) Mouse.Position.X, ( int ) Mouse.Position.Y, Button, false ) ) )
                return;

            base.OnButtonReleased( Button );
        }

        #region Connection

        protected override void OnRemove( )
        {
            if ( this.IsLink )
            {
                this.PreviousNode.NextNode = this.NextNode;
                this.NextNode.PreviousNode = this.PreviousNode;
            }
            base.OnRemove( );
        }

        /// <summary>
        /// Removes the entire connection.
        /// </summary>
        public void RemoveEntireConnection( )
        {
            IONode Node = this.FirstNode;
            while ( Node != null )
            {
                if ( !( Node is Input || Node is Output ) )
                    Node.Remove( );

                IONode Next = Node.NextNode;

                Node.PreviousNode = null;
                Node.NextNode = null;
                Node = Next;
            }
        }

        /// <summary>
        /// Checks whether two IONodes can connect.
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <returns></returns>
        public static bool CanConnect( IONode First, IONode Second )
        {
            if ( Second.HasPreviousNode || First.HasNextNode )
                return false;

            if ( First.Direction == Second.Direction )
                return false;

            if ( First is Output && First.Gate == Second.LastNode.Gate )
                return false;

            if ( Second is Input && First.FirstNode.Gate == Second.Gate )
                return false;

            return First.Type == NodeType.Numeric || Second.Type != NodeType.Numeric;
        }
        
        #endregion
    }
}
