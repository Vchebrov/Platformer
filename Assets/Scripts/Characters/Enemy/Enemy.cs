using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Fliper))]
[RequireComponent(typeof(EnemyAnimationHandler), typeof(EnemyBehaviorHandler))]
[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Parameters")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _chaseSpeed = 5f;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _deathAnimationDelay = 2f;

    [Header("External components")]
    [SerializeField] private Sword _sword;
    [SerializeField] private Health _health;
    
    private Fliper _fliper;
    private ObstacleChecker _obstacleChecker;
    private EnemyAnimationHandler _enemyAnimationHandler;
    private EnemyBehaviorHandler _enemyBehaviorHandler;
    private Mover _mover;
    private WaitForSeconds _attackDelay;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _enemyBehaviorHandler = GetComponent<EnemyBehaviorHandler>();
        _fliper = GetComponent<Fliper>();
        _enemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
        _mover = GetComponent<Mover>();
        _attackDelay = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _sword.SwordHit += OnSwordHit;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _sword.SwordHit -= OnSwordHit;
        _health.Died -= OnDied;
    }

    private void FixedUpdate()
    {
        Transform target = _enemyBehaviorHandler.GetTargetPosition();

        if (target)
        {
            ChaseHero(target);
        }
        else
        {
            Patroling();
        }
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnSwordHit()
    {
        _attacker.Attack();
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

    private void Patroling()
    {
        _enemyAnimationHandler.AnimateRunDisable();

        Vector2 movementDirection = _enemyBehaviorHandler.GetMovementDirection();
        bool lookDirection = movementDirection.x > 0;

        ActivateTurnAround(lookDirection);
        _mover.Move(movementDirection.x, _speed);
        _enemyAnimationHandler.AnimateWalkEnable();
    }

    private void ChaseHero(Transform target)
    {
        _enemyAnimationHandler.AnimateWalkDisable();
        _enemyAnimationHandler.AnimateRunEnable();
        Vector2 direction = (target.position - transform.position).normalized;
        bool lookDirection = direction.x > 0;
        ActivateTurnAround(lookDirection);

        if (_enemyBehaviorHandler.CanAttack())
        {
            _enemyAnimationHandler.AnimateRunDisable();
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(AttackRoutine(target));
            }
        }
        else
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
                _enemyAnimationHandler.AnimateAttackDisable();
            }

            _mover.Move(direction.x, _chaseSpeed);
        }
    }

    private IEnumerator AttackRoutine(Transform target)
    {
        while (_enemyBehaviorHandler.CanAttack() && target != null)
        {
            _enemyAnimationHandler.AnimateAttackEnable();
            yield return _attackDelay;
            _enemyAnimationHandler.AnimateAttackDisable();
            yield return null; 
        }
        
        _enemyAnimationHandler.AnimateAttackDisable();
        _attackCoroutine = null;
    }

    private void OnDied()
    {
        _enemyAnimationHandler.DisableAllAnimations();
        _enemyAnimationHandler.AnimateDeathEnable();
        StartCoroutine(DelayedDestroy());
    }
    
    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(_deathAnimationDelay);
        Destroy(gameObject);
    }
}
