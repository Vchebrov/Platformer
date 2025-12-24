using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private ObstacleChecker _obstacleChecker;
    
    private bool _lookToRight = true;
    
    public Vector2 GetMovementDirection()
    {
        if (_obstacleChecker.CheckAhead(_lookToRight))
        {
            _lookToRight = !_lookToRight;
        }
        
        return _lookToRight ? Vector2.right : Vector2.left;
    }
}

