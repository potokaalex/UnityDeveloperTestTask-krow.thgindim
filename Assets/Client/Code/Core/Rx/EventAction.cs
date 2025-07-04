using System;
using System.Collections.Generic;

namespace Client.Code.Core.Rx
{
    public class EventAction
    {
        private readonly List<Flow> _flows = new();

        public Flow Subscribe(Action action)
        {
            var flow = new Flow(action, this);
            _flows.Add(flow);
            return flow;
        }

        public void UnSubscribe(Flow flow) => _flows.Remove(flow);

        public void Invoke()
        {
            for (var i = 0; i < _flows.Count; i++)
                _flows[i].Invoke();
        }
    }
}