using Packages.DVUnityUtilities.Runtime.Other.Animations.System.Definitions;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DVUnityUtilities.Other.Animations.System
{
    public class AnimationSystem : MonoBehaviour
    {
        [Header("/// Settings ///")]
        [SerializeField] private bool _clearAnimationOnStart = false;
        [SerializeField] private bool _stopAnyAnimationOnTransformChanged = true;
        [SerializeField] private bool _playFirstFrameOfAnimationOnStop = true;
        [SerializeField] private Animation _animation;
        [SerializeField] private bool _preloadAnimations = false;

        [Header("/// Preregistered animations ///")]
        [SerializeField] private AnimationSystemClip[] _availableAnimations;

        private void Start()
        {
            if (_animation) InitAnimationComponentHandle(_animation);
            if (_clearAnimationOnStart) ClearAnimation();

            if (_preloadAnimations) PreloadAnimations();
        }

        private void PreloadAnimations()
        {
            if (_availableAnimations.Length > 0)
            {
                ApplyAnimation(_availableAnimations[0].AnimationClip.name);
                StopAnyAnimation();
            }
        }

        private void InitAnimationComponentHandle(Animation animation)
        {
            foreach (var ac in _availableAnimations)
            {
                var clip = ac.AnimationClip;
                clip.legacy = true;
                clip.wrapMode = ac.Wrap;
                animation.AddClip(clip, clip.name);
            }
        }

        private void ApplyAnimationInternalAnimationComponent(string name)
        {
            _animation.clip = _animation.GetClip(name);
            _animation.wrapMode = _animation.clip.wrapMode;
            // _animation.CrossFade(name, 1, PlayMode.StopAll);
            _animation[_animation.clip.name].speed = 1.0f;
            _animation.Play(name, PlayMode.StopAll);

            //Debug.Log($"{name} animation applyed | {_animation.clip?.name} | {_animation.clip.legacy} | " +
            //   $"{_animation.wrapMode} | {_animation.clip.wrapMode} | {_animation.enabled}" +
            //   $"{_animation.GetClipCount()} clips");
        }

        private void ClearAnimationInternalAnimationComponent()
        {
            if (!_availableAnimations.IsNullOrEmpty())
            {
                ApplyAnimation(_availableAnimations.First().AnimationClip.name);
            }
        }

        public void ApplyAnimation(string name)
        {
            if (_animation) ApplyAnimationInternalAnimationComponent(name);
        }

        public void ClearAnimation()
        {
            if (_animation) ClearAnimationInternalAnimationComponent();
        }

        public float GetAnimationDuration(string name)
        {
            var anim = _animation.GetClip(name);
            return anim.length;
        }

        public void StopAnyAnimation()
        {
            _animation.Stop();
            if (_playFirstFrameOfAnimationOnStop && this && gameObject.activeInHierarchy)
            {
                StartCoroutine(StopAnyAnimationInternal());
            }
        }

        private IEnumerator StopAnyAnimationInternal()
        {
            yield return null;
            if (_animation.clip)
            {
                _animation[_animation.clip.name].time = 0.0f;
                _animation[_animation.clip.name].speed = 0.0f;
                _animation.Play(_animation.clip.name);
            }
        }

        private void OnBeforeTransformParentChanged()
        {
            if (_stopAnyAnimationOnTransformChanged)
            {
                StopAnyAnimation();
            }
        }

        private void OnEnable()
        {
            if (_stopAnyAnimationOnTransformChanged)
            {
                StopAnyAnimation();
            }
        }

        private void OnDisable()
        {
            if(_stopAnyAnimationOnTransformChanged)
            {
                StopAnyAnimation();
            }
        }

        public AnimationSystemClip[] GetClips()
        {
            return _availableAnimations;
        }
    }
}