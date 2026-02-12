using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHitPoints = 100f;

    private float _hitPoints;

    public float HitPoints => _hitPoints;
    public float MaxHitPoints => _maxHitPoints;

    public event Action<float> ValueChanged;
    public event Action<float> InitialValueSet;
    public event Action Died;

    private void Start()
    {
        _hitPoints = _maxHitPoints;
        InitialValueSet?.Invoke(_hitPoints);
    }

    private void Update()
    {
        if (_hitPoints == 0f)
        {
            Died?.Invoke();
        }
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

        ValueChanged?.Invoke(_hitPoints);
    }

    public void Heal(float amount)
    {
        _hitPoints += amount;

        if (_hitPoints >= _maxHitPoints)
        {
            _hitPoints = _maxHitPoints;
        }

        ValueChanged?.Invoke(_hitPoints);
    }
}