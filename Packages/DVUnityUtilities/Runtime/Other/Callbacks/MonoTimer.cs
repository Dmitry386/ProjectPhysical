using DVUnityUtilities.Other.Coroutines;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.Callbacks
{
    internal class MonoTimer : MonoBehaviour
    {
        [SerializeField] private float _interval = 1f;
        [SerializeField] private bool _repeat = true;
        [SerializeField] private bool _startFromAwake = true;
        [SerializeField] private UnityEvent _onTick;

        private CoroutineTimer _timer;

        private void Awake()
        {
            _timer = new CoroutineTimer(this, _interval, _repeat);
            _timer.AutoDispose = false;
            _timer.OnTick += OnTick;
            if (_startFromAwake) _timer.Start();
        }

        public void StartTimer()
        {
            _timer?.Start();
        }

        private void OnTick(CoroutineTimer obj)
        {
            _onTick.Invoke();
        }

        private void OnDestroy()
        {
            _timer?.Dispose();
        }
    }
}