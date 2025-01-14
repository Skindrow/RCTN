using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] public string StatPref;
    [SerializeField] private float defaultValue;
    public float GetStat()
    {
        if (SaveSystem.HasKey(StatPref))
        {
            return (float)SaveSystem.LoadFloat(StatPref);
        }
        else
        {
            return defaultValue;
        }
    }
}
