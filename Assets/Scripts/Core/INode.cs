namespace Snek.Core
{
    public interface INode
    {
        public INode[] GetNeighbours();
        public int Cost { get;}
    }
}
