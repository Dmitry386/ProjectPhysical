using DVUnityUtilities;
using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.Destroyers
{
    internal class DestroyWithChance : MonoBehaviour
    {
        [SerializeField] private GameObject _object;
        [SerializeField] private float _chance = 50f;

        private void Awake()
        {
            if (RandomizeUtils.Chance(_chance))
            {
                if (_object) Destroy(_object);
            }
        }
    }
}