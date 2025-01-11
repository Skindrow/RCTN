using System.Collections;
using UnityEngine;

public class AIControls : MonoBehaviour
{
    private Squad squad;
    [SerializeField] private Vector2 maxAIZone;
    [SerializeField] private Vector2 minAIZone;
    [SerializeField] private float minForvardTime;
    [SerializeField] private float maxForvardTime;
    [SerializeField] private float attackTime;
    [SerializeField] private float timeAfterAttack;


    private bool isAttacked = false;
    private void Start()
    {
        squad = GetComponent<Squad>();
        StartMove();
        squad.OnSquadAttack += StartAttack;
    }
    private void OnDestroy()
    {
        squad.OnSquadAttack -= StartAttack;
    }
    private void StartAttack(HealthBehaviour unit)
    {
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
        }
        actionCoroutine = StartCoroutine(AttackCoroutine(unit));
    }

    private void StartMove()
    {
        Vector2 rndPoint = GetPointInAIZone();
        float rndTime = Random.Range(minForvardTime, maxForvardTime);
        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
        }
        actionCoroutine = StartCoroutine(MoveFlow(rndPoint, rndTime));
    }
    private Coroutine actionCoroutine;
    private IEnumerator MoveFlow(Vector2 pos, float time)
    {
        float estimatedTime = Time.time + time;
        while (estimatedTime > Time.time)
        {
            if (Vector2.Distance(squad.CenterOfSquad(), pos) > 1f)
                squad.SquadMove(pos);
            yield return new WaitForFixedUpdate();
        }
        StartMove();
    }
    private IEnumerator AttackCoroutine(HealthBehaviour unit)
    {
        isAttacked = true;
        float estimatedTime = Time.time + attackTime;
        Vector2 centerOfSquad = unit.GetComponent<Unit>().CurrentSquad.CenterOfSquad();
        while (estimatedTime > Time.time)
        {
            squad.SquadMove(centerOfSquad);

            yield return new WaitForFixedUpdate();
        }
        isAttacked = false;

        yield return new WaitForSeconds(timeAfterAttack);
        StartMove();
    }
    private Vector2 GetPointInAIZone()
    {
        float x = Random.Range(minAIZone.x, maxAIZone.x);
        float y = Random.Range(minAIZone.y, maxAIZone.y);

        return new Vector2(x, y);
    }
}
