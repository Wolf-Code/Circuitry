using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SharpLib2D.Resources.Loaders;

namespace SharpLib2D.Resources
{
    public static class Loader
    {
        public static string ResourceFolder { private set; get; }
        private static readonly Dictionary<string, ResourceLoader> Loaders = new Dictionary<string, ResourceLoader>( );

        static Loader( )
        {
            ResourceFolder = "Resources";

            foreach (
                Type T in
                    Assembly.GetExecutingAssembly( )
                        .GetTypes( )
                        .Where( O => O.IsSubclassOf( typeof ( ResourceLoader ) ) ) )
            {
                AddLoader( Activator.CreateInstance( T ) as ResourceLoader );
            }
        }

        public static void AddLoader( ResourceLoader Loader )
        {
            foreach ( string Extension in Loader.Extensions )
                Loaders.Add( Extension, Loader );
        }

        public static T Get<T>( string File ) where T : Resource
        {
            string Ext = File.Split( '.' ).Last( ).ToLower( );
            if ( Loaders.ContainsKey( Ext ) )
                return Loaders[ Ext ].Load( File ) as T;

            return Loaders.FirstOrDefault( O => O.Value.ResourceType == typeof ( T ) ).Value.Load( File ) as T;
        }

        private static void CacheFile( FileSystemInfo File )
        {
            ResourceLoader L;
            if ( Loaders.TryGetValue( File.Extension.TrimStart( '.' ).ToLower( ), out L ) )
                L.CacheResource( File.FullName );
            else
                throw new Exceptions.NoResourceLoaderFoundException( File );
        }

        public static void CacheFolder( string Folder )
        {
            DirectoryInfo D = new DirectoryInfo( Folder );
            FileInfo [ ] Files = D.GetFiles( );
            foreach ( FileInfo CacheFile in Files )
                Loader.CacheFile( CacheFile );

            DirectoryInfo [ ] Folders = D.GetDirectories( );
            foreach ( DirectoryInfo CacheFolder in Folders )
                Loader.CacheFolder( CacheFolder.FullName );
        }

        public static void ClearAllResources( )
        {
            foreach ( ResourceLoader Loader in Loaders.Select( O => O.Value ) )
                Loader.Dispose( );
        }
    }
}
