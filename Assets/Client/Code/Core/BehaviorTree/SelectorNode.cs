using System.Collections.Generic;

namespace Client.Code.Core.BehaviorTree
{
    public readonly struct SelectorNode : INode
    {
        private readonly List<INode> _children;

        public SelectorNode(params INode[] children) => _children = new(children);

        public NodeState Tick()
        {
            foreach (var child in _children)
            {
                var result = child.Tick();

                if (result is NodeState.Success or NodeState.Running)
                    return result;
            }

            return NodeState.Failure;
        }
    }
}