using UnityEngine;
[CreateAssetMenu(fileName = ("new Stat Data"))]
public class StatData : ScriptableObject
{
    public string StatPref;
    public float DefaultValue;


    public float GetStat()
    {
        return StatsHolder.Instance.GetValue(StatPref);
    }
}
