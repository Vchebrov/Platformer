using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private TargetDetector _detector;
    [SerializeField] private float _attackDamage;

    public void Attack()
    {
        IDamageable target = _detector.DefineTarget();
        if (target != null)
        {
            target.TakeDamage(_attackDamage);
        }
    }
}