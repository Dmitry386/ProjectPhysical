using UnityEngine;

namespace DVUnityUtilities.Other.CursorControl
{
    internal class CursorController : MonoBehaviour
    {
        [SerializeField] private CursorLockMode _lockMode = CursorLockMode.Locked;

        private void Awake()
        {
            if (IsPossibleToShowCursor()) Cursor.lockState = _lockMode;
            else Cursor.lockState = CursorLockMode.Locked;
        }

        public static bool IsPossibleToShowCursor()
        {
            return Application.isEditor;
        }

        public void SetCursorState(CursorLockMode lockmd)
        {
            Cursor.lockState = lockmd;
        }
    }
}