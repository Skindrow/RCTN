using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Squad : MonoBehaviour
{
    [SerializeField] private int squadFraction;
    [SerializeField] private List<Unit> startSquadMemders = new List<Unit>();
    [SerializeField] private bool isDeadUnitLeave;
    [SerializeField] private bool isCanRevive;
    private List<Unit> squadMembers = new List<Unit>();
    private List<DeadUnit> deadUnits = new List<DeadUnit>();

    private float leftBehindDistantion = 3f;

    private void Start()
    {
        InitializeStartUnits();
        if (isCanRevive)
        {
            OnSquadDead += ReviveSquad;
        }
    }
    private void OnDestroy()
    {
        OnSquadDead -= ReviveSquad;
    }
    public delegate void SquadAttackEvent(HealthBehaviour unit);
    public SquadAttackEvent OnSquadAttack;
    private void SquadAttackTrigger(HealthBehaviour unit)
    {
        OnSquadAttack?.Invoke(unit);
    }
    private void ReviveSquad(List<DeadUnit> squadDeadUnits)
    {
        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            Unit reviveUnit = squadDeadUnits[i].ReviveUnit();
            AddUnit(reviveUnit);
        }
        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            squadDeadUnits[i].BodyDestroy();
        }
    }
    private void InitializeStartUnits()
    {
        for (int i = 0; i < startSquadMemders.Count; i++)
        {
            AddUnit(startSquadMemders[i]);
        }
    }
    public void AddUnit(Unit unit)
    {
        if (!squadMembers.Contains(unit))
        {
            squadMembers.Add(unit);
        }
        unit.OnUnitDetect += SquadAttackTrigger;
        unit.CurrentSquad = this;
        unit.SetFraction(squadFraction);
    }
    public delegate void SquadDead(List<DeadUnit> deadSquadList);
    public static SquadDead OnSquadDead;
    public void RemoveUnit(Unit unit)
    {
        if (squadMembers.Contains(unit))
        {
            if (isDeadUnitLeave)
            {
                LeaveDeadUnit(unit);
            }

            unit.OnUnitDetect -= SquadAttackTrigger;
            squadMembers.Remove(unit);
        }
        if (isDeadUnitLeave && squadMembers.Count <= 0)
        {
            for (int i = 0; i < deadUnits.Count; i++)
            {
                deadUnits[i].IsCanRevive = true;
            }
            OnSquadDead?.Invoke(deadUnits);
        }
    }
    private void LeaveDeadUnit(Unit unit)
    {
        DeadUnit deadUnit = Instantiate(unit.DeadUnit, unit.transform.position, Quaternion.identity);
        deadUnits.Add(deadUnit);
    }
    public void SquadMove(Vector2 targetPosition)
    {
        Vector3 squadCenter = CenterOfSquad();
        for (int i = 0; i < squadMembers.Count; i++)
        {
            squadMembers[i].MoveTo(targetPosition);
        }
        for (int i = 0; i < squadMembers.Count; i++)
        {
            squadMembers[i].MoveToWithoutRotation(squadCenter, 0.3f);
        }
    }
    public Vector3 CenterOfSquad()
    {
        Vector3 averageVector = Vector3.zero;
        for (int i = 0; i < squadMembers.Count; i++)
        {
            averageVector += squadMembers[i].transform.position;
        }
        averageVector /= squadMembers.Count;
        return averageVector;
    }
    public Unit MostCloseUnit(Vector2 position)
    {
        float distance = float.MaxValue;
        Unit unit = null;
        for (int i = 0; i < squadMembers.Count; i++)
        {
            float distanceToUnit = Vector2.Distance(squadMembers[i].transform.position, position);
            if (distanceToUnit < distance)
            {
                unit = squadMembers[i];
            }
        }
        return unit;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(CenterOfSquad(), 0.3f);
    }

}
