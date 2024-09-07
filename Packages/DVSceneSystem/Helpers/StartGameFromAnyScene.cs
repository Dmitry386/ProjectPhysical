using DVUnityUtilities.Other.Coroutines;
using Packages.DVSceneSystem.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Packages.DVSceneSystem.Helpers
{
    internal class StartGameFromAnyScene : MonoBehaviour
    {
        [SerializeField] private string _startScene = "Bootstrap";
        [SerializeField, Min(0)] private float _delay = 0;

        private void Start()
        {
            new CoroutineTimer(this, _delay, false).Start().OnTick += OnTick;
        }

        private void OnTick(CoroutineTimer timer)
        {
            if (SceneSystem.GetInstance() == null)
            {
                SceneManager.LoadSceneAsync(_startScene, LoadSceneMode.Single);
            }
        }
    }
}