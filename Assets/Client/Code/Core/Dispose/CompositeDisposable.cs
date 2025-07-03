using System;
using System.Collections.Generic;

namespace Client.Code.Core.Dispose
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _items = new();

        public void Add(IDisposable disposable) => _items.Add(disposable);

        public void Remove(IDisposable disposable) => _items.Remove(disposable);

        public void Clear()
        {
            for (var i = 0; i < _items.Count; i++)
                _items[i].Dispose();
            _items.Clear();
        }

        public void Dispose() => Clear();
    }
}