using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public event Action SwordHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Hero hero))
            return;
        
        SwordHit?.Invoke();
    }
}