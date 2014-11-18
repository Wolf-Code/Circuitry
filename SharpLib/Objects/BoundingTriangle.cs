using OpenTK;
using SharpLib2D.Math;

namespace SharpLib2D.Objects
{
    public class BoundingTriangle : BoundingVolume
    {
        public Vector2 Corner1 { private set; get; }
        public Vector2 Corner2 { private set; get; }
        public Vector2 Corner3 { private set; get; }

        public override BoundingRectangle BoundingBox
        {
            get { return new BoundingRectangle( new Vector2( MinX, MinY ), new Vector2( MaxX, MaxY ) ); }
        }

        private float MinX
        {
            get { return System.Math.Min( Corner3.X, System.Math.Min( Corner1.X, Corner2.X ) ); }
        }

        private float MinY
        {
            get { return System.Math.Min( Corner3.Y, System.Math.Min( Corner1.Y, Corner2.Y ) ); }
        }

        private float MaxX
        {
            get { return System.Math.Max( Corner3.X, System.Math.Max( Corner1.X, Corner2.X ) ); }
        }

        private float MaxY
        {
            get { return System.Math.Max( Corner3.Y, System.Math.Max( Corner1.Y, Corner2.Y ) ); }
        }

        public BoundingTriangle( Vector2 Corner1, Vector2 Corner2, Vector2 Corner3 )
        {
            this.Corner1 = Corner1;
            this.Corner2 = Corner2;
            this.Corner3 = Corner3;
        }

        public override bool Contains( Vector2 Position )
        {
            Vector2 AB = ( this.Corner2 - this.Corner1 );
            Vector2 BC = ( this.Corner3 - this.Corner2 );
            Vector2 CA = ( this.Corner1 - this.Corner3 );

            Vector2 AP = ( Position - this.Corner1 );
            Vector2 BP = ( Position - this.Corner2 );
            Vector2 CP = ( Position - this.Corner3 );

            int C1 = ( int ) Vector.Cross( AP, AB );
            if ( C1 != 0 )
                C1 /= System.Math.Abs( C1 );

            int C2 = ( int ) Vector.Cross( BP, BC );
            if ( C2 != 0 )
                C2 /= System.Math.Abs( C2 );

            int C3 = ( int ) Vector.Cross( CP, CA );
            if ( C3 != 0 )
                C3 /= System.Math.Abs( C3 );

            return C1 == C2 && C2 == C3;
        }
    }
}