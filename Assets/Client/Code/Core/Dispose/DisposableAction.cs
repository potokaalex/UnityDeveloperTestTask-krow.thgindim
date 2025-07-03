using System;

namespace Client.Code.Core.Dispose
{
    public readonly struct DisposableAction : IDisposable
    {
        private readonly Action _action;

        public DisposableAction(Action action) => _action = action;

        public void Dispose() => _action.Invoke();
    }
}