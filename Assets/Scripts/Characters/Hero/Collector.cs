using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    public event Action<Transform> Touched;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_targetLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Touched?.Invoke(other.transform);
        }
    }
}