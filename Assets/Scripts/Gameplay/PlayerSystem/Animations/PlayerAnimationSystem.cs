using UnityEngine;

namespace Assets.Scripts.Gameplay.PlayerSystem.Animations
{
    internal class PlayerAnimationSystem : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
         
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private float _minSpeedToDetect = 0.01f;

        private void Update()
        {
            bool isRunning = Mathf.Abs(_rigidbody.velocity.x) >= _minSpeedToDetect
                || Mathf.Abs(_rigidbody.velocity.z) >= _minSpeedToDetect;

            _animator.SetBool(nameof(isRunning), isRunning);
        }
    }
}