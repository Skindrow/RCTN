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

    private Stat[] stats;
    [SerializeField] private StatData[] allStatData;

    private void Start()
    {
        InitializeStats();
    }
    private void InitializeStats()
    {
        stats = new Stat[allStatData.Length];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = new Stat(allStatData[i]);
            stats[i].LoadStat();
        }
    }
    public void ChangeStat(string statPref, float add)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].StatData.StatPref == statPref)
            {
                stats[i].AddStat(add);
            }
        }
    }
    public float GetValue(string statPref)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].StatData.StatPref == statPref)
            {
                return stats[i].CurrentValue;
            }
        }
        return 0f;
    }
}
[System.Serializable]
public class Stat
{
    public StatData StatData;

    private float currentValue;

    public float CurrentValue => currentValue;

    public Stat(StatData data)
    {
        StatData = data;
    }
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
        SaveSystem.SaveFloat(StatData.StatPref, currentValue);
    }
    public void LoadStat()
    {
        if (SaveSystem.HasKey(StatData.StatPref))
        {
            currentValue = SaveSystem.LoadFloat(StatData.StatPref);
        }
        else
        {
            SetStat(StatData.DefaultValue);
        }
    }
}
