using DVUnityUtilities.Other.ServiceLocator;
using UnityEngine;

namespace Assets.Scripts.Core.SaveControl
{
    [DefaultExecutionOrder(10)]
    internal class SaveLoader : MonoBehaviour
    {
        public static bool NeedToLoad;

        [SerializeField]
        private bool _loadOnStartIfNeed;

        [SerializeField]
        private SaveInventorySystem _saveInventorySystem;

        private void Start()
        {
            if (_loadOnStartIfNeed && NeedToLoad)
            {
                NeedToLoad = false;
                if (_saveInventorySystem)
                {
                    _saveInventorySystem.Load();
                }
            }
        }

        public void SetNeedToLoad()
        {
            NeedToLoad = true;
        }
    }
}