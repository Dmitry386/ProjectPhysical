using UnityEngine;

namespace DVUnityUtilities.Other.Pools
{
    public abstract class WCacheAutoRegistrationMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            WCache.Register(this);
        }

        protected virtual void OnDestroy()
        {
            WCache.DeRegister(this);
        }
    }
}