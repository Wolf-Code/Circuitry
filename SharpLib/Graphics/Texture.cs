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

        public int ID
        {
            protected set;
            get;
        }

        public int Width
        {
            protected set;
            get;
        }

        public int Height
        {
            protected set;
            get;
        }

        public static Texture Load( string Path )
        {
            if ( LoadedTextures.ContainsKey( Path ) )
                return LoadedTextures[ Path ];


            Texture T = new Texture
            {
                ID = GL.GenTexture( )
            };
            GL.BindTexture( TextureTarget.Texture2D, T.ID );

            System.IO.FileInfo Info = new System.IO.FileInfo( Path );
            if ( Info.Exists )
            {
                using ( Bitmap B = new Bitmap( Path ) )
                {
                    T.Width = B.Width;
                    T.Height = B.Height;

                    BitmapData bmp_data = B.LockBits( new System.Drawing.Rectangle( 0, 0, B.Width, B.Height ),
                        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb );

                    GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height,
                        0,
                        OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0 );

                    B.UnlockBits( bmp_data );
                }
                GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    ( int ) TextureMinFilter.Linear );
                GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    ( int ) TextureMagFilter.Linear );
            }
            else
                throw new System.IO.FileNotFoundException( "Unable to load texture '" + Path + "'" );

            LoadedTextures.Add( Path, T );

            return T;
        }

        public void Bind( )
        {
            Set( this );
        }

        #region Static Methods

        public static void Set( Texture T )
        {
            //GL.ActiveTexture( TextureUnit.Texture0 );
            GL.BindTexture( TextureTarget.Texture2D, T != null ? T.ID : 0 );
            BoundTexture = T;
        }

        public static void EnableTextures( bool Enabled = true )
        {
            if ( Enabled )
                GL.Enable( EnableCap.Texture2D );
            else
                GL.Disable( EnableCap.Texture2D );
        }

        #endregion
    }
}
