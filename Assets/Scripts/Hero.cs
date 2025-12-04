using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource), typeof(Animator))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private AudioClip _stepSoundLeft;
    [SerializeField] private AudioClip _stepSoundRight;

    private Rigidbody2D _rigidbody2d;
    private Animator _animator;
    private bool _lookToRight = true;
    private bool _isOnGround = true;
    private LayerMask _groundLayer;
    private string _groundTag = "Ground";
    private AudioSource _audioSource;
    private bool _isSoundOn = false;

    private WaitForSeconds _stepDelay;
    private float _delay = 0.3f;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.GetMask(_groundTag);
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _stepDelay = new WaitForSeconds(_delay);
    }

    private void Update()
    {
        if (_isOnGround)
        {
            _animator.SetBool("Jump", false);
        }
        
        var horizontalDirection = Input.GetAxis("Horizontal");
        _rigidbody2d.velocity = new Vector2(horizontalDirection * _speed, _rigidbody2d.velocity.y);

        _animator.SetBool("Walk", (horizontalDirection != 0 && _isOnGround));

        if (horizontalDirection > 0 && !_lookToRight)
        {
            Flip();
        }
        else if (horizontalDirection < 0 && _lookToRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && _isOnGround)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpForce);
            _animator.SetBool("Jump", true);
        }

        _isOnGround = Physics2D.OverlapCircle(_groundCheckPoint.transform.position, 0.5f, _groundLayer);

        if (horizontalDirection != 0 && _isOnGround && !_isSoundOn)
        {
            StartCoroutine(PlayStepSound());
        }
    }

    private IEnumerator PlayStepSound()
    {
        _isSoundOn = true;

        while (_animator.GetBool("Walk") && _isOnGround)
        {
            _audioSource.PlayOneShot(_stepSoundLeft);
            yield return _stepDelay;

            if (!_animator.GetBool("Walk") || !_isOnGround)
                break;

            _audioSource.PlayOneShot(_stepSoundRight);

            yield return _stepDelay;
        }

        _isSoundOn = false;
    }

    private void Flip()
    {
        _lookToRight = !_lookToRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}