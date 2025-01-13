using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private DeadUnit deadUnit;
    [SerializeField] private PhysicMover mover;
    [SerializeField] private HealthBehaviour health;
    [SerializeField] private Attacker attacker;
    [SerializeField] private float startMoveForce;
    [SerializeField] private float attackMoveMultiplier;
    [SerializeField] private float attractMultiplier;
    [SerializeField] private int fractionIndex;
    [SerializeField] private int damage;
    [SerializeField] private float detectRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackPerSecond;
    [SerializeField] private UnityEvent onWalk;
    [SerializeField] private UnityEvent onStop;
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

    private float moveForce;
    private const int PLAYER_INDEX = 0;

    private void Start()
    {
        moveForce = startMoveForce;
        StartCoroutine(TargetFindFlow());
        health.OnHealthEnd += OnDeath;
        if (fractionIndex == PLAYER_INDEX)
            onPlayerUnit?.Invoke();

        attacker.OnAttackStart += MoveDescreaseOnAttack;
        attacker.OnAttackEnd += MoveSetStandart;
    }
    private void OnDestroy()
    {
        health.OnHealthEnd -= OnDeath;


        attacker.OnAttackStart -= MoveDescreaseOnAttack;
        attacker.OnAttackEnd -= MoveSetStandart;
    }
    private void OnDeath(int forDelegate)
    {
        CurrentSquad.RemoveUnit(this);
    }
    public void SetFraction(int fraction)
    {
        fractionIndex = fraction;
        health.SetIndex(fraction);
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
                OnUnitDetect?.Invoke(unitHealth);
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
                    OnUnitDetect?.Invoke(unitHealth);

                    while (attacker.IsAttacking)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                }
            }
            yield return new WaitForSeconds(detectTick);
        }
    }
    public delegate void UnitDetectEvent(HealthBehaviour unitHealth);
    public UnitDetectEvent OnUnitDetect;
    public void MoveTo(Vector2 targetPosition)
    {
        RestartStopWaiter();
        mover.MoveToPosition(targetPosition, moveForce);

        Rotation(targetPosition);
        onWalk?.Invoke();
    }
    public void MoveTo(Vector2 targetPosition, float multiplier)
    {
        RestartStopWaiter();
        mover.MoveToPosition(targetPosition, moveForce * multiplier);

        Rotation(targetPosition);
        onWalk?.Invoke();
    }
    public void Attract(Vector2 targetPosition, float multiplier)
    {
        mover.MoveToPosition(targetPosition, moveForce * multiplier);

    }
    private Coroutine stopCoroutine;
    private void RestartStopWaiter()
    {
        if (stopCoroutine != null)
        {
            StopCoroutine(stopCoroutine);
        }
        stopCoroutine = StartCoroutine(OnStopTimer());
    }
    private IEnumerator OnStopTimer()
    {
        yield return new WaitForSeconds(0.1f);
        onStop?.Invoke();
    }
    private void MoveDescreaseOnAttack()
    {
        moveForce *= attackMoveMultiplier;
    }
    private void MoveSetStandart()
    {
        moveForce = startMoveForce;
    }
    #region Attract
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
    #endregion
    private IEnumerator Attract()
    {
        while (true)
        {
            if (currentTarget != null)
                Attract(currentTarget.transform.position, attractMultiplier);
            yield return new WaitForFixedUpdate();
        }
    }

    private void Rotation(Vector2 targetPosition)
    {
        if (rotatedTransform != null)
        {
            if (targetPosition.x > transform.position.x)
                rotatedTransform.eulerAngles = new Vector3(0, 0, 0);
            else if (targetPosition.x < transform.position.x)
                rotatedTransform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}