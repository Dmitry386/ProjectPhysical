using UnityEngine;

namespace Packages.DVRigidbodyCharacterController.Source
{
    public class RigidbodyCharacterController_Rotation : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 35;

        [SerializeField]
        private Rigidbody _rigidbody;

        private Vector3 _rotationDir;

        public void SetRotateDirection(Vector3 up)
        {
            _rotationDir = up;
        }

        private void Update()
        {
            transform.eulerAngles += _speed * Time.deltaTime * _rotationDir;
        }
    }
}