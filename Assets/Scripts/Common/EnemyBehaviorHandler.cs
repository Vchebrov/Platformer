using UnityEngine;

public class EnemyBehaviorHandler : MonoBehaviour
{
    [SerializeField] private ObstacleChecker _obstacleChecker;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private float _attackRange = 2f;
    private bool _lookToRight = true;

    public Vector2 GetMovementDirection()
    {
        if (_obstacleChecker.CheckAhead(_lookToRight))
        {
            _lookToRight = !_lookToRight;
        }
    
        return _lookToRight ? Vector2.right : Vector2.left;
    }

    public Transform GetTargetPosition()
    {
        return _targetDetector.GetTarget();
    }

    public bool CanAttack()
    {
        return _targetDetector.GetDistance() <= _attackRange;
    }
}