using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void Jump(float jumpForce)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
    }

    public void Move(float direction, float speed)
    {
        _rigidbody.velocity = new Vector2(speed * direction, _rigidbody.velocity.y);
    }

    public void StopMovement()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }
}
