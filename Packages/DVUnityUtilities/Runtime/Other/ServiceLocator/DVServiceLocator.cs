using System;
using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.ServiceLocator
{
    /// <summary>
    /// Ленивый Сервис Локатор. Использует GameObjectFindFirst<T> и кеширует его. В случае отсутствия создаёт.
    /// </summary>
    public class DVServiceLocator
    {
        private static Dictionary<Type, object> _services = new();

        public static void RegisterService<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        public static T GetServiceComponentLazy<T>() where T : Component
        {
            _services.TryGetValue(typeof(T), out var service);
            T r = service as T;

            if (!r)
            {
                r = GameObject.FindFirstObjectByType<T>(FindObjectsInactive.Include);
                if (!r)
                {
                    Debug.LogWarning($"[DVServiceLocator] Create service component: {typeof(T).Name}");

                    var go = new GameObject($"[SERVICE-{typeof(T).Name}]");
                    r = go.AddComponent<T>();
                    RegisterService(r);
                }
                else
                {
                    Debug.Log($"[DVServiceLocator] Founded service component: {typeof(T).Name}");
                    RegisterService(r);
                }
            }

            return r;
        }

        public static T GetServiceLazy<T>() where T : class, new()
        {
            _services.TryGetValue(typeof(T), out var service);
            T r = service as T;

            if (r == null)
            {
                Debug.Log($"[DVServiceLocator] Create service: {typeof(T).Name}");

                r = new T();
                RegisterService(r);
            }

            return r;
        }

        public static T GetService<T>() where T : class
        {
            _services.TryGetValue(typeof(T), out var service);

            if (service == null)
            {
                service = Util.FindObjectWithComponentOfType(typeof(T), true);
            }
            else if (service is Component c)
            {
                if (!c)
                {
                    service = null;
                }
            }


            return service as T;
        }
    }
}
