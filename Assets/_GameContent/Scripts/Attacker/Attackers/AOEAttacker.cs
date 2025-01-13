using System.Collections;
using UnityEngine;

public class AOEAttacker : Attacker
{
    [SerializeField] private ContactDamage aoeDamagerPref;
    [SerializeField] private GameObject aoeFX;
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
            if(target != null)
            {
                ContactDamage aoeGO = Instantiate(aoeDamagerPref, transform.position, Quaternion.identity);
                aoeGO.SetLayer(target.Index);
                aoeGO.SetDamage(damage);
                Instantiate(aoeFX, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(postAttackDelay);

            OnAttackEnd?.Invoke();
            isAttacked = false;
        }
    }
}
