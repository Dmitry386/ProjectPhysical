using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.PhysicsSystems.ColliderEnters
{
    public class TriggerEnterDetector : MonoBehaviour
    {
        [SerializeField] public UnityEvent<TriggerEnterDetector, Collider> OnEnter;
        [SerializeField] public UnityEvent<TriggerEnterDetector, Collider> OnExit;

        public void OnValidate()
        {
            if (TryGetComponent<Collider>(out var c))
            {
                if (!c.isTrigger) c.isTrigger = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnter.Invoke(this, other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit.Invoke(this, other);
        }
    }
}
