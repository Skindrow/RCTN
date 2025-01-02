using UnityEngine;
using UnityEngine.Events;


public class Attacker : MonoBehaviour
{
    [SerializeField] protected UnityEvent onAttack;

    public delegate void AttackStateEvent();
    public AttackStateEvent OnAttackStart;
    public AttackStateEvent OnAttackGo;

    public virtual void Attack(HealthBehaviour target , int damage)
    {
        Debug.LogError("Attack not overrided in " + gameObject.name);
    }

}
