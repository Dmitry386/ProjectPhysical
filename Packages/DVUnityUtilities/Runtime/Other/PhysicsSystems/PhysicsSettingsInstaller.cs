using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.PhysicsSystems
{
    internal class PhysicsSettingsInstaller : MonoBehaviour
    {
        [SerializeField] private List<CollisionIgnoreBetweenLayerInfo> _ignoreCollisionsBetween = new();

        private void Awake()
        {
            foreach (var c in _ignoreCollisionsBetween)
            {
                Physics.IgnoreLayerCollision(c.Layer1, c.Layer2);
            }
        }
    }
}