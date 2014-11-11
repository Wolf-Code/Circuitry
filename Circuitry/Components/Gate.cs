using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SharpLib2D.Graphics;
using SharpLib2D.Info;
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

        public const int SizeUnit = 60;

        public const int IODistance = SizeUnit / 3;

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

        protected void BuildAndShowOptionsMenu( params MenuEntry [ ] Entries )
        {
            MenuEntry[ ] Ents = new MenuEntry[ Entries.Length + 1 ];
            Ents[ 0 ] = new MenuEntry( "Remove", Control => Remove( ) );
            for ( int Q = 0; Q < Entries.Length; Q++ )
                Ents[ Q + 1 ] = Entries[ Q ];

            Circuit.ShowMenu( Ents );
        }

        public virtual void ShowOptionsMenu( )
        {
            BuildAndShowOptionsMenu( );
        }

        public override void OnButtonPressed( MouseButton Button )
        {
            if ( Circuit.OnChildMouseAction( this,
                new MouseButtonEventArgs( ( int ) Mouse.Position.X, ( int ) Mouse.Position.Y, Button, true ) ) )
                return;

            base.OnButtonPressed( Button );
        }

        public override void OnButtonReleased( MouseButton Button )
        {
            if ( Circuit.OnChildMouseAction( this,
                new MouseButtonEventArgs( ( int ) Mouse.Position.X, ( int ) Mouse.Position.Y, Button, false ) ) )
                return;

            base.OnButtonReleased( Button );
        }

        #region In/Out

        protected override void OnRemove( )
        {
            foreach ( Input I in this.Inputs )
                I.RemoveEntireConnection( );

            foreach ( Output O in this.Outputs )
                O.RemoveEntireConnection( );

            base.OnRemove( );
        }

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

            for( int Q = 0; Q < Inputs.Count; Q++ )
            {
                Inputs[ Q ].SetPosition( -( IODistance + Size.X / 2 ), Diff * ( Q + 1 ) - Size.Y / 2 );
            }
        }

        protected void LayoutOutputs( )
        {
            float Diff = Size.Y /( Outputs.Count + 1 );

            for ( int Q = 0; Q < Outputs.Count; Q++ )
            {
                Outputs[ Q ].SetPosition( Size.X / 2 + IODistance, Diff * ( Q + 1 ) - Size.Y / 2 );
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
                SharpLib2D.Resources.Texture.EnableTextures( false );

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
            SharpLib2D.Resources.Texture.Set( Path );

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
                Text.SetAlignments( Directions.HorizontalAlignment.Center, Directions.VerticalAlignment.Center );
                Text.DrawString( Name, "Lucida Console", SizeUnit / 6f, TextPosition, Color4.Black );
            }
        }
    }
}
