using OpenTK;
using SharpLib2D.Math;

namespace SharpLib2D.Entities
{
    public abstract class RotatableEntity : Entity
    {
        #region ToLocal / ToWorld

        public override Vector2 ToLocal( Vector2 WorldPosition )
        {
            return Vector.RotateAround( WorldPosition, Position, -RotationRadians ) - Position;
        }

        public override Vector2 ToWorld( Vector2 LocalPosition )
        {
            return Position + Forward * LocalPosition.X + Up * LocalPosition.Y;
        }

        #endregion

        #region Rotation

        private float LocalRotation, LocalRotationRadians;

        public float Rotation
        {
            get
            {
                return HasParent && ( Parent is RotatableEntity )
                    ? ( Parent as RotatableEntity ).Rotation + LocalRotation
                    : LocalRotation;
            }
        }

        public float RotationRadians
        {
            get
            {
                return HasParent && ( Parent is RotatableEntity )
                    ? ( Parent as RotatableEntity ).RotationRadians + LocalRotationRadians
                    : LocalRotationRadians;
            }
        }

        public Vector2 Forward
        {
            get
            {
                return
                    ( Vector.RotateAround( Position + Vector2.UnitX, Position, RotationRadians ) - Position )
                        .Normalized( );
            }
        }

        public Vector2 Up
        {
            get
            {
                return
                    ( Vector.RotateAround( Position + Vector2.UnitY, Position, RotationRadians ) - Position )
                        .Normalized( );
            }
        }

        /// <summary>
        /// Sets the entity's global rotation, independent of the entity's parent.
        /// </summary>
        /// <param name="Degrees"></param>
        public void SetRotationIgnoreParent( float Degrees )
        {
            if ( !HasParent || !( Parent is RotatableEntity ) )
            {
                SetRotation( Degrees );
                return;
            }

            float Deg = ( Parent as RotatableEntity ).Rotation;
            SetRotation( -Deg + Degrees );
        }

        /// <summary>
        /// Sets the entity's local rotation.
        /// </summary>
        /// <param name="Degrees"></param>
        public void SetRotation( float Degrees )
        {
            float Rad = MathHelper.DegreesToRadians( Degrees );

            LocalRotation = Degrees;
            LocalRotationRadians = Rad;
        }

        /// <summary>
        /// Sets the entity's global rotation, independent of the entity's parent.
        /// </summary>
        /// <param name="Radians"></param>
        public void SetRotationRadiansIgnoreParent( float Radians )
        {
            if ( !HasParent || !( Parent is RotatableEntity ) )
            {
                SetRotationRadians( Radians );
                return;
            }

            float Rad = ( Parent as RotatableEntity ).RotationRadians;
            SetRotation( -Rad + Radians );
        }

        /// <summary>
        /// Sets the entity's local rotation.
        /// </summary>
        /// <param name="Radians"></param>
        public void SetRotationRadians( float Radians )
        {
            float Deg = MathHelper.RadiansToDegrees( Radians );

            LocalRotation = Deg;
            LocalRotationRadians = Radians;
        }

        #endregion
    }
}
