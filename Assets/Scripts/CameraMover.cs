using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _heroReference;
    [SerializeField] private float _smoothing = 0.3f;

    private Vector3 _velocity = Vector3.zero;
    private float _xPositionMin = -31.1f;
    private float _xPositionMax = -0.2f;
    private float _yFixedPosition = 0.9f;
    private float _cameraXPosition;

    private void LateUpdate()
    {
        if (_heroReference.position.x > _xPositionMax)
        {
            _cameraXPosition = _xPositionMax;
        }
        else if (_heroReference.position.x < _xPositionMin)
        {
            _cameraXPosition = _xPositionMin;
        }
        else
        {
            _cameraXPosition = _heroReference.position.x;
        }
        
        Vector3 targetPosition =
            new Vector3(_cameraXPosition, _yFixedPosition, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothing);
    }

  
}