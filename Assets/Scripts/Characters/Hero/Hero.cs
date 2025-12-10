using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(AudioSource), typeof(Animator))]
[RequireComponent(typeof(Turnover), typeof(InputReader))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    
    [SerializeField] private AudioClip _stepSoundLeft;
    [SerializeField] private AudioClip _stepSoundRight;

    private InputReader _inputReader;
    private Mover _mover;
    private Turnover _turnover;
    private GroundDetector _groundDetector;
    private Animator _animator;
    private AudioSource _audioSource;

    private bool _lookToRight = true;
    private bool _isOnGround = true;
    private bool _isSoundOn = false;

    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private WaitForSeconds _stepDelay;
    private float _delay = 0.3f;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _audioSource = GetComponent<AudioSource>();
        _turnover = GetComponent<Turnover>();
        _animator = GetComponent<Animator>();
        _stepDelay = new WaitForSeconds(_delay);
        _inputReader = GetComponent<InputReader>();
        _groundDetector = GetComponentInChildren<GroundDetector>();
    }

    private void FixedUpdate()
    {
        if (_isOnGround)
        {
            _animator.SetBool(JumpHash, false);
        }

        _mover.Move(_inputReader.Direction, _speed);
        _animator.SetBool(WalkHash, (_inputReader.Direction != 0 && _groundDetector.IsOnGround));

        if (_inputReader.Direction > 0 && !_lookToRight)
        {
            _turnover.Flip(transform);
            _lookToRight = !_lookToRight;
        }
        else if (_inputReader.Direction < 0 && _lookToRight)
        {
            _turnover.Flip(transform);
            _lookToRight = !_lookToRight;
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsOnGround)
        {
            _mover.Jump(_jumpForce);
            _animator.SetBool(JumpHash, true);
        }

        if (_inputReader.Direction != 0 && _groundDetector.IsOnGround && !_isSoundOn)
        {
            StartCoroutine(PlayStepSound());
        }
    }

    private IEnumerator PlayStepSound()
    {
        _isSoundOn = true;

        while (_animator.GetBool(WalkHash) && _groundDetector.IsOnGround)
        {
            _audioSource.PlayOneShot(_stepSoundLeft);
            yield return _stepDelay;

            if (!_animator.GetBool(WalkHash) || !_groundDetector.IsOnGround)
                break;

            _audioSource.PlayOneShot(_stepSoundRight);

            yield return _stepDelay;
        }

        _isSoundOn = false;
    }
}