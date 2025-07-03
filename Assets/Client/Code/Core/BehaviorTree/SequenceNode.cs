using System.Collections.Generic;

namespace Client.Code.Gameplay.Customer
{
    public class SequenceNode : INode
    {
        private readonly List<INode> _children;
        private int _current;

        public SequenceNode(params INode[] nodes) => _children = new List<INode>(nodes);

        public NodeState Tick()
        {
            for (; _current < _children.Count; _current++)
            {
                var state = _children[_current].Tick();
                if (state == NodeState.Running)
                    return NodeState.Running;
                if (state == NodeState.Failure)
                {
                    _current = 0;
                    return NodeState.Failure;
                }
            }

            _current = 0;
            return NodeState.Success;
        }
    }
}