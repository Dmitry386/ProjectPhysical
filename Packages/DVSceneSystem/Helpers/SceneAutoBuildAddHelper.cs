//using Packages.DVSceneSystem.Definitions;
//using System.Linq;

//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEngine;
//#endif

//namespace Packages.DVSceneSystem.Helpers
//{
//    internal static class SceneAutoBuildAddHelper
//    {
//        internal static void ValidateSceneList(SceneInformation[] scenes)
//        {
//#if UNITY_EDITOR
//            if (IsAllScenesRegisteredInBuild(scenes)) return;

//            var scenesGUIDs = AssetDatabase.FindAssets("t:Scene");
//            var scenesPaths = scenesGUIDs.Select(AssetDatabase.GUIDToAssetPath);

//            Debug.Log($"Scenes count in files: {scenesPaths.Count()}");

//            foreach (var item in scenesPaths)
//            {
//                Debug.Log($" {item}  ");

//            }
//#endif
//        }

//        private static bool IsAllScenesRegisteredInBuild(SceneInformation[] waitScenes)
//        {
//#if UNITY_EDITOR
//            var buildScenes = EditorBuildSettings.scenes.Select(x=>x.na;

//#else
//            return false;
//#endif
//        }
//    }
//}