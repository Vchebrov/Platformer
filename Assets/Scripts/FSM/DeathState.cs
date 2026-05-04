using System.Collections.Generic;
using UnityEngine;

namespace FSM_for_test
{
    public class DeathState: IState
    {
        private EnemyAnimationHandler _animationHandler;
        private Health _health;
        
        public DeathState(EnemyAnimationHandler animationHandler, Health health)
        {
            _animationHandler = animationHandler;
            _health = health;
        }
        
        public void Enter()
        {
            _animationHandler.AnimateDeathEnable();
        }

        public void Exit(){}

        public void Update() {}

      
    }
}