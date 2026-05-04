using System.Collections;
using FSM_for_test;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Parameters")] [SerializeField]
    private float _attackRange = 2f;

    [SerializeField] private float _deathAnimationDelay = 2f;

    [Header("External components")] [SerializeField]
    private TargetDetector _targetDetector;

    [SerializeField] private Sword _sword;
    [SerializeField] private Mover _mover;
    [SerializeField] private Fliper _fliper;
    [SerializeField] private ObstacleChecker _obstacleChecker;
    [SerializeField] private EnemyAnimationHandler _animationHandler;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Health _health;

    private StateMachine _stateMachine;
    
    private void Awake()
    {
        _stateMachine = EnemyStateMachineFactory.Create(
            enemyTransform: transform,
            targetDetector: _targetDetector,
            sword: _sword,
            mover: _mover,
            fliper: _fliper,
            obstacleChecker: _obstacleChecker,
            animationHandler: _animationHandler,
            attacker: _attacker,
            health: _health,
            attackRange: _attackRange
        );
    }
    
    private void Update()
    {
        _stateMachine?.Update();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnDied()
    {
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(_deathAnimationDelay);
        Destroy(gameObject);
    }
}