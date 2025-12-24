using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Fliper))]
[RequireComponent(typeof(EnemyAnimationHandler))]
public class Enemy : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");

    [SerializeField] private Transform _eyes;
    [SerializeField] private float _speed = 1f;

    private Fliper _fliper;
    private Rigidbody2D _rigidbody2D;
    private ObstacleChecker _obstacleChecker;
    private EnemyAnimationHandler _enemyAnimationHandler;
    private Patrol _patrol;
    private Mover _mover;
    private bool _currentLookToRight = true;
    private bool _isTurning = false;

    private void Awake()
    {
        _patrol = GetComponent<Patrol>();
        _fliper = GetComponent<Fliper>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
        _obstacleChecker = _eyes.GetComponent<ObstacleChecker>();
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        Vector2 movementDirection = _patrol.GetMovementDirection();
        
        bool newLookToRight = movementDirection.x > 0;
        
        if (newLookToRight != _currentLookToRight && !_isTurning)
        {
            StartTurn(newLookToRight);
            return;
        }
        
        if (!_isTurning)
        {
            _mover.Move(movementDirection.x, _speed);
            _enemyAnimationHandler.AnimateWalk(true);
        }
    }
    
    private void StartTurn(bool newLookToRight)
    {
        _isTurning = true;
        _mover.StopMovement();
        _enemyAnimationHandler.AnimateWalk(false);
        
        _currentLookToRight = newLookToRight;
        _fliper.Flip(_currentLookToRight);
        
        _isTurning = false;
    }
}
