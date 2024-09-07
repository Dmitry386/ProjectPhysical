using Packages.DVParameters.Source;
using Packages.DVParameters.Source.Mono;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.DVParameters.Source.UI
{
    internal class FloatParameterToImageColor : MonoBehaviour
    {
        [SerializeField] private string _paramName;
        [SerializeField] private FloatDataParameters _parameters;
        [SerializeField] private Gradient _colors;
        [SerializeField] private Image[] _applyToImages;

        private void Awake()
        {
            _parameters.DataContainer.OnChanged += DataContainer_OnChanged;
        }

        private void Start()
        {
            DataContainer_OnChanged(null);
        }

        private void DataContainer_OnChanged(DataParameterContainer<float> obj)
        {
            if (_parameters.DataContainer.GetData(_paramName, out var parameter))
            {
                var normalizedParameter = parameter.Value / 100f;
                var color = _colors.Evaluate(normalizedParameter);

                for (int i = 0; i < _applyToImages.Length; i++)
                {
                    _applyToImages[i].color = color;
                }
            }
        }

        private void OnDestroy()
        {
            _parameters.DataContainer.OnChanged -= DataContainer_OnChanged;
        }
    }
}