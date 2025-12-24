using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(AudioSource), typeof(HeroAnimationHandler))]
[RequireComponent(typeof(Fliper), typeof(InputReader))]
public class Hero : MonoBehaviour {
    
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    [SerializeField] private Collector _collector;
    [SerializeField] private AudioClip _stepSoundLeft;
    [SerializeField] private AudioClip _stepSoundRight;
    [SerializeField] private GroundDetector _groundDetector;

    private InputReader _inputReader;
    private Mover _mover;
    private Fliper _fliper;
    private HeroAnimationHandler _heroAnimationHandler;
    private SoundHandler _soundHandler;

    private bool _lookToRight = true;
    private bool _isOnGround = true;
    private bool _isSoundOn = false;


    private WaitForSeconds _stepDelay;
    private float _delay = 0.3f;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _fliper = GetComponent<Fliper>();
        _heroAnimationHandler = GetComponent<HeroAnimationHandler>();
        _stepDelay = new WaitForSeconds(_delay);
        _inputReader = GetComponent<InputReader>();
        _soundHandler = GetComponent<SoundHandler>();
    }

    private void FixedUpdate()
    {
        if (_isOnGround)
        {
            _heroAnimationHandler.AnimateJump(!_isOnGround);
        }

        HandleMove(_inputReader.Direction);

        Jump();

        if (_inputReader.Direction != 0 && _groundDetector.IsOnGround && !_isSoundOn)
        {
            StartCoroutine(PlayStepSound());
        }
    }

    private void HandleMove(float direction)
    {
        _mover.Move(direction, _speed);
        _heroAnimationHandler.AnimateWalk(direction != 0 && _groundDetector.IsOnGround);


        if (direction > 0 && !_lookToRight)
        {
            _lookToRight = !_lookToRight;
            _fliper.Flip(_lookToRight);
        }
        else if (direction < 0 && _lookToRight)
        {
            _lookToRight = !_lookToRight;
            _fliper.Flip(_lookToRight);
        }
    }

    private void Jump()
    {
        if (_inputReader.GetIsJump() && _groundDetector.IsOnGround)
        {
            _mover.Jump(_jumpForce);
            _heroAnimationHandler.AnimateJump(true);
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