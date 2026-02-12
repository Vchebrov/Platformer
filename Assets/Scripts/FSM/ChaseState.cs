using UnityEngine;

namespace FSM_for_test
{
    public class ChaseState : IState
    {
        private Transform _target;
        private Fliper _fliper;
        private Mover _mover;
        private EnemyAnimationHandler _animationHandler;
        private Transform _selfTransform;

        private float _chaseSpeed = 5f;

        public ChaseState(Mover mover, Fliper fliper, EnemyAnimationHandler animationHandler, Transform transform)
        {
            _fliper = fliper;
            _mover = mover;
            _animationHandler = animationHandler;
            _selfTransform = transform;
        }

        public void GetTarget(Transform target)
        {
            _target = target;
        }

        public void Enter()
        {
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
            Vector2 direction = (_target.position - _selfTransform.position).normalized;
            bool lookDirection = direction.x > 0;
            ActivateTurnAround(lookDirection);

            _mover.Move(direction.x, _chaseSpeed);
        }

        // TODO: relocate ActivateTurnAround to separate class for this and Patroling scripts.
        private void ActivateTurnAround(bool newLookToRight)
        {
            if (_fliper.ShouldFlip(newLookToRight))
            {
                _fliper.Flip(newLookToRight);
            }
        }
    }
}