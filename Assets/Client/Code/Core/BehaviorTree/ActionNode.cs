using System;

namespace Client.Code.Gameplay.Customer
{
    public readonly struct ActionNode : INode
    {
        private readonly Action _action;

        public ActionNode(Action action) => _action = action;

        public NodeState Tick()
        {
            _action.Invoke();
            return NodeState.Success;
        }
    }
}