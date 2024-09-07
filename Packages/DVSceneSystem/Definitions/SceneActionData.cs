using System;
using UnityEngine.Events;

namespace Packages.DVSceneSystem.Definitions
{
    [Serializable]
    internal class SceneActionData
    {
        public string[] Scenes;
        public UnityEvent Actions;
    }
}
