using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(AudioSource), typeof(EnemyAnimationHandler))]
[RequireComponent(typeof(Fliper), typeof(InputReader))]
public class Hero : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    [SerializeField] private Collector _collector;
    [SerializeField] private AudioClip _stepSoundLeft;
    [SerializeField] private AudioClip _stepSoundRight;
    [SerializeField] private GroundDetector _groundDetector;

    private InputReader _inputReader;
    private Mover _mover;
    private Fliper _fliper;
    private EnemyAnimationHandler _heroAnimationHandler;
    private SoundHandler _soundHandler;

    private bool _lookToRight = true;
    private bool _isOnGround = true;
    private bool _isSoundOn = false;


    private WaitForSeconds _stepDelay;
    private float _delay = 0.3f;
    
    public event Action<Transform> Collected;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _fliper = GetComponent<Fliper>();
        _heroAnimationHandler = GetComponent<EnemyAnimationHandler>();
        _stepDelay = new WaitForSeconds(_delay);
        _inputReader = GetComponent<InputReader>();
        _soundHandler = GetComponent<SoundHandler>();
    }

    private void OnEnable()
    {
        _collector.Touched += OnCollected;
    }

    private void OnDisable()
    {
        _collector.Touched -= OnCollected;
    }

    private void FixedUpdate()
    {
        if (_isOnGround)
        {
            _heroAnimationHandler.AnimateJump(JumpHash, !_isOnGround);
        }

        Move();

        Jump();

        if (_inputReader.Direction != 0 && _groundDetector.IsOnGround && !_isSoundOn)
        {
            StartCoroutine(PlayStepSound());
        }
    }

    private void Move()
    {
        _mover.Move(_inputReader.Direction, _speed);
        _heroAnimationHandler.AnimateWalk(WalkHash, (_inputReader.Direction != 0 && _groundDetector.IsOnGround));


        if (_inputReader.Direction > 0 && !_lookToRight)
        {
            _lookToRight = !_lookToRight;
            _fliper.Flip(_lookToRight);
        }
        else if (_inputReader.Direction < 0 && _lookToRight)
        {
            _lookToRight = !_lookToRight;
            _fliper.Flip(_lookToRight);
        }
    }

    private void OnCollected(Transform coin)
    {
        Collected?.Invoke(coin);
        _soundHandler.CollectCoin();
    }

    private void Jump()
    {
        if (_inputReader.GetIsJump() && _groundDetector.IsOnGround)
        {
            _mover.Jump(_jumpForce);
            _heroAnimationHandler.AnimateJump(JumpHash, true);
        }
    }

    private IEnumerator PlayStepSound()
    {
        _isSoundOn = true;
        
        _soundHandler.PlaySound(_stepSoundLeft);
        yield return _stepDelay;
        
        _soundHandler.PlaySound(_stepSoundRight);
        
        yield return _stepDelay;
    
        _isSoundOn = false;
    }
}