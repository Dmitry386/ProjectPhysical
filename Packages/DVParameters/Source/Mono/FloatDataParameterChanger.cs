using DVUnityUtilities.Other.Coroutines;
using System;
using System.Linq;
using UnityEngine;

namespace Packages.DVParameters.Source.Mono
{
    public class FloatDataParameterChanger : MonoBehaviour
    {
        [Serializable]
        public class FloatDataParameterChangerSettings
        {
            public string Name;
            public float ChangePerTick;
        }

        [SerializeField, Min(0)]
        private float _interval = 1f;

        [SerializeField]
        private FloatDataParameters _parameters;

        [SerializeField]
        private FloatDataParameterChangerSettings[] _settings;

        private void OnEnable()
        {
            new CoroutineTimer(this, _interval, true).Start().OnTick += OnTick;
        }

        private void OnTick(CoroutineTimer obj)
        {
            var parameters = _parameters.DataContainer.GetAll();

            float newVal;
            FloatDataParameterChangerSettings sameSettings;

            foreach (var p in parameters)
            {
                sameSettings = _settings.FirstOrDefault(x => x.Name == p.Name);
                if (sameSettings != null)
                {
                    // newVal = Mathf.Clamp(p.Value + sameSettings.ChangePerTick, sameSettings.Min, sameSettings.Max);
                    newVal = p.Value + sameSettings.ChangePerTick;
                    _parameters.DataContainer.SetData(p.Name, newVal);
                }
            }
        }
    }
}