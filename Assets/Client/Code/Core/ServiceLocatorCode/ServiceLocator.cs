using System;
using System.Collections.Generic;
using Client.Code.Core.Dispose;

namespace Client.Code.Core.ServiceLocatorCode
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> _services = new();

        public IDisposable Register<TContract>(object service)
        {
            if (service is not TContract)
                throw new Exception($"Object of type: {service.GetType()} is not heritage from {typeof(TContract)}");
            _services.Add(typeof(TContract), service);
            return new DisposableAction(UnRegister<TContract>);
        }

        public void UnRegister<TContract>() => _services.Remove(typeof(TContract));

        public TContract Get<TContract>() => (TContract)_services[typeof(TContract)];

        public void Clear() => _services.Clear();
    }
}