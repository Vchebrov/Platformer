using System.Collections.Generic;
using UnityEngine;

namespace FSM_for_test
{
    public class DeathState: IState
    {
        private EnemyAnimationHandler _animationHandler;
        private Health _health;
        private Transform _obj;
        private float _deathAnimationDelay = 2f;

        public DeathState(EnemyAnimationHandler animationHandler, Health health, Transform obj)
        {
            _animationHandler = animationHandler;
            _health = health;
        }
        
        public void Enter()
        {
            _health.Died += OnDied;
        }

        public void Exit()
        {
            _health.Died -= OnDied;
        }

        public void Update()
        {
            
        }

        private void OnDied()
        {
            _animationHandler.AnimateDeathEnable();
        }
    }
}