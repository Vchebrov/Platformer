using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Fliper))]
[RequireComponent(typeof(EnemyAnimationHandler), typeof(Patrol))]
[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private Fliper _fliper;
    private Rigidbody2D _rigidbody2D;
    private ObstacleChecker _obstacleChecker;
    private EnemyAnimationHandler _enemyAnimationHandler;
    private Patrol _patrol;
    private Mover _mover;

    private void Awake()
    {
        _patrol = GetComponent<Patrol>();
        _fliper = GetComponent<Fliper>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _enemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        Vector2 movementDirection = _patrol.GetMovementDirection();

        bool lookDirection = movementDirection.x > 0;

        ActivateTurnAround(lookDirection);

        _mover.Move(movementDirection.x, _speed);
        _enemyAnimationHandler.AnimateWalkEnable();
    }

    private void ActivateTurnAround(bool newLookToRight)
    {
        if (_fliper.ShouldFlip(newLookToRight))
        {
            _mover.StopMovement();
            _enemyAnimationHandler.AnimateWalkDisable();
            _fliper.Flip(newLookToRight);
        }
    }
}