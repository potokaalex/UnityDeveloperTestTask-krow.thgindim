namespace Client.Code.Core.BehaviorTree
{
    public struct EmptyNode : INode
    {
        public NodeState Tick() => NodeState.Success;
    }
}