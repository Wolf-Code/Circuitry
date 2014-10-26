using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SharpLib2D.Entities;
using SharpLib2D.Entities.Camera;

namespace SharpLib2D.States
{
    public class State
    {
        #region Static

        private static readonly Stack<State> States = new Stack<State>( );

        public enum Events
        {
            Resize
        }

        /// <summary>
        /// The currently active state, or null if there is none.
        /// </summary>
        public static State ActiveState
        {
            get
            {
                return States.Count <= 0 ? null : States.Peek( );
            }
        }

        /// <summary>
        /// Starts a new state.
        /// </summary>
        /// <typeparam name="T">The type of state.</typeparam>
        /// <param name="Arguments">The state's arguments.</param>
        public static void StartState<T>( params object[ ] Arguments ) where T : State
        {
            State S = Activator.CreateInstance( typeof ( T ), Arguments ) as State;

            if ( S == null )
                throw new Exceptions.SharpException( "Attempted to start a state of type {0}, something went wrong.",
                    typeof ( T ) );

            if ( ActiveState != null )
                ActiveState.OnPause( );

            States.Push( S );
            S.OnStart( );
            S.OnResume( );
        }

        /// <summary>
        /// Stops the active state, resumes the previous one if there is any.
        /// </summary>
        public static void StopActiveState( )
        {
            if ( ActiveState == null )
                return;

            State S = States.Pop( );
            S.OnPause( );
            S.OnExit( );

            if ( ActiveState == null )
                return;

            ActiveState.OnResume( );
        }

        /// <summary>
        /// Performs an event on every state.
        /// </summary>
        /// <param name="E">The type of event.</param>
        public static void PerformEvent( Events E )
        {
            switch ( E )
            {
                case Events.Resize:
                    foreach ( State S in States )
                        S.OnResize( );
                    break;
            }
        }

        #endregion

        #region Object
        /// <summary>
        /// The entities in this state.
        /// </summary>
        public readonly List<UpdatableEntity> Entities = new List<UpdatableEntity>( );

        /// <summary>
        /// The state's camera, this influences rendering.
        /// </summary>
        public DefaultCamera Camera { protected set; get; }

        /// <summary>
        /// The entities in this state, ordered chronologically.
        /// </summary>
        public List<UpdatableEntity> OrderedEntities
        {
            get { return Entities.OrderByDescending( O => O.Z ).ToList( ); }
        }

        internal void AddEntity( UpdatableEntity Entity )
        {
            if ( !Entities.Contains( Entity ) )
            {
                Entities.Add( Entity );
                Entity.Z = OrderedEntities.Count > 0 ? OrderedEntities[ 0 ].Z + 1 : 0;
            }
            else
                throw new Exceptions.SharpException( "Attempted to add entity {0} to state {1} twice.", Entity, this );
        }

        internal void RemoveEntity( UpdatableEntity Entity )
        {
            if ( Entities.Contains( Entity ) )
                Entities.Remove( Entity );
            else
                throw new Exceptions.SharpException( "Attempted to remove entity {0} from state {1} twice.", Entity,
                    this );
        }

        /// <summary>
        /// Updates all entitie in the state.
        /// </summary>
        /// <param name="e"></param>
        public virtual void Update( FrameEventArgs e )
        {
            List<UpdatableEntity> Ents = OrderedEntities;

            for ( int X = 0; X < Ents.Count; X++ )
                Ents[ X ].Update( e );
        }

        /// <summary>
        /// Renders all entities in the state.
        /// </summary>
        /// <param name="e"></param>
        public virtual void Draw( FrameEventArgs e )
        {
            List<UpdatableEntity> Ents = OrderedEntities;

            foreach ( DrawableEntity T in Ents.OfType<DrawableEntity>( ) )
                T.Draw( e );
        }

        #region Transitions

        /// <summary>
        /// Gets called whenever the state is first started.
        /// </summary>
        protected virtual void OnStart( )
        {
            Camera = new DefaultCamera( );
            Camera.SetSize( Info.Screen.Size );
        }

        /// <summary>
        /// Gets called whenever the state is permantently closed.
        /// </summary>
        protected virtual void OnExit( )
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for ( int X = 0; X < Entities.Count; X++ )
                Entities[ X ].Remove( );
        }

        /// <summary>
        /// Gets called whenever another state starts and thus pauses this state. Is also called right before OnExit.
        /// </summary>
        protected virtual void OnPause( )
        {
            
        }

        /// <summary>
        /// Gets called whenever we may resume as a state. Is also called right after OnStart.
        /// </summary>
        protected virtual void OnResume( )
        {
            
        }

        #endregion

        #region Events

        /// <summary>
        /// Is called whenever a resize occurs.
        /// </summary>
        protected virtual void OnResize( )
        {
            this.Camera.SetSize( Info.Screen.Size );
        }

        #endregion

        #endregion
    }
}
