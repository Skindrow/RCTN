using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Squad : MonoBehaviour
{
    public List<Unit> squadMembers = new List<Unit>(); // Список для хранения отряда
    public float baseMoveSpeed = 5f; // Базовая скорость перемещения отряда
    public float maxMoveSpeed = 10f; // Максимальная скорость перемещения отряда


    void Start()
    {

        // Устанавливаем базовую скорость для всех юнитов и ссылку на отряд
        foreach (Unit member in squadMembers)
        {
            member.SetBaseSpeed(baseMoveSpeed);
            member.SetSquad(this);
        }
    }

    public void AddUnit(Unit unit)
    {
        if (!squadMembers.Contains(unit))
        {
            squadMembers.Add(unit);
            unit.SetBaseSpeed(baseMoveSpeed);
            unit.SetSquad(this);
        }
    }

    public void RemoveUnit(Unit unit)
    {
        if (squadMembers.Contains(unit))
        {
            squadMembers.Remove(unit);
        }
    }

    public void MoveSquadToPosition(Vector2 targetPosition)
    {
        foreach (Unit member in squadMembers)
        {
            member.MoveToPosition(targetPosition, maxMoveSpeed);
        }
    }

    public void StopSquadMovement()
    {
        foreach (Unit member in squadMembers)
        {
            member.Stop();
        }
    }
}
