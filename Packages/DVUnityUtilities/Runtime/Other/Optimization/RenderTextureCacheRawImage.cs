using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Packages.DVUnityUtilities.Runtime.Other.Optimization
{
    internal class RenderTextureCacheRawImage : MonoBehaviour
    {
        [SerializeField] private RawImage _img;
        [SerializeField] private VideoPlayer _player;

        private RenderTexture _cachedRenderTexture;
        private bool _cached;

        private void Awake()
        {
            CreateCache();
        }

        public void CreateCache()
        {
            if (_cached) return;
            _cached = true;

            var cur = _img.texture as RenderTexture;
            _cachedRenderTexture = new RenderTexture(Screen.width, Screen.height, cur.depth);

            _player.targetTexture = _cachedRenderTexture;
            _img.texture = _cachedRenderTexture;

            _cachedRenderTexture.Create(); 
        }

        private void OnDestroy()
        {
            _cachedRenderTexture.Release();
        }
    }
}
