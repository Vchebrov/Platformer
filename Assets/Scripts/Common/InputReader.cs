using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;
    private bool _isAttack;
    private KeyCode _jumpKeyW = KeyCode.W;
    private KeyCode _attackKeySpace = KeyCode.Space;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(_jumpKeyW))
            _isJump = true;

        if (Input.GetKeyDown(_attackKeySpace))
        {
            _isAttack =  true;
        }
    }

    public bool GetIsJump() => GetBoolJump(ref _isJump);

    public bool GetAttack() => GetBoolAttack(ref _isAttack);
    
    private bool GetBoolAttack(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
    
    private bool GetBoolJump(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
