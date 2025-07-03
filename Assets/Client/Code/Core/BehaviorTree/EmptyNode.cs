namespace Client.Code.Gameplay.Customer
{
    public struct EmptyNode : INode
    {
        public NodeState Tick() => NodeState.Success;
    }
}