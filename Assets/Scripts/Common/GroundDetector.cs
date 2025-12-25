using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _enemyLayer;

    private WaitForSeconds _wait;
    private float _groundCheckRadius = 0.5f;
    private float _timeDelay = 0.2f;
    private LayerMask _effectLayer;
        
    public bool IsOnGround { get; private set; }

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeDelay);
        _effectLayer = _groundLayer | _enemyLayer;
    }
    private void Start()
    {
        StartCoroutine(CheckGround());
    }

    private IEnumerator CheckGround()
    {
        while (enabled)
        {
            IsOnGround = Physics2D.OverlapCircle(transform.position, _groundCheckRadius, _effectLayer);
            yield return _wait;
        }
    }
}
