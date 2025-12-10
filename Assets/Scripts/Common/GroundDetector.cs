using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    public bool IsOnGround { get; private set; }

    private void Update()
    {
        IsOnGround = Physics2D.OverlapCircle(transform.position, 0.5f, _groundLayer);
    }
}
