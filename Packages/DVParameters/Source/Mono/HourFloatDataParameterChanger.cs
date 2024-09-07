//using DVUnityUtilities;
//using DVUnityUtilities.Other.ServiceLocator;
//using Packages.DVTimecycle.System;
//using Packages.DVTimecycle.System.Mono;
//using System.Linq;
//using UnityEngine;
//using static Packages.DVParameters.Source.Mono.FloatDataParameterChanger;

//namespace Packages.DVParameters.Source.Mono
//{
//    internal class HourFloatDataParameterChanger : MonoBehaviour
//    {
//        public static bool BlockAnyTick = false;

//        [SerializeField]
//        private FloatDataParameters _parameters;

//        [SerializeField]
//        private FloatDataParameterChangerSettings[] _settings;

//        private TimecycleSystem _timecycle;

//        private void Start()
//        {
//            _timecycle = DVServiceLocator.GetService<TimecycleSystem>();
//            if (_timecycle)
//            {
//                _timecycle.ActualTimecycle.OnChangeHour.AddListener(OnTick);
//            }
//            else
//            {
//                DebugUtils.LogWarning(this, $"Timecycle not found. Update stopped.");
//            }
//        }

//        private void OnTick(Timecycle arg0)
//        {
//            OnHourChanged();
//        }

//        private void OnHourChanged()
//        {
//            if (BlockAnyTick) return;

//            var parameters = _parameters.DataContainer.GetAll();

//            float newVal;
//            FloatDataParameterChangerSettings sameSettings;

//            foreach (var p in parameters)
//            {
//                sameSettings = _settings.FirstOrDefault(x => x.Name == p.Name);
//                if (sameSettings != null)
//                {
//                    // newVal = Mathf.Clamp(p.Value + sameSettings.ChangePerTick, sameSettings.Min, sameSettings.Max);
//                    newVal = p.Value + sameSettings.ChangePerTick;
//                    _parameters.DataContainer.SetData(p.Name, newVal);
//                }
//            }
//        }
//    }
//}