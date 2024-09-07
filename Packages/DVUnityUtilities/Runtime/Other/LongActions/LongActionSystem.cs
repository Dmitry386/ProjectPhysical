using DVUnityUtilities.Other.Coroutines;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.LongActions
{
    public class LongActionSystem : MonoBehaviour
    {
        [SerializeField] public UnityEvent<LongActionSystem> OnStarted;
        [SerializeField] public UnityEvent<LongActionSystem> OnFinished;

        private Action _currentAction;
        private float _secondsDuration = -1;
        private float _startTime = -1;

        public bool IsPossibleToStartAction()
        {
            return _currentAction == null;
        }

        public bool TryStartAction(Action action, float secondsDuration)
        {
            if (IsPossibleToStartAction())
            {
                StartCurrentAction(action, secondsDuration);
                return true;
            }
            return false;
        }

        public float GetStartTime()
        {
            if (!this || _currentAction == null) return -1;
            return _startTime;
        }

        public float GetTimeElapsed()
        {
            if (!this || _currentAction == null) return -1;
            return Time.timeSinceLevelLoad - _startTime;
        }

        public float GetDuration()
        {
            return _secondsDuration;
        }

        public void StopCurrentAction()
        {
            if (_currentAction != null)
            {
                _currentAction = null;
                OnFinished?.Invoke(this);
            }
            _secondsDuration = -1;
            _startTime = -1;
        }

        private void StartCurrentAction(Action action, float secondsDuration)
        {
            _secondsDuration = secondsDuration;
            _currentAction = action;
            _startTime = Time.timeSinceLevelLoad;

            new CoroutineTimer(this, _secondsDuration, false).Start().OnTick += LongActionUser_OnTick;
            OnStarted?.Invoke(this);
        }

        private void LongActionUser_OnTick(CoroutineTimer obj)
        {
            _currentAction?.Invoke();

            StopCurrentAction();
        }

        public bool IsActiveAction()
        {
            return _currentAction != null;
        }
    }
}