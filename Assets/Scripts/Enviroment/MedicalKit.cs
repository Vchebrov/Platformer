using System;
using Enviroment;
using UnityEngine;

public class MedicalKit : ItemsToCollect
{
    [SerializeField] private float _healingValue = 10f;
    
    public float HealingValue => _healingValue;
    
    public override void Collect()
    {
        Debug.Log("MedKit: " + _healingValue);
        base.Collect();
    }
}
