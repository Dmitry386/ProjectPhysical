//using UnityEngine;

//namespace DVUnityUtilities.Other.Animations.System.Callbacks
//{
//    public class AnimationSystemCallbacks : MonoBehaviour
//    {
//        [SerializeField] private AnimationSystemCallbackData[] _actions;
//        [SerializeField] private AnimationSystem _animationSystem;

//        private void Awake()
//        {
//            if (!_actions.IsNullOrEmpty())
//            {
//                var clips = _animationSystem.GetClips();

//                foreach (var clip in clips)
//                {
//                    foreach (var clip_event in clip.AnimationClip.events)
//                    {
//                       // clip_event.
//                    }
//                }
//            }
//        }
//    }
//}