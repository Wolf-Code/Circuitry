using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.Components
{
    public abstract class IONode : CircuitryEntity
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

        #endregion

        protected IONode( NodeType Type, string Name, string Description )
        {
            this.Type = Type;
            this.Name = Name;
            Value = 0f;
            this.Description = Description;
            SetSize( NodeSize, NodeSize );
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

        public override void Draw( FrameEventArgs e )
        {
            Circle.DrawOutlined( this.Position.X, this.Position.Y, NodeSize / 2f, 1f, Color4.Black, Color4.White );

            base.Draw( e );
        }

        public override void OnMouseEnter( )
        {
            //( ParentState as GwenState ).GwenCanvas.ToolTip.Show( );
            //( ParentState as GwenState ).GwenCanvas.ToolTip.SetToolTipText( this.Name + ": " + this.Description );
            base.OnMouseEnter( );
        }

        public override void OnMouseExit( )
        {
            //( ParentState as GwenState ).GwenCanvas.ToolTip.Hide( );
            base.OnMouseExit( );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Gate.Circuit.OnChildMouseAction( this,
                new MouseButtonEventArgs( ( int ) Mouse.Position.X, ( int ) Mouse.Position.Y, Button, true ) ) )
                return;

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( Gate.Circuit.OnChildMouseAction( this,
                new MouseButtonEventArgs( ( int ) Mouse.Position.X, ( int ) Mouse.Position.Y, Button, false ) ) )
                return;

            base.OnButtonReleased( Button );
        }

        #region Connection

        protected override void OnRemove( )
        {
            //Disconnect( );

            base.OnRemove( );
        }
        /*
        public void Disconnect( )
        {
            if ( !IsConnected )
                return;

            IONode Output = Direction == NodeDirection.Out ? this : Connection;

            Output.Gate.SetOutput( Output.Name, false );

            Connection.Connection = null;
            Connection = null;
        }

        public void ConnectTo( IONode Node )
        {
            if ( !CanConnect( Node ) )
                return;

            Connection = Node;
            Node.Connection = this;

            IONode Output = Direction == NodeDirection.Out ? this : Node;

            Output.Gate.SetOutput( Output.Name, Output.Value );
        }
        
        public bool CanConnect( IONode Node )
        {
            // Input can't connect to input, output can't connect to output.
            if ( Direction == Node.Direction )
                return false;

            IONode Input = Direction == NodeDirection.In ? this : Node;
            IONode Output = Direction == NodeDirection.Out ? this : Node;

            return ( ( Input.Type != NodeType.Binary ) ||
                   ( Input.Type == Output.Type ) ) && !Input.IsConnected;
        }
        */
        #endregion
    }
}
