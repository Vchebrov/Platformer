using UnityEngine;

public class HeroAnimationHandler : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    
    [SerializeField] private Animator _animator;

    public void AnimateWalk(bool IsOnGround)
    {
        _animator.SetBool(WalkHash, IsOnGround);
    }

    public void AnimateJump(bool IsOnGround)
    {
        _animator.SetBool(JumpHash, IsOnGround);
    }
}
