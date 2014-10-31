using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SharpLib2D.Graphics;
using SharpLib2D.Objects;
using Mouse = SharpLib2D.Info.Mouse;

namespace Circuitry.Components
{
    public abstract class Gate : CircuitryEntity
    {
        #region Properties

        #region IO

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

        #endregion

        #region Dragging

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

        #endregion

        #region Info

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

        protected virtual Vector2 TextPosition
        {
            get { return Position; }
        }

        protected string Texture { set; get; }

        #endregion

        public bool Active { internal set; get; }

        public const int IODistance = 25;

        public const int SizeUnit = 60;

        protected int Outline { set; get; }

        #endregion

        protected Gate( )
        {
            Inputs = new List<Input>( );
            Outputs = new List<Output>( );
            SetGateSize( 1, 1 );
            Outline = 3;
            Category = "Miscellaneous";
        }

        protected void SetGateSize( float W, float H )
        {
            SetSize( SizeUnit * W, SizeUnit * H );    
        }

        public virtual void Reset( )
        {
            foreach ( Output O in Outputs )
                O.SetValue( false );

            foreach ( Input I in Inputs )
                I.SetValue( false );
        }

        protected void ShowOptionsMenu( params MenuEntry [ ] Entries )
        {
            MenuEntry[ ] Ents = new MenuEntry[ Entries.Length + 1 ];
            Ents[ 0 ] = new MenuEntry( "Remove", Control => Remove( ) );
            for ( int X = 0; X < Entries.Length; X++ )
                Ents[ X + 1 ] = Entries[ X ];

            Circuit.ShowMenu( Ents );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            switch ( Button )
            {
                case MouseButton.Left:
                    if ( Circuit.CurrentState == Circuit.State.Build && MouseCanSelect( ) )
                    {
                        Dragging = true;
                        DragPosition = ToLocal( ParentState.Camera.ToWorld( Mouse.Position ) );
                    }
                    break;

                    case MouseButton.Right:
                    if ( Circuit.CurrentState == Circuit.State.Build && MouseCanSelect( ) )
                        ShowOptionsMenu( );
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
                Vector2 DPos = Mouse.Position - DragPosition;
                Vector2 Pos = !Circuit.SnapToGrid
                        ? ParentState.Camera.ToWorld( DPos )
                        : Circuit.SnapPositionToGrid( ParentState.Camera.ToWorld( DPos + new Vector2( Circuit.GridSize / 2f ) ) );

                SetPosition( Pos );
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
                SharpLib2D.Graphics.Texture.EnableTextures( false );

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

        private void DefaultDraw( string Path )
        {
            DrawIOConnectors( );

            Color.Set( 1f, 1f, 1f );
            SharpLib2D.Graphics.Texture.Set( Path );

            DrawTexturedSelf( );
        }

        protected void DefaultTexturedDraw( )
        {
            DefaultDraw( Texture );
        }

        protected virtual void DrawBody( )
        {
            Rectangle.DrawRoundedOutlined( TopLeft.X, TopLeft.Y, Size.X, Size.Y, Color4.Black,
                Color4.White, Outline );
        }

        protected void DefaultDraw( )
        {
            DrawIOConnectors( );

            DrawBody( );

            if ( Circuit != null && Circuit.ShowLabels )
            {
                Color.Set( Color4.Black );
                Text.SetAlignments( Text.HorizontalAlignment.Center, Text.VerticalAlignment.Center );
                Text.DrawString( Name, "Arial", 10f, TextPosition );
            }
        }
    }
}
