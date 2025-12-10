using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Turnover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _eyes;
    [SerializeField] private float _speed = 1f;

    private Turnover _turnover;
    private Rigidbody2D _rigidbody2D;
    private ObstacleChecker _obstacleChecker;
    private Animator _animator;
    private Mover _mover;
    
    private bool _lookToRight = true;

    private static readonly int WalkHash = Animator.StringToHash("Walk");
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _turnover = GetComponent<Turnover>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lookToRight = transform.localScale.x > 0;
        _animator = GetComponent<Animator>();
        _obstacleChecker = GetComponentInChildren<ObstacleChecker>();
    }

    private void FixedUpdate()
    {
        if (_obstacleChecker.CheckAhead(_lookToRight))
        {
            _mover.Move(_lookToRight ? 1 : -1, _speed);
            _animator.SetBool(WalkHash, true);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);

            _turnover.Flip(transform);
            _lookToRight = !_lookToRight;
        }
    }
}