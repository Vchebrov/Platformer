using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _eyes;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _wallCheckDistance = 2f;
    [SerializeField] private LayerMask _groundLayer;
    
    private Rigidbody2D _rigidbody2D;
    private bool _lookToRight = true;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lookToRight = transform.localScale.x > 0;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        bool canMove = CheckWallAhead();

        if (canMove)
        {
            _rigidbody2D.velocity = new Vector2(_speed * (_lookToRight ? 1 : -1), _rigidbody2D.velocity.y);
            _animator.SetBool("Walk", true);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);

            Flip();
        }
    }

    private bool CheckWallAhead()
    {
        Vector2 rayDirection = (_lookToRight ? Vector2.right : Vector2.left);
        rayDirection.Normalize();
        Vector2 raySource = _eyes.position;

        RaycastHit2D hit = Physics2D.Raycast(raySource,
            Vector2.down, _wallCheckDistance, _groundLayer);

        Debug.DrawRay(raySource,
            rayDirection * _wallCheckDistance,
            hit.collider != null ? Color.green : Color.blue);

        return hit.collider != null;
    }

    private void Flip()
    {
        _lookToRight = !_lookToRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

   
