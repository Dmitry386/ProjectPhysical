using Assets.Scripts.Core.InputControls;
using DVUnityUtilities.Other.ServiceLocator;
using Packages.DVSceneSystem.Core;
using UnityEngine;

namespace Assets.Scripts.Gameplay.EscapeMenu
{
    internal class EscapeMenuExit : MonoBehaviour
    {
        [SerializeField]
        private string _menuSceneName = "Menu";

        private IInputControl _inputControl;

        private SceneSystem _sceneSystem;

        private void Awake()
        {
            _sceneSystem = DVServiceLocator.GetService<SceneSystem>();
            _inputControl = DVServiceLocator.GetService<IInputControl>();
        }

        private void Update()
        {
            if (_inputControl != null)
            {
                if (_inputControl.InputData.EscapeRequest)
                {
                    EscapeToMenu();
                }
            }
        }

        public void EscapeToMenu()
        {
            if (_sceneSystem)
            {
                _sceneSystem.Load(_menuSceneName);
            }
        }
    }
}