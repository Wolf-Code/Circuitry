using System.IO;

namespace SharpLib2D.Resources
{
    public abstract class Resource
    {
        public string Path { protected set; get; }

        public abstract Resource LoadFromStream( Stream S );
    }
}
