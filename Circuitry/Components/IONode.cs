using System;
using OpenTK;
using OpenTK.Input;

namespace Circuitry.Components
{
    public abstract class IONode : CircuitryEntity
    {
        private static readonly SharpLib2D.Graphics.Texture NodeTexture;
        private static IONode StartNode;

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

        public IONode Connection
        {
            protected set;
            get;
        }

        public bool IsConnected
        {
            get
            {
                return this.Connection != null;
            }
        }

        public string Name
        {
            protected set;
            get;
        }

        public string Description
        {
            protected set;
            get;
        }

        public float Value
        {
            private set;
            get;
        }

        public bool BinaryValue
        {
            get
            {
                return Value == 1f;
            }
        }

        public const int NodeSize = 20;

        public Gate Gate
        {
            get
            {
                return this.Parent as Gate;
            }
        }

        #endregion

        static IONode( )
        {
            NodeTexture = SharpLib2D.Graphics.Texture.Load( "Resources\\Textures\\Components\\Node.png" );
        }

        protected IONode( NodeType Type, string Name, string Description )
        {
            this.Type = Type;
            this.Name = Name;
            Value = 0f;
            this.Description = Description;
            SetSize( NodeSize, NodeSize );
        }

        public void SetValue( float Value )
        {
            this.Value = Value;
        }

        public void SetValue( bool Value )
        {
            this.Value = Value ? 1f : 0f;
        }

        public override void Draw( FrameEventArgs e )
        {
            SharpLib2D.Graphics.Color.Set( 1f, 1f, 1f );
            SharpLib2D.Graphics.Texture.Set( NodeTexture );
            SharpLib2D.Graphics.Rectangle.DrawTextured( TopLeft.X, TopLeft.Y, Size.X, Size.Y );

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

        public override void OnButtonPressed( OpenTK.Input.MouseButton Button )
        {
            if ( Gate.Circuit.CurrentState == Circuit.State.Build && MouseCanSelect( ) )
            {
                switch ( Button )
                {
                    case OpenTK.Input.MouseButton.Left:
                        StartNode = this;
                        break;

                    case OpenTK.Input.MouseButton.Right:
                        Disconnect( );
                        break;
                }
            }
            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            switch ( Button )
            {
                case MouseButton.Left:
                    if ( Gate.Circuit.CurrentState == Circuit.State.Build )
                    {
                        SharpLib2D.Entities.MouseEntity Top =
                            this.Container.GetTopChild( this.ParentState.Camera.ToWorld( SharpLib2D.Info.Mouse.Position ) );

                        if ( Top is IONode )
                        {
                            IONode Node = Top as IONode;
                            if ( CanConnect( Node ) )
                                ConnectTo( Node );
                        }

                        StartNode = null;
                    }
                    break;
            }

            base.OnButtonReleased( Button );
        }

        #region Connection

        protected override void OnRemove( )
        {
            this.Disconnect( );

            base.OnRemove( );
        }

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

            IONode Output = this.Direction == NodeDirection.Out ? this : Node;

            Output.Gate.SetOutput( Output.Name, Output.Value );
        }

        public bool CanConnect( IONode Node )
        {
            // Input can't connect to input, output can't connect to output.
            if ( this.Direction == Node.Direction )
                return false;

            IONode Input = this.Direction == NodeDirection.In ? this : Node;
            IONode Output = this.Direction == NodeDirection.Out ? this : Node;

            return ( ( Input.Type != NodeType.Binary ) ||
                   ( Input.Type == Output.Type ) ) && !Input.IsConnected;
        }

        #endregion
    }
}
