using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace SharpLib2D.Resources.Loaders
{
    public abstract class ResourceLoader : IDisposable
    {
        public abstract string [ ] Extensions { get; }
        public abstract Type ResourceType { get; }

        protected ConcurrentDictionary<string, Resource> CachedResources = new ConcurrentDictionary<string, Resource>( );

        public void CacheResource( string Path )
        {
            CachedResources.TryAdd( Path, Load( Path ) );
        }

        public abstract Resource Load( string Path );
        public abstract Resource Load( Stream Stream );

        public void Dispose( )
        {
            foreach ( Resource R in CachedResources.Select( O => O.Value ) )
                R.Dispose( );

            CachedResources.Clear( );
        }
    }
}
