using System;
using System.Collections.Generic;
using Client.Code.Core.Dispose;

namespace Client.Code.Core.ServiceLocatorCode
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, List<object>> _services = new();

        public IDisposable Register(object service, params Type[] contracts)
        {
            CheckCanRegister(service, contracts);
            var disposable = new CompositeDisposable();
            foreach (var contract in contracts)
                Add(service, contract).AddTo(disposable);
            return disposable;
        }

        public void UnRegister(object service, params Type[] contracts)
        {
            foreach (var contract in contracts)
                Remove(service, contract);
        }

        private void Remove(object service, Type contract)
        {
            if (_services.TryGetValue(contract, out var list))
                list.Remove(service);
        }

        private void CheckCanRegister(object service, Type[] contracts)
        {
            var serviceType = service.GetType();
            foreach (var contract in contracts)
                if (!contract.IsAssignableFrom(serviceType))
                    throw new Exception($"Cant register object of type: {service.GetType()} with contract: {contract}");
        }

        private DisposableAction Add(object service, Type contract)
        {
            if (!_services.TryGetValue(contract, out var list))
            {
                list = new();
                _services[contract] = list;
            }

            if (list.Contains(service))
                throw new Exception($"Multiple {service.GetType()} registration with contract: {contract}.");

            list.Add(service);
            return new DisposableAction(() => UnRegister(service, contract));
        }

        public T Get<T>() => (T)_services[typeof(T)][0];

        public void GetAll<T>(List<T> outList)
        {
            outList.Clear();
            var services = _services[typeof(T)];
            foreach (var service in services)
                outList.Add((T)service);
        }

        public void Clear() => _services.Clear();
    }
}