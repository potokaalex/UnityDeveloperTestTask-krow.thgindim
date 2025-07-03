using System;

namespace Client.Code.Core.Dispose
{
    public static class DisposeExtensions
    {
        public static void AddTo(this IDisposable disposable, CompositeDisposable compositeDisposable) => compositeDisposable.Add(disposable);
    }
}