using System;

namespace Client.Code.Core.Dispose
{
    public readonly struct DisposableAction : IDisposable
    {
        private readonly Action _action;

        public DisposableAction(Action action) => _action = action;

        public void Dispose() => _action.Invoke();
    }

    public readonly struct DisposableAction<T> : IDisposable
    {
        private readonly Action<T> _action;
        private readonly T _value;

        public DisposableAction(Action<T> action, T value)
        {
            _value = value;
            _action = action;
        }

        public void Dispose() => _action.Invoke(_value);
    }
}