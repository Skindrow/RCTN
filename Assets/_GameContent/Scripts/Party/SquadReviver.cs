using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SquadReviver : MonoBehaviour
{
    [SerializeField] private GameObject fxRevive;
    [SerializeField] private GameObject fxAppear;
    [SerializeField] private StatData doubleReviveStat;
    [SerializeField] private StatData maxReviveStat;
    [SerializeField] private UnityEvent onRevive;
    private Squad squad;
    private int maxUnits = 200;
    private float doubleReviveChance = 0.0f;

    public int MaxUnits => maxUnits;
    private void Start()
    {
        if (doubleReviveStat != null)
            doubleReviveChance = doubleReviveStat.GetStat();
        if (maxReviveStat != null)
        {
            maxUnits = (int)(maxReviveStat.GetStat() * (float)maxUnits);
        }
        squad = GetComponent<Squad>();
        Squad.OnLeaveDeadBodies += ReviveSquad;
    }
    private void OnDestroy()
    {

        Squad.OnLeaveDeadBodies -= ReviveSquad;
    }

    private void ReviveSquad(List<DeadUnit> squadDeadUnits)
    {
        StartCoroutine(ReviveDelay(squadDeadUnits));
    }
    private IEnumerator ReviveDelay(List<DeadUnit> squadDeadUnits)
    {
        bool isRevived = false;

        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            if (fxRevive != null)
                Instantiate(fxRevive, squadDeadUnits[i].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            if (squad.UnitsCount >= maxUnits)
            {
                break;
            }
            else
            {
                isRevived = true;
            }
            if (fxAppear != null)
                Instantiate(fxAppear, squadDeadUnits[i].transform.position, Quaternion.identity);

            float doubleChanceCheck = Random.Range(0.0f, 1.0f);
            int countOfRevive = 1;
            if (doubleReviveChance > doubleChanceCheck)
            {
                countOfRevive = 2;
            }
            for (int z = 0; z < countOfRevive; z++)
            {
                Unit reviveUnit = squadDeadUnits[i].ReviveUnit();
                squad.AddUnit(reviveUnit);
            }
        }
        if (isRevived)
        {
            onRevive?.Invoke();
        }
        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            squadDeadUnits[i].BodyDestroy();
        }
    }
}
