using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.Animations.Simple
{
    internal class MeshRendererFade : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _shaderColorName = "_BaseColor";
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material[] _temporaryMaterials;

        [Header("Settings")]
        [SerializeField] private bool _fadeOnStart = true;
        [SerializeField] public UnityEvent<MeshRendererFade> OnStartFade;
        [SerializeField] public UnityEvent<MeshRendererFade> OnFaded;

        [SerializeField] public UnityEvent<MeshRendererFade> OnStartUnfade;
        [SerializeField] public UnityEvent<MeshRendererFade> OnUnfaded;

        private Material[] _defaultMaterials;
        private Material[] _fadeMaterials;
        private float _targetAlpha;
        private float _fadeDuration;
        private Coroutine _fadeCoroutine;

        private void Start()
        {
            CacheMaterials(); 
        }

        public void CacheMaterials()
        {
            _defaultMaterials = _meshRenderer.sharedMaterials;
            _fadeMaterials = new Material[_temporaryMaterials.Length];

            for (int i = 0; i < _fadeMaterials.Length; i++)
            {
                _fadeMaterials[i] = Instantiate(_temporaryMaterials[i]);
            }
        }

        public void StartFade(float duration)
        {
            StartFade(0, duration);
        }

        public void StartFade(float targetAlpha, float duration)
        {
            // Stop previous fade coroutine if exists
            StopFade();

            _targetAlpha = Mathf.Clamp01(targetAlpha);
            _fadeDuration = duration;
            _fadeCoroutine = StartCoroutine(FadeCoroutine());

            if (_targetAlpha == 0) OnStartFade.Invoke(this);
            else OnStartUnfade.Invoke(this);
        }

        private IEnumerator FadeCoroutine()
        {
            float timer = 0f;
            Color startColor;
            Color targetColor;

            // Cache initial colors
            _meshRenderer.sharedMaterials = _fadeMaterials;
            var mats = _meshRenderer.sharedMaterials;
            Color[] initialColors = new Color[mats.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                initialColors[i] = mats[i].GetColor(_shaderColorName);
            }

            while (timer < _fadeDuration)
            {
                timer += Time.deltaTime;
                float ratio = timer / _fadeDuration;

                // Lerp between initial color and target color
                for (int i = 0; i < _fadeMaterials.Length; i++)
                {
                    startColor = initialColors[i];
                    targetColor = startColor;
                    targetColor.a = Mathf.Lerp(startColor.a, _targetAlpha, ratio);
                    _fadeMaterials[i].SetColor(_shaderColorName, targetColor);
                }

                yield return null;
            }

            // Ensure final color is set correctly
            for (int i = 0; i < _defaultMaterials.Length; i++)
            {
                targetColor = initialColors[i];
                targetColor.a = _targetAlpha;
                _defaultMaterials[i].SetColor(_shaderColorName, targetColor);
            }

            _fadeCoroutine = null;

            if (_targetAlpha == 0) OnFaded.Invoke(this);
            else OnUnfaded.Invoke(this);
        }

        public void StopFade()
        {
            // Stop the fade coroutine if running
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
                RestoreMaterials();
            }
        }

        public void Unfade(float duration)
        {
            StartFade(1f, duration); // Unfade by setting alpha to 1 (fully visible)
        }

        private void RestoreMaterials()
        {
            _meshRenderer.sharedMaterials = _defaultMaterials;
        }

        private void OnDestroy()
        {
            foreach (var fm in _fadeMaterials)
            {
                Material.Destroy(fm);
            }

            _fadeMaterials = null;
        }
    }
}