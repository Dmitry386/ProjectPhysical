using System.Linq;
using UnityEngine;

namespace DVUnityUtilities
{
    public static class LayerUtils
    {
        public static bool IsLayerInLayerMask(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }

        public static int PhysicsMatrixToLayerMask(int myLayer)
        {
            int layerMask = 0;

            for (int i = 0; i < 32; i++)
            {
                if (!Physics.GetIgnoreLayerCollision(myLayer, i))
                {
                    layerMask = layerMask | 1 << i;
                }
            }

            return layerMask;
        }

        public static void SetLayerWithChilds(GameObject go, int layerNumber, int[] ignoreLayers = null)
        {
            foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
            {
                if (ignoreLayers == null || !ignoreLayers.Contains(trans.gameObject.layer))
                {
                    trans.gameObject.layer = layerNumber;
                }
            }
        }
    }
}