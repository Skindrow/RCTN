using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
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
