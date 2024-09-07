using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.PhysicsSystems
{
    [RequireComponent(typeof(Rigidbody))]
    internal class PhysicsMaxDepenetrationVelocity : MonoBehaviour
    {
        [SerializeField] private float _value = 1f;

        private void Awake()
        {
            GetComponent<Rigidbody>().maxDepenetrationVelocity = _value;
        }
    }
}
