using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Transform _target;
    [SerializeField, Min(0)] private float _range = 4;
   
    public static float SqrDistance(Vector2 start, Vector2 end)
    {
        return (end - start).sqrMagnitude;
    }

    public static bool IsEnoughClose(Vector2 start, Vector2 end, float distance)
    {
        return SqrDistance(start, end) <= distance * distance;
    }

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
        return IsEnoughClose(transform.position, _target.position, _range);
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
        return SqrDistance(transform.position, _target.position);
    }
}