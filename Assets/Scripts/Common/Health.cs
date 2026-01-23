using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHitPoints = 100f;
    [SerializeField] private Collector _collector;

    private float _hitPoints;
    
    public float HitPoints => _hitPoints;
    public float MaxHitPoints => _maxHitPoints;
    
    public event Action<float> HealthChanged;
    public event Action<float> InitialHealthSet;
    public event Action Died;

    private void Start()
    {
        _hitPoints = _maxHitPoints;
        InitialHealthSet?.Invoke(_hitPoints);
    }

    private void OnEnable()
    {
        SubscribeToCollector();
    }

    private void OnDisable()
    {
        UnsubscribeFromCollector();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0f)
        {
            Debug.Log("Урон не может быть отрицательным. Текущее значение: " + damage);
            return;
        }
       
        _hitPoints -= damage;
        
        if (_hitPoints < 0f)
        {
            _hitPoints = 0f;
        }
        
        HealthChanged?.Invoke(_hitPoints);
        
        if (_hitPoints == 0f)
        {
            Died?.Invoke();
        }
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
        _hitPoints += medicalKit.HealingValue;
        
        if (_hitPoints >= _maxHitPoints)
        {
            _hitPoints = _maxHitPoints;
        }
        
            HealthChanged?.Invoke(_hitPoints);
    }
}