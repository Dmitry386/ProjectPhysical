using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Packages.DVSceneSystem.Definitions
{
    [Serializable]
    public class SceneInformation
    {
        [Header("=== Settings ===")]
        public string Name;
       // public bool UnloadOnLoadOther = true;
        public LoadSceneMode LoadMode = LoadSceneMode.Additive;

        [Header("=== Load on activate (order important) ===")]
        public string[] UnityScenes;

        public GameObject[] Prefabs;

    }
}