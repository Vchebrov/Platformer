// using System;
// using UnityEngine;
//
// public class Health : MonoBehaviour
// {
//     [SerializeField] private int _maxHealth = 100;
//
//     public int Amount { get; private set; }
//
//     public event Action Ended;
//     public event Action<int> Changed;
//
//     private void Awake()
//     {
//         Amount = _maxHealth;
//     }
//
//     public void Add(int amount)
//     {
//         if (amount <= 0)
//             return;
//
//         if (amount + Amount > _maxHealth)
//             amount = _maxHealth - Amount;
//
//         Amount += amount;
//         Changed?.Invoke(amount);
//     }
//
//     public void Take(int amount)
//     {
//         if (amount <= 0)
//             return;
//
//         if (amount > Amount)
//             amount = Amount;
//
//         Amount -= amount;
//         Changed?.Invoke(amount);
//
//         if (Amount == 0)
//             Ended?.Invoke();
//     }
// }
// public class Player : MonoBehaviour
// {
//     private InputService _inputService;
//     private PlayerMover _mover;
//     private CharacterAnimator _characterAnimator;
//     private GroundDetector _groundDetector;
//     private Collector _collector;
//     private Wallet _wallet;
//     private PlayerAttacker _attacker;
//     private Rotator _rotator;
//     private Health _health;
//     private Pusher _pusher;
//
//     private void Awake()
//     {
//         _inputService = GetComponent<InputService>();
//         _mover = GetComponent<PlayerMover>();
//         _characterAnimator = GetComponent<CharacterAnimator>();
//         _groundDetector = GetComponent<GroundDetector>();
//         _collector = GetComponent<Collector>();
//         _wallet = GetComponent<Wallet>();
//         _attacker = GetComponent<PlayerAttacker>();
//         _rotator = GetComponent<Rotator>();
//         _health = GetComponent<Health>();
//         _pusher = GetComponent<Pusher>();
//     }
//
//     private void OnEnable()
//     {
//         _collector.PickedUp += OnCollect;
//         _health.Ended += OnHealthEnded;
//     }
//
//     private void OnDisable()
//     {
//         _collector.PickedUp -= OnCollect;
//         _health.Ended -= OnHealthEnded;
//     }
//
//     private void FixedUpdate()
//     {
//         UpdateMoving();
//         UpdateJumping();
//     }
//
//     private void Update()
//     {
//         UpdateAttacking();
//     }
//
//     public void TakeDamage(int amount)
//     {
//         _health.Take(amount);
//     }
//
//     public void Push(Vector2 direction, float power)
//     {
//         _pusher.Push(direction, power);
//     }
//
//     private void UpdateMoving()
//     {
//         if (_inputService.HorizontalAxis != 0)
//         {
//             _mover.Move(_inputService.HorizontalAxis, _groundDetector.IsGrounded());
//             _rotator.UpdateFacing(_inputService.HorizontalAxis);
//
//             if (_characterAnimator.IsWalkingAnim == false)
//             {
//                 _characterAnimator.SetWalkingAnimation();
//             }
//         }
//         else if (_characterAnimator.IsWalkingAnim)
//         {
//             _characterAnimator.SetIdleAnimation();
//         }
//     }
//
//     private void UpdateJumping()
//     {
//         if(_inputService.CanJump && _groundDetector.IsGrounded())
//             _mover.JumpUp();
//     }
//
//     private void UpdateAttacking()
//     {
//         if(_inputService.CanAttack)
//             _attacker.Shoot();
//     }
//
//     private void OnCollect(Pickupable pickupable)
//     {
//         if (pickupable.TryGetComponent(out Coin coin))
//             _wallet.AddCoins(coin.Price);
//
//         if (pickupable.TryGetComponent(out Medicine medicine))
//             _health.Add(medicine.HealthAmount);
//
//         pickupable.Collect();
//     }
//
//     private void OnHealthEnded()
//     {
//         Die();
//     }
//
//     private void Die()
//     {
//         gameObject.SetActive(false);
//     }
// }