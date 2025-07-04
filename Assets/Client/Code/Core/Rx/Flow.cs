using System;

namespace Client.Code.Core.Rx
{
    public class Flow : IDisposable
    {
        private readonly Action _action;
        private readonly EventAction _eventAction;
        private Func<bool> _condition;

        public Flow(Action action, EventAction eventAction)
        {
            _action = action;
            _eventAction = eventAction;
        }

        public Flow Invoke()
        {
            if (_condition == null || _condition.Invoke())
                _action.Invoke();
            return this;
        }

        public void Dispose() => _eventAction.UnSubscribe(this);

        public Flow When(Func<bool> condition)
        {
            if (_condition != null)
                throw new Exception("Cant set multiple condition!");

            _condition = condition;
            return this;
        }
    }
}