using System;
using System.Collections.Generic;
using UnityEngine;

namespace DVUnityUtilities.Other.PhysicsSystems.PhysicsLockDetectors
{
    public class PhysicsLockDetector : MonoBehaviour
    {
        [Serializable]
        public class PhysicsLockDetectorLineCastPoint
        {
            public Transform Point1;
            public Transform Point2;
        }

        [Header("!!! Require ANY convex collider to detect on this object !!!")]
        [Header("!!! Or use LINECAST points !!!")]

        [SerializeField] private LayerMask _layerMask = Physics.AllLayers;
        [SerializeField] private PhysicsLockDetectorLineCastPoint[] _lineCastPoints;

        private List<Collider> _colliders = new();

        private void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsLayerInLayerMask(_layerMask, other.gameObject.layer) && !_colliders.Contains(other))
            {
                _colliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _colliders.Remove(other);
        }

        private void OnDestroy()
        {
            _colliders.Clear();
        }

        public bool IsLocked(out List<Collider> refColliderList)
        {
            refColliderList = new();

            refColliderList.AddRange(_colliders);
            refColliderList.AddRange(GetLineCastColliders());

            return refColliderList.Count > 0;
        }

        public List<Collider> GetLineCastColliders()
        {
            var res = new List<Collider>();
            for (int i = 0; i < _lineCastPoints.Length; i++)
            {
                if (Physics.Linecast(_lineCastPoints[i].Point1.position, _lineCastPoints[i].Point2.position, out var hit, _layerMask, QueryTriggerInteraction.Ignore))
                {
                    res.Add(hit.collider);
                }
            }
            return res;
        }

        private void OnValidate()
        {
            if (TryGetComponent<Collider>(out var col) && !col.isTrigger)
            {
                col.isTrigger = true;
            }
        }

        private void OnDrawGizmos()
        {
            if (_lineCastPoints.Length > 0)
            {
                Gizmos.color = Color.red;

                for (int i = 0; i < _lineCastPoints.Length; i++)
                {
                    if (_lineCastPoints[i].Point1 && _lineCastPoints[i].Point2)
                        Gizmos.DrawLine(_lineCastPoints[i].Point1.position, _lineCastPoints[i].Point2.position);
                }
            }
        }
    }
}