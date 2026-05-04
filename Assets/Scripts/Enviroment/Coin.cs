using System;
using Enviroment;
using UnityEngine;

public class Coin : ItemsToCollect
{
    public override void Collect()
    {
        Debug.Log("Coin collected");
        base.Collect();
    }
}
