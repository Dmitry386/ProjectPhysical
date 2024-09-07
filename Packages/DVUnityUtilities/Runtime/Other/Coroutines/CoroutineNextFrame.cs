using Packages.DVUnityUtilities.Runtime.Other.Coroutines;
using System;
using System.Collections;
using UnityEngine;

namespace DVUnityUtilities.Other.Coroutines
{
    public class CoroutineNextFrame
    {
        public Action OnNextFrame;

        private TempTimerMono _empty;

        public CoroutineNextFrame Start()
        {
            _empty = new GameObject().AddComponent<TempTimerMono>();
            _empty.StartCoroutine(WaitNextFrame());
            return this;
        }

        private IEnumerator WaitNextFrame()
        {
            yield return null;
            OnNextFrame?.Invoke();
            GameObject.Destroy(_empty.gameObject);
        }

        private void OnDestroy()
        {
            OnNextFrame = null;
        }
    }
}
