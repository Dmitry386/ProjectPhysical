using DVUnityUtilities.Other.Animations.System.Definitions;
using DVUnityUtilities.Other.StateMachines;
using DVUnityUtilities.Other.StateMachines.Events;
using System;
using System.Linq;
using UnityEngine;

namespace DVUnityUtilities.Other.Animations.System
{
    public class StateMachineAnimationSystemRedirector : MonoBehaviour
    {
        [SerializeField] private AnimationSystem _animationSystem;
        [SerializeField] private AnimationStateData[] _data;
        private DefaultStateMachine _stateMachine;

        public void SetStateMachineToObserve(DefaultStateMachine stateMachine)
        {
            if (_stateMachine == stateMachine) return;
            if (_stateMachine != null) _stateMachine.OnStateChanged -= OnChangeState;

            _stateMachine = stateMachine;
            _stateMachine.OnStateChanged += OnChangeState;
        }

        private void OnChangeState(StateChangeEventArgs args)
        {
            InvokeAnimationForState(_stateMachine.GetState());
        }

        public void InvokeAnimationForState(string state)
        {
            var dataToPlay = _data.FirstOrDefault(x => x.EnableOnStates.Contains(state));
            _animationSystem.ApplyAnimation(dataToPlay.AnimationName);
        }
    }
}