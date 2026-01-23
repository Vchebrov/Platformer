using System;
using UnityEngine;

public interface ICollectible
{
    public event Action<ICollectible> Collected;
    
    public void Collect();

    public Transform GetTransform();
}