using UnityEngine;

public class AnimationHandler : MonoBehaviour
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
