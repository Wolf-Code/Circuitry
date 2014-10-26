using System;
using System.Collections.Generic;
using System.Linq;
using Circuitry.States;
using Gwen.Control;
using OpenTK;
using OpenTK.Input;
using SharpLib2D.Entities;

namespace Circuitry.Components
{
    public class MenuEntry
    {
        public string Text { private set; get; }
        public Base.GwenEventHandler Handler { private set; get; }

        public MenuEntry( string Text, Base.GwenEventHandler Handler )
        {
            this.Text = Text;
            this.Handler = Handler;
        }
    }

    public class Circuit : MouseEntityContainer
    {
        private readonly List<Signal> Signals = new List<Signal>( );

        public int GridSize { set; get; }
        public bool ShowGrid { set; get; }
        public bool SnapToGrid { set; get; }

        public enum State
        {
            Build,
            Build_Placing,
            Active,
            Paused
        }

        public State CurrentState
        {
            private set;
            get;
        }

        protected Gate GateToPlace
        {
            private set;
            get;
        }

        private bool DraggingCamera;
        private Menu Menu;

        public Circuit( )
        {
            CurrentState = State.Active;
            this.GridSize = 60;
            this.ShowGrid = true;
            this.SnapToGrid = true;
        }

        public void ToggleState( )
        {
            if ( CurrentState != State.Build )
            {
                CurrentState = State.Build;
                // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
                ClearSignals( );

                foreach ( Gate M in Children )
                    M.Reset( );
            }
            else
                CurrentState = State.Active;
        }

        public void ShowMenu( params MenuEntry[ ] Items )
        {
            if ( Menu != null )
            {
                Menu.DelayedDelete( );
                Menu = null;
            }

            Menu = new Menu( ( ParentState as GwenState ).GwenCanvas );
            Menu.SetPosition( SharpLib2D.Info.Mouse.Position.X, SharpLib2D.Info.Mouse.Position.Y );
            foreach ( MenuEntry Entry in Items )
            {
                MenuItem I = Menu.AddItem( Entry.Text );
                I.Clicked += Entry.Handler;
            }
        }

        public Vector2 SnapPositionToGrid( Vector2 SnapPosition )
        {
            float X = ( float )Math.Floor( SnapPosition.X / GridSize ) * GridSize;
            float Y = ( float )Math.Floor( SnapPosition.Y / GridSize ) * GridSize;

            return new Vector2( X, Y );
        }

        #region Gate Management

        public void StartGatePlacing( Type G )
        {
            if ( GateToPlace != null )
                GateToPlace.Remove( );

            GateToPlace = Activator.CreateInstance( G ) as Gate;
            CurrentState = State.Build_Placing;
        }

        public void AddGate( Gate G )
        {
            G.SetParent( this );
            G.Circuit = this;
        }

        #endregion

        #region Signals

        public void PushSignal( Output O )
        {
            if ( !O.IsConnected )
                return;
            Signal S = new Signal( O, O.Connection as Input );
            if ( !Signals.Contains( S ) )
                Signals.Add( S );
        }

        public void ClearSignals( )
        {
            Signals.Clear( );
        }

        #endregion

        #region Mouse Input

        public override void OnButtonPressed( MouseButton Button )
        {
            switch ( Button )
            {
                case MouseButton.Left:
                    switch ( CurrentState )
                    {
                        case State.Build_Placing:
                            if ( !UI.Manager.MouseInsideUI( ) )
                            {
                                AddGate( GateToPlace );
                                GateToPlace.Active = true;
                                GateToPlace = null;
                                CurrentState = State.Build;
                            }
                            break;

                        default:
                            if ( !UI.Manager.MouseInsideUI( ) )
                                DraggingCamera = true;
                            break;
                    }
                    break;

                case MouseButton.Right:
                    switch ( CurrentState )
                    {
                        case State.Build_Placing:
                            if ( !UI.Manager.MouseInsideUI( ) )
                            {
                                GateToPlace.Remove( );
                                GateToPlace = null;
                                CurrentState = State.Build;
                            }
                            break;
                    }
                    break;
            }

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            switch ( Button )
            {
                case MouseButton.Left:
                    if ( DraggingCamera )
                        DraggingCamera = false;
                    break;
            }
            base.OnButtonReleased( Button );
        }

