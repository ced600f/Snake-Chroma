using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Services
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        if (_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Service of type ({typeof(T)} is already registered.");
        }
        _services.Add(typeof(T), service);
    }

    public static T Get<T>()
    {
        if (!_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Service of type ({typeof(T)} is not registered.");
        }

        return (T)_services[typeof(T)];
    }

    public static void Remove<T>()
    {
        if (!_services.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException($"Service of type ({typeof(T)} is not registered.");
        }

        _services.Remove(typeof(T));
    }

}
