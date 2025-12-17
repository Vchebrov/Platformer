using UnityEngine;

public class Patrol : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction, float speed)
    {
        _rigidbody.velocity = new Vector2(speed * direction, _rigidbody.velocity.y);
    }
}