        #endregion

        #region Draw / Update / GetTopChild

        public override MouseEntity GetTopChild( Vector2 CheckPosition )
        {
            List<Entity> Ents = OrderedEntities;
            Entity E = Ents.FirstOrDefault( O => O.GetChildAt( CheckPosition ) != null );
            if ( E == null )
                return ( MouseEntity )Ents.FirstOrDefault( O => O.ContainsPosition( CheckPosition ) ) ?? this;

            Entity E2 = E.GetChildAt( CheckPosition );
            while ( E2 != null )
            {
                E = E2;
                E2 = E.GetChildAt( CheckPosition );
            }

            return ( MouseEntity )E;
        }

        public override void Update( FrameEventArgs e )
        {
            if ( DraggingCamera )
                ParentState.Camera.SetPosition( ParentState.Camera.Position - SharpLib2D.Info.Mouse.Delta );

            switch ( CurrentState )
            {
                case State.Active:
                    Signal[ ] Copy = Signals.ToArray( );
                    Signals.Clear( );

                    foreach ( Signal S in Copy )
                    {
                        S.In.SetValue( S.Out.Value );
                        S.In.Gate.OnInputChanged( S.In );
                    }
                    break;

                case State.Build_Placing:
                    Vector2 Pos = !SnapToGrid 
                        ? ParentState.Camera.ToWorld( SharpLib2D.Info.Mouse.Position ) 
                        : this.SnapPositionToGrid( ParentState.Camera.ToWorld( SharpLib2D.Info.Mouse.Position + new Vector2( GridSize / 2f ) ) );
                    
                    GateToPlace.SetPosition( Pos );
                        
                    break;
            }

            base.Update( e );
        }

        public override void Draw( FrameEventArgs e )
        {
            SharpLib2D.Graphics.Texture.EnableTextures( false );

            if( ShowGrid )
            {
                SharpLib2D.Graphics.Color.Set( 1f, 1f, 1f );

                SharpLib2D.Entities.Camera.DefaultCamera Cam = SharpLib2D.States.State.ActiveState.Camera;
                Vector2 P = this.SnapPositionToGrid( Cam.TopLeft );
                for ( float X = P.X; X < P.X + Cam.Size.X; X += GridSize )
                {
                    SharpLib2D.Graphics.Line.Draw( new Vector2( X, Cam.TopLeft.Y ), new Vector2( X, Cam.TopLeft.Y + Cam.Size.Y ) );
                }

                for ( float Y = P.Y; Y < P.Y + Cam.Size.Y; Y += GridSize )
                {
                    SharpLib2D.Graphics.Line.Draw( new Vector2( Cam.TopLeft.X, Y ), new Vector2( Cam.TopLeft.X + Cam.Size.X, Y ) );
                }
            }

            foreach ( Gate E in Children.OfType<Gate>( ) )
            {
                foreach ( Output O in E.Outputs )
                {
                    if ( !O.IsConnected || O.Direction != IONode.NodeDirection.Out ) continue;

                    Vector2 Diff = O.Connection.Position - O.Position;

                    SharpLib2D.Graphics.Color.Set( 0.2f, 0.2f, 0.2f );
                    SharpLib2D.Graphics.Line.DrawCubicBezierCurve( O.Position, O.Connection.Position, O.Position + new Vector2( Math.Abs( Diff.X ), 0 ), O.Connection.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 6f );

                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if ( !O.BinaryValue )
                        SharpLib2D.Graphics.Color.Set( 0.2f, 0.2f, 1f );
                    else
                        SharpLib2D.Graphics.Color.Set( 0.2f, 1f, 0.2f );

                    SharpLib2D.Graphics.Line.DrawCubicBezierCurve( O.Position, O.Connection.Position, O.Position + new Vector2( Math.Abs( Diff.X ), 0 ), O.Connection.Position - new Vector2( Math.Abs( Diff.X ), 0 ), 32, 4f );
                }
            }
            base.Draw( e );
        }

        #endregion
    }
}
