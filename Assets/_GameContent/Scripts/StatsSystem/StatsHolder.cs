using UnityEngine;

public class StatsHolder : MonoBehaviour
{

    #region singleton

    public static StatsHolder Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private Stat[] stats;

    private void Start()
    {
        InitializeStats();
    }
    private void InitializeStats()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i].LoadStat();
        }
    }
    public void ChangeStat(string statName, float add)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].StatName == statName)
            {
                stats[i].AddStat(add);
            }
        }
    }
}
[System.Serializable]
public class Stat
{
    public string StatName;
    public float DefaultValue;

    private float currentValue;

    public float CurrentValue => currentValue;

    public void SetStat(float currentValue)
    {
        this.currentValue = currentValue;
        SaveStat();
    }
    public void AddStat(float add)
    {
        this.currentValue += add;
        SaveStat();
    }
    public void SaveStat()
    {
        SaveSystem.SaveFloat(StatName, currentValue);
    }
    public void LoadStat()
    {
        if (SaveSystem.HasKey(StatName))
        {
            currentValue = SaveSystem.LoadFloat(StatName);
        }
        else
        {
            SetStat(DefaultValue);
        }
    }
}
