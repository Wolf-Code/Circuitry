
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SharpLib2D.Graphics;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        public bool ConnectingNodes { private set; get; }
        private IONode ConnectionNode;

        private void NodePathClick( MouseButton B )
        {
            if ( ConnectingNodes )
            {
                switch ( B )
                {
                    case MouseButton.Left:

                        IONode New = new Input( ConnectionNode.Type, "", "" );
                        New.SetParent( ConnectionNode );

                        Vector2 LocalPos = ConnectionNode.ToLocal( ParentState.Camera.ToWorld( SharpLib2D.Info.Mouse.Position ) );
                        New.SetPosition( LocalPos );

                        ConnectionNode.NextNode = New;
                        ConnectionNode = New;

                        break;

                        case MouseButton.Right:

                        break;
                }
            }
        }

        private void NodeMouseInput( IONode Node, MouseButtonEventArgs Args )
        {
            if ( Args.IsPressed )
            {
                if ( CurrentState == State.Build && !UI.Manager.MouseInsideUI(  ) )
                {
                    Console.WriteLine(Args);
                    switch ( Args.Button )
                    {
                        case MouseButton.Left:
                            if ( !ConnectingNodes )
                            {
                                if ( ( Node.IsInput && !Node.HasNextNode ) || ( Node.IsOutput && !Node.HasPreviousNode ) )
                                {
                                    ConnectingNodes = true;
                                    ConnectionNode = Node;
                                    Console.WriteLine( "Now creating node path." );
                                }
                            }
                            else
                            {
                                ConnectionNode.NextNode = Node;
                                ConnectingNodes = false;
                                Console.WriteLine( "And now stopping" );
                            }
                            break;

                        case MouseButton.Right:
                             if ( ConnectingNodes )
                            {
                                while( ConnectionNode.HasPreviousNode )
                                {
                                    ConnectionNode.PreviousNode = null;
                                    ConnectionNode = ConnectionNode.PreviousNode;
                                    ConnectionNode.NextNode.Remove( );

                                    ConnectionNode.NextNode = null;
                                }

                                ConnectingNodes = false;
                            }
                            break;
                    }
                }
            }
        }

        private void DrawConnections( )
        {
            foreach ( Gate E in Children.OfType<Gate>( ) )
            {
                foreach ( Output O in E.Outputs )
                {
                    if ( !O.HasNextNode ) continue;

                    IONode Cur = O;
                    IONode Next = O.NextNode;
                    Color4 C = Cur.BinaryValue ? Color4.LimeGreen : Color4.Blue;

                    while ( Next != null )
                    {
                        Vector2 Diff = Next.Position - Cur.Position;
                        Diff /= 2;

                        Color.Set( 0.2f, 0.2f, 0.2f );
                        /*Line.DrawCubicBezierCurve( Cur.Position, Cur.NextNode.Position,
                            Cur.Position + new Vector2( Math.Abs( Diff.X ), 0 ),
                            Cur.NextNode.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 6f );*/
                        Line.Draw( Cur.Position, Next.Position, 6f );

                        Color.Set( C );

                        /*Line.DrawCubicBezierCurve( Cur.Position, Cur.NextNode.Position,
                            Cur.Position + new Vector2( Math.Abs( Diff.X ), 0 ),
                            Cur.NextNode.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 4f );*/
                        Line.Draw( Cur.Position, Next.Position, 4f );

                        Cur = Next;
                        Next = Next.NextNode;
                    }
                }
            }
        }
    }
}
