using System;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    public event Action<MedicalKit> Collected;
    
    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
