using UnityEngine;

namespace FSM_for_test
{
    public class ChaseState : IState
    {
        private Transform _target;
        private TargetDetector _targetDetector; 
        private Fliper _fliper;
        private Mover _mover;
        private EnemyAnimationHandler _animationHandler;
        private Transform _selfTransform;

        private float _chaseSpeed = 5f;

        public ChaseState(
            Mover mover, 
            Fliper fliper, 
            EnemyAnimationHandler animationHandler, 
            Transform transform,
            TargetDetector targetDetector)  
        {
            _mover = mover;
            _fliper = fliper;
            _animationHandler = animationHandler;
            _selfTransform = transform;
            _targetDetector = targetDetector;
        }

        public void Enter()
        {
            _target = _targetDetector.GetTarget();
            _animationHandler.AnimateRunEnable();
        }

        public void Exit()
        {
            _animationHandler.AnimateRunDisable();
        }

        public void Update()
        {
            Chase();
        }
        
        private void Chase()
        {
            if (_target == null) return;

            Vector2 direction = (_target.position - _selfTransform.position).normalized;
            bool lookDirection = direction.x > 0;
            
            _fliper.TryFlip(lookDirection, _mover, shouldStop: false);

            _mover.Move(direction.x, _chaseSpeed);
        }
    }
}