using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace SharpLib2D.Resources.Loaders
{
    internal class Texture : ResourceLoader
    {
        public override string [ ] Extensions
        {
            get { return new [ ] { "jpg", "jpeg", "png" }; }
        }

        public override Type ResourceType
        {
            get { return typeof ( Texture ); }
        }

        public override Resource Load( string Path )
        {
            if ( CachedResources.ContainsKey( Path ) )
                return CachedResources[ Path ];

            FileInfo Info = new FileInfo( Path );
            if ( !Info.Exists ) throw new FileNotFoundException( "Unable to load texture '" + Path + "'" );

            using ( StreamReader R = new StreamReader( Path ) )
            {
                Resources.Texture T = Load( R.BaseStream ) as Resources.Texture;
                CachedResources.TryAdd( Path, T );

                return T;
            }
        }

        public override Resource Load( Stream Stream )
        {
            using ( Bitmap B = ( Bitmap ) Image.FromStream( Stream ) )
                return LoadTexture( B );
        }

        public static Resources.Texture LoadTexture( Bitmap B )
        {
            Resources.Texture T = new Resources.Texture
            {
                ID = GL.GenTexture( )
            };
            GL.BindTexture( TextureTarget.Texture2D, T.ID );

            T.Width = B.Width;
            T.Height = B.Height;

            BitmapData bmp_data = B.LockBits( new Rectangle( 0, 0, B.Width, B.Height ),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb );
            {
                GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height,
                    0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0 );
            }
            B.UnlockBits( bmp_data );

            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                ( int )TextureMinFilter.Linear );
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                ( int )TextureMagFilter.Linear );

            return T;
        }
    }
}
