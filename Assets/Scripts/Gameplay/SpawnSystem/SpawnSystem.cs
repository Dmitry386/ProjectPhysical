using DVUnityUtilities.Other.Pools;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gameplay.SpawnSystem
{
    public class SpawnSystem : MonoBehaviour
    {
        public bool TrySpawn(Transform t)
        {
            var spawnPoints = WCache.GetAll<SpawnPoint>();

            if (spawnPoints.Count == 0)
            {
                return false;
            }
            else
            {
                var spawn = spawnPoints.First();

                t.position = spawn.transform.position;
                t.rotation = spawn.transform.rotation;

                if (spawn.DestroyAfterSpawn)
                {
                    GameObject.Destroy(spawn.gameObject);
                }

                return true;
            }
        }
    }
}
