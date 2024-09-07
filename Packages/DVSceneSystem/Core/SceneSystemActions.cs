using Packages.DVSceneSystem.Definitions;
using System.Linq;
using UnityEngine;

namespace Packages.DVSceneSystem.Core
{
    [DefaultExecutionOrder(10)]
    internal class SceneSystemActions : MonoBehaviour
    {
        [SerializeField] private SceneActionData[] _actions;
        private SceneSystem _sceneSystem;

        private void Awake()
        {
            _sceneSystem = SceneSystem.GetInstance();

            if (_sceneSystem == null)
            {
                Debug.LogError("Null scene system");
                Destroy(this);
                return;
            }

            _sceneSystem.OnSceneChanged.AddListener(OnSceneChanged);
        }

        private void Start()
        {
            InvokeActions();
        }

        private void OnSceneChanged()
        {
            InvokeActions();
        }

        private void InvokeActions()
        {
            if (!_sceneSystem) return;

            var currentScene = _sceneSystem.GetCurrentScene();
            if (currentScene != null)
            {
                var acts = _actions.Where(x => x.Scenes.Contains(currentScene.Name));

                foreach (var act in acts)
                {
                    act.Actions.Invoke();
                }
            }
        }
    }
}