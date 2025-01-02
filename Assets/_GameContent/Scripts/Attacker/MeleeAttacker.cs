using System.Collections;
using UnityEngine;

public class MeleeAttacker : Attacker
{
    [SerializeField] private float animationAttackDelay;
    public override void Attack(HealthBehaviour target, int damage)
    {
        StartCoroutine(AttackDelay(target, damage));
    }
    private bool isAttacked = false;
    private IEnumerator AttackDelay(HealthBehaviour target, int damage)
    {
        if (!isAttacked)
        {
            OnAttackStart?.Invoke();
            onAttack?.Invoke();
            isAttacked = true;
            yield return new WaitForSeconds(animationAttackDelay);
            OnAttackGo?.Invoke();
            if (target != null)
                target.GetDamage(damage);

            isAttacked = false;
        }
    }
}
