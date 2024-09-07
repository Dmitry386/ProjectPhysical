using DVUnityUtilities.Other.UI.ProgressBar;
using Packages.DVParameters.Source.Mono;
using UnityEngine;

namespace Packages.DVParameters.Source.UI
{
    internal class FloatParameterToProgressBar : MonoBehaviour
    {
        [SerializeField] private ProgressBarUI _progressBar;
        [SerializeField] private FloatDataParameters _params;
        [SerializeField] private string _parameterName;

        private void Awake()
        {
            _params.DataContainer.OnChanged += DataContainer_OnChanged;
            DataContainer_OnChanged(null);
        }

        private void DataContainer_OnChanged(DataParameterContainer<float> obj)
        {
            if (_params.DataContainer.GetData(_parameterName, out var parameter))
            {
                _progressBar.SetValue(parameter.Value / parameter.Max);
            }
        }

        private void OnDestroy()
        {
            _params.DataContainer.OnChanged -= DataContainer_OnChanged;
        }
    }
}