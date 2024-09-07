using UnityEngine;

namespace Packages.DVRigidbodyCharacterController.Source
{
    public class RigidbodyCharacterController_Movement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5f;

        [SerializeField]
        private bool _moveOnlyIfGrounded = true;

        [SerializeField]
        private LayerMask _groundLayerMask = -1;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private CapsuleCollider _collider;

        private Vector3 _finalVelocity;

        private float _speedMultiplier = 1f;

        private void Update()
        {
            if (_moveOnlyIfGrounded)
            {
                if (!IsGrounded(out _))
                {
                    _finalVelocity.x = 0;
                    _finalVelocity.z = 0;
                }
            }

            _rigidbody.velocity = new Vector3(_finalVelocity.x, _rigidbody.velocity.y, _finalVelocity.z);
            //Debug.Log(_rigidbody.velocity);
        }

        public void SetVelocity(float x, float z)
        {
            var newVector3 = _rigidbody.transform.TransformDirection(new Vector3(x * _speed , 0, z * _speed));

            _finalVelocity = new Vector3(newVector3.x * _speed * _speedMultiplier * _speedMultiplier, _rigidbody.velocity.y, newVector3.z * _speed * _speedMultiplier);
        }

        public void SetSpeedMultiplier(float multiplier)
        {
            _speedMultiplier = multiplier;
        }

        public bool IsGrounded(out RaycastHit hit)
        {
            float rayLength = GetStartGroundDetectionRayLength();
            Vector3 startRayPos = GetStartGroundDetectionRayPosition();

            return Physics.Raycast(startRayPos, Vector3.down, out hit, rayLength, _groundLayerMask, QueryTriggerInteraction.Ignore);
        }

        private Vector3 GetStartGroundDetectionRayPosition()
        {
            return _rigidbody.transform.TransformPoint(_collider.center);
        }

        private float GetStartGroundDetectionRayLength()
        {
            return _collider.height;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(GetStartGroundDetectionRayPosition().normalized * GetStartGroundDetectionRayLength(), Vector3.down);
        }
    }
}