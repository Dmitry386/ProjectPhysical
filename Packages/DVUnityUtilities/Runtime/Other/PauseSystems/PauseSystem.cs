using UnityEngine;
using UnityEngine.Events;

namespace Packages.DVUnityUtilities.Runtime.Other.PauseSystems
{
    public class PauseSystem : MonoBehaviour
    {
        public UnityEvent<PauseSystem> OnChangePauseState;

        private void Awake()
        {
            Debug.Log("PauseSystem initialized");
        }

        public bool IsPaused()
        {
            return Time.timeScale == 0;
        }

        public void SetPauseStatus(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
            Debug.Log($"Time scale: {Time.timeScale}");

            OnChangePauseState?.Invoke(this);
        }

        private void OnDestroy()
        {
            SetPauseStatus(false);
        }
    }
}