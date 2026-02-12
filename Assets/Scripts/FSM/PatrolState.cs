using UnityEngine;

namespace FSM_for_test
{
    public class PatrolState: IState
    {
        private Mover _mover;
        private Fliper _fliper;
        private ObstacleChecker  _obstacleChecker;
        private EnemyAnimationHandler  _animationHandler;
        
        private float _speed = 1f;
        private bool _lookToRight = true;

        public PatrolState(Mover mover, Fliper fliper,  ObstacleChecker obstacleChecker, EnemyAnimationHandler  animationHandler)
        {
            _mover = mover;
            _fliper = fliper;
            _obstacleChecker = obstacleChecker;
            _animationHandler = animationHandler;
        }
        
        public void Enter()
        {
            _animationHandler.AnimateWalkEnable();
        }

        public void Exit()
        {
            _animationHandler.AnimateWalkDisable();
        }

        public void Update()
        {
            Patroling();
        }
        
        private void Patroling()
        {
            Vector2 movementDirection = GetMovementDirection();
            bool lookDirection = movementDirection.x > 0;
            ActivateTurnAround(lookDirection);
            _mover.Move(movementDirection.x, _speed);
        }
        
        public Vector2 GetMovementDirection()
        {
            if (_obstacleChecker.SeePathAhead(_lookToRight))
            {
                _lookToRight = !_lookToRight;
            }
    
            return _lookToRight ? Vector2.right : Vector2.left;
        }
        
        // TODO: relocate ActivateTurnAround to separate class for this and Chasing scripts.
        private void ActivateTurnAround(bool newLookToRight)
        {
            if (_fliper.ShouldFlip(newLookToRight))
            {
                _mover.StopMovement();
                _fliper.Flip(newLookToRight);
            }
        }
    }
}