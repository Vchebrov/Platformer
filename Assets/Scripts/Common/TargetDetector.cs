using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Transform _target;
    [SerializeField] private float _stopDistance = 10f;
    [SerializeField, Min(0)] private float _range = 2;

    public Transform GetTarget()
    {
        if (CanChase())
        {
            return _target;
        }

        return null;
    }

    public bool CanChase()
    {
        return Vector2.Distance(_target.position, transform.position) < _stopDistance;
    }

    public IDamageable DefineTarget()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, _range, _targetMask);

        if (target != null && target.gameObject.TryGetComponent(out IDamageable damageable))
        {
            return damageable;
        }

        return null;
    }

    public float GetDistance()
    {
        return Vector2.Distance(transform.position, _target.position);
    }
}

