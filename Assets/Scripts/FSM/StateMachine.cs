using System;

namespace FSM_for_test
{
    using System.Collections.Generic;
    using UnityEngine;

    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private Sword _sword;
        [SerializeField] private Mover _mover;
        [SerializeField] private Fliper _fliper;
        [SerializeField] private ObstacleChecker _obstacleChecker;
        [SerializeField] private EnemyAnimationHandler _animationHandler;
        [SerializeField] private Attacker _attacker;
        [SerializeField] private Health _health;

        private Dictionary<Type, IState> _states = new();
        private IState _currentState;
        private List<Transition> _transitions = new();

        private float _attackRange = 2f;

        void Awake()
        {
            var patrol = new PatrolState(_mover, _fliper, _obstacleChecker, _animationHandler);
            var chase = new ChaseState(_mover, _fliper, _animationHandler, gameObject.transform);
            var attack = new AttackState(_sword, _attacker, _animationHandler);
            var death = new DeathState(_animationHandler, _health, gameObject.transform);

            _states.Add(patrol.GetType(), patrol);
            _states.Add(chase.GetType(), chase);
            _states.Add(attack.GetType(), attack);
            _states.Add(death.GetType(), death);

            _transitions.Add(new Transition(
                typeof(PatrolState),
                typeof(ChaseState),
                () => _targetDetector.GetTarget() != null
            ));

            _transitions.Add(new Transition(
                typeof(ChaseState),
                typeof(PatrolState),
                () => _targetDetector.GetTarget() == null));

            _transitions.Add(new Transition(
                typeof(PatrolState),
                typeof(AttackState),
                () => _targetDetector.GetDistance() <= _attackRange
            ));

            _transitions.Add(new Transition(
                typeof(ChaseState),
                typeof(AttackState),
                () => _targetDetector.GetDistance() <= _attackRange
            ));

            _transitions.Add(new Transition(
                typeof(AttackState),
                typeof(ChaseState),
                () => _targetDetector.GetDistance() > _attackRange
            ));

            _currentState = patrol;
            _currentState.Enter();
        }

        void Update()
        {
            _currentState.Update();

            if (_health.HitPoints <= 0)
            {
                if (_states.TryGetValue(typeof(DeathState), out var deathState))
                {
                    _currentState.Exit();
                    _currentState = deathState;
                    _currentState.Enter();
                    return;
                }
            }

            foreach (var transition in _transitions)
            {
                if (transition.FromStateType == _currentState.GetType() &&
                    transition.Condition() &&
                    _states.TryGetValue(transition.ToStateType, out var nextState))
                {
                    _currentState.Exit();

                    if (nextState is ChaseState chaseState)
                    {
                        var target = _targetDetector.GetTarget();
                        if (target != null)
                        {
                            chaseState.GetTarget(target);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    _currentState = nextState;
                    _currentState.Enter();
                    break;
                }
            }
        }
    }
}