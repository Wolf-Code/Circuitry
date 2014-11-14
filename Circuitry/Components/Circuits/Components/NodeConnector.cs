using Circuitry.UI;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Entities;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.Components.Circuits.Components
{
    public class NodeConnector : ParentableEntity
    {
        private readonly Circuit Circuit;

        public bool ConnectingOutput { private set; get; }
        public IONode ConnectionNode { private set; get; }
        public bool ConnectingNodes { private set; get; }

        public NodeConnector( Circuit C )
        {
            this.Circuit = C;
            this.SetParent( C );
        }

        private bool IsMouseOnNode( out IONode Node )
        {
            Node = this.Circuit.GetTopChildAt( Mouse.WorldPosition ) as IONode;

            return Node != null;
        }

        private void Finish( IONode Node )
        {
            if ( ConnectingOutput )
            {
                if ( !IONode.CanConnect( ConnectionNode, Node ) )
                    return;

                ConnectionNode.NextNode = Node;
                Node.PreviousNode = ConnectionNode;
            }
            else
            {
                if ( !IONode.CanConnect( Node, ConnectionNode ) )
                    return;

                ConnectionNode.PreviousNode = Node;
                Node.NextNode = ConnectionNode;
            }

            ConnectingNodes = false;
        }

        public void Cancel( )
        {
            if ( !ConnectingNodes )
                return;

            ConnectionNode.RemoveEntireConnection( );

            ConnectingNodes = false;
        }

        #region Mouse Input

        private void OnLeftMouseDown( )
        {
            IONode Node;
            if ( IsMouseOnNode( out Node ) )
            {
                if ( !ConnectingNodes )
                {
                    if ( ( Node.IsInput && !Node.HasPreviousNode ) || ( Node.IsOutput && !Node.HasNextNode ) )
                    {
                        ConnectingNodes = true;
                        ConnectionNode = Node;
                        ConnectingOutput = Node.IsOutput;
                    }
                    else if ( !Manager.MouseInsideUI( ) )
                    {
                        Entity E = Node.IsLink ? Node as Entity : Node.Gate;
                        if ( E is Gate )
                            E.SetPosition( E.GetParent<Entity>( ).ToLocal( Mouse.WorldPosition ) );

                        Circuit.Dragger.StartDragging( E );
                    }
                }
                else
                    Finish( Node );
            }
            else
            {
                if ( !ConnectingNodes || Manager.MouseInsideUI( ) )
                    return;

                IONode New = new IONode( ConnectionNode.Type, ConnectionNode.Direction );
                New.SetParent( Circuit );

                Vector2 LocalPos = New.GetParent<Entity>(  ).ToLocal( ParentState.Camera.ToWorld( Mouse.Position ) );
                New.SetPosition( LocalPos );

                if ( ConnectingOutput )
                {
                    New.PreviousNode = ConnectionNode;
                    ConnectionNode.NextNode = New;
                    ConnectionNode = ConnectionNode.NextNode;
                }
                else
                {
                    New.NextNode = ConnectionNode;
                    ConnectionNode.PreviousNode = New;
                    ConnectionNode = ConnectionNode.PreviousNode;
                }
            }
        }

        private void OnLeftMouseUp( )
        {
            IONode Node;
            if ( IsMouseOnNode( out Node ) )
            {
                if ( !ConnectingNodes ) return;

                MouseEntity E = this.Circuit.GetTopChild( Mouse.WorldPosition );
                if ( E is IONode )
                    Finish( E as IONode );
            }
        }

        private void OnRightMouseDown( )
        {
            
        }

        private void OnRightMouseUp( )
        {
            IONode Node;
            if ( IsMouseOnNode( out Node ) )
            {
                if ( ConnectingNodes ) return;

                if ( Node.HasNextNode && Node.HasPreviousNode )
                {
                    Circuit.ShowMenu(
                        new MenuEntry( "Remove node", Control =>
                        {
                            Node.PreviousNode.NextNode = Node.NextNode;
                            Node.NextNode.PreviousNode = Node.PreviousNode;
                            Node.Remove( );
                        } ),
                        new MenuEntry( "Add node", Control =>
                        {
                            IONode New = new IONode( Node.Type, Node.Direction )
                            {
                                PreviousNode = Node,
                                NextNode = Node.NextNode
                            };
                            New.SetParent( Circuit );
                            New.SetPosition(
                                New.GetParent<Entity>( ).ToLocal( ( Node.Position + Node.NextNode.Position ) / 2 ) );
                            Node.NextNode.PreviousNode = New;
                            Node.NextNode = New;
                        } ),
                        new MenuEntry( "Remove entire connection", Control => Node.RemoveEntireConnection( ) ) );
                }
                else
                {
                    if ( Node.HasNextNode || Node.HasPreviousNode )
                    {
                        Circuit.ShowMenu(
                            new MenuEntry( "Add node", Control =>
                            {
                                if ( Node.HasNextNode )
                                {
                                    IONode New = new IONode( Node.Type, Node.Direction )
                                    {
                                        PreviousNode = Node,
                                        NextNode = Node.NextNode
                                    };
                                    New.SetParent( Circuit );
                                    New.SetPosition(
                                        New.GetParent<Entity>( )
                                            .ToLocal( ( Node.Position + Node.NextNode.Position ) / 2 ) );
                                    Node.NextNode.PreviousNode = New;
                                    Node.NextNode = New;
                                }
                                else
                                {
                                    IONode New = new IONode( Node.Type, Node.Direction )
                                    {
                                        NextNode = Node,
                                        PreviousNode = Node.PreviousNode
                                    };

                                    New.SetParent( Circuit );
                                    New.SetPosition(
                                        New.GetParent<Entity>( )
                                            .ToLocal( ( Node.Position + Node.PreviousNode.Position ) / 2 ) );
                                    Node.PreviousNode.NextNode = New;
                                    Node.PreviousNode = New;
                                }
                            } ),
                            new MenuEntry( "Remove entire connection", Control => Node.RemoveEntireConnection( ) ) );
                    }
                    else
                        Circuit.ShowMenu( new MenuEntry( "Remove connection",
                            Control => Node.RemoveEntireConnection( ) ) );
                }
            }
            else
                Cancel( );
        }

        #endregion

        public override void Update( FrameEventArgs e )
        {
            if ( Circuit.CurrentState != Circuit.State.Build || Manager.MouseInsideUI( ) ) return;

            if ( Mouse.IsPressed( MouseButton.Left ) )
                OnLeftMouseDown( );

            if ( Mouse.IsReleased( MouseButton.Left ) )
                OnLeftMouseUp( );

            if ( Mouse.IsPressed( MouseButton.Right ) )
                OnRightMouseDown( );

            if ( Mouse.IsReleased( MouseButton.Right ) )
                OnRightMouseUp( );

            base.Update( e );
        }
    }
}
