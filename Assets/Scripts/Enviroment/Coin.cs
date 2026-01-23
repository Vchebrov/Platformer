using System;

using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
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
