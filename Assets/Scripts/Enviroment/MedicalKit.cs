using System;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    [SerializeField] private float _healingValue = 10f;
    
    public float HealingValue => _healingValue;
    
    public event Action<MedicalKit> Collected;
    
    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
