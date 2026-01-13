using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int DetectHash = Animator.StringToHash("TargetFound");
    private static readonly int RunHash = Animator.StringToHash("Run");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int DeathHash = Animator.StringToHash("Death");
    
    [SerializeField] private Animator _animator;

    public void AnimateWalkEnable()
    {
        _animator.SetBool(WalkHash, true);
    }

    public void AnimateWalkDisable()
    {
        _animator.SetBool(WalkHash, false);
    }

    public void AnimateDetectEnable()
    {
        _animator.SetBool(DetectHash, true);
    }

    public void AnimateDetectDisable()
    {
        _animator.SetBool(DetectHash, false);
    }

    public void AnimateRunEnable()
    {
        _animator.SetBool(RunHash, true);
    }

    public void AnimateRunDisable()
    {
        _animator.SetBool(RunHash, false);
    }

    public void AnimateAttackEnable()
    {
        _animator.SetBool(AttackHash, true);
    }

    public void AnimateAttackDisable()
    {
        _animator.SetBool(AttackHash, false);
    }

    public void AnimateDeathEnable()
    {
        _animator.SetBool(DeathHash, true);
    }

    public void AnimateDeathDisable()
    {
        _animator.SetBool(DeathHash, false);
    }

    public void DisableAllAnimations()
    {
        _animator.SetBool(WalkHash, false);
        _animator.SetBool(DetectHash, false);
        _animator.SetBool(RunHash, false);
        _animator.SetBool(AttackHash, false);
    }
}