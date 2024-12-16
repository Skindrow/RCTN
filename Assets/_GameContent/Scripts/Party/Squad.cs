using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Squad : MonoBehaviour
{
    public List<Unit> squadMembers = new List<Unit>();

    private bool isAttacking = false;


    public void AddUnit(Unit unit)
    {

    }

    public void RemoveUnit(Unit unit)
    {
        if (squadMembers.Contains(unit))
        {
            squadMembers.Remove(unit);
        }
    }
    public void SquadMove(Vector2 targetPosition)
    {
        for (int i = 0; i < squadMembers.Count; i++)
        {
            squadMembers[i].MoveTo(targetPosition);
        }
    }

}
