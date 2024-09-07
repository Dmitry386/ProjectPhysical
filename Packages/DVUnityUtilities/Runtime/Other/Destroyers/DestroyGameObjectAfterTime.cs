using DVUnityUtilities.Other.Coroutines;
using UnityEngine;

namespace DVUnityUtilities.Other.Destroyers
{
    internal class DestroyGameObjectAfterTime : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _time = 1f;

        private void OnEnable()
        {
            new CoroutineTimer(this, _time, false).Start().OnTick += DestroyGameObjectAfterTime_OnTick; ;
        }

        private void DestroyGameObjectAfterTime_OnTick(CoroutineTimer obj)
        {
            Destroy(gameObject);
        }
    }
}