namespace SharpLib2D.Math
{
    public static class Random
    {
        private static readonly System.Random Generator;

        static Random( )
        {
            Generator = new System.Random( );
        }

        public static int Generate( int Min, int Max )
        {
            return Generator.Next( Min, Max );
        }
    }
}
