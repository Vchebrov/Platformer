using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoinCollector : MonoBehaviour
{
    public event Action<Transform> Touched;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Hero>(out _))
        {
            Touched?.Invoke(transform);
        }
    }
}