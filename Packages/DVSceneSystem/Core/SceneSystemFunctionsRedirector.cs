using UnityEngine;

namespace Packages.DVSceneSystem.Core
{
    public class SceneSystemFunctionsRedirector : MonoBehaviour
    {
        private SceneSystem _sceneSystem;

        public void Awake()
        {
            _sceneSystem = SceneSystem.GetInstance();
        }

        public void LoadScene(string name)
        {
            _sceneSystem?.Load(name);
        }
    }
}