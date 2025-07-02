using System;
using System.Collections.Generic;

namespace Client.Code.Core.ServiceLocator
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<TContract>(object service)
        {
            if (service is not TContract)
                throw new Exception($"Object of type: {service.GetType()} is not heritage from {typeof(TContract)}");
            _services.Add(typeof(TContract), service);
        }

        public static void UnRegister<TContract>() => _services.Remove(typeof(TContract));

        public static TContract Get<TContract>() => (TContract)_services[typeof(TContract)];

        public static void Clear() => _services.Clear();
    }
}