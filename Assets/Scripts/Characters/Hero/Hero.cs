using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(AudioSource), typeof(HeroAnimationHandler))]
[RequireComponent(typeof(Fliper), typeof(InputReader))]
public class Hero : MonoBehaviour, IDamageable
{
    [Header("Parameters")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip _stepSoundLeft;
    [SerializeField] private AudioClip _stepSoundRight;
    [SerializeField] private AudioClip _damageSound;
    
    [Header("External components")]
    [SerializeField] private Collector _collector;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private PunchEffect _punchEffect;
    [SerializeField] Health _health;
    
    private InputReader _inputReader;
    private Mover _mover;
    private Fliper _fliper;
    private HeroAnimationHandler _heroAnimationHandler;
    private SoundHandler _soundHandler;

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

    private void OnEnable()
    {
        _health.Died += OnDestroy;
        _collector.Taken += OnGetHealing;
    }

    private void OnDisable()
    {
        _health.Died -= OnDestroy;
        _collector.Taken -= OnGetHealing;
    }

    private void FixedUpdate()
    {
        HandleMove(_inputReader.Direction);

        TryToJump();

        HandleAttack();

        if (_inputReader.Direction != 0 && _groundDetector.IsOnGround && !_isSoundOn)
        {
            StartCoroutine(PlayStepSound());
        }
    }
    
    public void TakeDamage(float damage)
    {
        _soundHandler.PlaySound(_damageSound);
        _health.TakeDamage(damage);
    }

    private void HandleMove(float direction)
    {
        _mover.Move(direction, _speed);

        if (direction != 0 && _groundDetector.IsOnGround)
        {
            _heroAnimationHandler.AnimateWalkEnable();
        }
        else
        {
            _heroAnimationHandler.AnimateWalkDisable();
        }

        bool lookToRight = direction > 0;

        if (direction > 0 | direction < 0)
        {
            ActivateTurnAround(lookToRight);
        }
    }

    private void ActivateTurnAround(bool actualLook)
    {
        if (_fliper.ShouldFlip(actualLook))
        {
            _heroAnimationHandler.AnimateWalkDisable();
            _fliper.Flip(actualLook);
        }
    }

    private void TryToJump()
    {
        if (_inputReader.GetIsJump() && _groundDetector.IsOnGround)
        {
            _mover.Jump(_jumpForce);
            _heroAnimationHandler.AnimateJumpEnable();
        }
        else
        {
            _heroAnimationHandler.AnimateJumpDisable();
        }
    }

    private void HandleAttack()
    {
        if (_inputReader.GetAttack())
        {
            _heroAnimationHandler.AnimateAttackEnable();
            _attacker.Attack();
            _punchEffect.Create();
        }
        else
        {
            _heroAnimationHandler.AnimateAttackDisable();
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

    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }
    
    private void OnGetHealing(MedicalKit medicalKit)
    {
        _health.GetHealing(medicalKit);
    }
    
}