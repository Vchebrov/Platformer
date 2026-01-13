using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private SoundHandler _soundHandler;
    [SerializeField] private Health _health;
    [SerializeField] private float _maxHealth = 100f;
    
    public event Action<MedicalKit> Taken;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _soundHandler.CollectCoin();
            coin.Collect();
        }
        else if (collision.gameObject.TryGetComponent(out MedicalKit medKit) && _health.HitPoints != _maxHealth)
        {
            medKit.Collect();
            Taken?.Invoke(medKit);
        }
    }
}