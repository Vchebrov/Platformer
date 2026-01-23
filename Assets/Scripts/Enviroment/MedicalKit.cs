using System;
using UnityEngine;

public class MedicalKit : MonoBehaviour, ICollectible
{
    [SerializeField] private float _healingValue = 10f;
    
    public float HealingValue => _healingValue;
    
    public event Action<ICollectible> Collected;
    
    public void Collect()
    {
        Collected?.Invoke(this);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
