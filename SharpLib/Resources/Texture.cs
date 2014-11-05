using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace SharpLib2D.Resources
{
    public class Texture : Resource
    {
        #region Static Properties

        public static Texture BoundTexture
        {
            private set;
            get;
        }

        private static Dictionary<string, Texture> LoadedTextures = new Dictionary<string, Texture>( );

        #endregion

        #region Properties

        /// <summary>
        /// The texture's identifying number.
        /// </summary>
        public int ID
        {
            internal protected set;
            get;
        }

        /// <summary>
        /// The texture's width.
        /// </summary>
        public int Width
        {
            internal protected set;
            get;
        }

        /// <summary>
        /// The texture's height.
        /// </summary>
        public int Height
        {
            internal protected set;
            get;
        }

        #endregion

        /// <summary>
        /// Sets this as the active rendering texture.
        /// </summary>
        public void Bind( )
        {
            Set( this );
        }

        public RectangleF PixelRegionToUVRegion( Rectangle PixelRegion )
        {
            return new RectangleF(
                XToU( PixelRegion.Left ),
                YToV( PixelRegion.Top ),
                XToU( PixelRegion.Right ),
                YToV( PixelRegion.Bottom ) );
        }

        public float XToU( int X )
        {
            return X / ( float ) this.Width;
        }

        public float YToV( int Y )
        {
            return Y / ( float ) this.Height;
        }

        public Vector2 PixelToUV( int X, int Y )
        {
            return new Vector2( XToU( X ), YToV( Y ) );
        }

        public Point UVToPixel( float X, float Y )
        {
            return new Point( ( int ) ( X * this.Width ), ( int ) ( Y * this.Height ) );
        }

        public override void Dispose( )
        {
            GL.DeleteTexture( ID );
        }

        #region Static Methods

        public static void Set( Texture T )
        {
            //GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, T != null ? T.ID : 0 );
            BoundTexture = T;
        }

        public static void Set( string Path )
        {
            Set( Loader.Get<Texture>( Path ) );
        }

        public static bool EnableTextures( bool Enabled = true )
        {
            bool Old = GL.IsEnabled( EnableCap.Texture2D );

            if ( Enabled )
                GL.Enable( EnableCap.Texture2D );
            else
                GL.Disable( EnableCap.Texture2D );

            return Old;
        }

        #endregion
    }
}
