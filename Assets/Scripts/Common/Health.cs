using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Collector _collector;

    private float _health;
    
    public float HitPoints => _health;
    
    public event Action<float> HealthChanged;
    public event Action<float> InitialHealthSet;
    public event Action Died;

    private void Awake()
    {
        _health = _maxHealth;
        InitialHealthSet?.Invoke(_health);
    }

    private void OnEnable()
    {
        SubscribeToCollector();
    }

    private void OnDisable()
    {
        UnsubscribeFromCollector();
    }

    private void Update()
    {
        if (_health <= 0)
        {
            _health = 0;
            Died?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        HealthChanged?.Invoke(_health);
    }

    private void SubscribeToCollector()
    {
        if (_collector != null)
        {
            _collector.Taken += OnGetHealing;
        }
    }
    
    private void UnsubscribeFromCollector()
    {
        if (_collector != null)
        {
            _collector.Taken -= OnGetHealing;
        }
    }

    private void OnGetHealing(MedicalKit medicalKit)
    {
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health += medicalKit.HealingValue;
            HealthChanged?.Invoke(_health);
        }
    }
}