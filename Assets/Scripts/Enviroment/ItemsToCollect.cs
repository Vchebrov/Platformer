using System;
using UnityEngine;

namespace Enviroment
{
    public class ItemsToCollect: MonoBehaviour
    {
        public event Action<ItemsToCollect> OnCollected;

        public virtual void Collect()
        {
            OnCollected?.Invoke(this);
        }
    }
}