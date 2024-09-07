using UnityEngine;

namespace Packages.DVSceneSystem.Helpers
{
    internal class DontDestroyOnLoadMono : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}