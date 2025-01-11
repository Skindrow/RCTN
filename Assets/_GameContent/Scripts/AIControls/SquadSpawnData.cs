using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Wave")]
public class SquadSpawnData : ScriptableObject
{
    public UnitChances[] UnitsChances;
    public int MinCount;
    public int MaxCount;
    public int secondsBeforeSwitch;
    public static List<Unit> GetUnits(SquadSpawnData data)
    {
        int rndCount = Random.Range(data.MinCount, data.MaxCount);
        List<Unit> units = new List<Unit>();
        for (int i = 0; i < rndCount; i++)
        {
            Unit choosedUnit = GetRandomUnit(data);
            units.Add(choosedUnit);
        }
        return units;
    }
    public static Unit GetRandomUnit(SquadSpawnData data)
    {
        float totalWeight = 0f;
        foreach (UnitChances obj in data.UnitsChances)
        {
            totalWeight += obj.ChanceOfSpawn;
        }
        float randomValue = Random.Range(0f, totalWeight);

        float cumulativeWeight = 0f;
        for (int j = 0; j < data.UnitsChances.Length; j++)
        {
            cumulativeWeight += data.UnitsChances[j].ChanceOfSpawn;
            if (randomValue <= cumulativeWeight)
            {
                return data.UnitsChances[j].UnitPref;
            }
        }
        return null;
    }
}
[System.Serializable]
public class UnitChances
{
    public Unit UnitPref;
    public float ChanceOfSpawn;
}
