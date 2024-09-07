using DVUnityUtilities;
using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.FunctionsRedirectors
{
    public class DVUtilsFunctionsRedirector : MonoBehaviour
    {
        public void DeactivateChilds(GameObject go)
        {
            var childs = Util.GetAllChilds(go.transform, false);
            foreach (var c in childs)
            {
                c.gameObject.SetActive(false);
            }
        }

        public void DeactivateChildsOfCurrent()
        {
            DeactivateChilds(gameObject); 
        }
    }
}