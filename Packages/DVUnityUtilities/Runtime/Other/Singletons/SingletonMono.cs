using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    protected static T Instance;

    public static void RegisterIfNull()
    {
        if (!Instance)
        {
            var go = new GameObject(typeof(T).Name);
            Instance = go.AddComponent<T>();
            DontDestroyOnLoad(go);
        }
    }

    public static bool TryGetSingleton(out T instance, bool errorMessage = false)
    {
        RegisterIfNull();
        instance = Instance;

        if (!instance) Debug.LogError($"Singleton [{typeof(T).Name}] is null. Errors possible.");
        return instance;
    }
}