using UnityEngine;

public class Fliper : MonoBehaviour
{
    private Quaternion _faceRight;
    private Quaternion _faceLeft;
    private bool _lookToRight = true;

    private void Awake()
    {
        _faceRight = transform.rotation;
        _faceLeft = _faceRight * Quaternion.Euler(0, 180, 0);
    }

    public void Flip(bool faceRight)
    {
        if (faceRight != _lookToRight)
        {
            transform.rotation = faceRight ? _faceRight : _faceLeft;
            _lookToRight = !_lookToRight;
        }
    }

    public bool ShouldFlip(bool lookDirection)
    {
        return lookDirection != _lookToRight;
    }
}