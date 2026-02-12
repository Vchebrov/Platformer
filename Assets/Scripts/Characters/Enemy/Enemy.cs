using System.Collections;
using UnityEngine;

[RequireComponent( typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Parameters")]
    [SerializeField] private float _deathAnimationDelay = 2f;

    [Header("External components")]
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }
    
    private void OnDied()
    {
        StartCoroutine(DelayedDestroy());
    }
    
    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(_deathAnimationDelay);
        Destroy(gameObject);
    }
}
