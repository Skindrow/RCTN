using System.Collections;
using UnityEngine;

public class RangeAttacker : Attacker
{
    [SerializeField] private float animationAttackDelay;
    [SerializeField] private float postAttackDelay;
    [SerializeField] private Transform shootingTransform;
    [SerializeField] private Projectile projectile;
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
            {
                Projectile projectileGO = Instantiate(projectile, shootingTransform.position, Quaternion.identity);
                Vector2 direction = target.transform.position - transform.position;
                projectileGO.SetProjectile(direction.normalized, damage, target.Index);
            }
            yield return new WaitForSeconds(postAttackDelay);

            OnAttackEnd?.Invoke();
            isAttacked = false;
        }
    }
}
