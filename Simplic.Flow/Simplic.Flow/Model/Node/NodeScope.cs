namespace Simplic.Flow
{
    public class NodeScope<T> where T : Node
    {
        public T Node { get; set; }
        public DataPinScope Scope { get; set; }
    }
}
