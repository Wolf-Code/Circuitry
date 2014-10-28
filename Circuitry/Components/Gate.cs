using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SharpLib2D.Graphics;
using SharpLib2D.Objects;

namespace Circuitry.Components
{
    public abstract class Gate : CircuitryEntity
    {
        #region Properties
        public List<Input> Inputs
        {
            protected set;
            get;
        }

        public List<Output> Outputs
        {
            protected set;
            get;
        }

        public Circuit Circuit
        {
            set;
            get;
        }

        protected bool Dragging
        {
            private set;
            get;
        }

        protected Vector2 DragPosition
        {
            private set;
            get;
        }

        public string Category
        {
            protected set;
            get;
        }

        public virtual string Name
        {
            get
            {
                return GetType( ).ToString( ).Split( '.' ).Last( );
            }
        }

        public bool Active { internal set; get; }

        public const int IODistance = 40;

        public const int SizeUnit = 60;

        #endregion

        protected Gate( )
        {
            Inputs = new List<Input>( );
            Outputs = new List<Output>( );
            SetGateSize( 1, 1 );
            Category = "Miscellaneous";
        }

        protected void SetGateSize( float W, float H )
        {
            this.SetSize( SizeUnit * W, SizeUnit * H );    
        }

        public virtual void Reset( )
        {
            foreach ( Output O in Outputs )
            {
                this.SetOutput( O.Name, false );
            }

            foreach ( Input I in Inputs )
                I.SetValue( false );
        }

        protected void ShowOptionsMenu( params MenuEntry [ ] Entries )
        {
            MenuEntry[ ] Ents = new MenuEntry[ Entries.Length + 1 ];
            Ents[ 0 ] = new MenuEntry( "Remove", Control => this.Remove( ) );
            for ( int X = 0; X < Entries.Length; X++ )
                Ents[ X + 1 ] = Entries[ X ];

            this.Circuit.ShowMenu( Ents );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            switch ( Button )
            {
                case MouseButton.Left:
                    if ( Circuit.CurrentState == Circuit.State.Build && MouseCanSelect( ) )
                    {
                        Dragging = true;
                        DragPosition = ToLocal( ParentState.Camera.ToWorld( SharpLib2D.Info.Mouse.Position ) );
                    }
                    break;

                    case MouseButton.Right:
                    if ( Circuit.CurrentState == Circuit.State.Build && MouseCanSelect( ) )
                        this.ShowOptionsMenu( );
                    break;
            }

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( Circuit.CurrentState == Circuit.State.Build )
                Dragging = false;

            base.OnButtonReleased( Button );
        }

        public override void Update( FrameEventArgs e )
        {
            if ( Dragging )
            {
                Vector2 DPos = SharpLib2D.Info.Mouse.Position - DragPosition;
                Vector2 Pos = !this.Circuit.SnapToGrid
                        ? ParentState.Camera.ToWorld( DPos )
                        : this.Circuit.SnapPositionToGrid( ParentState.Camera.ToWorld( DPos + new Vector2( this.Circuit.GridSize / 2f ) ) );

                this.SetPosition( Pos );
            }

            base.Update( e );
        }

        #region In/Out

        public T GetIO<T>( string IOName ) where T : IONode
        {
            if ( typeof( T ) == typeof( Input ) )
                return GetInput( IOName ) as T;

            if ( typeof( T ) == typeof( Output ) )
                return GetOutput( IOName ) as T;

            return null;
        }

        public void SetOutput( string OutputName, double Value )
        {
            Output O = GetOutput( OutputName );
            O.SetValue( Value );
            Circuit.PushSignal( O );
        }

        public void SetOutput( string OutputName, bool Value )
        {
            SetOutput( OutputName, Value ? 1f : 0f );
        }

        public Input GetInput( string InputName )
        {
            return Inputs.FirstOrDefault( I => I.Name == InputName );
        }

        public Output GetOutput( string OutputName )
        {
            return Outputs.FirstOrDefault( O => O.Name == OutputName );
        }

        protected void LayoutInputs( )
        {
            float Diff = Size.Y / ( Inputs.Count + 1 );

            for( int X = 0; X < Inputs.Count; X++ )
            {
                Inputs[ X ].SetPosition( -( IODistance + Size.X / 2 ), Diff * ( X + 1 ) - Size.Y / 2 );
            }
        }

        protected void LayoutOutputs( )
        {
            float Diff = Size.Y /( Outputs.Count + 1 );

            for ( int X = 0; X < Outputs.Count; X++ )
            {
                Outputs[ X ].SetPosition( Size.X / 2 + IODistance, Diff * ( X + 1 ) - Size.Y / 2 );
            }
        }

        protected void AddInput( IONode.NodeType Type, string InputName, string Description )
        {
            Input I = new Input( Type, InputName, Description );
            I.SetParent( this );
            Inputs.Add( I );

            LayoutInputs( );
        }

        protected void AddOutput( IONode.NodeType Type, string OutputName, string Description )
        {
            Output O = new Output( Type, OutputName, Description );
            O.SetParent( this );
            Outputs.Add( O );

            LayoutOutputs( );
        }

        public virtual void OnInputChanged( Input I )
        {

        }

        protected void DrawTexturedSelf( )
        {
            Rectangle.DrawTextured( TopLeft.X, TopLeft.Y, Size.X, Size.Y );
        }

        protected void DrawIOConnectors( )
        {
            GL.BlendFunc( BlendingFactorSrc.One, BlendingFactorDest.Zero );
            {
                Texture.EnableTextures( false );

                Color.Set( 1f, 1f, 1f );
                foreach ( Input I in Inputs )
                {
                    Line.Draw( I.Position, I.Position + new Vector2( IODistance * 2, 0 ), 3f );
                }

                foreach ( Output O in Outputs )
                {
                    Line.Draw( O.Position - new Vector2( IODistance * 2, 0 ), O.Position, 3f );
                }
            }
            GL.BlendFunc( BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha );
        }

        #endregion

        protected void DefaultDraw( Texture T )
        {
            this.DrawIOConnectors( );

            Color.Set( 1f, 1f, 1f );
            T.Bind( );

            DrawTexturedSelf( );
        }
    }
}
