using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private PhysicMover mover;
    [SerializeField] private HealthBehaviour health;
    [SerializeField] private Detector detector;
    [SerializeField] private Attacker attacker;
    [SerializeField] private float moveForce;
    [SerializeField] private float attractForce;
    [SerializeField] private int fractionIndex;
    [SerializeField] private int enemyFraction;

    private HealthBehaviour currentTarget;
    public int FractionIndex => fractionIndex;
    public Squad CurrentSquad;

    private void Start()
    {
        health.SetIndex(enemyFraction);
        detector.OnNoUnitDetect += StopAttract;
        detector.OnUnitDetect += StartAttract;
        detector.SetIndex(fractionIndex);
    }
    private void OnDestroy()
    {
        detector.OnNoUnitDetect -= StopAttract;
        detector.OnUnitDetect -= StartAttract;
    }
    public void MoveTo(Vector2 targetPosition)
    {
        //Vector2 direction = targetPosition - new Vector2(transform.position.x, transform.position.y);
        //mover.MoveTo(direction, moveForce);
        mover.MoveToPosition(targetPosition, moveForce);
    }
    private Coroutine attractCoroutine;
    private void StartAttract(HealthBehaviour target)
    {
        currentTarget = target;
        attractCoroutine = StartCoroutine(Attract());
    }
    private void StopAttract(HealthBehaviour nullTarget)
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
                mover.MoveToPosition(currentTarget.transform.position, attractForce);
            yield return new WaitForFixedUpdate();
        }
    }
}