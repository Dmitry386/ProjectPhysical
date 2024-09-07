using DVUnityUtilities.Other.Coroutines;
using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.LayerCheckers
{
    [DefaultExecutionOrder(-1)]
    internal class OnLayerChecker : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _checkInterval = 0.25f;
        [SerializeField, Min(0)] private float _checkRadius = 0.2f;
        [SerializeField] private Transform _checkPoint;
        [SerializeField] private LayerMask _waterLayer;

        private bool _inLayerMask;
        private Transform _cachedTransform;
        private Collider[] _checkResults;

        private void OnEnable()
        {
            if (_checkInterval > 0)
            {
                _checkResults = new Collider[1];
                _cachedTransform = transform;
                new CoroutineTimer(this, _checkInterval, true).Start().OnTick += OnWaterDetectorTick;
            }
        }

        private void OnWaterDetectorTick(CoroutineTimer obj)
        {
            UpdateCheckStatus();
        }

        public void UpdateCheckStatus()
        {
            if (Physics.OverlapSphereNonAlloc(_cachedTransform.position, _checkRadius, _checkResults, _waterLayer, QueryTriggerInteraction.Ignore) > 0)
            {
                _inLayerMask = true;
            }
            else
            {
                _inLayerMask = false;
            }
        }

        public bool IsInLayerMask()
        {
            return _inLayerMask;
        }
    }
}