using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.Optimization
{
    internal class RenderTextureAutoCleanerOnEnableDisable : MonoBehaviour
    {
        [SerializeField] private RenderTexture _renderTexture;

        private void OnEnable()
        {
            _renderTexture.Release();
            _renderTexture.Create();
        }

        private void OnDisable()
        {
            _renderTexture.Release();
        }
    }
}