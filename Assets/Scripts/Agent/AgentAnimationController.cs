using UnityEngine;

public enum AnimationState
{
    Idle,
    Run,
    Attack,
    Death
}

public class AgentAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Init(AnimatorOverrideController overrideController)
    {
        _animator.runtimeAnimatorController = overrideController;
    }

    public void SetState(AnimationState state)
    {
        switch (state)
        {
            case AnimationState.Idle:
                idle();
                break;
            case AnimationState.Run:
                Run();
                break;
            case AnimationState.Attack:
                Attack();
                break;
            case AnimationState.Death:
                Death();
                break;
            default:
                break;
        }
    }

    private void idle()
    {
        _animator.SetBool("IsIdle", true);
        _animator.SetBool("IsMove", false);
    }

    private void Run()
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsMove", true);
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    private void Death()
    {
        _animator.SetTrigger("Death");
    }
}
