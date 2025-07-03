using System;

namespace Client.Code.Gameplay.Customer
{
    public readonly struct WaitUntilNode : INode
    {
        private readonly Func<bool> _predicate;
            
        public WaitUntilNode(Func<bool> predicate) => _predicate = predicate;
            
        public NodeState Tick() => _predicate.Invoke() ? NodeState.Success : NodeState.Running;
    }
}