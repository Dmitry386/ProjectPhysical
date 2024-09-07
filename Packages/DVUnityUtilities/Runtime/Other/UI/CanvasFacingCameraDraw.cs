using DVUnityUtilities.Other.Coroutines;
using UnityEngine;

namespace DVUnityUtilities.Other.UI
{
    [RequireComponent(typeof(Canvas))]
    internal class CanvasFacingCameraDraw : MonoBehaviour
    {
        [SerializeField, Min(-1)]
        [Tooltip("Use -1 to draw in any place")]
        private float _drawDistance = 50f;

        [SerializeField, Min(0)]
        private float _checkDrawDistanceInterval = 1f;

        private Transform _camera;
        private Transform _cachedTransform;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _cachedTransform = transform;
            _camera = Camera.main.transform;
        }

        private void OnEnable()
        {
            if (_checkDrawDistanceInterval > 0)
            {
                new CoroutineTimer(this, _checkDrawDistanceInterval, true).Start().OnTick += CanvasCameraFacing_OnTick; ;
            }
        }

        private void CanvasCameraFacing_OnTick(CoroutineTimer obj)
        {
            _canvas.enabled = Vector3.Distance(_camera.position, _cachedTransform.position) <= _drawDistance;
        }

        private void Update()
        {
            _cachedTransform.forward = _camera.forward;
        }
    }
}