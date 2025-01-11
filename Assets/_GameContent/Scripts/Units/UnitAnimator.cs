using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform rotatedTransform;
    [SerializeField] private float walkAnimationThreshold;
    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    public void WalkAnimation()
    {
        animator.SetBool("IsWalk", true);
    }

    public void StopAnimation()
    {
        animator.SetBool("IsWalk", false);
    }


}
