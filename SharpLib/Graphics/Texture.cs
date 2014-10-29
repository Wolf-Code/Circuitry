using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK.Graphics.OpenGL;

namespace SharpLib2D.Graphics
{
    public class Texture
    {
        #region Static Properties

        public static Texture BoundTexture
        {
            private set;
            get;
        }

        private static Dictionary<string, Texture> LoadedTextures = new Dictionary<string, Texture>( );

        #endregion

        /// <summary>
        /// The texture's identifying number.
        /// </summary>
        public int ID
        {
            protected set;
            get;
        }

        /// <summary>
        /// The texture's width.
        /// </summary>
        public int Width
        {
            protected set;
            get;
        }

        /// <summary>
        /// The texture's height.
        /// </summary>
        public int Height
        {
            protected set;
            get;
        }

        /// <summary>
        /// Sets this as the active rendering texture.
        /// </summary>
        public void Bind( )
        {
            Set( this );
        }

        /// <summary>
        /// Clears the texture from memory.
        /// </summary>
        public void Remove( )
        {
            GL.DeleteTexture( ID );
        }

        #region Static Methods

        public static Texture Load( Bitmap B )
        {
            Texture T = new Texture
            {
                ID = GL.GenTexture( )
            };
            GL.BindTexture( TextureTarget.Texture2D, T.ID );

            T.Width = B.Width;
            T.Height = B.Height;

            BitmapData bmp_data = B.LockBits( new System.Drawing.Rectangle( 0, 0, B.Width, B.Height ),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb );
            {
                GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height,
                    0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0 );
            }
            B.UnlockBits( bmp_data );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                ( int ) TextureMinFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                ( int ) TextureMagFilter.Linear );

            return T;
        }

        public static Texture Load( string Path )
        {
            if ( LoadedTextures.ContainsKey( Path ) )
                return LoadedTextures[ Path ];

            System.IO.FileInfo Info = new System.IO.FileInfo( Path );
            if ( !Info.Exists ) throw new System.IO.FileNotFoundException( "Unable to load texture '" + Path + "'" );
            
            using ( Bitmap B = new Bitmap( Path ) )
            {
                Texture T = Load( B );
                LoadedTextures.Add( Path, T );

                return T;
            }
        }

        private static void Set( Texture T )
        {
            //GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, T != null ? T.ID : 0 );
            BoundTexture = T;
        }

        public static void Set( string Texture )
        {
            Set( Load( Texture ) );
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
