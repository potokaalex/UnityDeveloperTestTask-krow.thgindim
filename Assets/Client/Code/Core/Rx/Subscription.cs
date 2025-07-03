using System;

namespace Client.Code.Core.Rx
{
    public readonly struct Subscription : IDisposable
    {
        private readonly Action _action;
        private readonly EventAction _eventAction;

        public Subscription(Action action, EventAction eventAction)
        {
            _action = action;
            _eventAction = eventAction;
        }

        public Subscription Call()
        {
            _action.Invoke();
            return this;
        }

        public void Dispose() => _eventAction.UnSubscribe(_action);
    }
}