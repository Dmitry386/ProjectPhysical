using DVUnityUtilities.Other.Pools;
using UnityEngine;

namespace Assets.Scripts.Gameplay.SpawnSystem
{
    public class SpawnPoint : WCacheAutoRegistrationMonoBehaviour
    {
        [field: SerializeField]
        public bool DestroyAfterSpawn { get; } = true; 
    }
}