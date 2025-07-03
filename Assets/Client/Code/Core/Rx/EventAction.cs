using System;
using System.Collections.Generic;

namespace Client.Code.Core.Rx
{
    public class EventAction
    {
        private readonly List<Action> _actions = new();

        public Subscription Subscribe(Action action)
        {
            _actions.Add(action);
            return new Subscription(action, this);
        }

        public void UnSubscribe(Action action) => _actions.Remove(action);

        public void Invoke()
        {
            for (var i = 0; i < _actions.Count; i++)
                _actions[i].Invoke();
        }
    }
}