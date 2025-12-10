using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    // [SerializeField] private LayerMask _targetLayer;
    
    private int _targetLayer;
    public event Action<Transform> Touched;
    private string _targetLayerName = "Coin";

    private void Awake()
    {
        _targetLayer = LayerMask.NameToLayer(_targetLayerName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.layer == _targetLayer)
        {
            Debug.Log("Coin touched");
            Touched?.Invoke(other.transform);
        }
    }
}