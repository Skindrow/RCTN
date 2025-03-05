using UnityEngine;
using UnityEngine.Events;


public class Attacker : MonoBehaviour
{
    [SerializeField] protected UnityEvent onAttack;
    [SerializeField] protected float animationAttackDelay;
    [SerializeField] protected float postAttackDelay;
    [SerializeField] private StatData statData;
    public delegate void AttackStateEvent();
    public AttackStateEvent OnAttackStart;
    public AttackStateEvent OnAttackDo;
    public AttackStateEvent OnAttackEnd;

    protected bool isAttacked = false;

    public bool IsAttacking => isAttacked;

    private void Start()
    {
        StatInitialize();
    }
    protected void StatInitialize()
    {
        if (statData != null)
        {
            postAttackDelay /= statData.GetStat();
        }
    }
    public virtual void Attack(HealthBehaviour target, int damage)
    {
        Debug.LogError("Attack not overrided in " + gameObject.name);
    }

}
