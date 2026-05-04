using System;

namespace FSM_for_test
{
    using System.Collections.Generic;
    using UnityEngine;

    public class StateMachine : MonoBehaviour
    {
        private Dictionary<Type, IState> _states = new();
        private IState _currentState;
        private List<Transition> _transitions = new();

        public void Initialize(List<Transition> transitions, Dictionary<Type, IState> states, Type startStateType)
        {
          _transitions = transitions;
          _states = states;

          if (_states.TryGetValue(startStateType, out var startState))
          {
              _currentState = startState;
              _currentState.Enter();
          }
        }

        public void Update()
        {
            if (_currentState == null)
                return;
            
            var currentType = _currentState.GetType();

            Transition actualTransition = null;

            foreach (var transition in _transitions)
            {
                if (transition.FromStateType == currentType && transition.Condition())
                {
                    actualTransition = transition;
                    break;
                }
            }

            if (actualTransition != null)
            {
                ChangeState(actualTransition);
                return;
            }
            
            _currentState.Update();
        }

        private void ChangeState(Transition transition)
        {
            if (!_states.TryGetValue(transition.ToStateType, out var newState)) return;
            if (newState == _currentState) return;

            _currentState.Exit();
            
            if (transition.PayloadProvider != null && newState is IPayloadState<object> payloadState)
            {
                var payload = transition.PayloadProvider();
                payloadState.Enter(payload);
            }
            else if (newState is IState state)
            {
                state.Enter();
            }

            _currentState = newState;
        }
       
    }
}