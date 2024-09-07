using Packages.DVSceneSystem.Core;
using UnityEngine;

namespace Packages.DVSceneSystem.UI
{
    public class LoadScreenUI : MonoBehaviour
    {
        [SerializeField] private SceneSystem _sceneSystem;

        private void Awake()
        {
            _sceneSystem.OnSceneChanged.AddListener(OnSceneChanged);
            _sceneSystem.OnStartChanging.AddListener(OnStartChanging);
        }

        private void OnStartChanging()
        {
            gameObject.SetActive(true);
            Debug.Log("Scene screen activated");
        }

        private void OnSceneChanged()
        {
            gameObject.SetActive(false);
            Debug.Log("Scene screen deactivated");
        }
    }
}
