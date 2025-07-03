namespace Client.Code.Core.BehaviorTree
{
    public interface INode
    {
        NodeState Tick();
    }
}