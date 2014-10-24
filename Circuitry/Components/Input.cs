
namespace Circuitry.Components
{
    public class Input : IONode
    {
        public Input( NodeType Type, string Name, string Description )
            : base( Type, Name, Description )
        {
            Direction = NodeDirection.In;
        }
    }
}
