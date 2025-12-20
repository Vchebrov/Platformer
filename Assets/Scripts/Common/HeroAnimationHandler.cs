using UnityEngine;

public class HeroAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void AnimateWalk(int WalkHash, bool IsOnGround)
    {
        _animator.SetBool(WalkHash, IsOnGround);
    }

    public void AnimateJump(int JumpHash, bool IsOnGround)
    {
        _animator.SetBool(JumpHash, IsOnGround);
    }
}
