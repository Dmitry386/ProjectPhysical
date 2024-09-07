using DVUnityUtilities.Other.Coroutines;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.ObjectFlipDetection
{
    public class ObjectFlipDetector : MonoBehaviour
    {
        [Header("=== Settings ===")]
        [SerializeField, Min(0)] private float _tickTime = 0.5f;
        [SerializeField, Min(0)] private float _minAngleForFlip = 70f;

        //[Header("=== Rigidbody ===")]
        //[SerializeField] private Rigidbody _rigidbody;
        //[SerializeField] private bool _checkForFlipIfGravityDisabled = false;

        [Header("=== Special Detections ===")]
        [SerializeField] private bool _checkValidAngles = false;
        [SerializeField] private Vector3 _minValidAngles = Vector3.one * -360;
        [SerializeField] private Vector3 _maxValidAngles = Vector3.one * 360;

        [Header("=== Events ===")]
        [SerializeField] public UnityEvent<Transform> OnFlip;
        [SerializeField] public UnityEvent<Transform> OnNormalized;

        private bool _lastCheckIsFlip;

        private void OnEnable()
        {
            new CoroutineTimer(this, _tickTime, true).Start().OnTick += OnTick;
        }

        private void OnTick(CoroutineTimer obj)
        {
            if (IsFlipped())
            {
                if (!_lastCheckIsFlip)
                {
                    OnFlip.Invoke(transform);
                }
                _lastCheckIsFlip = true;
            }
            else
            {
                if (_lastCheckIsFlip)
                {
                    OnNormalized.Invoke(transform);
                }
                _lastCheckIsFlip = false;
            }
        }

        public bool IsFlipped()
        {
            if (_checkValidAngles)
            {
                var rot = transform.eulerAngles;

                // Нормализуем углы в диапазон от -180 до 180
                rot.x = NormalizeAngle(rot.x);
                rot.y = NormalizeAngle(rot.y);
                rot.z = NormalizeAngle(rot.z);

                //Debug.Log(rot);
                if (rot.x >= _minValidAngles.x && rot.x <= _maxValidAngles.x &&
                    rot.y >= _minValidAngles.y && rot.y <= _maxValidAngles.y &&
                    rot.z >= _minValidAngles.z && rot.z <= _maxValidAngles.z)
                {
                    //Debug.Log("valid rot");
                    return Util.IsFlipped(transform.up, Vector3.up, _minAngleForFlip);
                }
                //Debug.Log("invalid rot");
                return true;
            }
            return Util.IsFlipped(transform.up, Vector3.up, _minAngleForFlip);
        }

        private float NormalizeAngle(float angle)
        {
            angle = angle % 360;
            if (angle > 180)
                angle -= 360;
            return angle;
        }
    }
}