using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string attackAnimationName;
    [SerializeField] private string idleAnimationName;
    [SerializeField] private string walkAnimationName;


    public void AttackAnimation()
    {
        animator.Play(attackAnimationName);
    }

}
