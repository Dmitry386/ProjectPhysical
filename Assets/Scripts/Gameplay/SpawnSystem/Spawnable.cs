using DVUnityUtilities.Other.ServiceLocator;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay.SpawnSystem
{
    public class Spawnable : MonoBehaviour
    {
        private SpawnSystem _spawnSystem;

        private void Start()
        {
            _spawnSystem = DVServiceLocator.GetService<SpawnSystem>();
            StartCoroutine(SpawnRequesting());
        }

        private IEnumerator SpawnRequesting()
        {
            while (_spawnSystem)
            {
                if (_spawnSystem.TrySpawn(transform))
                {
                    break;
                }
                yield return null;
            }
        }
    }
}