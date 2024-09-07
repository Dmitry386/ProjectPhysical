using Packages.DVParameters.Source;
using Packages.DVParameters.Source.Mono;
using UnityEngine;

namespace Packages.DVParameters.Source.UI
{
    internal class FloatParameterLerpRectTransformBetweenAnchorPosition : MonoBehaviour
    {
        [SerializeField] private string _paramName;
        [SerializeField] private FloatDataParameters _parameters;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Vector2 _lower;
        [SerializeField] private Vector2 _upper;

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
                _rect.anchoredPosition = Vector2.Lerp(_lower, _upper, normalizedParameter);
            }
        }

        private void OnDestroy()
        {
            _parameters.DataContainer.OnChanged -= DataContainer_OnChanged;
        }
    }
}