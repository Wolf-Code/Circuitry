
namespace Circuitry.Components
{
    public class Output : IONode
    {
        public Output( NodeType Type, string Name, string Description )
            : base( Type, Name, Description )
        {
            Direction = NodeDirection.Out;
        }
    }
}
