using Packages.DVSceneSystem.Definitions;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Packages.DVSceneSystem.Core
{
    public class SceneSystem : MonoBehaviour
    {
        private static SceneSystem _instance;

        //[Header("=== Validate ===")]
        //[SerializeField] private bool _autoAddScenesToBuild = true;

        [Header("=== Main ===")]
        [SerializeField] private bool _loadFirstSceneOnStart = true;
        [SerializeField] private SceneInformation[] _scenes;

        [Header("=== Events ===")]
        [SerializeField] public UnityEvent OnStartChanging;
        [SerializeField] public UnityEvent OnSceneChanged;

        private SceneInformation _currentDynamicScene;
        private bool _isLoading;

        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning($"Double singleton {name}");
                GameObject.Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            var sc = _scenes.FirstOrDefault();
            if (sc != null) Load(sc);
        }

        public void Load(string sceneName)
        {
            Load(_scenes.FirstOrDefault(x => x.Name == sceneName));
        }

        private void Load(SceneInformation sc)
        {
            Debug.Log($"[{GetType().Name}] load scene request ({sc.Name})");

            if (_isLoading)
            {
                Debug.LogError("Received a request to load the next scene while the scene is loading. Canceled.");
                return;
            }

            StopAllCoroutines();
            StartCoroutine(LoadSceneAsync(sc));
        }

        public static SceneSystem GetInstance()
        {
            return _instance;
        }

        public static bool IsLoading()
        {
            return _instance._isLoading;
        }

        private IEnumerator LoadSceneAsync(SceneInformation sc)
        {
            if (sc == null)
            {
                yield return null;
            }
            else
            {
                Debug.Log($"[{GetType().Name}] start scene loading ({sc.Name})");
                _isLoading = true;
                OnStartChanging?.Invoke();

                // load scenes
                _currentDynamicScene = sc;
                string unityScene;
                for (int i = 0; i < sc.UnityScenes.Length; i++)
                {
                    unityScene = sc.UnityScenes[i];
                    var aop = SceneManager.LoadSceneAsync(unityScene, sc.LoadMode);

                    while (aop != null && !aop.isDone)
                    {
                        yield return aop;
                    }
                }

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sc.UnityScenes.Last()));

                // load prefabs
                foreach (var prefab in sc.Prefabs)
                {
                    Instantiate(prefab);
                }

                //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sc.UnityScenes.Last()));

                _isLoading = false;
                Debug.Log($"[{GetType().Name}] finish scene loading ({sc.Name})");
                OnSceneChanged?.Invoke();
            }
        } 

        public SceneInformation GetCurrentScene()
        {
            return _currentDynamicScene;
        }

        public void ApplicationQuit()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}