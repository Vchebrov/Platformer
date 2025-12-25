using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    
    [SerializeField] private Animator _animator;

    public void AnimateWalkEnable()
    {
        _animator.SetBool(WalkHash, true);
    }

    public void AnimateWalkDisable()
    {
        _animator.SetBool(WalkHash, false);
    }
}
