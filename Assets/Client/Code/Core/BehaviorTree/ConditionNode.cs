using System;

namespace Client.Code.Core.BehaviorTree
{
    public readonly struct ConditionNode : INode
    {
        private readonly Func<bool> _predicate;

        public ConditionNode(Func<bool> predicate) => _predicate = predicate;

        public NodeState Tick() => _predicate.Invoke() ? NodeState.Success : NodeState.Failure;
    }
}