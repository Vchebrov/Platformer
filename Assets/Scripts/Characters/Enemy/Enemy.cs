using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Fliper))]
public class Enemy : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");

    [SerializeField] private Transform _eyes;
    [SerializeField] private float _speed = 1f;

    private Fliper _fliper;
    private Rigidbody2D _rigidbody2D;
    private ObstacleChecker _obstacleChecker;
    private AnimationHandler _animationHandler;
    private Patrol _patrol;

    private bool _lookToRight = true;

    private void Awake()
    {
        _patrol = GetComponent<Patrol>();
        _fliper = GetComponent<Fliper>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lookToRight = transform.localScale.x > 0;
        _animationHandler = GetComponent<AnimationHandler>();
        _obstacleChecker = GetComponentInChildren<ObstacleChecker>();
    }

    private void FixedUpdate()
    {
        if (_obstacleChecker.CheckAhead(_lookToRight))
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            _lookToRight = !_lookToRight;
            
            _fliper.Flip(_lookToRight);
        }
        else
        {
            _patrol.Move(_lookToRight ? 1 : -1, _speed);
            _animationHandler.AnimateWalk(WalkHash, true);
        }
    }
}