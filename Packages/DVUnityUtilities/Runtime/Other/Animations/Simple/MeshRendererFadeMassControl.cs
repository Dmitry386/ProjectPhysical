using UnityEngine;

namespace DVUnityUtilities.Other.Animations.Simple
{
    internal class MeshRendererFadeMassControl : MonoBehaviour
    {
        [SerializeField] private MeshRendererFade[] _faders;

        public void StartFade(float seconds)
        {
            foreach (var item in _faders)
            {
                item.StartFade(seconds);
            }
        }

        public void StopFade()
        {
            foreach (var item in _faders)
            {
                item.StopFade();
            }
        }

        public void Unfade(float seconds)
        {
            foreach (var item in _faders)
            {
                item.Unfade(seconds);
            }
        }
    }
}