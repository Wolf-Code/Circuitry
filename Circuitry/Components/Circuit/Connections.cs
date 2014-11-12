﻿using System;
using System.Linq;
using Circuitry.UI;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SharpLib2D.Entities;
using SharpLib2D.Graphics;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.Components
{
    public partial class Circuit
    {
        public bool ConnectingNodes { private set; get; }
        private bool ConnectingOutput;
        private IONode ConnectionNode;

        private void NodePathClick( MouseButton B )
        {
            if ( ConnectingNodes )
            {
                switch ( B )
                {
                    case MouseButton.Left:
                        if ( Manager.MouseInsideUI( ) )
                            return;

                        IONode New = new IONode( ConnectionNode.Type, ConnectionNode.Direction );
                        New.SetParent( ConnectionNode.Gate );

                        Vector2 LocalPos = New.Gate.ToLocal( ParentState.Camera.ToWorld( Mouse.Position ) );
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

                        break;

                        case MouseButton.Right:
                        if ( ConnectingNodes )
                            CancelConnecting( );
                        break;
                }
            }
        }

        private void NodeLeftMouseDown( IONode Node )
        {
            if ( !ConnectingNodes )
            {
                if ( ( Node.IsInput && !Node.HasNextNode ) || ( Node.IsOutput && !Node.HasPreviousNode ) )
                {
                    ConnectingNodes = true;
                    ConnectionNode = Node;
                    ConnectingOutput = Node.IsOutput;
                }
                else
                {
                    if ( !Manager.MouseInsideUI( ) )
                    {
                        if ( !Node.IsInput && !Node.IsOutput )
                            Dragger.StartDragging( Node );
                        else
                            Dragger.StartDragging( Node.Gate );
                    }
                }
            }
            else
            {
                FinishConnection( Node );
            }
        }

        private bool FinishConnection( IONode Node )
        {
            if ( ConnectingOutput )
            {
                if ( !IONode.CanConnect( ConnectionNode, Node ) )
                    return false;

                ConnectionNode.NextNode = Node;
                Node.PreviousNode = ConnectionNode;
            }
            else
            {
                if ( !IONode.CanConnect( Node, ConnectionNode ) )
                    return false;

                ConnectionNode.PreviousNode = Node;
                Node.NextNode = ConnectionNode;
            }

            ConnectingNodes = false;
            return true;
        }

        private void CancelConnecting( )
        {
            ConnectionNode.RemoveEntireConnection( );

            ConnectingNodes = false;
        }

        private void NodeMouseInput( IONode Node, MouseButtonEventArgs Args )
        {
            if ( CurrentState != State.Build || Manager.MouseInsideUI( ) ) return;

            if ( Args.IsPressed )
            {
                switch ( Args.Button )
                {
                    case MouseButton.Left:
                        NodeLeftMouseDown( Node );
                        break;
                }
            }
            else
            {
                switch ( Args.Button )
                {
                        // Instantly connecting when dragging from node 1 to node 2.
                    case MouseButton.Left:
                        if ( ConnectingNodes )
                        {
                            MouseEntity E = Container.GetTopChild( Mouse.WorldPosition );
                            if ( E is IONode )
                                FinishConnection( E as IONode );
                        }
                        break;

                    case MouseButton.Right:
                        if ( !ConnectingNodes )
                        {
                            if ( Node.HasNextNode && Node.HasPreviousNode )
                            {
                                ShowMenu(
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
                                        New.SetParent( Node.Parent );
                                        New.SetPosition(
                                            New.Gate.ToLocal( ( Node.Position + Node.NextNode.Position ) / 2 ) );
                                        Node.NextNode.PreviousNode = New;
                                        Node.NextNode = New;
                                    } ),
                                    new MenuEntry( "Remove entire connection", Control => Node.RemoveEntireConnection( ) ) );
                            }
                            else
                                ShowMenu( new MenuEntry( "Remove connection",
                                    Control => Node.RemoveEntireConnection( ) ) );
                        }
                        break;
                }
            }
        }

        private static void DrawConnectionLine( Color4 Col, Vector2 Start, Vector2 End )
        {
            Color.Set( 0.2f, 0.2f, 0.2f );
            /*Line.DrawCubicBezierCurve( Cur.Position, Cur.NextNode.Position,
                Cur.Position + new Vector2( Math.Abs( Diff.X ), 0 ),
                Cur.NextNode.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 6f );*/
            Line.Draw( Start, End, 6f );

            Color.Set( Col );

            /*Line.DrawCubicBezierCurve( Cur.Position, Cur.NextNode.Position,
                Cur.Position + new Vector2( Math.Abs( Diff.X ), 0 ),
                Cur.NextNode.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 4f );*/
            Line.Draw( Start, End, 4f );
        }

        private void DrawConnection( IONode Start )
        {
            IONode Cur = Start;
            IONode Next = Start.NextNode;
            Color4 C = Cur.BinaryValue ? Color4.LimeGreen : Color4.Blue;

            while ( Next != null )
            {
                DrawConnectionLine( C, Cur.Position, Next.Position );

                Cur = Next;
                Next = Next.NextNode;
            }

            if ( ConnectingNodes )
            {
                if ( Cur == ConnectionNode )
                {
                    DrawConnectionLine( C, Cur.Position, Mouse.WorldPosition );
                }
                else if ( Start == ConnectionNode )
                    DrawConnectionLine( C, Start.Position, Mouse.WorldPosition );

                IONode.DrawNode( Mouse.WorldPosition );
            }
        }

        private void DrawConnections( )
        {
            foreach ( Gate E in Children.OfType<Gate>( ) )
            {
                foreach ( Output O in E.Outputs )
                {
                    DrawConnection( O );
                }

                foreach ( Input I in E.Inputs )
                {
                    DrawConnection( I.FirstNode );
                }
            }
        }
    }
}
