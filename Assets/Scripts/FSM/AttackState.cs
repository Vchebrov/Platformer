

using UnityEngine;

namespace FSM_for_test
{
    public class AttackState : IState
    {
        private Sword _sword;
        private Attacker _attacker;
        private EnemyAnimationHandler _animationHandler;

        public AttackState(Sword sword, Attacker attacker, EnemyAnimationHandler animationHandler)
        {
            _sword = sword;
            _attacker = attacker;
            _animationHandler = animationHandler;
        }
        
        public void Enter()
        {
            _sword.SwordHit += OnSwordHit;
            _animationHandler.AnimateAttackEnable();
        }

        public void Exit()
        {
            _animationHandler.AnimateAttackDisable();
            _sword.SwordHit -= OnSwordHit;
        }

        public void Update()
        {
           
        }
        
        private void OnSwordHit()
        {
            Debug.Log("Hit");
            _attacker.Attack();
        }
    }
}