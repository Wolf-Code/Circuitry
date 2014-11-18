using OpenTK;

namespace SharpLib2D.Math
{
    public static class Vector
    {
        /// <summary>
        /// Gets a normal of a given direction.
        /// </summary>
        /// <param name="Start">The starting position.</param>
        /// <param name="End">The end position.</param>
        /// <returns>The line's normal.</returns>
        public static Vector2 Normal( Vector2 Start, Vector2 End )
        {
            Vector2 Direction = End - Start;
            Direction.Normalize( );

            return new Vector2( -Direction.Y, Direction.X );
        }

        /// <summary>
        /// Rotates a vector around another vector, with a given amount of radians.
        /// </summary>
        /// <param name="Vector">The vector to rotate.</param>
        /// <param name="RotationPoint">The point to rotate around.</param>
        /// <param name="Radians">The amount to rotate, in radians.</param>
        /// <returns>The <see cref="Vector2"/> rotated around <paramref name="RotationPoint"/>.</returns>
        public static Vector2 RotateAround( Vector2 Vector, Vector2 RotationPoint, float Radians )
        {
            Vector2 Dir = ( RotationPoint - Vector );
            float Length = Dir.Length;
            Vector2 Norm = Dir / Length;

            return RotationPoint + Vector2.Transform( Norm, Quaternion.FromAxisAngle( Vector3.UnitZ, Radians ) ) * Length;
        }

        /// <summary>
        /// Transforms a 2D vector using a 4x4 Matrix.
        /// </summary>
        /// <param name="Vector">The vector to transform.</param>
        /// <param name="Matrix">The matrix to transform with.</param>
        /// <returns></returns>
        public static Vector2 Transform( Vector2 Vector, Matrix4 Matrix )
        {
            return Vector3.Transform( new Vector3( Vector ), Matrix ).Xy;
        }

        public static float Cross( Vector2 V1, Vector2 V2 )
        {
            return ( V1.X * V2.Y ) - ( V1.Y * V2.X );
        }
    }
}
