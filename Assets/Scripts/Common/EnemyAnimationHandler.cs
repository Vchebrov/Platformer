using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    
    [SerializeField] private Animator _animator;

    public void AnimateWalk(bool IsOnGround)
    {
        _animator.SetBool(WalkHash, IsOnGround);
    }
}
