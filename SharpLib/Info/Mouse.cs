using System;
using OpenTK;
using OpenTK.Input;

namespace SharpLib2D.Info
{
    public static class Mouse
    {
        private static MouseState m_PreviousState, m_CurrentState;

        public static EventHandler<MouseMoveEventArgs> OnMouseMove;
        public static EventHandler<MouseButtonEventArgs> OnMouseButton;

        /// <summary>
        /// The mouse position on the screen.
        /// </summary>
        public static Vector2 Position
        {
            get { return new Vector2( m_CurrentState.X, m_CurrentState.Y ); }
        }

        /// <summary>
        /// The mouse movement during the current frame.
        /// </summary>
        public static Vector2 Delta
        {
            get { return Position - new Vector2( m_PreviousState.X, m_PreviousState.Y ); }
        }

        /// <summary>
        /// Returns true if the button is held down, false otherwise.
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public static bool IsDown( MouseButton Button )
        {
            return m_CurrentState.IsButtonDown( Button );
        }

        /// <summary>
        /// Returns true if the button is not held down, false otherwise.
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public static bool IsUp( MouseButton Button )
        {
            return m_CurrentState.IsButtonUp( Button );
        }

        /// <summary>
        /// Returns true if the button was released during the current frame, false otherwise.
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public static bool IsReleased( MouseButton Button )
        {
            return m_CurrentState.IsButtonUp( Button ) && m_PreviousState.IsButtonDown( Button );
        }

        /// <summary>
        /// Returns true if the button was pressed during the current frame, false otherwise.
        /// </summary>
        /// <param name="Button"></param>
        /// <returns></returns>
        public static bool IsPressed( MouseButton Button )
        {
            return m_CurrentState.IsButtonDown( Button ) && m_PreviousState.IsButtonUp( Button );
        }

        internal static void Update( FrameEventArgs e )
        {
            m_PreviousState = m_CurrentState;
            m_CurrentState = OpenTK.Input.Mouse.GetState( );

            if ( OnMouseButton != null )
                foreach ( MouseButton B in Enum.GetValues( typeof ( MouseButton ) ) )
                {
                    if ( IsPressed( B ) )
                        OnMouseButton( null, new MouseButtonEventArgs( ( int ) Position.X, ( int ) Position.Y, B, true ) );

                    if ( IsReleased( B ) )
                        OnMouseButton( null,
                            new MouseButtonEventArgs( ( int ) Position.X, ( int ) Position.Y, B, false ) );
                }

            if ( ( int ) Delta.X == 0 && ( int ) Delta.Y == 0 ) return;

            if ( OnMouseMove != null )
                OnMouseMove( null,
                    new MouseMoveEventArgs( ( int ) Position.X, ( int ) Position.Y, ( int ) Delta.X, ( int ) Delta.Y ) );
        }
    }
}