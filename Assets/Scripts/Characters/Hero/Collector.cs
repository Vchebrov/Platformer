using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private SoundHandler _soundHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _soundHandler.CollectCoin();
            coin.Collect();
        }
    }
}