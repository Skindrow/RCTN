using System.Collections;
using UnityEngine;

public class MeleeAttacker : Attacker
{
    [SerializeField] private float animationAttackDelay;
    [SerializeField] private float postAttackDelay;
    public override void Attack(HealthBehaviour target, int damage)
    {
        StartCoroutine(AttackDelay(target, damage));
    }
    private IEnumerator AttackDelay(HealthBehaviour target, int damage)
    {
        if (!isAttacked)
        {
            OnAttackStart?.Invoke();
            onAttack?.Invoke();
            isAttacked = true;
            yield return new WaitForSeconds(animationAttackDelay);
            OnAttackDo?.Invoke();
            if (target != null)
                target.GetDamage(damage);
            yield return new WaitForSeconds(postAttackDelay);

            OnAttackEnd?.Invoke();
            isAttacked = false;
        }
    }
}
