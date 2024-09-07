using UnityEngine;

namespace DVUnityUtilities.Other.ActivationSwitch
{
    internal class GameObjectActivationHelper : MonoBehaviour
    {
        public void SwitchActivation()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}