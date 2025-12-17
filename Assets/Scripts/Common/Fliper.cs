using UnityEngine;

public class Fliper : MonoBehaviour
{
    private Quaternion _faceRight;
    private Quaternion _faceLeft;
    
    private void Awake()
    {
        _faceRight = transform.rotation;
        _faceLeft = _faceRight * Quaternion.Euler(0, 180, 0);
    }
    
    public void Flip(bool faceRight)
    {
        transform.rotation = faceRight ? _faceRight : _faceLeft;
    }
}