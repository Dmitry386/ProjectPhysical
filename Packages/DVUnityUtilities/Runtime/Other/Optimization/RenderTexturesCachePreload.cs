using DVUnityUtilities;
using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.Optimization
{
    internal class RenderTexturesCachePreload : MonoBehaviour
    {
        private void Awake()
        {
            var all = Util.FindObjectsWithComponentOfType<RenderTextureCacheRawImage>(true);

            foreach (var i in all)
            {
                i.CreateCache();
            }
        }
    }
}
