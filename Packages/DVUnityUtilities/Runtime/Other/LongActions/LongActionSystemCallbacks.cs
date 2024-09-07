using DVUnityUtilities.Other.LongActions;
using UnityEngine;
using UnityEngine.Events;

namespace Packages.DVUnityUtilities.Runtime.Other.LongActions
{
    internal class LongActionSystemCallbacks : MonoBehaviour
    {
        [SerializeField] public UnityEvent<LongActionSystem> OnStarted;
        [SerializeField] public UnityEvent<LongActionSystem> OnFinished;

        private LongActionSystem _system;

        private void Awake()
        {
            _system = FindAnyObjectByType<LongActionSystem>();
            _system.OnStarted.AddListener(OnStartedInternal);
            _system.OnFinished.AddListener(OnFinishedInternal);
        }

        private void OnStartedInternal(LongActionSystem arg0)
        {
            OnStarted?.Invoke(arg0);
        }

        private void OnFinishedInternal(LongActionSystem arg0)
        {
            OnFinished?.Invoke(arg0);
        }
    }
}