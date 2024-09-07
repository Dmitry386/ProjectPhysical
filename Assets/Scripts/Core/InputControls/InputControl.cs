using Assets.Scripts.Core.InputControls.Data;
using UnityEngine;

namespace Assets.Scripts.Core.InputControls
{
    [DefaultExecutionOrder(-1)]
    public class InputControl : MonoBehaviour, IInputControl
    {
        public InputData InputData { get; } = new();

        private KeyControls _input;

        public void Awake()
        {
            _input = new();
            _input.Enable();
        }

        private void Update()
        {
            InputData.LocalMoveDirection = _input.Gameplay.Movement.ReadValue<Vector2>();
            InputData.EscapeRequest = _input.UI.Menu.triggered;
            InputData.Inventory = _input.UI.Inventory.triggered;
        }
    }
}