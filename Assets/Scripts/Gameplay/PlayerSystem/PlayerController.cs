using Assets.Scripts.Core.InputControls;
using DVUnityUtilities.Other.ServiceLocator;
using Packages.DVParameters.Source.Mono;
using Packages.DVRigidbodyCharacterController.Source;
using UnityEngine;

namespace Assets.Scripts.Gameplay.PlayerSystem
{
    internal class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private RigidbodyCharacterController_Movement _movementControl;

        [SerializeField]
        private RigidbodyCharacterController_Rotation _rotationControl;

        [SerializeField]
        private FloatDataParameters _floatParams;

        [SerializeField]
        private string _speedMultiplierParameterName = "Speed";

        private IInputControl _input;

        private void Start()
        {
            _input = DVServiceLocator.GetService<IInputControl>();
        }

        private void Update()
        {
            if (_input != null)
            {
                UpdateSpeedMultiplier();
                UpdateMovement();
            }
        }

        private void UpdateMovement()
        {
            var inputDirection = _input.InputData.LocalMoveDirection;

            if (_movementControl)
            {
                _movementControl.SetVelocity(0, inputDirection.y);
            }

            if (_rotationControl)
            {
                _rotationControl.SetRotateDirection(inputDirection.x * Vector3.up);
            }
        }

        private void UpdateSpeedMultiplier()
        {
            float speedMultiplier = 1;

            if (_floatParams && _floatParams.DataContainer.GetData(_speedMultiplierParameterName, out var parameter))
            {
                speedMultiplier = parameter.Value;
            }

            _movementControl.SetSpeedMultiplier(Mathf.Clamp(speedMultiplier, 1, int.MaxValue));
        }
    }
}