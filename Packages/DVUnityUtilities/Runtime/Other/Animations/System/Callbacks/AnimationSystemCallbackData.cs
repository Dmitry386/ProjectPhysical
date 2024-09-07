using System;
using UnityEngine.Events;

namespace DVUnityUtilities.Other.Animations.System.Callbacks
{
    [Serializable]
    public class AnimationSystemCallbackData
    {
        public string EventName;
        public UnityEvent Actions;
    }
}