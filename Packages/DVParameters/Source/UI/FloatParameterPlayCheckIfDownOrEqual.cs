using DVUnityUtilities.Other.Coroutines;
using Packages.DVParameters.Source.Mono;
using UnityEngine;
using UnityEngine.Events;

namespace Packages.DVParameters.Source.UI
{
    internal class FloatParameterPlayCheckIfDownOrEqual : MonoBehaviour
    {
        [SerializeField] private string _paramName;
        [SerializeField] private float _downOrEqualValue = 20f;
        [SerializeField] private FloatDataParameters _parameters;
        [SerializeField] private float _playInterval = 30f;

        [SerializeField] public UnityEvent<FloatParameterPlayCheckIfDownOrEqual> OnDownOrEqual;

        private void OnEnable()
        {
            new CoroutineTimer(this, _playInterval, true).Start().OnTick += OnTick;
        }

        private void OnTick(CoroutineTimer obj)
        {
            if (_parameters.DataContainer.GetData(_paramName, out var val))
            {
                if (val.Value <= _downOrEqualValue)
                {
                    OnDownOrEqual?.Invoke(this);
                }
            }
        }
    }
}