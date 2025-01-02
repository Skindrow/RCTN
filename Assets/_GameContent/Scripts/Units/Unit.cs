using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private DeadUnit deadUnit;
    [SerializeField] private PhysicMover mover;
    [SerializeField] private HealthBehaviour health;
    [SerializeField] private Attacker attacker;
    [SerializeField] private float moveForce;
    [SerializeField] private float attractForce;
    [SerializeField] private int fractionIndex;
    [SerializeField] private int damage;
    [SerializeField] private float detectRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackPerSecond;
    [SerializeField] private UnityEvent onWalk;
    [SerializeField] private UnityEvent onPlayerUnit;
    [Header("Animation")]
    [SerializeField] private Transform rotatedTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private string attackAnimationName;
    [SerializeField] private string walkAnimationName;
    [SerializeField] private string idleAnimationName;

    public DeadUnit DeadUnit => deadUnit;
    private float detectTick = 0.1f;
    private HealthBehaviour currentTarget;
    public int FractionIndex => fractionIndex;
    public Squad CurrentSquad;

    private const int PLAYER_INDEX = 0;

    private void Start()
    {
        StartCoroutine(TargetFindFlow());
        health.OnHealthEnd += OnDeath;
        if (fractionIndex == PLAYER_INDEX)
            onPlayerUnit?.Invoke();
    }
    private void OnDestroy()
    {
        health.OnHealthEnd -= OnDeath;
    }
    private void OnDeath(int forDelegate)
    {
        CurrentSquad.RemoveUnit(this);
    }
    public void SetFraction(int fraction)
    {
        fractionIndex = fraction;
        if (fractionIndex == PLAYER_INDEX)
            onPlayerUnit?.Invoke();
    }
    private IEnumerator TargetFindFlow()
    {
        while (true)
        {
            HealthBehaviour unitHealth = TargetFinder.GetTargetInRadius(fractionIndex, transform, detectRadius, true);
            if (unitHealth != null)
            {
                StartAttract(unitHealth);
            }
            else
            {
                StopAttract();
            }
            if (attractCoroutine != null)
            {
                float currentTargetDistance = Vector2.Distance(transform.position, currentTarget.transform.position);
                if (currentTargetDistance <= attackRadius)
                {
                    attacker.Attack(currentTarget, damage);
                    yield return new WaitForSeconds(1.0f / attackPerSecond);
                }
            }
            yield return new WaitForSeconds(detectTick);
        }
    }
    public delegate void UnitDetectEvent(HealthBehaviour unitHealth);
    public UnitDetectEvent OnUnitDetect;
    public UnitDetectEvent OnNoUnitDetect;
    public void MoveTo(Vector2 targetPosition)
    {
        if (rotatedTransform != null)
        {
            if (targetPosition.x > transform.position.x)
                rotatedTransform.eulerAngles = new Vector3(0, 0, 0);
            else if (targetPosition.x < transform.position.x)
                rotatedTransform.eulerAngles = new Vector3(0, -180, 0);
        }
        mover.MoveToPosition(targetPosition, moveForce);

        onWalk?.Invoke();
    }
    private Coroutine attractCoroutine;
    private void StartAttract(HealthBehaviour target)
    {
        StopAttract();
        currentTarget = target;
        attractCoroutine = StartCoroutine(Attract());
    }
    private void StopAttract()
    {
        if (attractCoroutine != null)
        {
            StopCoroutine(attractCoroutine);
            attractCoroutine = null;
        }
        currentTarget = null;
    }
    private IEnumerator Attract()
    {
        while (true)
        {
            if (currentTarget != null)
                MoveTo(currentTarget.transform.position);
            yield return new WaitForFixedUpdate();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}