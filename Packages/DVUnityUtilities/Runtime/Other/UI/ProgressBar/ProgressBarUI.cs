using UnityEngine;
using UnityEngine.UI;

namespace DVUnityUtilities.Other.UI.ProgressBar
{
    [SelectionBase]
    [RequireComponent(typeof(Image))]
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _fillValue = 1f;
        [SerializeField] private Image _img;

        /// <summary>
        /// 0.0 - 1.0f
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(float value)
        {
            _fillValue = Mathf.Clamp(value, 0f, 1f);
            UpdateVisual();
        }

        public float GetValue()
        {
            return _fillValue;
        }

        private void UpdateVisual()
        {
            _img.fillAmount = _fillValue;
        }

        private void OnValidate()
        {
            _img ??= GetComponent<Image>();
            _img.type = Image.Type.Filled;
            UpdateVisual();
        }
    }
}