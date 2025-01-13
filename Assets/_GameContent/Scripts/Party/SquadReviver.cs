using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SquadReviver : MonoBehaviour
{
    [SerializeField] private GameObject fxRevive;
    [SerializeField] private GameObject fxAppear;
    private Squad squad;

    private void Start()
    {
        squad = GetComponent<Squad>();
        Squad.OnLeaveDeadBodies += ReviveSquad;
    }
    private void OnDestroy()
    {

        Squad.OnLeaveDeadBodies += ReviveSquad;
    }

    private void ReviveSquad(List<DeadUnit> squadDeadUnits)
    {
        StartCoroutine(ReviveDelay(squadDeadUnits));
    }
    private IEnumerator ReviveDelay(List<DeadUnit> squadDeadUnits)
    {
        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            if (fxRevive != null)
                Instantiate(fxRevive, squadDeadUnits[i].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            if (fxAppear != null)
                Instantiate(fxAppear, squadDeadUnits[i].transform.position, Quaternion.identity);
            Unit reviveUnit = squadDeadUnits[i].ReviveUnit();
            squad.AddUnit(reviveUnit);
        }
        for (int i = 0; i < squadDeadUnits.Count; i++)
        {
            squadDeadUnits[i].BodyDestroy();
        }
    }
}
