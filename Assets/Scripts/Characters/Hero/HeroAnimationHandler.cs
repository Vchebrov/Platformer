using UnityEngine;

public class HeroAnimationHandler : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    
    [SerializeField] private Animator _animator;

    public void AnimateWalkEnable()
    {
        _animator.SetBool(WalkHash, true);
    }

    public void AnimateWalkDisable()
    {
        _animator.SetBool(WalkHash, false);
    }

    public void AnimateJumpEnable()
    {
        _animator.SetBool(JumpHash, true);
    }

    public void AnimateJumpDisable()
    {
        _animator.SetBool(JumpHash, false);
    }

    public void AnimateAttackEnable()
    {
        _animator.SetBool(AttackHash, true);
    }

    public void AnimateAttackDisable()
    {
        _animator.SetBool(AttackHash, false);
    }
}
