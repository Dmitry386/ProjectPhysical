using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.Performance
{
    internal class FpsLock : MonoBehaviour
    {
        [SerializeField] private bool _editorIgnore = true;
        [SerializeField, Min(-1)] private int _fpsLock = 60;

        private void Awake()
        {
            if (Application.isEditor && _editorIgnore)
            {
                Application.targetFrameRate = 1000;
            }
            else
            {
                Application.targetFrameRate = _fpsLock;
            }
        }
    }
}