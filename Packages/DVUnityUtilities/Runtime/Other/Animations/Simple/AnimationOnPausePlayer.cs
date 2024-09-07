using System;
using System.Collections;
using UnityEngine;

namespace Packages.DVUnityUtilities.Runtime.Other.Animations.Simple
{
    internal class AnimationOnPausePlayer : MonoBehaviour
    {
        public Animation Animation;

        public void Play(string name)
        {
            if (Animation) StartCoroutine(Play(Animation, name, false, null));
        }

        public static IEnumerator Play(Animation animation, string clipName, bool useTimeScale, Action onComplete)
        {
            if (!useTimeScale)
            {
                //  Debug.Log("Started this animation! ( " + clipName + " ) ");
                AnimationState _currState = animation[clipName];
                bool isPlaying = true;
                float _startTime = 0F;
                float _progressTime = 0F;
                float _timeAtLastFrame = 0F;
                float _timeAtCurrentFrame = 0F;
                float deltaTime = 0F;


                animation.Play(clipName);

                _timeAtLastFrame = Time.realtimeSinceStartup;
                while (isPlaying)
                {
                    _timeAtCurrentFrame = Time.realtimeSinceStartup;
                    deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
                    _timeAtLastFrame = _timeAtCurrentFrame;

                    _progressTime += deltaTime;
                    _currState.normalizedTime = _progressTime / _currState.length;
                    animation.Sample();

                    if (_progressTime >= _currState.length)
                    {
                        if (_currState.wrapMode != WrapMode.Loop)
                        {
                            isPlaying = false;
                        }
                        else
                        {
                            _progressTime = 0.0f;
                        }
                    }

                    yield return new WaitForEndOfFrame();
                }
                yield return null;
                onComplete?.Invoke();
            }
            else
            {
                animation.Play(clipName);
            }
        }
    }
}
