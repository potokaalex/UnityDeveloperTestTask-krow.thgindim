using Client.Code.Gameplay.Customer;

namespace Client.Code.Core.BehaviorTree
{
    public readonly struct RepeatNode : INode
    {
        private readonly INode _child;

        public RepeatNode(INode child) => _child = child;

        public NodeState Tick()
        {
            _child.Tick();
            return NodeState.Running;
        }
    }
}