using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private float _wallCheckDistance = 1.5f;
    
    public bool CheckAhead(bool lookSide)
    {
        Vector2 rayDirection = (lookSide ? Vector2.right : Vector2.left);
        rayDirection.Normalize();
        Vector2 raySource = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(raySource,
            Vector2.down, _wallCheckDistance, _obstacleLayer);

        Debug.DrawRay(raySource,
            rayDirection * _wallCheckDistance,
            hit.collider != null ? Color.green : Color.blue);

        return hit.collider != null;
    }
}
