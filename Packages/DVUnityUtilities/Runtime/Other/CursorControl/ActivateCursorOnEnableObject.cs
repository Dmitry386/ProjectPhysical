using UnityEngine;

namespace DVUnityUtilities.Other.CursorControl
{
    internal class ActivateCursorOnEnableObject : MonoBehaviour
    {
        private void OnEnable()
        {
            if (CursorController.IsPossibleToShowCursor())
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
